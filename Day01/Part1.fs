module Day01.Part1

open System.IO

let run () =
    let textContent = File.ReadAllText "./Day01/input.txt"
    printfn "%A" textContent
    printfn "Day 01 Solution"
