using NUnit.Framework;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.DBSteps.Projects;
using RestSharpNetCoreDesafioB2.Requests.Projects;
using System;
using System.Collections.Generic;
using RestSharpNetCoreDesafioB2.Helpers;
using System.Text;
using RestSharp;

namespace RestSharpNetCoreDesafioB2.Tests.Projects
{
    [TestFixture]
    public class SubProjectsTests : TestBase
    {
        private POST_CreateSubProjectResquest subProjeto;
        private DEL_DeleteOneSubProjectRequest deleteSubProject;

        IRestResponse<dynamic> response;

        #region Test POST
        [Test]
        [Parallelizable]
        public void CreateSubProjectSucesso()
        {
            #region Parameters
            //Remove Hierarchy exist
            ProjectsBDSteps.DeleteHierarchyProject();

            List<string> dataProjectId = ProjectsBDSteps.ReturnProjectIDRandom();
            string idProject = dataProjectId[0];

            List<string> dataProjectName = ProjectsBDSteps.ReturnProjectByNameRandom();
            string name = dataProjectName[0];

            // Expected Result
            string statusCodeExpected = "NoContent";//"BadRequest";
            #endregion

            #region Request
            subProjeto = new POST_CreateSubProjectResquest(idProject);

            subProjeto.SetJosnBoby(name);

            response = subProjeto.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            #endregion
        }

        [Test]
        [Parallelizable]
        public void CreateSubProjectErro()
        {
            #region Parameters
            //Remove Hierarchy exist
            ProjectsBDSteps.DeleteHierarchyProject();

            List<string> dataProjectId = ProjectsBDSteps.ReturnProjectByName();
            string idProject = dataProjectId[0];

            List<string> dataProjectName = ProjectsBDSteps.ReturnProjectByNameRandom();
            string name = dataProjectName[0];

            // Expected Result
            string statusCodeExpected = "BadRequest";
            #endregion

            #region Request
            subProjeto = new POST_CreateSubProjectResquest(idProject);

            subProjeto.SetJosnBoby(name);

            response = subProjeto.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            #endregion
        }

        [Test]
        [Parallelizable]
        public void SubProjetcWithInexistProject()
        {
            #region Parameters
            List<string> dataProjectId = ProjectsBDSteps.ReturnProjectAndID();
            string idProject = dataProjectId[0];

            string notProject = "Inexist Project";

            // Expected Result
            string statusCodeExpected = "NotFound";
            string message = "Project 'Inexist Project' not found";
            string codeResponse = "700";
            string localized = "Project \"Inexist Project\" not found.";
            #endregion

            #region Request
            subProjeto = new POST_CreateSubProjectResquest(idProject);

            subProjeto.SetJosnBoby(notProject);

            response = subProjeto.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(message, response.Data["message"].ToString());
                Assert.AreEqual(codeResponse, response.Data["code"].ToString());
                Assert.AreEqual(localized, response.Data["localized"].ToString());
            });
            #endregion
        }
        #endregion

        #region Test DELETE
        [Test]
        [Parallelizable]
        public void DeleteOneSubProject()
        {
            #region Parameters
            List<string> dataProjectId = ProjectsBDSteps.ReturnIDSubProject();
            string idProject = dataProjectId[0];

            // Expected Result
            string statusCodeExpected = "OK";
            #endregion

            #region Request
            deleteSubProject = new DEL_DeleteOneSubProjectRequest(idProject);

            response = deleteSubProject.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            #endregion
        }

        [Test]
        [Parallelizable]
        public void DeleteOneSubProjectNotAuthorized()
        {
            #region Parameters
            string idProject = "1988Wm";

            // Expected Result
            string statusCodeExpected = "Forbidden";
            #endregion

            #region Request
            deleteSubProject = new DEL_DeleteOneSubProjectRequest(idProject);

            response = deleteSubProject.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            #endregion
        }

        [Test]
        [Parallelizable]
        public void DeleteOneSubProjectIDInvalid()
        {
            #region Parameters
            string idProject = "Will1988Wm";

            // Expected Result
            string statusCodeExpected = "BadRequest";
            #endregion

            #region Request
            deleteSubProject = new DEL_DeleteOneSubProjectRequest(idProject);

            response = deleteSubProject.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            #endregion
        }
        #endregion
    }
}
