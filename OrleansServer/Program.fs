// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

module Echo
open System;
open Orleans.Runtime.Host;
open OrleansGrainsInterfaces;

[<EntryPoint>]
let main argv = 

    use silo = new SiloHost("Demo")
    silo.LoadOrleansConfig()
    silo.InitializeOrleansSilo()

    printfn "%O" (silo.StartOrleansSilo())

    Console.Read() |> ignore
    0 // return an integer exit code
