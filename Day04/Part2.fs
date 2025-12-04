module Day04.Part2

open System.IO

let run () =
    let res = File.ReadAllLines "./Day03/input.txt"
    printfn "Day 4:2 Solution"
    printfn "%A" res
