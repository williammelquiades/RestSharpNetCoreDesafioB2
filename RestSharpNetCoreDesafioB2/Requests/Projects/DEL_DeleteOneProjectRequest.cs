using RestSharpNetCoreDesafioB2.Bases;
using RestSharp;

namespace RestSharpNetCoreDesafioB2.Requests.Projects
{
    public class DEL_DeleteOneProjectRequest : RequestBase
    {
        public DEL_DeleteOneProjectRequest(string project_id)
        {
            requestService = "/api/rest/projects/{project_id}";

            method = Method.DELETE;

            parameters.Add("project_id", project_id);
        }
    }
}
