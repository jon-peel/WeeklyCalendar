module WeeklyCalendar.ConfigReader
open FSharp.Configuration
open WeeklyCalendar.Domain
open System.IO
open Microsoft.Extensions.Configuration
open System

type private AgendaConfig = YamlConfig<"agenda.yaml">

let private tryFindConfigFile () =
    let paths = [
        "/config/agenda.yaml"  // absolute path
        "agenda.yaml"          // relative path
    ]
    paths |> List.tryFind File.Exists

let private readAgenda () =
    let config = AgendaConfig()
    match tryFindConfigFile () with
    | Some path -> config.Load path
    | None -> failwith "Could not find agenda.yaml in any of the expected locations"    
    ( config.location,
      [ for event in config.agenda do
            let a: Event =
                { Day = event.day            
                  Name = event.name
                  Color = event.color
                  Start = event.start
                  End = event.``end`` }
            yield a ],
      [ for sched in config.schedule do
            let a: ScheduledEvent = 
                { Date = DateOnly.Parse sched.date
                  Name = sched.name
                  Color = sched.color
                  Start = TimeOnly.FromTimeSpan sched.start
                  End = TimeOnly.FromTimeSpan sched.``end`` }
            yield a] )


let private readWeatherApiKey (config: ConfigurationManager) =
    config["WEATHER_API_KEY"]

let read config = 
    let (location, agenda, schedule) = readAgenda ()
    { Location = location
      Agenda = agenda
      Schedule = schedule
      WeatherApiKey = readWeatherApiKey config }