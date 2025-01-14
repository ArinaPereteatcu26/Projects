using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;
using Newtonsoft.Json;

class Program
{
    private static string apiKey = "d72804d438269939aa5049bcb7726617";
    static string apiBaseUrl = "https://api.openweathermap.org/data/2.5/weather";
    static string cityName = string.Empty;

    static async Task Main(string[] args)
    {
        Console.Title = "Weather App";
        Console.ForegroundColor = ConsoleColor.DarkMagenta;

        while (true)
        {
            Console.WriteLine("==============================");
            Console.WriteLine("Welcome to the Weather App");
            Console.WriteLine("==============================");
            Console.WriteLine("1. Enter City Name:");
            Console.WriteLine("2. Get Weather: ");
            Console.WriteLine("3. Get Temperature: ");
            Console.WriteLine("4. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Enter City Name:");
                    cityName = Console.ReadLine();
                    Console.WriteLine($"City entered: {cityName}");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    if (string.IsNullOrWhiteSpace(cityName))
                    {
                        Console.WriteLine("Enter city name first");
                    }
                    else
                    {
                        Console.WriteLine("Fetching weather");
                        var weather = await FetchWeatherData(cityName);
                        if (weather != null)
                        {
                            Console.WriteLine($"Weather: {cityName}: {weather.Weather[0].Description}");
                        }
                        else
                        {
                            Console.WriteLine("No weather data found");
                        }
                    }

                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    break;

                case "3":
                    Console.Clear();
                    if (string.IsNullOrWhiteSpace(cityName))
                    {
                        Console.WriteLine("Enter city name first");
                    }
                    else
                    {
                        Console.WriteLine("Fetching weather");
                        var weather = await FetchWeatherData(cityName);
                        if (weather != null)
                        {
                            Console.WriteLine($"Temperature: {cityName}: {weather.Main.Temperature} C");
                        }
                        else
                        {
                            Console.WriteLine("No weather data found");
                        }
                    }

                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine("Exit");
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    break;
            }
        }
    }

//asynchronous method that returns a task and provides a weatherResponse object
    static async Task<WeatherResponse> FetchWeatherData(string city)
    {
        using (HttpClient client = new HttpClient())  //sent HTTP requests
        {
            try
            {
                //query parameter for the city name and API key
                string requestUrl = $"{apiBaseUrl}?q={city}&appid={apiKey}&units=metric";
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<WeatherResponse>(responseData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
    
}





