using RestSharp;
using System.Text;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;
using System.IO;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class POST_CreateNoteInIssueRequest : RequestBase
    {
        public POST_CreateNoteInIssueRequest(string issue_id)
        {

            requestService = "/api/rest/issues/{issue_id}/notes";

            method = Method.POST;

            parameters.Add("issue_id", issue_id);
        }

        protected string viewStatus = string.Empty;

        public void SetJsonBody(string viewStatus, string text)
        {
            if (viewStatus.Equals(string.Empty))
            {
                viewStatus = "public";
                jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Issues/CreateNotes.json", Encoding.UTF8);
                jsonBody = jsonBody
                        .Replace("$text", text)
                        .Replace("$viewStatus", viewStatus);
            }
            else
                jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Issues/CreateNotes.json", Encoding.UTF8);
                jsonBody = jsonBody
                .Replace("$text", text)
                .Replace("$viewStatus", viewStatus);
        }
    }
}
