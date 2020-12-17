using System.IO;
using System.Text;
using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class POST_CreateIssueWithAtttachmentsRequest : RequestBase
    {
        public POST_CreateIssueWithAtttachmentsRequest()
        {
            requestService = "/api/rest/issues";

            method = Method.POST;

        }
        public void SetFile(string name, string path)
        {

        }

        public void SetJsonBody(
                                string setFile,
                                string projectName
                                )

        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Issues/CreateIssueWithAttachments.json", Encoding.UTF8);
            jsonBody = jsonBody.//Replace("$summary", summary)
                               //.Replace("$description", description)
                               //.Replace("$nomePriority", categoryName)
                               Replace("content", setFile)
                               .Replace("$projectName", projectName);


        }

    }
}
