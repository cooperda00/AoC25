module Day05.Part2

open System.IO

type Range = { Start: int64; End: int64 }

let getRanges lines =
    let indexToSplit = lines |> Array.findIndex (fun line -> line = "")
    lines |> Array.splitAt indexToSplit |> fst


let parseRange (range: string) : Range =
    let parts = range.Split "-"

    { Start = int64 parts.[0]
      End = int64 parts.[1] }

let convertRangesIntoStartAndEndRecord (ranges: string array) = ranges |> Array.map parseRange

let mergeRanges (ranges: Range array) =
    let sorted = ranges |> Array.sortBy (fun range -> range.Start)

    (Array.empty, sorted)
    ||> Array.fold (fun out range ->
        if Array.isEmpty out then
            [| range |]
        else
            let previousRange = out |> Array.last
            let canMerge = range.Start <= previousRange.End + int64 1 // +1 so we Merge toucing [10 - 20][21 - 30] = [10 - 30]

            if canMerge then
                let newRange =
                    { Start = previousRange.Start
                      End = max previousRange.End range.End }

                out |> Array.updateAt (out.Length - 1) newRange
            else
                Array.append out [| range |]

    )

let sumDifferences (ranges: Range array) =
    ranges
    |> Array.map (fun range -> range.End - range.Start + int64 1) // +1 so that we are inclusive of start eg [10, 20] = 11
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
