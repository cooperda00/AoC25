module Day05.Part1

open System.IO

let run () =
    let lines = File.ReadAllLines "./Day05/input.txt"

    printfn "Day 5:1 Solution"
    printfn "%A" lines
