namespace WeatherApp.Models;

class WeatherResponse
{
    public WeatherMain Main { get; set; }
    public WeatherDetails[] Weather { get; set; }
}