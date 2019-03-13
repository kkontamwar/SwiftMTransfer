using SwiftMTransfer.BusinessLayer;
using SwiftMTransfer.DBOperations;
using SwiftMTransfer.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwiftMTransfer.Controllers
{
	public class UserController : ApiController
	{
		// GET: User

		// GET api/values
		public IEnumerable<string> Get()
		{
			return new string[] { "Get from UserController", "Get from UserController" };
		}

		/// <summary>
		/// Saves users to data Base
		/// </summary>
		/// <returns></returns>
		// POST api/values
		public HttpResponseMessage Post(User value)
		{
			int accID = 0;
			if (value != null)
			{
				accID = DBOperation.Execute_Tsql_NonQuery(TSQL.Build_User_Insert(value));
			}
			if (accID == 0)
			{
				var message = string.Format("Account could not be created for = {0}", value.FirstName);
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