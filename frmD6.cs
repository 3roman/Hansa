using Hansa.Model;
using Hansa.Utility;
using System;
using System.Data;
using System.Windows.Forms;

namespace Hansa
{
    public partial class FrmD6 : Form
    {
        private const int Insulation = 100;
        private const int MarginLength = 150;
        private readonly Bracket _bracket;

        public FrmD6(Bracket bracket)
        {
            InitializeComponent();
            _bracket = bracket;
            Common.DataTable2Listview(lstD6, _bracket.EndWelding ? D6_EndWelding() : D6_SideWelding());
        }

        private DataTable D6_SideWelding()
        {
            var columName = LocateColumn(_bracket.ArmLength);
            var dt = SQLiteHelper.Read("Hansa.db", "SELECT * FROM d6 WHERE type='I'");
            var query = dt.AsEnumerable().Where(t => t.Field<double>(columName) > _bracket.Load);
            var table = query.AsDataView().ToTable(true, new string[] { "steel", columName });
            var beamLength = Common.Round2Ten(_bracket.ArmLength + _bracket.OD / 2 + Insulation + MarginLength);
            Common.Copy2Clipboard($"D6\tI\t\t\t{_bracket.Elevation}\t\t{beamLength}" +
                $"\t\t\t\t\t\t\t1\t\t\t{table.Rows[0]["steel"]}\t\t\t\t\t\t1");

            return table;
        }

        private DataTable D6_EndWelding()
        {
            var columName = LocateColumn(_bracket.ArmLength);
            var dt = SQLiteHelper.Read("Hansa.db", "SELECT * FROM d6 WHERE type='II'");
            var query = dt.AsEnumerable().Where(t => t.Field<double>(columName) > _bracket.Load);
            var table = query.AsDataView().ToTable(true, new string[] { "steel", columName });
            var beamLength = Common.Round2Ten(_bracket.ArmLength + _bracket.OD / 2 + Insulation + MarginLength);
            Common.Copy2Clipboard($"D6\tII\t\t\t{_bracket.Elevation}\t\t{beamLength}" +
                $"\t\t\t\t\t\t\t1\t\t\t{table.Rows[0]["steel"]}\t\t\t\t\t\t1");

            return table;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string LocateColumn(double armLength)
        {
            var column = string.Empty;

            if (armLength <= 250)
            {
                column = "L≤250";
            }
            else if (armLength > 250 && armLength <= 500)
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

            return column;
        }
    }
}