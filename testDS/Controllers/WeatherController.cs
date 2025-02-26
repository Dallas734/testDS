using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Core.ExcelPackage;
using testDS.ViewModels;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using testDS.Models;
using testDS.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace testDS.Controllers
{
    public class WeatherController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public WeatherController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Import()
        {
            return View();
        }

        public async Task<IActionResult> ViewWeather(string beginInputDate = "", string endInputDate = "", int page = 1, int pageSize = 8)
        {
            var filtredWeatherModels = _appDbContext.Weathers.AsQueryable();
            var cnt = await filtredWeatherModels.CountAsync();

            if (beginInputDate != "")
            {
				var beginDate = DateTime.ParseExact(beginInputDate, "yyyy-MM", CultureInfo.InvariantCulture);
				filtredWeatherModels = filtredWeatherModels.Where(w => w.Date >= DateOnly.FromDateTime(beginDate));
			}

			if (endInputDate != "")
			{
				DateTime endDate = DateTime.ParseExact(endInputDate, "yyyy-MM", CultureInfo.InvariantCulture);
                int lastDay = DateTime.DaysInMonth(endDate.Year, endDate.Month);
                DateTime endDateWithLastDay = new DateTime(endDate.Year, endDate.Month, lastDay);
				filtredWeatherModels = filtredWeatherModels.Where(w => w.Date <= DateOnly.FromDateTime(endDateWithLastDay));
			}

			int skipCount = (page - 1) * pageSize;
            List<WeatherModel> weathersFromDB = new List<WeatherModel>();
            weathersFromDB = await filtredWeatherModels
				                            .OrderBy(w => w.Date)
											.Skip(skipCount)
                                            .Take(pageSize).ToListAsync();
            int totalCount = filtredWeatherModels.Count();

			ViewWeatherViewModel weatherViewModel = new ViewWeatherViewModel()
            { 
                WeatherModels = weathersFromDB,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                BeginDate = beginInputDate,
                EndDate = endInputDate
            };
            return View(weatherViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ImportWeather(WeatherImportViewModel model)
        {
            if (model.formFiles == null || model.formFiles.Count == 0)
            {
                ModelState.AddModelError("", "Выберите файлы для загрузки!");
                return View(model);
            }

            var importedData = new List<WeatherModel>();

            foreach (IFormFile file in model.formFiles)
            {
                if (file.Length > 0)
                {
                    using var stream = file.OpenReadStream();
                    IWorkbook workbook = new XSSFWorkbook(stream);
                    ISheet sheet = workbook.GetSheetAt(0);

                    for (int i = 4; i <= sheet.LastRowNum; i++)
                    {
                        try
                        {
                            IRow row = sheet.GetRow(i);

                            if (row == null || row.PhysicalNumberOfCells == 0)
                                continue;

                            WeatherModel importModel = new WeatherModel()
                            {
                                Date = DateOnly.Parse(row.GetCell(0)?.ToString() ?? throw new Exception("Дата отсутствует")),
                                Time = TimeOnly.Parse(row.GetCell(1)?.ToString() ?? throw new Exception("Время отсутствует")),
                                Temp = double.TryParse(row.GetCell(2)?.ToString(), out double temp) ? temp : throw new Exception("Ошибка в температуре"),
                                Humadity = double.TryParse(row.GetCell(3)?.ToString(), out double hum) ? hum : throw new Exception("Ошибка в влажности"),
                                Td = double.TryParse(row.GetCell(4)?.ToString(), out double td) ? td : throw new Exception("Ошибка в Td"),
                                AtmosphericPressure = int.TryParse(row.GetCell(5)?.ToString(), out int pressure) ? pressure : throw new Exception("Ошибка в давлении"),
                                WindDirection = row.GetCell(6)?.ToString() ?? "Не указан",
                                WindSpeed = int.TryParse(row.GetCell(7)?.ToString(), out int windSpeed) ? windSpeed : 0,
                                Cloudiness = int.TryParse(row.GetCell(8)?.ToString(), out int cloud) ? cloud : 0,
                                H = int.TryParse(row.GetCell(9)?.ToString(), out int h) ? h : 0,
                                VV = int.TryParse(row.GetCell(10)?.ToString(), out int vv) ? vv : 0,
                                WeatherPhenomena = row.GetCell(11)?.ToString() ?? "Нет данных"
                            };

                            if (ModelState.IsValid)
                                importedData.Add(importModel);
                            else
                            {
                                TempData["ErrorMessage"] = "Произошла ошибка при импорте!";
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", $"Ошибка в строке {i + 1}: {ex.Message}");
                        }
                    }
                }
            }

            if (importedData.Any())
            {
                await _appDbContext.Weathers.AddRangeAsync(importedData);
                await _appDbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Импортировано {importedData.Count} записей!";
            }

            return RedirectToAction("Import");
        }
    }
}
