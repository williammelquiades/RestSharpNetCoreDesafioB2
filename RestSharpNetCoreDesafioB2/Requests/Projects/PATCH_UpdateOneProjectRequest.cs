using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;
using System.IO;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Requests.Projects
{
    public class PATCH_UpdateOneProjectRequest : RequestBase
    {
        public PATCH_UpdateOneProjectRequest(string project_id)
        {
            requestService = "/api/rest/projects/{project_id}";

            method = Method.PATCH;

            parameters.Add("project_id", project_id);
        }

        public void SetJsonBody(string project_id, string name)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Projects/UpdateProject.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$id", project_id)
                               .Replace("$name", name);
        }

    }
}
