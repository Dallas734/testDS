using System.ComponentModel.DataAnnotations;

namespace testDS.Models
{
    public class WeatherModel
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public double Temp {  get; set; }
        public double Humadity { get; set; }
        public double Td {  get; set; }
        public int AtmosphericPressure { get; set; }
        public string? WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public int? Cloudiness { get; set; }
        public int H { get; set;}
        public int? VV { get; set; }
        public string? WeatherPhenomena { get; set; }
    }
}
