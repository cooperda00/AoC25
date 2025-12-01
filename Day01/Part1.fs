module Day01.Part1

open System.IO

type Operation =
    | Add
    | Subtract

type Instruction = (Operation * int)

let parseInstruction (str: string) =
    let chars = str.ToCharArray()
    let head = Array.head chars

    let operation: Operation =
        match head with
        | 'L' -> Subtract
        | 'R' -> Add
        | _ -> failwith $"Unknown operation: {head}"

    let amount = Array.tail chars |> System.String |> int
    operation, amount

let calculateNewLocation (startPosition: int) (instruction: Instruction) : int =
    match instruction with
    | Add, amount -> (startPosition + amount) % 100
    | Subtract, amount ->
        let result = (startPosition - amount) % 100
        if result < 0 then result + 100 else result


let run () =
    let textContent = File.ReadLines "./Day01/input.txt"

    let res =
        ((0, 50), textContent)
        ||> Seq.fold (fun (out, startPosition) line ->
            let instruction = parseInstruction line
            let newLocation = calculateNewLocation startPosition instruction

            let count =
                match newLocation with
                | 0 -> out + 1
                | _ -> out

            count, newLocation)

    printfn "Day 01 Solution"
    printfn "%A" res
