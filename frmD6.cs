using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Hansa
{
    public partial class FrmD6 : Form
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
        public FrmD6(int totaLoad, int armLength, int elevation, int od, int insulation)
        {
            _armLength = armLength;
            _totaLoad = totaLoad;
            _elevation = elevation;
            _od = od;
            _insulation = insulation;

            InitializeComponent();

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
            var weldingType = MessageBoxEx.Show("选择与结构件焊接型式", "选择型式", MessageBoxButtons.OKCancel, new string[] { "侧焊", "端焊" });
            if (DialogResult.OK == weldingType)
            {
                Common.DataTable2Listview(lstCantilever, D6I());
            }
            else
            {
                Common.DataTable2Listview(lstCantilever, D6II());
            }
        }

        /// <summary>
        /// 侧焊单悬臂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private DataTable D6I()
        {
            var dt = SQLiteHelper.Read("Hansa.db", "SELECT * FROM d6 WHERE type='I'");
            var columName = GetColumName(_armLength);
            var query = from row in dt.AsEnumerable()
                        where row.Field<double>(columName) > _totaLoad
                        select row;
            var table = query.AsDataView().ToTable(true, new string[] { "steel", columName });

            var beamLength = Common.Round2Ten(_armLength + _od / 2 + _insulation + 200 + 150);
            Common.Copy2Clipboard($"D6\tI\t\t\t{_elevation}\t\t{beamLength}" +
                $"\t\t\t\t\t\t\t1\t\t\t{table.Rows[0]["steel"]}\t\t\t\t\t\t1");

            return table;
        }

        /// <summary>
        /// 端焊单悬臂
        /// </summary>
        /// <returns></returns>
        private DataTable D6II()
        {
            var dt = SQLiteHelper.Read("Hansa.db", "SELECT * FROM d6 WHERE type='II'");
            var columName = GetColumName(_armLength);
            var query = from row in dt.AsEnumerable()
                        where row.Field<double>(columName) > _totaLoad
                        select row;

            var table = query.AsDataView().ToTable(true, new string[] { "steel", columName });

            var beamLength = Common.Round2Ten(_armLength + _od / 2 + _insulation + 200);
            Common.Copy2Clipboard($"D6\tII\t\t\t{_elevation}\t\t{beamLength}" +
                $"\t\t\t\t\t\t\t1\t\t\t{table.Rows[0]["steel"]}\t\t\t\t\t\t1");

            return table;
        }


        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 获取列名
        private static string GetColumName(double armLength)
        {
            string columName = string.Empty;

            if (armLength <= 250)
            {
                columName = "L≤250";
            }
            else if (armLength > 250 && armLength <= 500)
            {
                columName = "L≤500";
            }
            else if (armLength > 500 && armLength <= 750)
            {
                columName = "L≤750";
            }
            else if (armLength > 750 && armLength <= 1000)
            {
                columName = "L≤1000";
            }

            return columName;
        }
    }
}