using SwiftMTransfer.BusinessLayer;
using SwiftMTransfer.DBOperations;
using SwiftMTransfer.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwiftMTransfer.Controllers
{

	public class UserController : ApiController
	{
		// GET: User

		// GET api/values
		[Route("api/User/GetAllUsers")]
		[HttpGet]
		public HttpResponseMessage GetAllUsers()
		{

			string result = DBOperation.Tsql_ExecuteReader(TSQL.Build_Get_AllUsers());
			if (string.IsNullOrEmpty(result))
			{
				var message = string.Format("Error retriving the message");
				HttpError err = new HttpError(message);
				return Request.CreateResponse(HttpStatusCode.NotFound, err);
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.OK, result);
			}

		}

		[HttpGet]
		[Route("api/User/GetSelf")]
		public HttpResponseMessage GetSelf()
		{
			string result = DBOperation.Tsql_ExecuteReader(TSQL.Build_Get_SelfUsers());
			if (string.IsNullOrEmpty(result))
			{
				var message = string.Format("Error retriving the message");
				HttpError err = new HttpError(message);
				return Request.CreateResponse(HttpStatusCode.NotFound, err);
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
		}

		/// <summary>
		/// Saves users to data Base
		/// </summary>
		/// <returns></returns>
		// POST api/values
		[HttpPost]
		public HttpResponseMessage User(User value)
		{
			bool isSelf = false;
			string result = DBOperation.Tsql_ExecuteReader(TSQL.Build_Get_SelfUsers());
			if (string.IsNullOrEmpty(result))
			{
				isSelf = true;
			}
			

				string accID = string.Empty;
			if (value != null)
			{
				accID = DBOperation.Tsql_NonQuery(TSQL.Build_User_Insert(value,isSelf));
			}
			if (string.IsNullOrEmpty(accID))
			{
				var message = string.Format("Account could not be created for = {0}", value.AddUser.FirstName);
				HttpError err = new HttpError(message);
				return Request.CreateResponse(HttpStatusCode.NotFound, err);
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.OK, accID);
			}
		}



	}
}