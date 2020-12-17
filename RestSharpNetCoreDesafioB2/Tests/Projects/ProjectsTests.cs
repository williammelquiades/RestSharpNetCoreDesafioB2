using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.DBSteps.Projects;
using RestSharpNetCoreDesafioB2.Helpers;
using RestSharpNetCoreDesafioB2.Requests.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Tests.Projects
{
    [TestFixture]
    public class ProjectsTests : TestBase
    {
        private POST_CreateProjectRequest createProjet;
        private GET_AllProjectsRequest allProjects;
        private GET_OneProjectRequest oneProject;
        private DEL_DeleteOneProjectRequest oneDelete;
        private PATCH_UpdateOneProjectRequest updateProject;
        private POST_CreateOneVersion oneVersion;

        IRestResponse<dynamic> response;

        #region Tests GETs Project
        [Test]
        public void GetAllProjectsBDSucess()
        {
            #region Parameters
            List<string> quantityProjectBD = ProjectsBDSteps.ReturnAllProject();
            //Expected Result
            int statusCodeResponse = 200;
            #endregion

            #region Request
            allProjects = new GET_AllProjectsRequest();

            response = allProjects.ExecuteRequest();
            JObject resultJsonBody = JObject.Parse(response.Data.ToString());
            #endregion

            #region Asserts
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, (int)response.StatusCode);
                foreach (JToken IdProjeto in resultJsonBody.SelectTokens("*.id"))
                {
                    string id = IdProjeto.ToString();
                    Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(quantityProjectBD, id));
                }
            });
            #endregion
        }

        [Test]
        public void GetOneProjectBDSucess()
        {
            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectAndID();
            string project_id = dataProject[0];
            string nameProject = dataProject[1];

            //Expected Result
            string statusCodeResponse = "OK";
            #endregion

            #region Request
            oneProject = new GET_OneProjectRequest(project_id);

            response = oneProject.ExecuteRequest();
            #endregion

            #region Asserts

            Assert.Multiple(() =>
            {

                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());
                Assert.AreEqual(project_id, response.Data["projects"][0]["id"].ToString());
                Assert.AreEqual(nameProject, response.Data["projects"][0]["name"].ToString());

            });
            #endregion
        }
        #endregion

        #region Tests POSTs project
        [Test]
        public void CreateProjectSucess()
        {

            #region Parameters
            string name = "Project Rest API Automation " + GeneralHelpers.ReturnStringWithRandomNumbers(3);
            string nameStatus = "development";
            string labelStatus = "development";
            string description = "Report problems with the actual bug tracker here.";
            string file_path = "/tmp/";
            string nameView_state = "public";
            string labelView_state = "public";

            //Expected Result
            string statusCodeResponse = "Created";
            #endregion

            #region Request
            createProjet = new POST_CreateProjectRequest();

            createProjet.SetJsonBody(name, nameStatus, labelStatus, description, file_path, nameView_state, labelView_state);

            response = createProjet.ExecuteRequest();
            #endregion

            #region Asserts
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());
                Assert.AreEqual(name, response.Data["project"]["name"].ToString());
                Assert.AreEqual(description, response.Data["project"]["description"].ToString());
                StringAssert.IsMatch("(\\d+)", response.Data["project"]["id"].ToString());
            });
            #endregion
        }

        [Test]
        public void CreateProjectWithNameEqual()
        {

            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectByName();

            string name = dataProject[0];
            string nameStatus = "development";
            string labelStatus = "development";
            string description = "Report problems with the actual bug tracker here.";
            string file_path = "/tmp/";
            string nameView_state = "public";
            string labelView_state = "public";

            //Expected Result
            //string statusCodeResponse = "InternalServerError";
            #endregion

            #region Request
            createProjet = new POST_CreateProjectRequest();

            createProjet.SetJsonBody(name, nameStatus, labelStatus, description, file_path, nameView_state, labelView_state);

            response = createProjet.ExecuteRequest();

            string statusCodeResponse = "InternalServerError";
            #endregion

            #region Asserts
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());
            });
            #endregion
        }

        [Test]
        public void CreateOneProjectVersion()
        {
            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectIDRandom();
            string project_id = dataProject[0];

            string nameWithVersion = "v.0." + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            string descriptionVersion = "Descript_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            
            //Expected Result
            string statusCodeResponse = null;
            #endregion

            #region Request
            oneVersion = new POST_CreateOneVersion(project_id);

            oneVersion.SetJsonBody(nameWithVersion, descriptionVersion);

            oneVersion.ExecuteRequest();

            string test = response.Content;

            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());
            });
            #endregion
        }

        #endregion

        #region Tests PATCH Project

        [Test]
        public void UpdateOneProject()
        {
            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectIDRandom();
            string project_id = dataProject[0];

            string newName = "Project API v.0." + GeneralHelpers.ReturnStringWithRandomNumbers(3) + " Automation";

            // Expected Result
            int statusCodeEsperado = 200;
            #endregion

            #region Request
            updateProject = new PATCH_UpdateOneProjectRequest(project_id);

            updateProject.SetJsonBody(project_id, newName);

            response = updateProject.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCodeEsperado, (int)response.StatusCode);
            #endregion
        }

        #endregion

        #region Test DELETE 

        [Test]
        public void DeleteAProject()
        {
            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectIDRandom();
            string project_id = dataProject[0];

            // Expected Result
            int statusCodeEsperado = 200;
            #endregion

            #region Request
            oneDelete = new DEL_DeleteOneProjectRequest(project_id);

            response = oneDelete.ExecuteRequest();
            #endregion

            #region Asserts
            Assert.AreEqual(statusCodeEsperado, (int)response.StatusCode);
            #endregion
        }


        #endregion

    }
}
