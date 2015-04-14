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
