using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;
using System.IO;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Requests.Projects
{
    public class POST_CreateSubProjectResquest : RequestBase
    {
        public POST_CreateSubProjectResquest(string project_id)
        {
            requestService = "/api/rest/projects/{project_id}/subprojects";

            method = Method.POST;

            parameters.Add("project_id", project_id);

        }

        public void SetJosnBoby(string nomeSubProjeto)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Projects/CreateSubProject.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("nomeProjeto", nomeSubProjeto);

        }
    }
}
