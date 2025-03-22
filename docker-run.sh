#!/bin/bash

if [ -d "/photos" ]; then
    echo "/photos directory found. Creating symlink..."
    rm -rf /app/wwwroot/photos || { echo "Failed to remove existing photos"; exit 1; }
    ln -sf /photos /app/wwwroot/photos || { echo "Failed to create symlink"; exit 1; }
fi

exec dotnet WeeklyCalendar.dll
