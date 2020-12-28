using RestSharpNetCoreDesafioB2.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class DEL_OneNoteRequest : RequestBase
    {
        public DEL_OneNoteRequest(string issue_note_id, string issue_id)
        {
            requestService = "/api/rest/issues/{issue_id}/notes/{issue_note_id}";

            method = Method.DELETE;

            parameters.Add("issue_note_id", issue_note_id);
            parameters.Add("issue_id", issue_id);

        }
    }
}
