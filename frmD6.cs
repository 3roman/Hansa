using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HASA
{
    public partial class FrmD6 : Form
    {
        private int distance;
        private int pipeLoad;
        private int elevation;
        private int od;
        private int insulation;

        public FrmD6()
        {
            //InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipeLoad">单位KN</param>
        /// <param name="distance">单位mm</param>
        /// <param name="elevation">单位mm</param>
        public FrmD6(int pipeLoad, int distance, int elevation, int od, int insulation)
        {
            this.distance = distance;
            this.pipeLoad = pipeLoad;
            this.elevation = elevation;
            this.od = od;
            this.insulation = insulation;

            InitializeComponent();
        }

        private void FrmD6_Load(object sender, EventArgs e)
        {
            #region 手动加
            //var lvi = new ListViewItem
            //{
            //    Text = "∠50×6"
            //};
            //lvi.SubItems.Add("1.4");
            //lvi.SubItems.Add("0.5");
            //lvi.SubItems.Add("-");
            //lvi.SubItems.Add("-");
            //lstD1.Items.Add(lvi);

            //lvi = new ListViewItem
            //{
            //    Text = "∠75×7"
            //};
            //lvi.SubItems.Add("3.6");
            //lvi.SubItems.Add("1.8");
            //lvi.SubItems.Add("0.9");
            //lvi.SubItems.Add("0.5");
            //lstD1.Items.Add(lvi);

            //lvi = new ListViewItem
            //{
            //    Text = "∠100×10"
            //};
            //lvi.SubItems.Add("10");
            //lvi.SubItems.Add("5");
            //lvi.SubItems.Add("2");
            //lvi.SubItems.Add("1.2");
            //lstD1.Items.Add(lvi);


            //lvi = new ListViewItem
            //{
            //    Text = "[12.6"
            //};
            //lvi.SubItems.Add("12");
            //lvi.SubItems.Add("5.8");
            //lvi.SubItems.Add("2.5");
            //lvi.SubItems.Add("1.4");
            //lstD1.Items.Add(lvi);

            //lvi = new ListViewItem
            //{
            //    Text = "[16"
            //};
            //lvi.SubItems.Add("20");
            //lvi.SubItems.Add("10");
            //lvi.SubItems.Add("5");
            //lvi.SubItems.Add("2.8");
            //lstD1.Items.Add(lvi);

            //lvi = new ListViewItem
            //{
            //    Text = "H125×125"
            //};
            //lvi.SubItems.Add("39");
            //lvi.SubItems.Add("19");
            //lvi.SubItems.Add("13");
            //lvi.SubItems.Add("7.6");
            //lstD1.Items.Add(lvi);

            //lvi = new ListViewItem
            //{
            //    Text = "H150×150"
            //};
            //lvi.SubItems.Add("65");
            //lvi.SubItems.Add("32");
            //lvi.SubItems.Add("22");
            //lvi.SubItems.Add("15");
            //lstD1.Items.Add(lvi);

            //lvi = new ListViewItem
            //{
            //    Text = "H200×200"
            //};
            //lvi.SubItems.Add("-");
            //lvi.SubItems.Add("70");
            //lvi.SubItems.Add("46");
            //lvi.SubItems.Add("33");
            //lstD1.Items.Add(lvi);

            //lvi = new ListViewItem
            //{
            //    Text = "H250×250"
            //};
            //lvi.SubItems.Add("-");
            //lvi.SubItems.Add("120");
            //lvi.SubItems.Add("83");
            //lvi.SubItems.Add("60");
            //lstD1.Items.Add(lvi);
            #endregion
            var ret = MessageBoxEx.Show("选择与结构件焊接型式", "选择型式", MessageBoxButtons.OKCancel, new string[] { "侧焊", "端焊" });
            if (ret == DialogResult.OK)
            {
                Common.DataTableToListview(lstCantilever, D6I());
            }
            else
            {
                Common.DataTableToListview(lstCantilever, D6II());
            }
        }

        /// <summary>
        /// 侧焊单悬臂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private DataTable D6I()
        {
            var dt = SQLiteHelper.Read("HASA.db", "SELECT * FROM d6 WHERE type='I'");
            var colName = DetermineColumn(distance);
            var query = from row in dt.AsEnumerable()
                        where row.Field<double>(colName) > pipeLoad
                        select row;
            var table = query.AsDataView().ToTable(true, new string[] { "steel", colName });

            var len = Common.Round2Ten(distance + od / 2 + insulation + 180);
            Common.Copy2Clipboard($"D6\tI\t\t{pipeLoad * 1000}\tEL.{elevation}\t\t{len}" +
                $"\t\t\t\t\t\t\t1\t\t\t{table.Rows[0]["steel"]}\t\t\t\t\t\t1");

            return table;
        }

        /// <summary>
        /// 侧焊单悬臂
        /// </summary>
        /// <returns></returns>
        private DataTable D6II()
        {
            var dt = SQLiteHelper.Read("HASA.db", "SELECT * FROM d6 WHERE type='II'");
            var colName = DetermineColumn(distance);
            var query = from row in dt.AsEnumerable()
                        where row.Field<double>(colName) > pipeLoad
                        select row;

            var table = query.AsDataView().ToTable(true, new string[] { "steel", colName });

            var len = Common.Round2Ten(distance + od / 2 + insulation + 180);
            Common.Copy2Clipboard($"D6\tII\t\t{pipeLoad * 1000}\tEL.{elevation}\t\t{len}" +
                $"\t\t\t\t\t\t\t1\t\t\t{table.Rows[0]["steel"]}\t\t\t\t\t\t1");

            return table;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string DetermineColumn(double distance)
        {
            string column = string.Empty;

            if (distance <= 250)
            {
                column = "L≤250";
            }
            else if (distance > 250 && distance <= 500)
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

            return column;
        }
    }
}