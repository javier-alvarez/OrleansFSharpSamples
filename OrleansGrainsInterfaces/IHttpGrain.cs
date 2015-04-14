namespace OrleansGrainsInterfaces
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using Orleans;

    public interface IHttpGrain : IGrain
    {
        Task<HttpStatusCode> Get(Uri uri);
    }
}
