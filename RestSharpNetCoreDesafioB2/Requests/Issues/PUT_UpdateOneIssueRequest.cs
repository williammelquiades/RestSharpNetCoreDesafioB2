using System.IO;
using System.Text;
using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class PUT_UpdateOneIssueRequest : RequestBase
    {
        public PUT_UpdateOneIssueRequest(string issue_id)
        {
            requestService = "/api/rest/issues/{issue_id}";

            method = Method.PATCH;

            parameters.Add("issue_id", issue_id);
        }

        public void SetJsonBoby(string summary,
                                string priorityName
                                )
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Issues/UpdateIssues.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$summary", summary);
            jsonBody = jsonBody.Replace("$priorityName", priorityName);

        }
    }
}
