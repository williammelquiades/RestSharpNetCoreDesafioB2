using RestSharpNetCoreDesafioB2.Bases;
using RestSharp;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class POST_MonitorsIssueRequest : RequestBase
    {
        public POST_MonitorsIssueRequest(string issue_id)
        {
            requestService = "/api/rest/issues/{issue_id}/monitors";

            method = Method.POST;

            parameters.Add("issue_id", issue_id);
        }
    }
}
