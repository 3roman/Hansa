using System;
using System.Data;
using System.Windows.Forms;

namespace HASA
{
    internal static class Common
    {
        public static double CalculatePipeWeight(int od, double wallThickness)
        {
            return 0.02466 * (od - wallThickness) * wallThickness;
        }

        public static double CalculateWaterWeight(double od, double wallThickness)
        {
            od /= 1000;
            wallThickness /= 1000;

            return 0.785 * Math.Pow(od - 2 * wallThickness, 2) * 1000;
        }

        public static double CalculateInsulationWeight(double od, double insulation)
        {
            od /= 1000;
            insulation /= 1000;

            return 0.785 * (Math.Pow(od + 2 * insulation, 2) - Math.Pow(od, 2)) * 200;
        }

        public static void Copy2Clipboard(string context)
        {
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Text, context);
        }

        public static void DataTableToListview(ListView lv, DataTable dt)
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

        public static int Round2Ten(object num)
        {
            var number = Convert.ToInt32(num);

            return (number / 10 + 1) * 10;
        }
    }
}
