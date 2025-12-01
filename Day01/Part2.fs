module Day01.Part2

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


let calculateZeroes (startPosition: int) (instruction: Instruction) =
    let operation, amount = instruction

    match operation with
    | Add ->
        let completeLoops = amount / 100
        let remainder = amount % 100
        let crossedZero = if startPosition + remainder >= 100 then 1 else 0
        completeLoops + crossedZero
    | Subtract ->
        let completeLoops = amount / 100
        let remainder = amount % 100
        let crossedZero = if remainder > startPosition then 1 else 0
        completeLoops + crossedZero

let calcZeroesMut startPosition instruction =
    let mutable position = startPosition
    let mutable count = 0
    let operation, turns = instruction

    match operation with
    | Add ->
        for _ = 1 to turns do
            let newPosition =
                match position with
                | 99 -> 0
                | num -> num + 1

            if newPosition = 0 then
                count <- count + 1


            position <- newPosition

        count
    | Subtract ->
        for _ = 1 to turns do
            let newPosition =
                match position with
                | 0 -> 99
                | num -> num - 1

            if newPosition = 0 then
                count <- count + 1

            position <- newPosition

        count

let calculateResult startPosition line =
    let instruction = parseInstruction line
    let newLocation = calculateNewLocation startPosition instruction
    let count = calcZeroesMut startPosition instruction
    count, newLocation


let run () =
    let textContent = File.ReadLines "./Day01/input.txt"

    let res =
        ((0, 50), textContent)
        ||> Seq.fold (fun (out, startPosition) line ->
            let count, newLocation = calculateResult startPosition line
            out + count, newLocation)



    printfn "Day 01 Solution"
    printfn "%A" res
