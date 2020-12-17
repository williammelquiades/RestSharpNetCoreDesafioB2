using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.DBSteps.Projects;
using RestSharpNetCoreDesafioB2.DBSteps.Users;
using RestSharpNetCoreDesafioB2.Helpers;
using RestSharpNetCoreDesafioB2.Requests.Projects;
using RestSharpNetCoreDesafioB2.Requests.Users;
using System;
using System.Collections.Generic;
using System.Text;


namespace RestSharpNetCoreDesafioB2.Tests.Users
{
    [TestFixture]
    public class UsersTests : TestBase
    {
        private POST_CreateOneUserRequest createUser;
        private GET_MyUserInfoRequest myUser;
        private DEL_OneUserRequest deleteUser;
        private PUT_ResetUserRequest resetPass;

        IRestResponse<dynamic> response;

        [Test]
        public void CreateUserSucess()
        {
            #region Parameters
            string username = "Usuario N1 " + GeneralHelpers.ReturnStringWithRandomCharacters(4);
            string password = "!@#$123";
            string real_name = "Pokemon" + GeneralHelpers.ReturnStringWithRandomNumbers(3);
            string email = "usern1@b2tec" + GeneralHelpers.ReturnStringWithRandomNumbers(2) + ".com";
            string nameLevel = "updater";
            string enabled = "true";
            string protectedLevel = "false";

            //Expected Result
            string statusCodeResponse = "Created";
            #endregion

            #region Request

            createUser = new POST_CreateOneUserRequest();

            createUser.SetJsonBody(username, password, real_name, email, nameLevel, enabled, protectedLevel);

            response = createUser.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());
            });
            #endregion
        }

        [Test]
        public void CreateUserIgual()
        {
            #region Parameters
            List<string> dataUser = UsersBDSteps.ReturnUsersRandom();
            string username = dataUser[1];
            string password = "!@#$123";
            string real_name = dataUser[2];
            string email = dataUser[3];
            string nameLevel = "updater";//dataUser[7];
            string enabled = "true";
            string protectedLevel = "false";

            //Expected Result
            string statusCodeResponse = "BadRequest";
            string message = "Username '" + username + "' already used.";
            string statusCode = "800";
            string localized = "That username is already being used. Please go back and select another one.";
            #endregion

            #region Request

            createUser = new POST_CreateOneUserRequest();

            createUser.SetJsonBody(username, password, real_name, email, nameLevel, enabled, protectedLevel);

            response = createUser.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());
                Assert.AreEqual(message, response.Data["message"].ToString());
                Assert.AreEqual(statusCode, response.Data["code"].ToString());
                Assert.AreEqual(localized, response.Data["localized"].ToString());
            });
            #endregion
        }

        [Test]
        public void GetInfoMYUser()
        {
            #region Parameters

            List<string> dataUser = UsersBDSteps.ReturnMyUser();

            //Expected Result
            int statusCodeEsperado = 200;
            string userName = dataUser[1];
            #endregion

            #region Request

            myUser = new GET_MyUserInfoRequest();

            response = myUser.ExecuteRequest();

            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, (int)response.StatusCode);
                Assert.AreEqual(userName, response.Data["name"].ToString());
            });
            #endregion
        }

        [Test]
        public void DeleteUserRandom()
        {
            #region Parameters

            List<string> dataUser = UsersBDSteps.ReturnUsersRandom();
            string idUser = dataUser[0];

            //Expected Result
            string statusCodeResponse = "NoContent";
            #endregion

            #region Request

            deleteUser = new DEL_OneUserRequest(idUser);

            response = deleteUser.ExecuteRequest();

            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, response.StatusCode.ToString());
            });
            #endregion
        }


        [Test]
        public void ResetPasswordUserRandom()
        {
            #region Parameters

            List<string> dataUser = UsersBDSteps.ReturnUsersRandom();
            string idUser = dataUser[0];

            //Expected Result
            int statusCodeResponse = 404;
            string message = "Not found";
            #endregion

            #region Request

            resetPass = new PUT_ResetUserRequest(idUser);

            response = resetPass.ExecuteRequest();

            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeResponse, (int)response.StatusCode);
                Assert.AreEqual(message, response.Data["message"].ToString());
            });
            #endregion
        }
    }
}
