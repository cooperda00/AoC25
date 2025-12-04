module Day04.Part2

open System.IO

let positionHasRoll char =
    match char with
    | char when char = '@' -> true
    | _ -> false

let generateKeysToCheck rowIndex columnIndex totalRows totalColumns =
    let top =
        if rowIndex = 0 then
            []
        elif columnIndex = totalColumns - 1 then
            [ $"{rowIndex - 1}:{columnIndex - 1}"; $"{rowIndex - 1}:{columnIndex}" ]
        else
            [ $"{rowIndex - 1}:{columnIndex - 1}"
              $"{rowIndex - 1}:{columnIndex}"
              $"{rowIndex - 1}:{columnIndex + 1}" ]

    let middle =
        if columnIndex = totalColumns - 1 then
            [ $"{rowIndex}:{columnIndex - 1}" ]
        else
            [ $"{rowIndex}:{columnIndex - 1}"; $"{rowIndex}:{columnIndex + 1}" ]

    let bottom =
        if rowIndex = totalRows - 1 then
            []
        elif columnIndex = totalColumns - 1 then
            [ $"{rowIndex + 1}:{columnIndex - 1}"; $"{rowIndex + 1}:{columnIndex}" ]
        else
            [ $"{rowIndex + 1}:{columnIndex - 1}"
              $"{rowIndex + 1}:{columnIndex}"
              $"{rowIndex + 1}:{columnIndex + 1}" ]

    let keys = top @ middle @ bottom
    let validKeys = keys |> List.filter (fun str -> str.Contains '-' <> true)
    validKeys

let parseLine
    (line: string)
    (rowIndex: int)
    (totalLines: int)
    (totalColumns: int)
    (lookup: Map<string, char>)
    : Map<string, char> * int =
    line
    |> Seq.indexed
    |> Seq.fold
        (fun (currentMap, count) (columnIndex, char) ->
            match char with
            | '@' ->
                let key = $"{rowIndex}:{columnIndex}"

                match currentMap.TryFind key with
                | Some '@' ->
                    let keysToCheck = generateKeysToCheck rowIndex columnIndex totalLines totalColumns

                    let numberOfSurroundingRolls =
                        keysToCheck
                        |> List.filter (fun lookupKey ->
                            match currentMap.TryFind lookupKey with
                            | Some char when positionHasRoll char -> true
                            | _ -> false)
                        |> List.length

                    if numberOfSurroundingRolls < 4 then
                        Map.add key 'x' currentMap, count + 1
                    else
                        currentMap, count
                | _ -> currentMap, count
            | _ -> currentMap, count)
        (lookup, 0)

let doOnePass (lookup: Map<string, char>) (lines: string array) =
    let totalLines = lines.Length

    lines
    |> Array.indexed
    |> Array.fold
        (fun (currentMap, totalCount) (rowIndex, line) ->
            let totalColumns = line.Length
            let newMap, lineCount = parseLine line rowIndex totalLines totalColumns currentMap
            newMap, totalCount + lineCount)
        (lookup, 0)

let rec removeUntilStable lookup lines totalRemoved =
    let newMap, removedThisPass = doOnePass lookup lines

    if removedThisPass = 0 then
        totalRemoved
    else
        removeUntilStable newMap lines (totalRemoved + removedThisPass)

let createLookup lines =
    lines
    |> Array.indexed
    |> Array.fold
        (fun outMap (rowIndex, line) ->
            line
            |> Seq.indexed
            |> Seq.fold
                (fun innerMap (columnIndex, char) ->
                    let key = $"{rowIndex}:{columnIndex}"
                    Map.add key char innerMap)
                outMap)
        Map.empty

let run () =
    let stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let lines = File.ReadAllLines "./Day04/input.txt"
    let lookup = createLookup lines
    let result = removeUntilStable lookup lines 0
    stopWatch.Stop() // 600ms
    printfn "Day 4:2 Solution"
    printfn "%f" stopWatch.Elapsed.TotalMilliseconds
    printfn "%A" result
