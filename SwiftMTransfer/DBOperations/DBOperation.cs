using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SwiftMTransfer.DBOperations
{
	public class DBOperation
	{
		private static SqlConnection GetSqlConnection()
		{
			SqlConnection connection;
			try
			{
				var cb = new SqlConnectionStringBuilder();
				cb.DataSource = "tcp:swiftm.database.windows.net";
				cb.UserID = "fiserv";
				cb.Password = "password@123";
				cb.InitialCatalog = "SwiftMDB";
				connection = new SqlConnection(cb.ConnectionString);
				connection.Open();
				//return Submit_Tsql_NonQuery(connection, tsqlSourceCode);
				return connection;
			}
			catch (SqlException)
			{
				return null;
			}

		}

		public static string Tsql_ExecuteReader(string tsql)
		{
			SqlConnection nwindConn = GetSqlConnection();
			SqlCommand selectCMD = new SqlCommand(tsql, nwindConn);
			SqlDataAdapter customerDA = new SqlDataAdapter();
			customerDA.SelectCommand = selectCMD;
			//nwindConn.Open();
			DataSet customerDS = new DataSet();
			customerDA.Fill(customerDS, "Customers");
			nwindConn.Close();
			return DataTableToJSONWithStringBuilder(customerDS.Tables[0]);


		}
		public static string DataTableToJSONWithStringBuilder(DataTable table)
		{
			if (table != null)
			{
				var JSONString = new StringBuilder();
				if (table.Rows.Count > 0)
				{
					JSONString.Append("[");
					for (int i = 0; i < table.Rows.Count; i++)
					{
						JSONString.Append("{");
						for (int j = 0; j < table.Columns.Count; j++)
						{
							if (j < table.Columns.Count - 1)
							{
								JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
							}
							else if (j == table.Columns.Count - 1)
							{
								JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
							}
						}
						if (i == table.Rows.Count - 1)
						{
							JSONString.Append("}");
						}
						else
						{
							JSONString.Append("},");
						}
					}
					JSONString.Append("]");
				}
				return JSONString.ToString();
			}
			return string.Empty;
		}

		public static string Tsql_NonQuery(string tsqlSourceCode, string parameterName = null, string parameterValue = null)
		{
			try
			{
				SqlConnection connection = GetSqlConnection();

				using (var command = new SqlCommand(tsqlSourceCode, connection))
				{
					if (parameterName != null)
					{
						command.Parameters.AddWithValue(parameterName, parameterValue);
					}
					var accountId = command.ExecuteScalar().ToString(); ;
					return accountId;
				}
			}
			catch (System.Exception)
			{
				throw;
			}

		}
	}
}