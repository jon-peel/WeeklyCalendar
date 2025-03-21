# Specification

## Overview

A web application, designed to be self hosted, to show a basic calendar of weekly events and photo galary.

## Layout

The page should be designed to fit on a standard iPad screen.
The overview of the layout is:
```example
+-------------------+
|         A         |
+-------------+-----+
|             |     |
|      B      |  C  |
|             |     |
+-------------+-----+
```

- *A*: The date, time, and any alerts
- *B*: The photo slideshow
- *C*: The daily agenda


### Date Pannel

The date pannel Will show the current date, formatted Week day, Month, Day. E.g. "Thu, Mar 20".
On the right of the panel, it should show the time.
The time should be in 24 hour time, and show the hour and the minute.
A ver thin bar (underline) under the time should indicate the progress seconds, being empty at 0 seconds and being full at 59 seconds.

# Calendar

The calendar events are defined in `config.yaml`.
An example layout of this is 

```yaml
Thursday:
  Classes:
    Color: red
    start: 15:30
    end: 17:30

  Cleaning:
    Color: blue
    starts: 19:00
    ends: 19:30

Friday:
  Builders:
    Color: yellow
    start: 09:30
    end: 12:45
```

In this example, on thursdays there is a time block called "Classes", it should be colourd in a red.
It starts and ends as indicated.
It is okay if block names are repeated.