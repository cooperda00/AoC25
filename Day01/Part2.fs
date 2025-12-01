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

        position, count
    | Subtract ->
        for _ = 1 to turns do
            let newPosition =
                match position with
                | 0 -> 99
                | num -> num - 1

            if newPosition = 0 then
                count <- count + 1

            position <- newPosition

        position, count


let calcZeroes startPosition instruction =
    let operation, turns = instruction

    let step =
        match operation with
        | Add -> fun pos -> if pos = 99 then 0 else pos + 1
        | Subtract -> fun pos -> if pos = 0 then 99 else pos - 1

    // Builds a sequence based on the number of turns and startPosition. The internal will be the position
    let positions =
        startPosition
        // Infinitely steps through according to the step function
        |> Seq.unfold (fun pos ->
            let next = step pos
            Some(next, next))
        // Because Seq is lazy, the .take here determines the max number of iterations
        |> Seq.take turns

    // The end position is the last item in a sequence
    let endPosition = positions |> Seq.last
    // Filter to only zeroes and count
    let zeroCount = positions |> Seq.filter ((=) 0) |> Seq.length

    endPosition, zeroCount

let calculateResult startPosition line =
    let instruction = parseInstruction line
    let position, count = calcZeroes startPosition instruction
    count, position


let run () =
    let textContent = File.ReadLines "./Day01/input.txt"

    let res =
        ((0, 50), textContent)
        ||> Seq.fold (fun (out, startPosition) line ->
            let count, newLocation = calculateResult startPosition line
            out + count, newLocation)



    printfn "Day 01 Solution"
    printfn "%A" res
