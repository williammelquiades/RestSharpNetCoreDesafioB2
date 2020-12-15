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

        IRestResponse<dynamic> response;

        #region Test POST
        [Test]
        public void CreateSubProjectSucesso()
        {
            #region Parameters
            //Remove Hierarchy exist
            ProjectsBDSteps.DeleteHierarchyProject();

            List<string> dataProjectId = ProjectsBDSteps.ReturnProjectAndID();
            string idProject = dataProjectId[0];

            List<string> dataProjectName = ProjectsBDSteps.ReturnProjectByNameRandom();
            string name = dataProjectName[0];

            // Result Response
            string statusCodeResponse = "NoContent";//"BadRequest";
            #endregion

            #region Request
            subProjeto = new POST_CreateSubProjectResquest(idProject);

            subProjeto.SetJosnBoby(name);

            response = subProjeto.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());
            #endregion
        }
       
        [Test]
        public void SubProjetcWithInexistProjet()
        {
            #region Parameters
            List<string> dataProjectId = ProjectsBDSteps.ReturnProjectAndID();
            string idProject = dataProjectId[0];

            string notProject = "Inexist Project";

            // Result Response
            string statusCodeResponse = "NotFound";
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
                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());
                Assert.AreEqual(message, response.Data["message"].ToString());
                Assert.AreEqual(codeResponse, response.Data["code"].ToString());
                Assert.AreEqual(localized, response.Data["localized"].ToString());
            });
            #endregion
        }
        #endregion
    }
}
