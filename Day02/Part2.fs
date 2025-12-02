module Day02.Part2

open System.IO
open System.Numerics

type EvenOrOdd =
    | Even
    | Odd

let getNumberOfDigits (num: bigint) : int = num.ToString().Length

let getEvenOrOdd (numberOfDigits: int) : EvenOrOdd =
    if numberOfDigits % 2 = 0 then Even else Odd

// Creates windows of digits up to half the length [.], [., .]
// Check if the whole is evenly divisible by the length of each window, if not ignore
// Repeats the window and check if the result matches the input
// "118118"
// -> 1 -> 111111 -> ❌
// -> 11 -> 111111 -> ❌
// -> 118 -> 118118 -> ✅
let isRepeatingPattern (digits: string) : bool =
    let length = digits.Length

    let matchesPattern patternLength =
        if length % patternLength <> 0 then
            false
        else
            let window = digits.Substring(0, patternLength)
            let repetitions = length / patternLength
            String.replicate repetitions window = digits

    [ 1 .. length / 2 ] |> List.exists matchesPattern

let run () =
    let textContent = File.ReadAllText "./Day02/input.txt"
    let split = textContent.Split ","

    let withSequence =
        split
        |> Array.map (fun x ->
            let splitOnDash = x.Split("-")
            let startOfSequence = bigint.Parse splitOnDash.[0]
            let endOfSequence = bigint.Parse splitOnDash.[1]
            let fullSequence = [| startOfSequence..endOfSequence |] |> Seq.ofArray

            let filteredDoubles =
                fullSequence
                |> Seq.filter (fun num ->
                    let digits = num.ToString()
                    isRepeatingPattern digits)

            filteredDoubles)
        |> Seq.concat
        |> Seq.sum


    printfn "Day 2:1 Solution"
    printfn "%A" withSequence
