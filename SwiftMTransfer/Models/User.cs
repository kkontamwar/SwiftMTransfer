namespace SwiftMTransfer.Models
{
	public class User
	{
		public AddUser AddUser { get; set; }
	}
	public class AddUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailID { get; set; }
		public string PhoneNumber { get; set; }
	}
}