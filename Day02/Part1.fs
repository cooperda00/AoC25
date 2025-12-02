module Day02.Part1

open System.IO
open System.Numerics

type EvenOrOdd =
    | Even
    | Odd

let getNumberOfDigits (num: bigint) : int = num.ToString().Length

let getEvenOrOdd (numberOfDigits: int) : EvenOrOdd =
    if numberOfDigits % 2 = 0 then Even else Odd


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
                    let numberOfDigits = getNumberOfDigits num

                    match getEvenOrOdd numberOfDigits with
                    | Odd -> false
                    | Even ->
                        let parts = num.ToString() |> Seq.splitInto 2 |> Seq.toArray
                        let first, second = parts.[0], parts.[1]
                        if first = second then true else false)

            filteredDoubles)
        |> Seq.concat
        |> Seq.sum


    printfn "Day 2:1 Solution"
    printfn "%A" withSequence
