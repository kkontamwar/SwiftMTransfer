using Newtonsoft.Json;
using SwiftMTransfer.BusinessLayer;
using SwiftMTransfer.DBOperations;
using SwiftMTransfer.Models;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwiftMTransfer.Controllers
{

	public class UserController : ApiController
	{
		// GET: User

		// GET api/values
		[Route("api/User/GetAllPayeeAccounts")]
		[HttpGet]
		public List<string> GetAllPayeeAccounts()
		{
			List<string> lstResult = new List<string>();
			string result = DBOperation.Tsql_ExecuteReader(TSQL.Build_Get_AllUsers());
			if (!string.IsNullOrEmpty(result))
			{
				DataTable payeedt = JsonConvert.DeserializeObject<DataTable>(result);
				foreach (DataRow item in payeedt.Rows)
				{
					lstResult.Add(item["AccountNumber"].ToString());
				}
			}
			return lstResult;

			//if (string.IsNullOrEmpty(result))
			//{
			//	var message = string.Format("Error retriving the message");
			//	HttpError err = new HttpError(message);
			//	return Request.CreateResponse(HttpStatusCode.NotFound, err);
			//}
			//else
			//{
			//	return Request.CreateResponse(HttpStatusCode.OK, result);
			//}

		}

		[HttpGet]
		[Route("api/User/GetSelf")]
		public string GetSelf()
		{
			string result = DBOperation.Tsql_ExecuteReader(TSQL.Build_Get_SelfUsers());
			if (!string.IsNullOrEmpty(result))
			{
				DataTable payeedt = JsonConvert.DeserializeObject<DataTable>(result);
				return payeedt.Rows[0][0].ToString();
			}
			return string.Empty;



			//if (string.IsNullOrEmpty(result))
			//{
			//	var message = string.Format("Error retriving the message");
			//	HttpError err = new HttpError(message);
			//	return Request.CreateResponse(HttpStatusCode.NotFound, err);
			//}
			//else
			//{
			//	return Request.CreateResponse(HttpStatusCode.OK, result);
			//}
		}


		[HttpGet]
		[Route("api/User/TestUserControl")]
		public string TestUserControl()
		{
			return "Run  CI CD automation pipeline!!!";
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
				accID = DBOperation.Tsql_NonQuery(TSQL.Build_User_Insert(value, isSelf));
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