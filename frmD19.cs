using System;
using System.Data;
using System.Windows.Forms;

namespace HASA
{
    public partial class FrmD19 : Form
    {
        private double distance;
        private double force;

        public FrmD19()
        {
            //InitializeComponent();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="distance">单位为mm</param>
        /// <param name="force">单位为KN</param>
        public FrmD19(double force, double distance)
        {
            this.distance = distance;
            this.force = force;

            InitializeComponent();
        }

        private void FrmD19_Load(object sender, EventArgs e)
        {
            var ret = MessageBoxEx.Show("选择与结构件焊接型式", "选择型式", MessageBoxButtons.OKCancel, new string[] { "端焊", "侧焊" });
            if (ret == DialogResult.OK)
            {
                DataTableToListview(lstShelf, D19I());
            }
            else
            {
                DataTableToListview(lstShelf, D19II());
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
                        where row.Field<double>(colName) > force
                        select row;
            return query.AsDataView().ToTable(true, new string[] { "steel", colName });
        }

        /// <summary>
        /// 侧焊三角撑
        /// </summary>
        /// <returns></returns>
        private DataTable D19II()
        {
            var dt = SQLiteHelper.Read("HASA.db", "SELECT * FROM d19 WHERE type='II'");
            var colName = DetermineColumn(distance);
            var query = from row in dt.AsEnumerable()
                        where row.Field<double>(colName) > force
                        select row;
            return query.AsDataView().ToTable(true, new string[] { "steel", colName });
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

        private static void DataTableToListview(ListView lv, DataTable dt)
        {
            if (null != dt)
            {
                lv.Items.Clear();
                lv.Columns.Clear();
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    lv.Columns.Add(dt.Columns[i].Caption.ToString());
                }
                for (var i = 0; i < lv.Columns.Count; i++)
                {
                    if (0 != i)
                    {
                        lv.Columns[i].TextAlign = HorizontalAlignment.Center;
                    }
                }
                foreach (DataRow dr in dt.Rows)
                {
                    var lvi = new ListViewItem();
                    lvi.SubItems[0].Text = dr[0].ToString();
                    for (var i = 1; i < dt.Columns.Count; i++)
                    {
                        lvi.SubItems.Add(dr[i].ToString());
                    }
                    lv.Items.Add(lvi);
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
    }
}
