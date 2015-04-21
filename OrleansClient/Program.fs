open System
open System.Threading.Tasks
open FSharpx.Task

let Ignore (task:Task) =
      let continuation (t : Task) : unit =
          if t.IsFaulted 
          then raise t.Exception
          else ()
      task.ContinueWith continuation

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

                let ts1 = TaskScheduler.Current; // Grab the current TPL task scheduler
                assert ts1.GetType().Namespace.StartsWith("Orleans") // Check that it is indeed an Orleans scheduler
                let! _ = Task.Delay(2000) |> Ignore

                // check the scheduler
                let ts2 = TaskScheduler.Current; // Grab the current TPL task scheduler
                assert ts2.GetType().Namespace.StartsWith("Orleans") // Check that it is indeed still an Orleans scheduler

                assert (ts1=ts2)

            } |> Task.WaitAll
        with 
        | _ -> printfn "hello"


    0 // return an integer exit code
