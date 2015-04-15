module HttpGrain

open System
open System.Threading.Tasks
open Orleans
open OrleansGrainsInterfaces
open System.Net.Http
open FSharpx.Task

type HttpGrain() = 
    inherit Orleans.Grain()
    let mutable counter: int = 0
    interface IHttpGrain with
        override this.Get(uri:Uri) =
            let task = TaskBuilder(scheduler = TaskScheduler.Current)
            counter <- counter + 1
            if (counter % 10 = 0) then raise (System.Exception("Multiple of 10!"))
            task {
                let httpClient = new HttpClient()
                let! response = httpClient.GetAsync(uri)
                return response.StatusCode
                }

