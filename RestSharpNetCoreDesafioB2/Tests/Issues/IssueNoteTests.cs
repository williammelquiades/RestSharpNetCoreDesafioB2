using RestSharp;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.DBSteps.Issues;
using RestSharpNetCoreDesafioB2.Requests.Issues;
using RestSharpNetCoreDesafioB2.Helpers;


namespace RestSharpNetCoreDesafioB2.Tests.Issues
{
    [TestFixture]
    public class IssueNoteTests : TestBase
    {
        private POST_CreateNoteInIssueRequest noteIssue;
        private DEL_OneNoteRequest deleteNote;

        IRestResponse<dynamic> response;

        #region Data Driven Providers
        public static IEnumerable DataDrivenIssue()
        {
            return GeneralHelpers.ReturnCSVData(GeneralHelpers.ReturnProjectPath() + "Resources/addNoteInIssue.csv");
        }
        #endregion

        [Test, TestCaseSource("DataDrivenIssue")]
        [Parallelizable]
        public void CreateNoteInIssueDataDriven(ArrayList dataTest)
        {
            #region Parameters
            List<string> dataIssue = IssuesBDSteps.ReturnIdIssuesRandom();
            string issue_id = dataIssue[0];

            string nameView_state = "";
            string text = dataTest[0].ToString();

            //Expected Result
            string statusCodeEsperado = "Created";

            #endregion

            #region Request
            noteIssue = new POST_CreateNoteInIssueRequest(issue_id);

            noteIssue.SetJsonBody(nameView_state, text);

            response = noteIssue.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            #endregion
        }

        [Test]
        [Parallelizable]
        public void CreateNoteInIssueSucess()
        {
            #region Parameters
            List<string> dataIssue = IssuesBDSteps.ReturnIdIssuesRandom();
            string issue_id = dataIssue[0];

            string viewStatus = "private";
            string text = "test note " + GeneralHelpers.ReturnStringWithRandomCharacters(10);

            //Expected Result
            string statusCodeEsperado = "Created";

            #endregion

            #region Request
            noteIssue = new POST_CreateNoteInIssueRequest(issue_id);

            noteIssue.SetJsonBody(viewStatus,text);

            response = noteIssue.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            #endregion
        }

        [Test]
        [Parallelizable]
        public void CreateNoteInIssueErro()
        {
            #region Parameters
            List<string> dataIssue = IssuesBDSteps.ReturnIdIssuesRandom();

            string issue_id = dataIssue[0];
            string nameView_state = "";
            string text = "";

            //Expected Result
            string statusCodeExpected = "BadRequest";
            string message = "Issue note not specified.";
            string code = "11";
            string localized = "A necessary field \"Note\" was empty. Please recheck your inputs.";
            #endregion

            #region Request
            noteIssue = new POST_CreateNoteInIssueRequest(issue_id);

            noteIssue.SetJsonBody(nameView_state,text);

            response = noteIssue.ExecuteRequest();
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
        public void DeleteAnIssueNote()
        {
            #region Parameters
            List<string> dataIssue = IssuesBDSteps.GetNoteIssue();
            string issue_id = dataIssue[0];
            string issue_note_id = dataIssue[1];

            //Expected Result
            string statusCodeExpected = "OK";
            #endregion

            #region Request
            deleteNote = new DEL_OneNoteRequest(issue_id,issue_note_id);

            response = deleteNote.ExecuteRequest();
            #endregion

            #region Assert
            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            #endregion
        }

        [Test]
        [Parallelizable]
        public void DeleteAnIssueNoteDataInvalida()
        {
            #region Parameters
            List<string> dataIssue = IssuesBDSteps.ReturnIdIssuesRandom();

            string issue_id = dataIssue[0];
            string issue_note_id = "1988a";

            //Expected Result
            string statusCodeExpected = "NotFound";
            string message = "Issue note #1988a not found";
            string code = "600";
            string localized = "Note not found.";
            #endregion

            #region Request
            deleteNote = new DEL_OneNoteRequest(issue_id, issue_note_id);

            response = deleteNote.ExecuteRequest();
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
        public void DeleteAnIssueNoteIdZero()
        {
            #region Parameters
            List<string> dataIssue = IssuesBDSteps.ReturnIdIssuesRandom();

            string issue_id = dataIssue[0];
            string issue_note_id = "0";

            //Expected Result
            string message = "'id' must be >= 1";
            string code = "29";
            string localized = "Invalid value for 'id'";
            string statusCodeExpected = "BadRequest";
            #endregion

            #region Request
            deleteNote = new DEL_OneNoteRequest(issue_id, issue_note_id);

            response = deleteNote.ExecuteRequest();
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

    }
}
