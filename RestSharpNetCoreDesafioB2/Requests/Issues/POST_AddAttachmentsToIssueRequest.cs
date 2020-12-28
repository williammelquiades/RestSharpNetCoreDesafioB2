using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;
using RestSharp;
using System.IO;
using System.Text;
using System;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class POST_AddAttachmentsToIssueRequest : RequestBase
    {
        public POST_AddAttachmentsToIssueRequest(string issue_id)
        {
            requestService = "/api/rest/issues/{issue_id}/files";

            method = Method.POST;

            parameters.Add("issue_id", issue_id);
        }

        public void addFile(string path , string nameFile)
        {
            byte[] file_path = File.ReadAllBytes(nameFile);
            string encode = Convert.ToBase64String(file_path);

            submitFiles.Add(nameFile, encode);
        }

        public void SetJsonBody(
                               string setFile,
                               string nameFile)

        {
            byte[] file_path = File.ReadAllBytes(setFile);
            string encode = Convert.ToBase64String(file_path);

            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Issues/AddAttachments.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$nameFile", nameFile)
                               .Replace("$content", encode);


        }

    }
}
