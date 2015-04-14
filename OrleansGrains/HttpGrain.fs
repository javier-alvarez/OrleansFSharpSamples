module HttpGrain

open System
open System.Threading.Tasks
open Orleans
open OrleansGrainsInterfaces
open System.Net.Http
open FSharpx.Task

type HttpGrain() = 
    inherit Orleans.Grain()
    interface IHttpGrain with
        override this.Get(uri:Uri) =
            let task = TaskBuilder(scheduler = TaskScheduler.Current)
            task {
                let httpClient = new HttpClient()
                let! response = httpClient.GetAsync(uri)
                return response.StatusCode
                }

