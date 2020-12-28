using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;

namespace RestSharpNetCoreDesafioB2.Requests.Projects
{
    public class DEL_DeleteOneSubProjectRequest : RequestBase
    {
        public DEL_DeleteOneSubProjectRequest(string project_id)
        {
            requestService = "/api/rest/projects/{project_id}";

            method = Method.DELETE;

            parameters.Add("project_id", project_id);
        }
    }
}