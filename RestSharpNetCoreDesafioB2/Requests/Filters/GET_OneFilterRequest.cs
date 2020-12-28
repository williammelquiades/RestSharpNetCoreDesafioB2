using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;

namespace RestSharpNetCoreDesafioB2.Requests.Filters
{
    public class GET_OneFilterRequest : RequestBase
    {
        public GET_OneFilterRequest(string filter_id)
        {
            requestService = "/api/rest/filters/{filter_id}";

            method = Method.GET;

            parameters.Add("filter_id", filter_id);
        }
    }
}
