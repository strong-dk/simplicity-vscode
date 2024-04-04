using System;
using System.Net.Http;
using System.Threading.Tasks;

public class WeatherFetcher
{
  private readonly string apiKey;
  private readonly string apiUrl;

  public WeatherFetcher(string apiKey) {
    this.apiKey = apiKey;
    this.apiUrl = "http://api.openweathermap.org/data/2.5/weather?q=Copenhagen&appid=" + apiKey;
  }

  public async Task<WeatherData> GetWeatherAsync() {

    using (var httpClient = new HttpClient()) {

      try {
        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WeatherData weatherData = WeatherData.FromJson(responseBody);
        return weatherData;
      }
      catch (HttpRequestException ex) {
        Console.WriteLine($"Error fetching weather data: {ex.Message}");
        throw;
      }

    }

  }
}

public class WeatherData {
  public MainData Main { get; set; }
  public string Name { get; set; }

  public static WeatherData FromJson(string json) =>
    System.Text.Json.JsonSerializer.Deserialize<WeatherData>(json);

}

public class MainData {
  public float Temp { get; set; }
  public int Humidity { get; set; }
}
