module Day04.Part1

open System.IO

let run () =
    let res = File.ReadAllLines "./Day04/input.txt"
    printfn "Day 4:1 Solution"
    printfn "%A" res
