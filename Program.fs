open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open WeeklyCalendar.Handlers.MainHandler

let errorHandler (ex : Exception) (logger : Microsoft.Extensions.Logging.ILogger) =
    printfn "An unhandled exception has occurred while executing the request."
    printfn "%s" ex.Message
    //logger.Log(LogLevel.Error,  "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

let webApp config =
    choose [
        route "/" >=> mainHandler config
    ]


[<EntryPoint>]
let main args =
    let config = WeeklyCalendar.ConfigReader.read ()
    let builder = WebApplication.CreateBuilder(args)
    builder.Services.AddGiraffe() |> ignore

    let app = builder.Build()

    if app.Environment.IsDevelopment() then
        app.UseDeveloperExceptionPage() |> ignore

    app.UseStaticFiles() |> ignore
    app.UseGiraffe (webApp config)
    app.UseGiraffeErrorHandler(errorHandler) |> ignore

    app.Run()

    0 // Exit code

