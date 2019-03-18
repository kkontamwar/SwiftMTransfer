using System.Xml.Serialization;

namespace SwiftMTransfer.Models
{
	//public class FromUser
	//{
	//	public string AccountNumber { get; set; }
	//	public string Ammount { get; set; }
	//}

	//public class ToUser
	//{
	//	public string AccountNumber { get; set; }
	//}

	//public class AddMoney
	//{
	//	public FromUser FromUser { get; set; }
	//	public ToUser ToUser { get; set; }
	//	public string Description { get; set; }
	//}

	//public class AddMoneyDetail
	//{
	//	public AddMoney AddMoney { get; set; }
	//}

	public class FromUser
	{
		public string AccountNumber { get; set; }
		public string Ammount { get; set; }
	}

	public class ToUser
	{
		public string AccountNumber { get; set; }
	}

	public class AddMoney
	{
		public FromUser FromUser { get; set; }
		public ToUser ToUser { get; set; }
		public string Description { get; set; }
	}

	public class AddMoneyDetail
	{
		public AddMoney AddMoney { get; set; }
	}

	public class RootObject
	{
		public string Balance { get; set; }
	}
}