open System

// let parseA str =
//     match str with
//     | s when String.IsNullOrEmpty s -> false, ""
//     | s when s.[0] = 'A' -> true, str.[1..]
//     | _ -> false, str

type ParseResult<'a> =
    | Success of 'a
    | Failure of string

type Parser<'T> = Parser of (string -> ParseResult<'T * string>)

let run parser input =
    let (Parser innerFn) = parser
    innerFn input

let parseChar charToMatch =
    let inner str =
        match str with
        | s when String.IsNullOrEmpty s -> Failure "No more input"
        | s when s.[0] = charToMatch -> Success(charToMatch, str.[1..])
        | _ -> Failure(sprintf "Expecting '%c'. Got '%c'" charToMatch str.[0])

    Parser inner


let andThen parser1 parser2 =
    let innerFn input =
        let result1 = run parser1 input

        match result1 with
        | Failure err -> Failure err
        | Success(value1, remaining1) ->
            let result2 = run parser2 remaining1

            match result2 with
            | Failure err -> Failure err
            | Success(value2, remaining2) -> Success((value1, value2), remaining2)

    Parser innerFn

let (.>>.) = andThen

// let orElse parser1 parser2 =
//   let innerFn input =
//     // run parser1 with the input
//     let result1 = run parser1 input

//     // test the result for Failure/Success
//     match result1 with
//     | Success result ->
//       // if success, return the original result
//       result1

//     | Failure err ->
//       // if failed, run parser2 with the input
//       let result2 = run parser2 input

//       // return parser2's result
//       result2

//   // return the inner function
//   Parser innerFn

let parseA = parseChar 'A'
let parseB = parseChar 'B'
let parseAbInfix = parseA .>>. parseB

let parseAbNonInfix = andThen parseA parseB

let res = run parseAbNonInfix "ABcdef"

printfn "%A" res
