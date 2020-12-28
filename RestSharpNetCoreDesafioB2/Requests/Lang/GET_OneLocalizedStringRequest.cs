using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;

namespace RestSharpNetCoreDesafioB2.Requests.Lang
{
    public class GET_OneLocalizedStringRequest : RequestBase
    {
        public GET_OneLocalizedStringRequest(string all_projects)
        {
            requestService = "/api/rest/lang";

            method = Method.GET;

            parameters.Add("string", all_projects);
        }
    }
}
