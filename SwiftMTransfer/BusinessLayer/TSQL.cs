using SwiftMTransfer.Models;
using System;

namespace SwiftMTransfer.BusinessLayer
{
	public static class TSQL
	{
		public static string Build_User_Insert(User value, bool isSelf)
		{
			return string.Format(@"INSERT INTO UserRegistration (FirstName, LastName,EmailID,PhoneNumber,IsSelf)
					  VALUES   ('{0}', '{1}', '{2}', '{3}','{4}');SELECT SCOPE_IDENTITY();", value.AddUser.FirstName, value.AddUser.LastName, value.AddUser.EmailID, value.AddUser.PhoneNumber, isSelf);
		}


		public static string Build_Get_AllUsers()
		{
			return @"select AccountNumber,IsSelf from UserRegistration;";
		}

		public static string Build_Get_SelfUsers()
		{
			return @"select AccountNumber from UserRegistration where IsSelf = 1;";
		}

		public static string Build_Add_Money(AddMoney value, string Credit, string Debit, string Balance)
		{
			return string.Format(@"INSERT INTO UserTransaction (AccountNumber, TransactionDate,Credit,Debit,Balance,TransactionStatus,TransactionDescription)
					  VALUES   ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');SELECT SCOPE_IDENTITY();", value.toAccNumber, DateTime.Now, Credit, Debit, Balance, "Sucess", value.description);
		}

		public static string Build_Get_Balance()
		{
			return "SELECT Balance FROM UserTransaction WHERE TransactionID = ( SELECT IDENT_CURRENT('UserTransaction'));";
		}

		public static string Build_Get_AllTransaction()
		{
			return @"select * from UserTransaction;";
		}

	}
}