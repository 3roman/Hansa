using System;
using System.Data;
using System.Windows.Forms;
using Hansa.Model;
using Hansa.Utility;

namespace Hansa
{
    public partial class FrmD19 : Form
    {
        private const int Insulation = 100;
        private const int MarginLength = 150;
        private readonly Bracket _bracket;

        public FrmD19(Bracket bracket)
        {
            InitializeComponent();
            _bracket = bracket;
            Common.DataTable2Listview(lstD19, _bracket.EndWelding ? D19_EndWelding() : D19_SideWelding());
        }

        private DataTable D19_SideWelding()
        {
            var columName = LocateColumn(_bracket.ArmLength);
            var dt = SQLiteHelper.Read("Hansa.db", "SELECT * FROM d19 WHERE type='I'");
            var query = dt.AsEnumerable().Where(t => t.Field<double>(columName) > _bracket.Load);
            var table = query.AsDataView().ToTable(true, "steel", columName);
            var temp = table.Rows[0]["steel"].ToString().Split('(', ')');
            var beamLength = Common.Round2Ten(_bracket.ArmLength + _bracket.OD / 2 + Insulation + MarginLength);
            Common.Copy2Clipboard($"D19\tI\t\t\t{_bracket.Elevation}\t\t{beamLength}" +
                $"\t{_bracket.ArmLength}\t\t\t\t\t\t1\t\t\t{temp[0]}\t{temp[1]}\t\t\t\t\t1,1");

            return table;
        }

        private DataTable D19_EndWelding()
        {
            var columName = LocateColumn(_bracket.ArmLength);
            var dt = SQLiteHelper.Read("Hansa.db", "SELECT * FROM d19 WHERE type='II'");
            var query = dt.AsEnumerable().Where(t => t.Field<double>(columName) > _bracket.Load);
            var table = query.AsDataView().ToTable(true, "steel", columName);
            var temp = table.Rows[0]["steel"].ToString().Split('(', ')');
            var beamLength = Common.Round2Ten(_bracket.ArmLength + _bracket.OD / 2 + Insulation + MarginLength);
            Common.Copy2Clipboard($"D19\tII\t\t\t{_bracket.Elevation}\t\t{beamLength}" +
                $"\t{_bracket.ArmLength}\t\t\t\t\t\t1\t\t\t{temp[0]}\t{temp[1]}\t\t\t\t\t1,1");

            return table;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string LocateColumn(double armLength)
        {
            var column = string.Empty;

            if (armLength <= 500)
            {
                column = "L≤500";
            }
            else if (armLength > 500 && armLength <= 750)
            {
                column = "L≤750";
            }
            else if (armLength > 750 && armLength <= 1000)
            {
                column = "L≤1000";
            }
            else if (armLength > 1000 && armLength <= 1250)
            {
                column = "L≤1250";
            }
            else if (armLength > 1250 && armLength <= 1500)
            {
                column = "L≤1500";
            }
            else if (armLength > 1500 && armLength <= 1750)
            {
                column = "L≤1750";
            }
            else if (armLength > 1750 && armLength <= 2000)
            {
                column = "L≤2000";
            }

            return column;
        }
    }
}
