module Day05.Part1

open System.IO

let getRangesAndIds lines =
    let indexToSplit = lines |> Array.findIndex (fun line -> line = "")
    let split = lines |> Array.splitAt indexToSplit
    let ranges, ids = split
    let filteredIds = ids |> Array.filter (fun id -> id <> "")
    ranges, filteredIds

let convertRangesIntoSet (ranges: string array) =
    ranges
    |> Array.collect (fun range ->
        let startAndEnd = range.Split "-"
        let startId = int startAndEnd.[0]
        let endId = int startAndEnd.[1]
        [| startId..endId |])
    |> Array.map string
    |> Set.ofArray

let getNumberOfFreshIngredients (lookupSet: Set<string>) (ids: string array) : int =
    ids |> Array.filter (fun id -> lookupSet.Contains id) |> Array.length

let run () =
    let lines = File.ReadAllLines "./Day05/input.txt"
    let ranges, ids = getRangesAndIds lines
    let lookupSet = convertRangesIntoSet ranges
    let numberOfFreshIngredients = getNumberOfFreshIngredients lookupSet ids


    printfn "Day 5:1 Solution"
    printfn "%A" numberOfFreshIngredients
