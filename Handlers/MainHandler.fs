// Generated by Copilot
module WeeklyCalendar.Handlers.MainHandler

open Giraffe
open WeeklyCalendar.Views.MainView

let mainHandler config: HttpHandler =
    fun next ctx ->
        htmlView (mainView config) next ctx
