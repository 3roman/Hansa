using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HASA
{
    public partial class FrmMain : Form
    {
        private readonly Dictionary<int, int> DN2DO = new Dictionary<int, int>() { { 0, 0 }, { 10, 18 }, { 15, 22 }, { 20, 27 }, { 25, 34 }, { 32, 43 }, { 40, 49 }, { 50, 61 }, { 65, 76 }, { 80, 89 }, { 100, 115 }, { 125, 142 }, { 150, 169 }, { 200, 220 }, { 250, 273 }, { 300, 325 }, { 350, 377 }, { 400, 426 }, { 450, 480 }, { 500, 530 }, { 550, 559 }, { 600, 630 }, { 650, 660 }, { 700, 720 }, { 750, 762 }, { 800, 820 } };

        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnTotalLoad_Click(object sender, EventArgs e)
        {
            #region convert control value to number
            int dn1 = Convert.ToInt32(cbxDN1.Text);
            int dn2 = Convert.ToInt32(cbxDN2.Text);
            int dn3 = Convert.ToInt32(cbxDN3.Text);
            double pipeWall1 = Convert.ToDouble(txtPipeWall1.Text);
            double pipeWall2 = Convert.ToDouble(txtPipeWall2.Text);
            double pipeWall3 = Convert.ToDouble(txtPipeWall3.Text);
            double span1 = Convert.ToDouble(txtSpan1.Text);
            double span2 = Convert.ToDouble(txtSpan2.Text);
            double span3 = Convert.ToDouble(txtSpan3.Text);
            double insulation1 = Convert.ToDouble(txtInsulation1.Text);
            double insulation2 = Convert.ToDouble(txtInsulation2.Text);
            double insulation3 = Convert.ToDouble(txtInsulation3.Text);
            double cload1 = Convert.ToDouble(txtCload1.Text);
            double cload2 = Convert.ToDouble(txtCload2.Text);
            double cload3 = Convert.ToDouble(txtCload3.Text);
            #endregion

            #region pipeweight
            int do1 = DN2DO[dn1];
            int do2 = DN2DO[dn2];
            int do3 = DN2DO[dn3];
            var pipeWeight1 = Common.CalculatePipeWeight(do1, pipeWall1);
            var pipeWeight2 = Common.CalculatePipeWeight(do2, pipeWall2);
            var pipeWeight3 = Common.CalculatePipeWeight(do3, pipeWall3);
            #endregion

            #region water weight
            var waterWeight1 = Common.CalculateWaterWeight(do1, pipeWall1);
            var waterWeight2 = Common.CalculateWaterWeight(do2, pipeWall2);
            var waterWeight3 = Common.CalculateWaterWeight(do3, pipeWall3);
            #endregion

            #region insulation weight
            var insulationWeight1 = Common.CalculateInsulationWeight(do1, insulation1);
            var insulationWeight2 = Common.CalculateInsulationWeight(do2, insulation2);
            var insulationWeight3 = Common.CalculateInsulationWeight(do3, insulation3);
            #endregion

            #region total weight
            var tload1 = (pipeWeight1 + waterWeight1 + insulationWeight1) * span1 + cload1;
            var tload2 = (pipeWeight2 + waterWeight2 + insulationWeight2) * span2 + cload2;
            var tload3 = (pipeWeight3 + waterWeight3 + insulationWeight3) * span3 + cload3;
            #endregion

            txtTload1.Text = (int)tload1 + string.Empty;
            txtTload2.Text = (int)tload2 + string.Empty;
            txtTload3.Text = (int)tload3 + string.Empty;
        }

        private void BtnD6_Click(object sender, EventArgs e)
        {
            var p1 = Convert.ToInt32(txtTload1.Text) * 10 / 1000;
            //var p2 = Convert.ToInt32(txtTload2.Text) * 10 / 1000;
            //var p3 = Convert.ToInt32(txtTload3.Text) * 10 / 1000;
            var l1 = Convert.ToInt32(txtL1.Text);
            //var l2 = Convert.ToInt32(txtL2.Text);
            //var l3 = Convert.ToInt32(txtL3.Text);
            var elevation = Convert.ToInt32(txtElevation_D6.Text);

            if (p1 <= 0)
            {
                MessageBox.Show("必须输入总荷载", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ;
            }
            if (l1 <= 0)
            {
                MessageBox.Show("必须输入距离", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ;
            }

            var dlg = new FrmD6(p1, l1, elevation);
            dlg.ShowDialog();
        }

        private void BtnD19_Click(object sender, EventArgs e)
        {
            var p1 = Convert.ToInt32(txtTload1.Text) * 10 / 1000;
            //var p2 = Convert.ToInt32(txtTload2.Text) * 10 / 1000;
            //var p3 = Convert.ToInt32(txtTload3.Text) * 10 / 1000;
            var l4 = Convert.ToInt32(txtL4.Text);
            //var l5 = Convert.ToInt32(txtL5.Text);
            //var l6 = Convert.ToInt32(txtL6.Text);
            var elevation = Convert.ToInt32(txtElevation_D19.Text);

            if (p1 <= 0)
            {
                MessageBox.Show("必须输入总荷载", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (l4 <= 0)
            {
                MessageBox.Show("必须输入距离", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dlg = new FrmD19(p1, l4, elevation);
            dlg.ShowDialog();
        }

        private void BtnB2_1_Click(object sender, EventArgs e)
        {
            Common.CopytToClipboard("AAA\tBBB");

            //  开始计算前清空
            txtLug.Clear();
            txtRod.Clear();
            txtClamp.Clear();
            txtRodLength.Clear();

            var elevationI = int.Parse(txtElevationI_B2_1.Text);
            var elevationII = int.Parse(txtElevationII_B2_1.Text);
            var pipeLoad = int.Parse(txtPipeLoad_B2_1.Text);
            var dn = cbxDN_B2_1.Text;
            var hasInsulation = chkHasInsulation_B2_1.Checked;
            var isBritish = chkIsBritish_B2_1.Checked;

            // 吊杆选型
            var sql = $"SELECT * FROM a16 WHERE f > {pipeLoad} LIMIT 0,1";
            var dt = SQLiteHelper.Read("HASA.db", sql);
            var rodSeries = string.Empty + dt.Rows[0]["series"];
            int rodLength = 0;
            // 吊耳选型
            var lugSeries = $"A19({dt.Rows[0]["d"]})";
            sql = $"SELECT * FROM a19 WHERE series = '{lugSeries}' ";
            dt = SQLiteHelper.Read("HASA.db", sql);
            var lugLength = Convert.ToInt32(dt.Rows[0]["f"]);
            // 管夹选型
            string ClampSeries = string.Empty;
            int ClampLength = 0;
            if (!isBritish && hasInsulation)
            {
                ClampSeries = $"A7-1({dn})";
                sql = $"SELECT * FROM a7_1 WHERE series = '{ClampSeries}'";
                dt = SQLiteHelper.Read("HASA.db", sql);
                ClampLength = Convert.ToInt32(dt.Rows[0]["b"]) / 2 + Convert.ToInt32(dt.Rows[0]["c"]);
            }
            else if (!isBritish && !hasInsulation)
            {
                ClampSeries = $"A5-1({dn})";
                sql = $"SELECT * FROM a5_1 WHERE series = '{ClampSeries}'";
                dt = SQLiteHelper.Read("HASA.db", sql);
                var f = Convert.ToInt32(dt.Rows[0]["f"]) / 2;
                if (f > pipeLoad)
                {
                    // 基准型管夹
                    ClampLength = Convert.ToInt32(dt.Rows[0]["b"]) / 2;
                }
                else
                {
                    // 重准型管夹
                    ClampSeries = $"A6-1({dn})";
                    sql = $"SELECT * FROM a6_1 WHERE series = '{ClampSeries}'";
                    dt = SQLiteHelper.Read("HASA.db", sql);
                    ClampLength = Convert.ToInt32(dt.Rows[0]["b"]) / 2;
                }
            }
            else if (isBritish && hasInsulation)
            {
                // TODO
                MessageBox.Show("还未开发");
                return;
            }
            else if (isBritish && !hasInsulation)
            {
                // TODO
                MessageBox.Show("还未开发");
                return;
            }

            //计算吊杆长度
            rodLength = elevationI - elevationII - lugLength - ClampLength;
            txtLug.Text = lugSeries;
            txtRod.Text = rodSeries;
            txtClamp.Text = ClampSeries;
            txtRodLength.Text = rodLength + string.Empty;

            var type = hasInsulation ? "II" : "I";
            Common.CopytToClipboard($"B2-1\t{type}\t\t{pipeLoad}\t{elevationI}\t{elevationII}\t{rodLength}" +
                $"\t\t\t\t\t\t\t1\t\t\t{lugSeries}\t{rodSeries}\t{ClampSeries}\t\t\t\t1,1,1");
        }

        private void TabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = sender as TabControl;
            switch (tab.SelectedIndex)
            {
                case 0:
                    AcceptButton = null;
                    break;
                case 1:
                    AcceptButton = BtnB2_1;
                    txtElevationI_B2_1.Focus();
                    txtElevationI_B2_1.SelectAll();
                    break;
                case 3:
                    break;
            }
        }
    }
}

