module Day04.Part1

open System.IO

let positionHasScroll char =
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

let parseLine (line: string) (rowIndex: int) (totalLines: int) (lookup: Map<string, char>) : int =
    let totalColumns = line.Length

    line
    |> Seq.indexed
    |> Seq.map (fun (columnIndex, char) ->
        match char with
        | '@' ->
            let keysToCheck = generateKeysToCheck rowIndex columnIndex totalLines totalColumns

            let contents =
                keysToCheck
                |> List.map (fun lookupKey ->
                    let res = lookup.TryFind lookupKey

                    match res with
                    | None -> '.'
                    | Some char -> char)

            let numberOfSurroundingRolls =
                contents |> List.filter positionHasScroll |> List.length

            let result = if numberOfSurroundingRolls < 4 then 1 else 0

            result

        | _ -> 0)
    |> Seq.sum


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
    let lines = File.ReadAllLines "./Day04/input.txt"
    let lookup = createLookup lines
    let totalLines = lines.Length
    let indexdLines = lines |> Array.indexed

    let res =
        indexdLines
        |> Array.map (fun (rowIndex, line) -> parseLine line rowIndex totalLines lookup)
        |> Array.sum

    printfn "Day 4:1 Solution"
    printfn "%A" res
