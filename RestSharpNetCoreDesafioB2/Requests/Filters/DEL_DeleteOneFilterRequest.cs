using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;
using System.IO;
using System.Text;
using RestSharp;

namespace RestSharpNetCoreDesafioB2.Requests.Filters
{
    public class DEL_DeleteOneFilterRequest : RequestBase
    {
        public DEL_DeleteOneFilterRequest(string filter_id)
        {
            requestService = "/api/rest/filters/{filter_id}";

            method = Method.DELETE;

            parameters.Add("filter_id", filter_id);
        }
    }
}
