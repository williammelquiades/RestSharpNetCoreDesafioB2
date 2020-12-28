using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.DBSteps.Issues;
using RestSharpNetCoreDesafioB2.DBSteps.Projects;
using RestSharpNetCoreDesafioB2.Helpers;
using RestSharpNetCoreDesafioB2.Requests.Issues;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Tests.Issues
{
    [TestFixture]
    public class IssueTests : TestBase
    {
        private POST_CreateAnIssueRequest sendOneIssue;
        private GET_OneIssueRequest oneIssue;
        private POST_CreateIssueWithAtttachmentsRequest issueAttachments;
        private GET_IssueForPageRequest issuePage;
        private GET_IssueReportMeRequest reportMe;
        private POST_AddAttachmentsToIssueRequest addAttachments;

        IRestResponse<dynamic> response;

        [Test]
        [Parallelizable]
        public void CreateAnIssueErro()
        {

            #region Parameters
            string summary = "This is a test issue";
            string description = "Description for sample REST issue.";
            string categoryName = "General";
            string projectName = "Projeto Api";
            string nomePriority = "normal";

            //Expected Result
            string statusCodeExpected = "BadRequest";
            string message = "Project not specified";
            string code = "11";
            string localized = "A necessary field \"project\" was empty. Please recheck your inputs.";

            #endregion


            #region Request
            sendOneIssue = new POST_CreateAnIssueRequest();

            sendOneIssue.SetJsonBody(summary, description, categoryName, projectName);

            response = sendOneIssue.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(message, response.Data["message"].ToString());
                Assert.AreEqual(code, response.Data["code"].ToString());
                Assert.AreEqual(localized, response.Data["localized"].ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void CreateAnIssueSucess()
        {
            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectByNameRandom();
            string nameProject = dataProject[0];

            string summary = "Automation Test Api RestSharp";
            string description = "Description " + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string categoryName = "General";

            // Expected Result
            string statusCodeExpected = "Created"; // "OK";
            #endregion

            #region Request
            sendOneIssue = new POST_CreateAnIssueRequest();

            sendOneIssue.SetJsonBody(summary, description, categoryName, nameProject);

            response = sendOneIssue.ExecuteRequest();

            //List<string> dadosProblema = SolicitacaoDBSteps.RetornaInfoTarefaCriadaDB(description);
            #endregion


            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());           
                Assert.AreEqual(summary, response.Data["issue"]["summary"].ToString());
                Assert.AreEqual(description, response.Data["issue"]["description"].ToString());
                Assert.AreEqual(nameProject, response.Data["issue"]["project"]["name"].ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void CreateAnIssueAttachmentsSucess()
        {

            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectByNameRandom();
            string nameProject = dataProject[0];

            string summary = "Automation Test Api RestSharp";
            string description = "Description " + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string categoryName = "General";

            string nameFile = "test.pdf";
            string path = GeneralHelpers.ReturnProjectPath() + @"Resources/test.pdf";

            // Expected Result
            string statusCodeExpected = "Created";
            #endregion

            #region Request
            issueAttachments = new POST_CreateIssueWithAtttachmentsRequest();

            issueAttachments.SetJsonBody(path,nameFile,nameProject,summary,description,categoryName);
            
            issueAttachments.addFile(nameFile, path);

            response = issueAttachments.ExecuteRequest();
            #endregion


            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(summary, response.Data["issue"]["summary"].ToString());
                Assert.AreEqual(description, response.Data["issue"]["description"].ToString());
                Assert.AreEqual(nameProject, response.Data["issue"]["project"]["name"].ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void CreateAnIssueAttachmentsLarger2mb()
        {

            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectByNameRandom();
            string nameProject = dataProject[0];

            string summary = "Automation Test Api RestSharp";
            string description = "Description " + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string categoryName = "General";

            string nameFile = "test.pdf";
            string path = GeneralHelpers.ReturnProjectPath() + @"Resources/user-handbook.pdf";

            // Expected Result
            string statusCodeExpected = "BadRequest";
            int statusCode = 400;
            string message = "File '"+nameFile+"' too big";
            #endregion

            #region Request
            issueAttachments = new POST_CreateIssueWithAtttachmentsRequest();

            issueAttachments.SetJsonBody(path, nameFile, nameProject, summary, description, categoryName);

            issueAttachments.addFile(nameFile, path);

            response = issueAttachments.ExecuteRequest();
            #endregion


            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(statusCode, (int)response.StatusCode);
                Assert.AreEqual(message, response.Data["message"].ToString());

            });
            #endregion
        }


        [Test]
        [Parallelizable]
        public void AddAttachmentsInAnIssueSucess()
        {

            #region Parameters
            List<string> dataIssue = IssuesBDSteps.ReturnIdIssuesRandom();
            string issue_id = dataIssue[0];

            string nameFile = "test.pdf";
            string path = GeneralHelpers.ReturnProjectPath() + @"Resources/test.pdf";

            // Expected Result
            string statusCodeExpected = "Created";
            #endregion

            #region Request
            addAttachments = new POST_AddAttachmentsToIssueRequest(issue_id);

            addAttachments.SetJsonBody(path, nameFile);

            addAttachments.addFile(path, nameFile);

            response = addAttachments.ExecuteRequest();
            #endregion


            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                //Assert.AreEqual(summary, response.Data["issue"]["summary"].ToString());
                //Assert.AreEqual(description, response.Data["issue"]["description"].ToString());
                //Assert.AreEqual(nameProject, response.Data["issue"]["project"]["name"].ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void GetOneIssueSucess()
        {
            #region Parameters
            List<string> dataIssue = IssuesBDSteps.ReturnIssuesRandom();
            string issue_id = dataIssue[0];

            //Expected Result
            string statusCodeExpected = "OK";
    
            string summary = dataIssue[1];
            string description = dataIssue[2];
            string nameProject = dataIssue[4];
  
            #endregion

            #region Request
            oneIssue = new GET_OneIssueRequest(issue_id);

            response = oneIssue.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());          
                Assert.AreEqual(summary, response.Data["issues"][0]["summary"].ToString());
                Assert.AreEqual(description, response.Data["issues"][0]["description"].ToString());          
                Assert.AreEqual(nameProject, response.Data["issues"][0]["project"]["name"].ToString());             
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void GetOneIssueInvalidData()
        {
            #region Parameters 
            string issue_id = "0";

            //Expected Result
            string statusCodeExpected = "NotFound";
            string message = "Issue #0 not found";
            string code = "1100";
            string localized = "Issue 0 not found.";
            #endregion

            #region Request
            oneIssue = new GET_OneIssueRequest(issue_id);

            response = oneIssue.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(message, response.Data["message"].ToString());
                Assert.AreEqual(code, response.Data["code"].ToString());
                Assert.AreEqual(localized, response.Data["localized"].ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void GetAllIssue()
        {
            #region Parameters 
            string page_size = "20";
            string page = "1";

            //Expected Result
            string statusCodeExpected = "OK";
            #endregion

            #region Request
            issuePage = new GET_IssueForPageRequest(page_size, page);

            response = issuePage.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void GetIssueReportByMe()
        {
            #region Parameters 
            string filter_id = "reported";

            //Expected Result
            string statusCodeExpected = "OK";
            #endregion

            #region Request
            reportMe = new GET_IssueReportMeRequest(filter_id);

            response = reportMe.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void GetIssueAssignedToMe()
        {
            #region Parameters 
            string filter_id = "assigned";

            //Expected Result
            string statusCodeExpected = "OK"; //NotFound

            #endregion

            #region Request
            reportMe = new GET_IssueReportMeRequest(filter_id);

            response = reportMe.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void GetIssueMonitoredByMe()
        {
            #region Parameters 
            string filter_id = "monitored";

            //Expected Result
            string statusCodeExpected = "OK";

            #endregion

            #region Request
            reportMe = new GET_IssueReportMeRequest(filter_id);

            response = reportMe.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void GetIssueReportUnassigned()
        {
            #region Parameters 
            string filter_id = "unassigned";

            //Expected Result
            string statusCodeExpected = "OK";
            
            #endregion

            #region Request
            reportMe = new GET_IssueReportMeRequest(filter_id);

            response = reportMe.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void GetIssueReportUnassignedErro()
        {
            #region Parameters 
            string filter_id = "NotFound";

            //Expected Result
            string statusCodeExpected = "OK";
            int status_code = 404;
            string fault_string = "Unknown filter 'NotFound'";
            #endregion

            #region Request
            reportMe = new GET_IssueReportMeRequest(filter_id);

            response = reportMe.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
               // Assert.AreEqual(status_code, (int)response.Data["issues"]["status_code"]);
                Assert.AreEqual(fault_string, response.Data["issues"]["fault_string"].ToString());
            });
            #endregion
        }

    }
}
