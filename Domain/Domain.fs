namespace WeeklyCalendar.Domain
open System
open System.Text.Json.Serialization
open System.Threading.Tasks

type Event = { Day: string; Name: string; Start: TimeSpan; End: TimeSpan; Color: string }
type ScheduledEvent = { Date: DateOnly; Name: string; Start: TimeOnly; End: TimeOnly; Color: string }

[<Measure>]
type C

type GetWeather = Unit -> WeatherForecast option
and WeatherForecast = {
    Sunrise: TimeOnly
    Sunset: TimeOnly
    Hourly: HourlyForecast list
}
and HourlyForecast = {
    Hour: TimeOnly
    Temp: float<C>
    Condition: Condition
}
and Condition = { Icon: string; Description: string; }


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