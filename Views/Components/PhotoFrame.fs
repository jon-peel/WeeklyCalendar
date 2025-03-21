module PhotoFrame 
open Giraffe.ViewEngine
open System.IO

let getPhotos() =
    let photoDir = Path.Combine("wwwroot", "photos")
    (Directory.GetParent photoDir).FullName |> printfn "Photo directory: %s"
    if Directory.Exists(photoDir) then
        Directory.GetFiles(photoDir, "*.*")
        |> Array.filter (fun f -> 
            let ext = Path.GetExtension(f).ToLower()
            ext = ".jpg" || ext = ".jpeg" || ext = ".png" || ext = ".avif")
        |> Array.map (fun f -> Path.GetFileName(f))
    else
        printfn "Photo directory not found: %s" photoDir
        Array.empty

let getRandomPhoto() =
    let photos = getPhotos()
    printfn "Found %d photos" photos.Length
    for photo in photos do
        printfn "Photo: %s" photo

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
    
    div [ _class "card h-100" ] [
        div [ _class "card-body d-flex align-items-center justify-content-center" ] [
            img [
                _src photoUrl
                _class "img-fluid"
                _style "max-height: 100%; object-fit: contain;"
            ]
        ]
    ]