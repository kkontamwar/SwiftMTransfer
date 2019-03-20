using Newtonsoft.Json;
using SwiftMTransfer.BusinessLayer;
using SwiftMTransfer.DBOperations;
using SwiftMTransfer.Models;
using System;
using System.Collections.Generic;
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

		[HttpPost]
		public HttpResponseMessage AddMoney(AddMoney value)
		{
			long balance = 0;
			string credit = "0";
			string debit = "0";


			string result = DBOperation.Tsql_ExecuteReader(TSQL.Build_Get_Balance());
			if (!string.IsNullOrEmpty(result))
			{
				DataTable prvBal = JsonConvert.DeserializeObject<DataTable>(result);
				balance = Convert.ToInt64(prvBal.Rows[0][0]);
			}


			if (value.fromAccNumber != value.toAccNumber)//Debit
			{
				debit = Convert.ToString(value.transAmount);
				balance = balance - Convert.ToInt64(value.transAmount);
			}
			else
			{

				credit = Convert.ToString(value.transAmount);
				balance = balance + Convert.ToInt64(value.transAmount);
			}


			//if (string.IsNullOrEmpty(result))
			//{

			//	balance = Convert.ToInt64(value.transAmount);

			//}
			//else
			//{
			//	DataTable prvBal = JsonConvert.DeserializeObject<DataTable>(result);
			//	balance = Convert.ToInt64(prvBal.Rows[0][0]);
			//}


			//if (value.fromAccNumber != value.toAccNumber)
			//{
			//	debit = Convert.ToString(value.transAmount);
			//	balance = balance - Convert.ToInt64(value.transAmount);
			//}
			//else
			//{
			//	credit = Convert.ToString(value.transAmount);
			//	if (!string.IsNullOrEmpty(result))
			//	{
			//		balance = balance + Convert.ToInt64(value.transAmount);
			//	}
			//}


			var transactID = DBOperation.Tsql_NonQuery(TSQL.Build_Add_Money(value, credit, debit, Convert.ToString(balance)));

			if (string.IsNullOrEmpty(transactID))
			{
				var message = string.Format("Could not add amount to  = {0}", value.toAccNumber);
				HttpError err = new HttpError(message);
				return Request.CreateResponse(HttpStatusCode.NotFound, err);
			}
			else
			{
				var message = string.Format("Amount successfully added to account  = {0}", value.toAccNumber);
				HttpError err = new HttpError(message);
				return Request.CreateResponse(HttpStatusCode.OK, message);

			}
		}

		[Route("api/User/GetAllTransactions")]
		[HttpGet]
		public List<TransactionHistory> GetAllTransactions()
		{
			List<TransactionHistory> lstTransHistColl = new List<TransactionHistory>();
			string result = DBOperation.Tsql_ExecuteReader(TSQL.Build_Get_AllTransaction());
			DataTable allTransDetails = JsonConvert.DeserializeObject<DataTable>(result);
			foreach (DataRow item in allTransDetails.Rows)
			{
				TransactionHistory objHis = new TransactionHistory();
				objHis.TransactionID = item["TransactionID"].ToString();
				objHis.AccountNumber = item["AccountNumber"].ToString();
				objHis.TransactionDate = item["TransactionDate"].ToString();
				objHis.Credit = item["Credit"].ToString();
				objHis.Debit = item["Debit"].ToString();
				objHis.Balance = item["Balance"].ToString();
				objHis.TransactionStatus = item["TransactionStatus"].ToString();
				objHis.TransactionDescription = item["TransactionDescription"].ToString();
				lstTransHistColl.Add(objHis);
			}

			return lstTransHistColl;

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
	}
}