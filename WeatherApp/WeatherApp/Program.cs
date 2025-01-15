﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WeatherApp.Models;

class Program
{

    private static string _apiKey;
    static string _apiBaseUrl = "https://api.openweathermap.org/data/2.5/weather";
    static string _cityName = string.Empty;


    static async Task Main()
    {
        LoadConfiguration();
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
                    _cityName = Console.ReadLine();
                    Console.WriteLine($"City entered: {_cityName}");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Clear();
                    if (string.IsNullOrWhiteSpace(_cityName))
                    {
                        var weather = await FetchWeatherData(_cityName);

                        if (weather != null)
                        {
                            Console.WriteLine($"Weather in {_cityName}: {weather.Weather[0].Description}");
                        }
                        else
                        {
                            Console.WriteLine("No weather data found");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Fetching weather...");
                        var weather = await FetchWeatherData(_cityName);
                        if (weather != null)
                        {
                            Console.WriteLine($"Weather in {_cityName}: {weather.Weather[0].Description}");
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
                    if (string.IsNullOrWhiteSpace(_cityName))
                    {
                        Console.WriteLine("Enter city name first");
                    }
                    else
                    {
                        Console.WriteLine("Fetching temperature");
                        var weather = await FetchWeatherData(_cityName);
                        if (weather != null)
                        {
                            Console.WriteLine($"Temperature in {_cityName}: {weather.Main.Temp} C");
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
        using (HttpClient client = new HttpClient()) //sent HTTP requests
        {
            try
            {
                //query parameter for the city name and API key
                string requestUrl = $"{_apiBaseUrl}?q={city}&appid={_apiKey}&units=metric";
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(
                        $"Error: Unable to retrieve weather for {city}. Status code: {response.StatusCode}");
                    return null;
                }

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
    private static void LoadConfiguration()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

        _apiKey = config["WeatherApiKey"];
        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new Exception("API Key not found in configuration.");
        }
    }
}
/*public static async Task<string> GetLocationFromIP()
{
    using (HttpClient client = new HttpClient())
    {
        string ipApiUrl = "http://ip-api.com/json";
        var response = await client.GetStringAsync(ipApiUrl);
        dynamic data = JsonConvert.DeserializeObject(response);
        return $"{data.city}";
    }*/
        
    





