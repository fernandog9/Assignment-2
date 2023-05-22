// See https://aka.ms/new-console-template for more information


using System;
using System.Data;
using Npgsql;

class Sample
{
    static void Main(string[] args)
    {
        // Connect to a PostgreSQL database
        NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1:5432;User Id=fg9; " +
           "Password=1014671;Database=prods;");
        conn.Open();

        // Define a query returning a single row result set
        NpgsqlCommand command = new NpgsqlCommand("SELECT invoice_num FROM invoice WHERE cust_id = '1619' AND invoice_date = '13-SEP-07';", conn);

        // Execute the query and obtain the value of the first column of the first row
        //Int64 count = (Int64)command.ExecuteScalar();
        NpgsqlDataReader reader = command.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);

        print_results(dt);

        conn.Close();
    }

    static void Main(string[] args)
    {
        // Connect to a PostgreSQL database
        NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1:5432;User Id=fg9; " +
           "Password=1014671;Database=prods;");
        conn.Open();

        // Define a query returning a single row result set
        NpgsqlCommand command = new NpgsqlCommand("SELECT rep_id, SUM(cust_balance) FROM customer GROUP BY rep_id HAVING SUM (cust_balance) > '12000';", conn);

        // Execute the query and obtain the value of the first column of the first row
        //Int64 count = (Int64)command.ExecuteScalar();
        NpgsqlDataReader reader = command.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Load(reader);

        print_results(dt);

        conn.Close();
    }

    static void print_results(DataTable data)
    {
        Console.WriteLine();
        Dictionary<string, int> colWidths = new Dictionary<string, int>();

        foreach (DataColumn col in data.Columns)
        {
            Console.Write(col.ColumnName);
            var maxLabelSize = data.Rows.OfType<DataRow>()
                    .Select(m => (m.Field<object>(col.ColumnName)?.ToString() ?? "").Length)
                    .OrderByDescending(m => m).FirstOrDefault();

            colWidths.Add(col.ColumnName, maxLabelSize);
            for (int i = 0; i < maxLabelSize - col.ColumnName.Length + 14; i++) Console.Write(" ");
        }

        Console.WriteLine();

        foreach (DataRow dataRow in data.Rows)
        {
            for (int j = 0; j < dataRow.ItemArray.Length; j++)
            {
                Console.Write(dataRow.ItemArray[j]);
                for (int i = 0; i < colWidths[data.Columns[j].ColumnName] - dataRow.ItemArray[j].ToString().Length + 14; i++) Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}

