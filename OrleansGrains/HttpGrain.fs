module HttpGrain

open System
open System.Threading.Tasks
open Orleans
open OrleansGrainsInterfaces
open System.Net.Http
open FSharpx.Task

let Ignore (task:Task) =
      let continuation (t : Task) : unit =
          if t.IsFaulted 
          then raise t.Exception
          else ()
      task.ContinueWith continuation

type HttpGrain() = 
    inherit Orleans.Grain()
    let mutable counter: int = 0
    interface IHttpGrain with
        override this.Get(uri:Uri) =
            let task = TaskBuilder(scheduler = TaskScheduler.Current)
            counter <- counter + 1
            if (counter % 10 = 0) then raise (System.Exception("Multiple of 10!"))
            task {

                let ts1 = TaskScheduler.Current; // Grabs the Orleans task scheduler
                assert ts1.GetType().Namespace.StartsWith("Orleans")
                let! _ = Task.Delay(100) |> Ignore

                // check the scheduler
                let ts2 = TaskScheduler.Current; // Grabs the Orleans task scheduler
                assert ts2.GetType().Namespace.StartsWith("Orleans")

                assert (ts1=ts2)

                let httpClient = new HttpClient()
                let! response = httpClient.GetAsync(uri)
                return response.StatusCode
                }

