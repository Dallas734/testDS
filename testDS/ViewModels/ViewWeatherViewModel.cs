using testDS.Models;

namespace testDS.ViewModels
{
	public class ViewWeatherViewModel
	{
		public DateOnly SelectedDate { get; set; }
		public string? SelectedMonth { get; set; }
		public int TotalCount { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public string? BeginDate { get; set; }
		public string? EndDate { get; set; }
		public List<WeatherModel> WeatherModels { get; set; } = new List<WeatherModel>();
	}
}
