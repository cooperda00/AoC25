module Day02.Part2

open System.IO

let run () =
    let textContent = File.ReadLines "./Day02/input.txt"
    printfn "Day 2:2 Solution"
    printfn "%A" textContent
