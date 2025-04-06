namespace WeeklyCalendar

open WeeklyCalendar.Domain
open WeeklyCalendar

type Environment =
    { GetWeather: GetWeather
      Settings: SettingsReader.Results }
    interface IGetWeather with
        member t.GetWeather = t.GetWeather
    interface ISettings with
        member t.Agenda = t.Settings.Agenda
        member t.Location = t.Settings.Location
        member t.Schedule = t.Settings.Schedule
        member t.WeatherApiKey = t.Settings.WeatherApiKey
        
module Environment =
    let build configuration =
         let settings = SettingsReader.read configuration    
         let getWeather () = WeatherApiService.getHourlyWeather settings.WeatherApiKey settings.Location |> Result.toOption         
         { GetWeather = getWeather 
           Settings = settings }

