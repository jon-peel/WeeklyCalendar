module WeeklyCalendar.Views.Components.PhotoFrame 
open Giraffe.ViewEngine
open System.IO

let getPhotos() =
    let photoDir = Path.Combine("wwwroot", "photos")    
    if Directory.Exists(photoDir) then
        Directory.GetFiles(photoDir, "*.*")
        |> Array.filter (fun f -> 
            let ext = Path.GetExtension(f).ToLower()
            ext = ".jpg" || ext = ".jpeg" || ext = ".png" || ext = ".avif")
        |> Array.map (fun f -> Path.GetFileName(f))
    else        
        Array.empty

let getRandomPhoto() =
    let photos = getPhotos()
    if photos.Length > 0 then
        let random = System.Random()
        let index = random.Next(photos.Length)
        Some photos.[index]
    else
        None

let photoFrame () = 
    let photoUrl = 
        match getRandomPhoto() with
        | Some photo -> sprintf "/photos/%s" photo
        | None -> "/images/no-photo.png"  // Fallback image
    
    div [ _id "photo-frame" ; _class "card h-100" ] [
        div [ _class "photo-container" ] [
            img [
                _src photoUrl
                _class "photo-display"
                _alt "Photo"
            ]
        ]
    ]