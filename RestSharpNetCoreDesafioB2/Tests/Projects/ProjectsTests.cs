using NUnit.Framework;
using RestSharp;
using  RestSharpNetCoreDesafioB2.Bases;
using  RestSharpNetCoreDesafioB2.Helpers;
using  RestSharpNetCoreDesafioB2.Requests.Projetos;
using System;
using System.Collections.Generic;
using System.Text;

namespace  RestSharpNetCoreDesafioB2.Tests.Projetos
{
    [TestFixture]
    public class ProjectsTests : TestBase
    {
        private POST_CreateProjectRequest createProjet;
        private GET_AllProjectsRequest allProjects;
        private GET_OneProjectRequest oneProject;

        IRestResponse<dynamic> response;

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

            //Contain Result in Request
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
        public void GetAllProjectsSucess()
        {
            #region Parameters
            //Contain Result in Request
            int statusCodeResponse = 200;
            #endregion

            #region Request
            allProjects = new GET_AllProjectsRequest();

            response = allProjects.ExecuteRequest();
            #endregion

            #region Asserts
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, (int)response.StatusCode);
                Assert.AreEqual(13, (int)response.Headers.Count);
            });
            #endregion
        }

        [Test]
        public void GetOneProjectSucess()
        {
            #region Parameters
            string projeto = "1";
            //Contain Result in Request
            int statusCodeResponse = 200;
            #endregion

            #region Request
            oneProject = new GET_OneProjectRequest(projeto);

            response = oneProject.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, (int)response.StatusCode);
                Assert.AreEqual(13, (int)response.Headers.Count);
            });
            #endregion
        }
    }
}
