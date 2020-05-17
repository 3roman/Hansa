using System;
using System.Data;
using System.Windows.Forms;

namespace HASA
{
    public partial class FrmD19 : Form
    {
        private int _armLength;
        private int _totaLoad;
        private int _elevation;
        private int _od;
        private int _insulation;

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="totaLoad">单位KN</param>
        /// <param name="armLength">单位mm</param>
        /// <param name="elevation">单位mm</param>
        /// <param name="od">单位mm</param>
        /// <param name="insulation">单位mm</param>
        public FrmD19(int totaLoad, int armLength, int elevation, int od, int insulation)
        {
            _armLength = armLength;
            _totaLoad = totaLoad;
            _elevation = elevation;
            _od = od;
            _insulation = insulation;

            InitializeComponent();

            var ret = MessageBoxEx.Show("选择与结构件焊接型式", "选择型式", MessageBoxButtons.OKCancel, new string[] { "侧焊", "端焊" });
            if (ret == DialogResult.OK)
            {
                Common.DataTable2Listview(lstShelf, D19I());
            }
            else
            {
                Common.DataTable2Listview(lstShelf, D19II());
            }
        }
      
        /// <summary>
        /// 侧焊三角撑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private DataTable D19I()
        {
            #region 手动增加数据
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
            #endregion
            var dt = SQLiteHelper.Read("HASA.db", "SELECT * FROM d19 WHERE type='I'");
            var columName = GetColumName(_armLength);
            var query = from row in dt.AsEnumerable()
                        where row.Field<double>(columName) > _totaLoad
                        select row;
            var table = query.AsDataView().ToTable(true, new string[] { "steel", columName });

            var temp = table.Rows[0]["steel"].ToString().Split(new char[]{'(', ')'});

            var beamLength = Common.Round2Ten(_armLength + _od / 2 + _insulation + 200 + 150);
            Common.Copy2Clipboard($"D19\tI\t\t\t{_elevation}\t\t{beamLength}" +
                $"\t{_armLength}\t\t\t\t\t\t1\t\t\t{temp[0]}\t{temp[1]}\t\t\t\t\t1,1");

            return table;
        }

        /// <summary>
        /// 端焊三角撑
        /// </summary>
        /// <returns></returns>
        private DataTable D19II()
        {
            var dt = SQLiteHelper.Read("HASA.db", "SELECT * FROM d19 WHERE type='II'");
            var columName = GetColumName(_armLength);
            var query = from row in dt.AsEnumerable()
                        where row.Field<double>(columName) > _totaLoad
                        select row;
            var table = query.AsDataView().ToTable(true, new string[] { "steel", columName });

            var temp = table.Rows[0]["steel"].ToString().Split(new char[] { '(', ')' });

            var beamLength = Common.Round2Ten(_armLength + _od / 2 + _insulation + 200);
            Common.Copy2Clipboard($"D19\tII\t\t\t{_elevation}\t\t{beamLength}" +
                $"\t{_armLength}\t\t\t\t\t\t1\t\t\t{temp[0]}\t{temp[1]}\t\t\t\t\t1,1");

            return table;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string GetColumName(double distance)
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
