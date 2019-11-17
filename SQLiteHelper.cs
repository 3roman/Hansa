using System.Data;
using System.Data.SQLite;

namespace HASA
{
    class SQLiteHelper
    {
        public static DataTable Read(string database, string sql)
        {
            DataTable dt = null;
            SQLiteDataAdapter adapter = null;
            SQLiteConnection conn = null;

            try
            {
                conn = new SQLiteConnection($"Data Source={database}");
                conn.Open();
                adapter = new SQLiteDataAdapter(sql, conn);
                var ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
    }
}
