// Generated by Copilot
module WeeklyCalendar.Views.Components.DateTimePanel

open Giraffe.ViewEngine
open System

let dateTimePanel () =
    div [ _class "date-time-panel p-2 bg-light border-bottom" ] [
        div [ _class "d-flex justify-content-between align-items-center" ] [
            div [ _class "date-display h4 mb-0" ] [
                button [ _id "activate" ] [ str "Activate" ]
                str (DateTime.Now.ToString("ddd, MMM dd"))
            ]
            TimeDisplay.timeDisplay ()
        ]
    ]
