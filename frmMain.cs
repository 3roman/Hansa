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
                return;
            }
            if (l1 <= 0)
            {
                MessageBox.Show("必须输入距离", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
            // 开始计算前清空
            txtClamp_B2_1.Clear();
            txtRod_B2_1.Clear();
            txtLug_B2_1.Clear();
            txtRodLength_B2_1.Clear();

            var EL1 = Convert.ToInt32(txtEL1_B2_1.Text);
            var EL2 = Convert.ToInt32(txtEL2_B2_1.Text);
            var DN = cbxDN_B2_1.Text;
            var needCheckLoad = chkCheckLoad_B2_1.Checked;
            var pipeLoad = Convert.ToInt32(txtPipeLoad_B2_1.Text);
            string table = string.Empty;
            if (rioBritishPipe_B2_1.Checked && rioBaseType_B2_1.Checked)
            {
                table = "b2_2a";
            }
            else if (!rioBritishPipe_B2_1.Checked && rioBaseType_B2_1.Checked)
            {
                table = "b2_2b";
            }
            else if (rioBritishPipe_B2_1.Checked && rioInsualationType1_B2_1.Checked)
            {
                table = "b2_3a";
            }
            else if (!rioBritishPipe_B2_1.Checked && rioInsualationType1_B2_1.Checked)
            {
                table = "b2_3b";
            }
            else if (!rioBritishPipe_B2_1.Checked && rioInsualationType2_B2_1.Checked)
            {
                table = "b2_xb";
            }

            var sql = $"SELECT * FROM {table} WHERE dn='{DN}'";
            var dt = SQLiteHelper.Read("HASA.db", sql);
            var clamp = dt.Rows[0]["clamp"] + string.Empty;
            var rod = dt.Rows[0]["rod"] + string.Empty;
            var lug = dt.Rows[0]["lug"] + string.Empty;
            var E = Convert.ToInt32(dt.Rows[0]["e"]);
            var F = Convert.ToInt32(dt.Rows[0]["f"]);
            var allowableLoad = Convert.ToInt32(dt.Rows[0]["load"]);
            if (chkCheckLoad_B2_1.Checked && pipeLoad > allowableLoad)
            {
                sql = $"SELECT * FROM {table} WHERE load > {pipeLoad} LIMIT 0,1";
                dt = SQLiteHelper.Read("HASA.db", sql);
                if (0 == dt.Rows.Count)
                {
                    MessageBox.Show("管道荷载过大，无法自动选型!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                rod = dt.Rows[0]["rod"] + string.Empty;
                lug = dt.Rows[0]["lug"] + string.Empty;
                F = Convert.ToInt32(dt.Rows[0]["f"]);
            }

            var rodLength = EL1 - EL2 - E - F;
            txtClamp_B2_1.Text = clamp;
            txtRod_B2_1.Text = rod;
            txtLug_B2_1.Text = lug;
            txtRodLength_B2_1.Text = rodLength + string.Empty;

            var type = rioBaseType_B2_1.Checked ? "I" : "II";
            Common.Copy2Clipboard($"B2-1\t{type}\t\t\tEL.{EL1}\tEL.{EL2}\t{rodLength}" +
                $"\t\t\t\t\t\t\t1\t\t\t{lug}\t{rod}\t{clamp}\t\t\t\t1,1,1");

            if (chkB1_1.Checked)
            {
                var m = rod.Replace("A16(", "").Replace(")", "");
                sql = $"SELECT * FROM iso_nut WHERE m='{m}'";
                dt = SQLiteHelper.Read("HASA.db", sql);
                var h = Convert.ToInt32(dt.Rows[0]["h"]);
                rodLength = EL1 - EL2 - E + 3 * h;
                txtClamp_B2_1.Text = clamp;
                txtRod_B2_1.Text = rod;
                txtLug_B2_1.Text = string.Empty;
                txtRodLength_B2_1.Text = rodLength + string.Empty;
                Common.Copy2Clipboard($"B1-1\t{type}\t\t\tEL.{EL1}\tEL.{EL2}\t{rodLength}" +
                $"\t\t\t\t\t\t\t1\t\t\t{rod}\t{clamp}\t\t\t\t\t1,1");
            }
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
                    txtEL1_B2_1.Focus();
                    txtEL1_B2_1.SelectAll();
                    break;
                case 3:
                    break;
            }
        }

        private void ChkCheckLoad_B2_1_CheckedChanged(object sender, EventArgs e)
        {
            txtPipeLoad_B2_1.Enabled = chkCheckLoad_B2_1.Checked;
        }
    }
}

