open System
open System.Threading.Tasks
open FSharpx.Task

[<EntryPoint>]
let main argv = 

    Orleans.GrainClient.Initialize("ClientConfiguration.xml")
    let grain = OrleansGrainsInterfaces.HttpGrainFactory.GetGrain(1L)
    let task = TaskBuilder(scheduler = TaskScheduler.Current)
    while true do 
        try
            task{
                let! statusCode = grain.Get(Uri("http://www.bing.com"))
                printfn "%s Bing responded %s" (DateTime.Now.ToString("O")) (statusCode.ToString())
            } |> Task.WaitAll
        with 
        | _ -> printfn "hello"


    0 // return an integer exit code
