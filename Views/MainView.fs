// Generated by Copilot
module WeeklyCalendar.Views.MainView

open Giraffe.ViewEngine
open WeeklyCalendar.Views.Components
open WeeklyCalendar.Views.Components.DateTimePanel
open WeeklyCalendar.Views.Components.DailyAgenda

let mainView env =
    html [] [
        head [] [
            title [] [ str "Weekly Calendar" ]
            link [ _rel "stylesheet"
                   _href "https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" ]
            link [ _rel "stylesheet"
                   _href "./css/app.css" ]
            link [ _rel "shortcut icon"
                   _type "image/x-icon"
                   _href "/favicon.svg" ]
            meta [_name "viewport"; _content "width=device-width, initial-scale=1"]
            meta [_name "theme-color"; _content "#000000"]
            link [_rel "manifest"; _href "/manifest.json"]
            meta [_name "apple-mobile-web-app-capable"; _content "yes"]
            meta [_name "apple-mobile-web-app-status-bar-style"; _content "black"]
            script [ _src "https://unpkg.com/htmx.org@1.9.6" ] []
        ]
        body [] [
            div [ _class "container-fluid p-0 main-container" ] [
                // Section A: Date Time Panel
                // dateTimePanel getWeather
                
                ConfigView.closedView

                // Section B & C: Main content
                div [ _class "row m-0 content-row" ] [                    
                    div [ _class "col-9 p-2" ] [
                        div [ 
                            _class "photo-container"
                            attr "hx-get" "/api/config"
                            attr "hx-target" "#config-box"
                         ] [
                            StandByVideo.view ()  // Behind
                            PhotoFrame.photoFrame ()  // In front
                        ]
                    ]
                    div [ _class "col-3 p-2 agenda-container" ] [
                        dailyAgenda env
                    ]                    
                ]                
            ]            
            script [_src "/js/app.js"] []
        ]
    ]
