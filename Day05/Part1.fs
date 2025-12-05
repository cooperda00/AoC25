module Day05.Part1

open System.IO

let getRangesAndIds lines =
    let indexToSplit = lines |> Array.findIndex (fun line -> line = "")
    let split = lines |> Array.splitAt indexToSplit
    let ranges, ids = split
    let filteredIds = ids |> Array.filter (fun id -> id <> "")
    ranges, filteredIds

type Range = { Start: int64; End: int64 }

let parseRange (range: string) : Range =
    let parts = range.Split "-"

    { Start = int64 parts.[0]
      End = int64 parts.[1] }

let convertRangesIntoStartAndEndRecord (ranges: string array) = ranges |> Array.map parseRange

let isInRange (ranges: Range array) (id: int64) : bool =
    ranges |> Array.exists (fun r -> id >= r.Start && id <= r.End)

let getNumberOfFreshIngredients (ranges: Range array) (ids: string array) : int =
    ids
    |> Array.filter (fun id ->
        let idAsInt = int64 id
        isInRange ranges idAsInt)
    |> Array.length

let run () =
    let lines = File.ReadAllLines "./Day05/input.txt"
    let ranges, ids = getRangesAndIds lines

    let numberOfFreshIngredients =
        getNumberOfFreshIngredients (convertRangesIntoStartAndEndRecord ranges) ids


    printfn "Day 5:1 Solution"
    printfn "%A" numberOfFreshIngredients
