using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace HASA
{
    internal static class SQLiteHelper
    {
        public static DataTable Read(string database, string sql)
        {
            DataTable dt = null;
            SQLiteDataAdapter adapter = null;
            SQLiteConnection conn = null;
            DataSet ds = new DataSet();

            try
            {
                conn = new SQLiteConnection($"Data Source={database}");
                adapter = new SQLiteDataAdapter(sql, conn);
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (ConnectionState.Open == conn.State )
                {
                    conn.Close();
                }
            }

            return dt;
        }
    }
}
