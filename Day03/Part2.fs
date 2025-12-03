module Day03.Part2

open System.IO

let findLargestWithinRange (startIndex: int) (endIndex: int) (arr: char array) =
    arr[startIndex..endIndex]
    |> Array.indexed
    |> Array.maxBy snd // characters are compared by unicode so '9' > '8'. Returns first occurence if multiple
    |> fun (windowIndex, digit) -> digit, startIndex + windowIndex + 1


let calculateNumberFromLine (input: char array) =
    let finalIndex = input.Length - 1

    (0, 12)
    |> List.unfold (fun (position, remaining) ->
        if remaining = 0 then
            None
        else
            let digit, nextPosition =
                input |> findLargestWithinRange position (finalIndex - remaining + 1)

            Some(digit, (nextPosition, remaining - 1)))
    |> List.map string
    |> String.concat ""


let run () =
    let res =
        File.ReadAllLines "./Day03/input.txt"
        |> Array.map (fun line -> line |> Seq.toArray |> calculateNumberFromLine)
        |> Array.sumBy bigint.Parse

    printfn "Day 3:2 Solution"
    printfn "%A" res
