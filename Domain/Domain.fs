namespace WeeklyCalendar.Domain
open System
open System.Text.Json.Serialization
open System.Threading.Tasks

type Event = { Day: string; Name: string; Start: TimeSpan; End: TimeSpan; Color: string }
type ScheduledEvent = { Date: DateOnly; Name: string; Start: TimeOnly; End: TimeOnly; Color: string }

type GetWeather = Unit -> Task<WeatherResponse>
and WeatherResponse = {
    Current: Current
    Forecast: Forecast
}
and Current = {
    [<JsonPropertyName("temp_c")>] TempC: float
    Condition: Condition
}
and Condition = {
    Text: string
    Icon: string
}
and Forecast = { 
    ForecastDay: ForecastDay list 
}
and ForecastDay = {
    astro: Astro
    hour: Hour list    
}
and Astro = {
    Sunrise: string
    Sunset: string
} with member t.SunriseHour = DateTime.Parse(t.Sunrise).Hour
       member t.SunsetHour = DateTime.Parse(t.Sunset).Hour


and Hour = {
    time: string
    [<JsonPropertyName("temp_c")>] TempC: float
    Condition: Condition
} with member t.HourTime = DateTime.Parse(t.time) 

type ISettings =
    abstract member Location: string
    abstract member Agenda: Event list
    abstract member Schedule: ScheduledEvent list
    abstract member WeatherApiKey: string
   
type IGetWeather =
    abstract member GetWeather: GetWeather