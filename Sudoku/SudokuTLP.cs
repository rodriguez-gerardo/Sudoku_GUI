using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    class SudokuTLP : Form
    {
        public SudokuTLP()
        {
            TableLayoutPanel p = new TableLayoutPanel();
            Button btn = new System.Windows.Forms.Button();
            // 
            // tableLayoutPanel1
            // 
            p.ColumnCount = 9;
            for (int i = 0; i < p.ColumnCount; i++)
            {
                ColumnStyle c = new ColumnStyle();

                c.SizeType = SizeType.Absolute;
                c.Width = 100f;

                p.ColumnStyles.Add(c);

                p.CellPaint += TLP_CellPaint;

            }
            p.Location = new System.Drawing.Point(10, 10);

            p.RowCount = 9;
            for (int i = 0; i < p.RowCount; i++)
            {
                RowStyle r = new RowStyle();

                r.SizeType = SizeType.Absolute;
                r.Height = 100f;
                p.RowStyles.Add(r);
            }

            p.Size = new System.Drawing.Size(900, 900);

         //   p.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            p.Controls.Add(btn, 3, 4);


            btn.Text = "button1";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1000, 1000);
            this.Controls.Add(p);
        }

        private void TLP_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, new Point(e.CellBounds.Right, e.CellBounds.Top));
        }
    }
}
