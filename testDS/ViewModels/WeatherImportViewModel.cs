using testDS.Models;

namespace testDS.ViewModels
{
    public class WeatherImportViewModel
    {
        public List<IFormFile>? formFiles { get; set; }

        public string? Message { get; set; }

        public List<WeatherModel> weatherModels { get; set; } = new List<WeatherModel>();
    }
}
