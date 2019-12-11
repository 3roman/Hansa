using System;
using System.Data;
using System.Windows.Forms;

namespace HASA
{
    public partial class FrmD19 : Form
    {
        private int distance;
        private int pipeLoad;
        private int elevation;
        private int od;
        private int insulation;

        public FrmD19()
        {
            //InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipeLoad">单位KN</param>
        /// <param name="distance">单位mm</param>
        /// <param name="elevation">单位mm</param>
        public FrmD19(int pipeLoad, int distance, int elevation, int od, int insulation)
        {
            this.distance = distance;
            this.pipeLoad = pipeLoad;
            this.elevation = elevation;
            this.od = od;
            this.insulation = insulation;

            InitializeComponent();
        }

        private void FrmD19_Load(object sender, EventArgs e)
        {
            var ret = MessageBoxEx.Show("选择与结构件焊接型式", "选择型式", MessageBoxButtons.OKCancel, new string[] { "侧焊", "端焊" });
            if (ret == DialogResult.OK)
            {
                Common.DataTableToListview(lstShelf, D19I());
            }
            else
            {
                Common.DataTableToListview(lstShelf, D19II());
            }
        }

        /// <summary>
        /// 侧焊三角撑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private DataTable D19I()
        {
            var dt = SQLiteHelper.Read("HASA.db", "SELECT * FROM d19 WHERE type='I'");
            //// 创建列
            //dt.Columns.Add("适用型钢", typeof(string));
            //dt.Columns.Add("L≤500", typeof(double));
            //dt.Columns.Add("L≤750", typeof(double));
            //dt.Columns.Add("L≤1000", typeof(double));
            //dt.Columns.Add("L≤1250", typeof(double));
            //dt.Columns.Add("L≤1500", typeof(double));
            //dt.Columns.Add("L≤1750", typeof(double));
            //dt.Columns.Add("L≤2000", typeof(double));
            //// 插入行
            //dt.Rows.Add("H125×125(∠100×10) ", 40, 27, 20, 16, 14, 0, 0);
            //dt.Rows.Add("H150×150(∠125×10) ", 62, 43, 32, 26, 22, 0, 0);
            //dt.Rows.Add("H200×200(∠160×12) ", 120, 90, 68, 55, 46, 40, 35);
            //dt.Rows.Add("H250×250(∠200×14) ", 220, 159, 120, 100, 84, 73, 62);

            var colName = DetermineColumn(distance);
            var query = from row in dt.AsEnumerable()
                        where row.Field<double>(colName) > pipeLoad
                        select row;
            var table = query.AsDataView().ToTable(true, new string[] { "steel", colName });

            var temp = table.Rows[0]["steel"].ToString().Split(new char[]{'(', ')'});

            var len = Common.Round2Ten(distance + od / 2 + insulation + 180);
            Common.Copy2Clipboard($"D19\tI\t\t\t{elevation}\t\t{len}" +
                $"\t{distance}\t\t\t\t\t\t1\t\t\t{temp[0]}\t{temp[1]}\t\t\t\t\t1,1");

            return table;
        }

        /// <summary>
        /// 端焊三角撑
        /// </summary>
        /// <returns></returns>
        private DataTable D19II()
        {
            var dt = SQLiteHelper.Read("HASA.db", "SELECT * FROM d19 WHERE type='II'");
            var colName = DetermineColumn(distance);
            var query = from row in dt.AsEnumerable()
                        where row.Field<double>(colName) > pipeLoad
                        select row;
            var table = query.AsDataView().ToTable(true, new string[] { "steel", colName });

            var temp = table.Rows[0]["steel"].ToString().Split(new char[] { '(', ')' });

            var len = Common.Round2Ten(distance + od / 2 + insulation + 180);
            Common.Copy2Clipboard($"D19\tII\t\t\t{elevation}\t\t{len}" +
                $"\t{distance}\t\t\t\t\t\t1\t\t\t{temp[0]}\t{temp[1]}\t\t\t\t\t1,1");

            return table;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string DetermineColumn(double distance)
        {
            string column = string.Empty;

            if (distance <= 500)
            {
                column = "L≤500";
            }
            else if (distance > 500 && distance <= 750)
            {
                column = "L≤750";
            }
            else if (distance > 750 && distance <= 1000)
            {
                column = "L≤1000";
            }
            else if (distance > 1000 && distance <= 1250)
            {
                column = "L≤1250";
            }
            else if (distance > 1250 && distance <= 1500)
            {
                column = "L≤1500";
            }
            else if (distance > 1500 && distance <= 1750)
            {
                column = "L≤1750";
            }
            else if (distance > 1750 && distance <= 2000)
            {
                column = "L≤2000";
            }

            return column;
        }
    }
}
