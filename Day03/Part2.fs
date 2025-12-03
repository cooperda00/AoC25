module Day03.Part2

open System.IO


let run () =
    let res =
        File.ReadAllLines "./Day03/input.txt"
        |> Array.map (fun line ->
            let charsArray = Seq.toArray line
            let allButLast = Array.take (Array.length charsArray - 1) charsArray // we need at least an element at the end
            let largest = allButLast |> Array.sortDescending |> Array.head
            let largestIndex = Array.findIndex (fun num -> num = largest) charsArray
            let restChars = charsArray[largestIndex + 1 ..]
            let secondLargest = restChars |> Array.sortDescending |> Array.head
            let digits = $"{largest}{secondLargest}"
            let number = int digits
            number)
        |> Array.sum


    printfn "Day 3:1 Solution"
    printfn "%A" res
