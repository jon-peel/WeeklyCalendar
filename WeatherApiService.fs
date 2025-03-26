module WeeklyCalendar.WeatherApiService

open System.Net.Http
open System.Text.Json
open WeeklyCalendar.Domain


let getHourlyWeather (apiKey: string) (location: string) =
    task {
        use client = new HttpClient()                                   
        let url = $"http://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={location}&days=1&aqi=no&alerts=no"
        
        try 
            let! response = client.GetStringAsync(url)
            let options = JsonSerializerOptions()
            options.PropertyNameCaseInsensitive <- true
        
            return JsonSerializer.Deserialize<WeatherResponse>(response, options)
        with e ->
            return! failwith $"Failed to get weather: {e.Message}"
    }