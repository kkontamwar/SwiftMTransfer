using SwiftMTransfer.Models;

namespace SwiftMTransfer.BusinessLayer
{
	public static class TSQL
	{
		public static string Build_User_Insert(User value)
		{
			return string.Format(@"INSERT INTO tabUserRegistrationt (FirstName, LastName,EmailID,PhoneNumber)
					  VALUES   ('{0}', '{1}', '{2}', '{3}');SELECT SCOPE_IDENTITY();", value.FirstName, value.LastName, value.EmailID, value.PhoneNumber);
		}
	}
}