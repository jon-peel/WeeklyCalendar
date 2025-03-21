module WeeklyCalendar.ConfigReader
open FSharp.Configuration
open WeeklyCalendar.Domain

type private AgendaConfig = YamlConfig<"agenda.yaml">

let private readAgenda () =
    let config = AgendaConfig()
    do config.Load "agenda.yaml"

    [ for event in config.agenda do
        yield { 
            Day = event.day            
            Name = event.name
            Color = event.color
            Start = event.start
            End = event.``end``
        } ]

let read () = 
    { Events = readAgenda () }