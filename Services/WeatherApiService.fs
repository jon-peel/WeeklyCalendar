module WeeklyCalendar.WeatherApiService

open System
open FSharpPlus
open FSharp.Data
open FsToolkit.ErrorHandling
open WeeklyCalendar.Domain

type GetHourlyWeatherResult = JsonProvider<"GetGetHourlyWeather.json", ResolutionFolder = __SOURCE_DIRECTORY__>

let getHourlyWeather (apiKey: string) (location: string) =
    let convertHour (h: GetHourlyWeatherResult.Hour) =
        { Hour = TimeOnly.FromDateTime h.Time
          Temp = (float h.TempC) * 1.0<C>
          Condition = { Description = h.Condition.Text; Icon = h.Condition.Icon } }
    result {
        try
            let url = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={location}&days=1&aqi=no&alerts=no"
            let json = Http.RequestString url
            let result = GetHourlyWeatherResult.Parse json
            let! forecastDay = result.Forecast.Forecastday |> Seq.tryHead |>> Ok |> Option.defaultValue (Error "No forecast in result set")
            let weather =
                { Sunrise = TimeOnly.FromDateTime forecastDay.Astro.Sunrise 
                  Sunset = TimeOnly.FromDateTime forecastDay.Astro.Sunset
                  Hourly = [ for h in forecastDay.Hour do convertHour h ] }
            return weather
        with e ->
            return! Error $"Failed to get weather: {e.Message}"
    }