module WeeklyCalendar.Views.Components.DailyAgenda

open System
open Giraffe.ViewEngine
open WeeklyCalendar.Domain

let startAt = 0
let endAt = 23


let private timeSlot (weather:WeatherResponse) hour =
    let pixelsPerHour = 60
    let top = (hour - startAt) * pixelsPerHour
    let timeSlotClass =
        weather.Forecast.ForecastDay
        |> Seq.tryHead
        |> function
        | Some d when hour <= d.astro.SunriseHour || hour >= d.astro.SunsetHour -> "time-slot night"
        | _ -> "time-slot"
    div [ _class timeSlotClass 
          _style $"top: {top}px"
          _id $"hour-{hour}" ] [
        strong [] [ str $"{hour}:00" ]
        div [ _class "time-slot-weather" ] 
            (weather.Forecast.ForecastDay
             |> Seq.collect (fun f -> f.hour)
             |> Seq.filter (fun h -> h.HourTime.Hour = hour)
             |> Seq.map (fun h ->  [ 
                tag "img" [ _src h.Condition.Icon; _title h.Condition.Text ] []
                str $" {h.TempC}°"
                ])
             |> Seq.tryHead
             |> Option.defaultValue [])
    ]

let private eventBlock (event: Event) =
    let pixelsPerHour = 60.0
    let startHourOffset = float(event.Start.Hours - startAt)
    let startMinuteOffset = float(event.Start.Minutes) / 60.0
    let top = (startHourOffset + startMinuteOffset) * pixelsPerHour
    let height = (event.End - event.Start).TotalHours * pixelsPerHour
    let colorClass = 
        match event.Color.ToLower() with
        | "red" -> Some "event-block-red"
        | "green" -> Some "event-block-green"
        | "blue" -> Some "event-block-blue"
        | "yellow" -> Some "event-block-yellow"
        | _ -> None
    
    let baseClasses = ["event-block"]
    let classes = 
        match colorClass with
        | Some c -> baseClasses @ [c]
        | None -> baseClasses
    
    let styles = 
        match colorClass with
        | Some _ -> $"top: {top}px; height: {height}px"
        | None -> $"top: {top}px; height: {height}px; background-color: {event.Color}"
        
    div [ _class (String.concat " " classes)
          _style styles ] [
        div [ _class "event-name" ] [ 
            str $"{event.Name} ({event.Start}-{event.End})" 
        ]
    ]

let private currentTimeIndicator (time: TimeSpan) =
    let pixelsPerHour = 60.0
    let hourOffset = float(time.Hours - startAt)
    let minuteOffset = float(time.Minutes) / 60.0
    let top = (hourOffset + minuteOffset) * pixelsPerHour
    div [ _class "current-time-indicator"
          _id "current-time-indicator"
          _style $"top: {top}px" ] []

let dailyAgenda (config: Config) (getWeather: GetWeather) =
    let weather = getWeather "Sochi" |> Async.AwaitTask |> Async.RunSynchronously
    div [ _class "daily-agenda" ] [
        let date = DateTime.Now
        let day = date.DayOfWeek.ToString()
        let currentTime = date.TimeOfDay
        let events = config.Events |> Seq.where (fun e -> e.Day = day)    
    
        div [ _class "time-grid" ] [
            for hour in startAt .. endAt do
                timeSlot weather hour
        ]
        div [ _class "events-container" ] [
            for event in events do
                eventBlock event
            currentTimeIndicator currentTime
        ]
    ]
