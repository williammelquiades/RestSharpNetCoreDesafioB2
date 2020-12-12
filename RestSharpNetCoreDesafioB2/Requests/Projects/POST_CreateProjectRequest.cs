using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;
using System.IO;
using System.Text;


namespace RestSharpNetCoreDesafioB2.Requests.Projetos
{
    public class POST_CreateProjectRequest : RequestBase
    {
        public POST_CreateProjectRequest()
        {
            requestService = "/api/rest/projects/";
            method = Method.POST;
        }

        public void SetJsonBody(
                                string name,
                                string nameStatus,
                                string labelStatus,
                                string description,
                                string file_path,
                                string nameView_state,
                                string labelView_state
                                )
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Projetos/CreateProject.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$name", name)
            .Replace("$nameStatus", nameStatus)
            .Replace("$labelStatus", labelStatus)
            .Replace("$description", description)
            .Replace("$file_path", file_path)
            .Replace("$nameView_state", nameView_state)
            .Replace("$labelView_state", labelView_state);
        }
    }
}
