open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Giraffe
open dotenv.net
open WeeklyCalendar.Handlers
open WeeklyCalendar

let errorHandler (ex : Exception) (logger : Microsoft.Extensions.Logging.ILogger) =
    printfn "An unhandled exception has occurred while executing the request."
    printfn "%s" ex.Message
    //logger.Log(LogLevel.Error,  "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

let webApp  env =
    choose [
        route "/" >=> mainHandler env
        subRoute "/api" (apiHandler env)
    ]


[<EntryPoint>]
let main args =
    DotEnv.Load()

    let builder = WebApplication.CreateBuilder(args)
    let env = Environment.build builder.Configuration
    
    builder.Services.AddGiraffe() |> ignore

    let app = builder.Build()

    if app.Environment.IsDevelopment() then
        app.UseDeveloperExceptionPage() |> ignore

    app.UseStaticFiles() |> ignore
    app.UseGiraffe (webApp env)
    app.UseGiraffeErrorHandler(errorHandler) |> ignore

    app.Run()

    0 // Exit code

