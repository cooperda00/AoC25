module Day05.Part2

open System.IO

let getRanges lines =
    let indexToSplit = lines |> Array.findIndex (fun line -> line = "")
    let split = lines |> Array.splitAt indexToSplit
    let ranges, _ = split
    ranges

type Range = { Start: int64; End: int64 }

let parseRange (range: string) : Range =
    let parts = range.Split "-"

    { Start = int64 parts.[0]
      End = int64 parts.[1] }

let convertRangesIntoStartAndEndRecord (ranges: string array) = ranges |> Array.map parseRange

let mergeRanges (ranges: Range array) =
    let sorted = ranges |> Array.sortBy (fun range -> range.Start, range.End)

    (Array.empty, sorted)
    ||> Array.fold (fun out range ->
        if Array.isEmpty out then
            [| range |]
        else
            let previousRange = out |> Array.last
            let canMerge = range.Start <= previousRange.End

            if canMerge then
                let newRange =
                    { Start = previousRange.Start
                      End = range.End }

                out |> Array.updateAt (out.Length - 1) newRange
            else
                Array.append out [| range |]

    )

let sumDifferences (ranges: Range array) =
    ranges
    |> Array.map (fun range -> range.End - range.Start + int64 1)
    |> Array.sum


let run () =
    let res =
        File.ReadAllLines "./Day05/input.txt"
        |> getRanges
        |> convertRangesIntoStartAndEndRecord
        |> mergeRanges
        |> sumDifferences

    printfn "Day 5:2 Solution"
    printfn "%A" res
