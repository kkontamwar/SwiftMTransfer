namespace SwiftMTransfer.Models
{
	public class TransactionHistory
	{
		public string TransactionID { get; set; }
		public string AccountNumber { get; set; }
		public string TransactionDate { get; set; }
		public string Credit { get; set; }
		public string Debit { get; set; }
		public string Balance { get; set; }
		public string TransactionStatus { get; set; }
		public string TransactionDescription { get; set; }
	}
}