// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open System

[<EntryPoint>]
let main argv = 

    Orleans.GrainClient.Initialize("ClientConfiguration.xml")
    let grain = OrleansGrainsInterfaces.HttpGrainFactory.GetGrain(1L)

    while true do 
        async{
            let! statusCode = (grain.Get(Uri("http://www.bing.com"))) |> Async.AwaitTask 
            printfn "%s Bing responded %s" (DateTime.Now.ToString("O")) (statusCode.ToString())
        }|> Async.RunSynchronously


    0 // return an integer exit code
