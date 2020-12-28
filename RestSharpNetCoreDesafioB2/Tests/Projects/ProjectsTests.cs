using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.DBSteps.Projects;
using RestSharpNetCoreDesafioB2.Helpers;
using RestSharpNetCoreDesafioB2.Requests.Projects;
using System;
using System.Collections;
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
        [Parallelizable]
        public void GetAllProjectsBDSucess()
        {
            #region Parameters
            List<string> quantityProjectBD = ProjectsBDSteps.ReturnAllProject();

            //Expected Result
            int statusCode = 200;
            #endregion

            #region Request
            allProjects = new GET_AllProjectsRequest();

            response = allProjects.ExecuteRequest();
            JObject resultJsonBody = JObject.Parse(response.Data.ToString());
            #endregion

            #region Asserts
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCode, (int)response.StatusCode);
                foreach (JToken IdProjeto in resultJsonBody.SelectTokens("*.id"))
                {
                    string id = IdProjeto.ToString();
                    Assert.IsTrue(GeneralHelpers.VerificaSeStringEstaContidaNaLista(quantityProjectBD, id));
                }
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void GetOneProjectBDSucess()
        {
            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectAndID();
            string project_id = dataProject[0];
            string nameProject = dataProject[1];

            //Expected Result
            string statusCodeExpected = "OK";
            #endregion

            #region Request
            oneProject = new GET_OneProjectRequest(project_id);

            response = oneProject.ExecuteRequest();
            #endregion

            #region Asserts

            Assert.Multiple(() =>
            {

                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(project_id, response.Data["projects"][0]["id"].ToString());
                Assert.AreEqual(nameProject, response.Data["projects"][0]["name"].ToString());

            });
            #endregion
        }
        #endregion

        #region Tests POSTs project
        [Test]
        [Parallelizable]
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
            string statusCodeExpected = "Created";
            #endregion

            #region Request
            createProjet = new POST_CreateProjectRequest();

            createProjet.SetJsonBody(name, nameStatus, labelStatus, description, file_path, nameView_state, labelView_state);

            response = createProjet.ExecuteRequest();
            #endregion

            #region Asserts
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(name, response.Data["project"]["name"].ToString());
                Assert.AreEqual(description, response.Data["project"]["description"].ToString());
                StringAssert.IsMatch("(\\d+)", response.Data["project"]["id"].ToString());
            });
            #endregion
        }

        #region Data Driven Providers
        public static IEnumerable DataDrivenProjects()
        {
            return GeneralHelpers.ReturnCSVData(GeneralHelpers.ReturnProjectPath() + "Resources/addProjects.csv");
        }
        #endregion

        [Test, TestCaseSource("DataDrivenProjects")]
        [Parallelizable]
        public void CreateProjectSucessDataDriven(ArrayList dataTest)
        {

            #region Parameters
            string name = "Project Rest API Automation " + GeneralHelpers.ReturnStringWithRandomNumbers(3);
            string nameStatus = dataTest[0].ToString();
            string labelStatus = dataTest[1].ToString();
            string description = dataTest[2].ToString();
            string file_path = dataTest[3].ToString();
            string nameView_state = dataTest[4].ToString();
            string labelView_state = dataTest[5].ToString();

            //Expected Result
            string statusCodeExpected = "Created";
            #endregion

            #region Request
            createProjet = new POST_CreateProjectRequest();

            createProjet.SetJsonBody(name, nameStatus, labelStatus, description, file_path, nameView_state, labelView_state);

            response = createProjet.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(name, response.Data["project"]["name"].ToString());
                Assert.AreEqual(description, response.Data["project"]["description"].ToString());
                StringAssert.IsMatch("(\\d+)", response.Data["project"]["id"].ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
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
            string statusCodeExpected = "InternalServerError";
            #endregion

            #region Request
            createProjet = new POST_CreateProjectRequest();

            createProjet.SetJsonBody(name, nameStatus, labelStatus, description, file_path, nameView_state, labelView_state);

            response = createProjet.ExecuteRequest();

            #endregion

            #region Asserts
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void CreateOneProjectVersion()
        {
            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectIDRandom();
            string project_id = dataProject[0];
            //string project_id = "1";

            string nameWithVersion = "v.0." + GeneralHelpers.ReturnStringWithRandomNumbers(3);
            string descriptionVersion = "Descript_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            
            //Expected Result
            int statusCode = 204;
            #endregion

            #region Request
            oneVersion = new POST_CreateOneVersion(project_id);

            oneVersion.SetJsonBody(nameWithVersion, descriptionVersion);

            response = oneVersion.ExecuteRequest();

            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCode, (int)response.StatusCode);
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void CreateOneProjectVersionNull()
        {
            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectIDRandom();
            string project_id = dataProject[0];
            string project__id = string.Empty;

            string descriptionVersion = "Descript_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);

            //Expected Result
            int statusCode = 400;
            string statusCodeExpected = "BadRequest";
            string message = "Invalid version name";
            string code = "11";
            string localized = "A necessary field \"name\" was empty. Please recheck your inputs.";

            #endregion

            #region Request
            oneVersion = new POST_CreateOneVersion(project_id);

            oneVersion.SetJsonBody(project__id, descriptionVersion);

            response = oneVersion.ExecuteRequest();

            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCode, (int)response.StatusCode);
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(message, response.Data["message"].ToString());
                Assert.AreEqual(code, response.Data["code"].ToString());
                Assert.AreEqual(localized, response.Data["localized"].ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void CreateOneProjectVersionString()
        {
            #region Parameters

            string project_id = "William";
            string descriptionVersion = "Descript_" + GeneralHelpers.ReturnStringWithRandomCharacters(4);

            //Expected Result
            int statusCode = 400;
            string statusCodeExpected = "BadRequest";
            string message = "'project_id' must be numeric";
            string code = "29";
            string localized = "Invalid value for 'project_id'";

            #endregion

            #region Request
            oneVersion = new POST_CreateOneVersion(project_id);

            oneVersion.SetJsonBody(project_id, descriptionVersion);

            response = oneVersion.ExecuteRequest();

            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCode, (int)response.StatusCode);
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(message, response.Data["message"].ToString());
                Assert.AreEqual(code, response.Data["code"].ToString());
                Assert.AreEqual(localized, response.Data["localized"].ToString());
            });
            #endregion
        }

        #endregion

        #region Tests PATCH Project

        [Test]
        [Parallelizable]
        public void UpdateOneProject()
        {
            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectIDRandom();
            string project_id = dataProject[0];

            string newName = "Project API v.0." + GeneralHelpers.ReturnStringWithRandomNumbers(3) + " Automation";

            // Expected Result
            int statusCode = 200;
            #endregion

            #region Request
            updateProject = new PATCH_UpdateOneProjectRequest(project_id);

            updateProject.SetJsonBody(project_id, newName);

            response = updateProject.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCode, (int)response.StatusCode);
            #endregion
        }

        #endregion

        #region Test DELETE 

        [Test]
        [Parallelizable]
        public void DeleteAProject()
        {
            #region Parameters
            List<string> dataProject = ProjectsBDSteps.ReturnProjectIDRandom();
            string project_id = dataProject[0];

            // Expected Result
            int statusCode = 200;
            #endregion

            #region Request
            oneDelete = new DEL_DeleteOneProjectRequest(project_id);

            response = oneDelete.ExecuteRequest();
            #endregion

            #region Asserts
            Assert.AreEqual(statusCode, (int)response.StatusCode);
            #endregion
        }

        #endregion

    }
}
