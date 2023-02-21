

using Microsoft.Data.SqlClient;

string connectionString = "server=localhost\\sqlexpress;" +
                            "database=SalesDb;" +
                            "trusted_connection=true;" +
                            "trustServerCertificate=true;";
SqlConnection sqlConn = new SqlConnection(connectionString);
sqlConn.Open();
if (sqlConn.State != System.Data.ConnectionState.Open)
{
    throw new Exception("I screwed my connection string");
}
Console.WriteLine("Connection opened successfully!");

string sql = "SELECT * from Customers where sales > 90000 order by sales desc;";
SqlCommand cmd = new SqlCommand(sql, sqlConn);
SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    var id = Convert.ToInt32(reader["Id"]);
    var name = Convert.ToString(reader["Name"]);
    var city = Convert.ToString (reader["City"]);
    var state = Convert.ToString(reader["state"]);
    var sales = Convert.ToDecimal(reader["Sales"]);
    var active = Convert.ToBoolean(reader["active"]); 
    Console.WriteLine($"{id} | {name} | {city}, {state} | {sales} | {(active ? "yes" : "no")}");
}
reader.Close();
Console.WriteLine();
sql = "select * from orders order by date desc;";
cmd = new SqlCommand(sql, sqlConn);
reader = cmd.ExecuteReader();
while (reader.Read())
{
    var id = Convert.ToInt32(reader["Id"]);
    var customerId = Convert.ToInt32(reader["CustomerId"].Equals(System.DBNull.Value) ? (int?)null : Convert.ToInt32(reader["CustomerID"]));
    var date = Convert.ToDateTime(reader["Date"]);
    var description = Convert.ToString(reader["Description"]);
    Console.WriteLine($"{id} | {customerId} | {date} | {description}");
}
reader.Close();
sqlConn.Close();