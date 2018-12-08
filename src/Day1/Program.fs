open System
open System.Collections.Generic

module Seq =
    let iterWhile (f : 'T -> bool) (seq : IEnumerable<'T>) = 
        let rec iterLoop f (xs : IEnumerator<'T>) = 
            match xs.MoveNext() with
                | true -> if f xs.Current then iterLoop f xs
                | false -> ()
        iterLoop f (seq.GetEnumerator())

let FindFirstRepeat (freqs : Int32[]) =
    let freqStream = Seq.initInfinite (fun i -> freqs.[i % freqs.Length])
    let freqSet = new HashSet<Int32>()
    let mutable current = 0
    freqStream 
        |> Seq.iterWhile (fun i -> 
            current <- current + i
            freqSet.Add(current))
    current

[<EntryPoint>]
let main argv =
    let freqs = 
        argv 
        |> Array.map (fun s -> Int32.Parse(s.Trim(',', ' ')))
    let freq = 
        freqs
        |> Array.reduce (fun acc item -> acc + item)
    let firstRepeat = FindFirstRepeat freqs
    printf "Current: %d; First Repeat: %d" freq firstRepeat
    Console.ReadLine() |> ignore
    0