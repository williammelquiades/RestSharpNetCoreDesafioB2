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
    public class CreateIssueTests : TestBase
    {
        private POST_CreateAnIssueRequest sendOneIssue;
        private GET_OneIssue oneIssue;

        IRestResponse<dynamic> response;

        [Test]
        public void CreateAnIssueErro()
        {

            #region Parameters
            string summary = "This is a test issue";
            string description = "Description for sample REST issue.";
            string categoryName = "General";
            string projectName = "Projeto Api";
            string nomePriority = "normal";


            //Resultado esperado
            string statusCodeResponse = "BadRequest";
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
                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());
                Assert.AreEqual(message, response.Data["message"].ToString());
                Assert.AreEqual(code, response.Data["code"].ToString());
                Assert.AreEqual(localized, response.Data["localized"].ToString());
            });
            #endregion
        }

        [Test]
        public void CreateAnIssueMinimal()
        {

            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectByNameRandom();
            string nameProject = dataProject[0];

            string summary = "Automation Test Api";
            string description = "Description " + GeneralHelpers.ReturnStringWithRandomCharacters(5);
            string categoryName = "General";

            // Result Response
            string statusCodeResponse = "Created";
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
                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());           
                //Assert.AreEqual(summary, response.Data["issue"]["summary"].ToString());
                //Assert.AreEqual(description, response.Data["issue"]["description"].ToString());
                //Assert.AreEqual(nameProject, response.Data["issue"]["project"]["name"].ToString());
                



            });
            #endregion
        }


        [Test]
        public void GetOneIssue()
        {
            #region Parameters
            List<string> dataIssue = IssuesBDSteps.ReturnIssuesRandom();
            string issue_id = dataIssue[0];

            //Resultado esperado
            string statusCodeResponse = "OK";
    
            string summary = dataIssue[1];
            string description = dataIssue[2];
            string nameProject = dataIssue[4];
  
            #endregion

            #region Request
            oneIssue = new GET_OneIssue(issue_id);

            response = oneIssue.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());          
                Assert.AreEqual(summary, response.Data["issues"][0]["summary"].ToString());
                Assert.AreEqual(description, response.Data["issues"][0]["description"].ToString());          
                Assert.AreEqual(nameProject, response.Data["issues"][0]["project"]["name"].ToString());             
            });
            #endregion
        }


    }
}
