using Newtonsoft.Json;
using SwiftMTransfer.BusinessLayer;
using SwiftMTransfer.DBOperations;
using SwiftMTransfer.Models;
using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwiftMTransfer.Controllers
{
	public class TransactController : ApiController
	{
		// GET: Transact

		/// <summary>
		/// Saves users to data Base
		/// </summary>
		/// <returns></returns>
		// POST api/values
		//public HttpResponseMessage Post(AddMoneyDetail value)
		//{
		//	int accID = 0;
		//	if (value != null)
		//	{
		//		accID = DBOperation.Execute_Tsql_NonQuery(TSQL.Build_Add_Money(value));
		//	}
		//	if (accID == 0)
		//	{
		//		var message = string.Format("Account could not be created for = {0}", value.AddMoney.ToUser);
		//		HttpError err = new HttpError(message);
		//		return Request.CreateResponse(HttpStatusCode.NotFound, err);
		//	}
		//	else
		//	{
		//		return Request.CreateResponse(HttpStatusCode.OK, accID);
		//	}
		//}


		[HttpPost]
		public HttpResponseMessage AddMoney(AddMoneyDetail value)
		{
			long balance = 0;
			string credit = "0";
			string debit = "0";
			string result = DBOperation.Tsql_ExecuteReader(TSQL.Build_Get_Balance());


			if (string.IsNullOrEmpty(result))
			{
			
				balance = Convert.ToInt64(value.AddMoney.FromUser.Ammount);

			}
			else
			{
				DataTable prvBal = JsonConvert.DeserializeObject<DataTable>(result);
				balance = Convert.ToInt64(prvBal.Rows[0][0]);
			}
			

			if (value.AddMoney.FromUser.AccountNumber != value.AddMoney.ToUser.AccountNumber)
			{
				debit = Convert.ToString(value.AddMoney.FromUser.Ammount);
				balance = balance - Convert.ToInt64(value.AddMoney.FromUser.Ammount);
			}
			else
			{
				credit = Convert.ToString(value.AddMoney.FromUser.Ammount);
				if (!string.IsNullOrEmpty(result))
				{
					balance = balance + Convert.ToInt64(value.AddMoney.FromUser.Ammount);
				}
			}


			var transactID = DBOperation.Tsql_NonQuery(TSQL.Build_Add_Money(value, credit, debit, Convert.ToString(balance)));

			if (string.IsNullOrEmpty(transactID))
			{
				var message = string.Format("Could not add amount to  = {0}", value.AddMoney.ToUser.AccountNumber);
				HttpError err = new HttpError(message);
				return Request.CreateResponse(HttpStatusCode.NotFound, err);
			}
			else
			{
				var message = string.Format("Amount successfully added to account  = {0}", value.AddMoney.ToUser.AccountNumber);
				HttpError err = new HttpError(message);
				return Request.CreateResponse(HttpStatusCode.OK, message);

			}
		}
	}
}