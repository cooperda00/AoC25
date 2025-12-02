module Program

[<EntryPoint>]
let main argv =
    match argv with
    | [| "1:1" |] -> Day01.Part1.run ()
    | [| "1:2" |] -> Day01.Part2.run ()
    | [| "2:1" |] -> Day02.Part1.run ()
    | [| "2:2" |] -> Day02.Part2.run ()
    | [| day |] -> printfn "Unknown day: %s" day
    | _ -> printfn "Usage: dotnet run <day:part>"

    0
