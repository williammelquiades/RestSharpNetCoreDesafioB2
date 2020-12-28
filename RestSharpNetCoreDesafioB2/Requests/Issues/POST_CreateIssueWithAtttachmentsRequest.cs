using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;
using System.IO;
using System.Text;
using RestSharp;
using System;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class POST_CreateIssueWithAtttachmentsRequest : RequestBase
    {
        public POST_CreateIssueWithAtttachmentsRequest()
        {
            requestService = "/api/rest/issues";

            method = Method.POST;

        }
        public void addFile(string name, string path)
        {
            byte[] file_path = File.ReadAllBytes(path);
            string encode = Convert.ToBase64String(file_path);

            submitFiles.Add(name, encode);
        }

        public void SetJsonBody(
                                string setFile,
                                string nameFile,
                                string projectName,
                                string summary,
                                string description,
                                string categoryName
                                )

        {

            byte[] file_path = File.ReadAllBytes(setFile);
            string encode = Convert.ToBase64String(file_path);

            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Issues/CreateIssueWithAttachments.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$summary", summary)
                               .Replace("$description", description)
                               .Replace("$nomePriority", categoryName)
                               .Replace("$nameFile", nameFile)
                               .Replace("$content", encode)
                               .Replace("$projectName", projectName);


        }

    }
}
