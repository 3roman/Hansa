using System;
using System.Data;
using System.Windows.Forms;

namespace Hansa.Utility
{
    internal static class Common
    {
        public static void Copy2Clipboard(string context)
        {
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Text, context);
        }

        public static void DataTable2Listview(ListView lv, DataTable dt)
        {
            if (null == dt) return;
            lv.Items.Clear();
            lv.Columns.Clear();
            for (var i = 0; i < dt.Columns.Count; i++)
            {
                lv.Columns.Add(dt.Columns[i].Caption);
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

        public static int Round2Ten(object num)
        {
            var number = Convert.ToInt32(num);
            return (number / 10 ) * 10;
        }
    }
}
