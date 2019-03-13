using System.Data.SqlClient;

namespace SwiftMTransfer.DBOperations
{
	public class DBOperation
	{
		public static int Execute_Tsql_NonQuery(string tsqlSourceCode)
		{
			try
			{
				var cb = new SqlConnectionStringBuilder();
				cb.DataSource = "your_server.database.windows.net";
				cb.UserID = "your_user";
				cb.Password = "your_password";
				cb.InitialCatalog = "your_database";

				using (var connection = new SqlConnection(cb.ConnectionString))
				{
					connection.Open();
					return Submit_Tsql_NonQuery(connection, tsqlSourceCode);
				}

			}
			catch (SqlException)
			{
				return 0;
			}

		}

		private static int Submit_Tsql_NonQuery(SqlConnection connection, string tsqlSourceCode, string parameterName = null, string parameterValue = null)
		{
			using (var command = new SqlCommand(tsqlSourceCode, connection))
			{
				if (parameterName != null)
				{
					command.Parameters.AddWithValue(parameterName, parameterValue);
				}
				int accountId = (int)command.ExecuteScalar();
				return accountId;
			}
		}
	}
}