module Day03.Part2

open System.IO

// let findLargestWithinRange (startIndex: int) (endIndex: int) (arr: char array) =
//     arr[startIndex..endIndex]
//     |> Array.indexed
//     |> Array.maxBy snd // characters are compared by unicode so '9' > '8'. Returns first occurence if multiple
//     |> fun (windowIndex, digit) -> digit, startIndex + windowIndex + 1

let findLargestWithinRange (startIndex: int) (endIndex: int) (arr: char array) =
    let mutable maxDigit = '0'
    let mutable maxPos = startIndex

    for i in startIndex..endIndex do
        if arr[i] > maxDigit then
            maxDigit <- arr[i]
            maxPos <- i

    maxDigit, maxPos + 1

let calculateNumberFromLine (input: char array) =
    let finalIndex = input.Length - 1
    let first, firstIndex = input |> findLargestWithinRange 0 (finalIndex - 11)

    let second, secondIndex =
        input |> findLargestWithinRange firstIndex (finalIndex - 10)

    let third, thirdIndex = input |> findLargestWithinRange secondIndex (finalIndex - 9)

    let fourth, fourthIndex =
        input |> findLargestWithinRange thirdIndex (finalIndex - 8)

    let fifth, fifthIndex = input |> findLargestWithinRange fourthIndex (finalIndex - 7)

    let sixth, sixthIndex = input |> findLargestWithinRange fifthIndex (finalIndex - 6)

    let seventh, seventhIndex =
        input |> findLargestWithinRange sixthIndex (finalIndex - 5)

    let eighth, eighthIndex =
        input |> findLargestWithinRange seventhIndex (finalIndex - 4)

    let ninth, ninthIndex = input |> findLargestWithinRange eighthIndex (finalIndex - 3)

    let tenth, tenthIndex = input |> findLargestWithinRange ninthIndex (finalIndex - 2)

    let eleventh, eleventhIndex =
        input |> findLargestWithinRange tenthIndex (finalIndex - 1)

    let twelfth, _ = input |> findLargestWithinRange eleventhIndex finalIndex
    $"{first}{second}{third}{fourth}{fifth}{sixth}{seventh}{eighth}{ninth}{tenth}{eleventh}{twelfth}"


let run () =
    let res =
        File.ReadAllLines "./Day03/input.txt"
        |> Array.map (fun line -> line |> Seq.toArray |> calculateNumberFromLine)
        |> Array.sumBy bigint.Parse

    printfn "Day 3:2 Solution"
    printfn "%A" res
