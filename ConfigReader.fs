module WeeklyCalendar.ConfigReader
open FSharp.Configuration
open WeeklyCalendar.Domain
open System.IO
open Microsoft.Extensions.Configuration

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

    [ for event in config.agenda do
        yield { 
            Day = event.day            
            Name = event.name
            Color = event.color
            Start = event.start
            End = event.``end``
        } ]


let private readWeatherApiKey (config: ConfigurationManager) =
    config["WEATHER_API_KEY"]

let read config = 
    { Events = readAgenda ()
      WeatherApiKey = readWeatherApiKey config }