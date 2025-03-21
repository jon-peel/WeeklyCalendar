namespace WeeklyCalendar.Domain
open System

type Event = { Day: string; Name: string; Start: TimeSpan; End: TimeSpan; Color: string }

type Config = { Events: Event list }