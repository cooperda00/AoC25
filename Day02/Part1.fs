module Day02.Part1

open System.IO

// Iteratate over that list. If the number is odd, ignore
// If the number is even, split it in two
// If the first half and second half are equal - add to output sequence
// Sum numbers in the output sequence

let run () =
    let textContent = File.ReadAllText "./Day02/input.txt"
    let split = textContent.Split ","

    let withSequence =
        split
        |> Array.map (fun x ->
            let splitOnDash = x.Split("-")
            let startOfSequence = int splitOnDash.[0]
            let endOfSequence = int splitOnDash.[1]
            let fullSequence = [| startOfSequence..endOfSequence |] |> Seq.ofArray
            fullSequence)

    printfn "Day 2:1 Solution"
    printfn "%A" withSequence
