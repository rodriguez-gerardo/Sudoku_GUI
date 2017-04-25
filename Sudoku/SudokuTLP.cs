using System;
using System.IO;
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

            p.ColumnCount = 9;
            p.RowCount = 9;

            for (int i = 0; i < p.ColumnCount; i++)
            {
                ColumnStyle c = new ColumnStyle();
                c.SizeType = SizeType.Absolute;
                c.Width = 70f;

                p.ColumnStyles.Add(c);
            }

            p.Location = new System.Drawing.Point(30, 30);

            for (int i = 0; i < p.RowCount; i++)
            {
                RowStyle r = new RowStyle();
                r.SizeType = SizeType.Absolute;
                r.Height = 70f;

                p.RowStyles.Add(r);
            }

            CallCombo(p);
        
            p.Size = new System.Drawing.Size(630, 630);

            this.ClientSize = new System.Drawing.Size(700, 700);
            this.Controls.Add(p);

            p.Paint += new PaintEventHandler(PaintPanel);
        }

        public void CallCombo(TableLayoutPanel p)
        {
            String[,] puzzle = ReadFile();

            for (int row = 0; row < p.RowCount; row++)
            {
                for(int col = 0; col < p.ColumnCount; col++)
                {
                    AddComboBox(p, col, row);
                    Console.Write(puzzle[row, col]);
                }
                Console.Write(Environment.NewLine);
            }
        }
        public void AddComboBox(TableLayoutPanel p, int c, int r)
        {
            ComboBox cb = new ComboBox();

            cb.Height = 40;
            cb.Width = 45;
            cb.Anchor = (AnchorStyles.None);
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Font = new Font(FontFamily.GenericSansSerif, 15.0f, FontStyle.Regular);

            cb.DisplayMember = "Text";
            cb.ValueMember = "Value";

            var num = new[] {
                new {Text = " ", Value = "0" },
                new {Text = "1", Value = "1" },
                new {Text = "2", Value = "2" },
                new {Text = "3", Value = "3" },
                new {Text = "4", Value = "4" },
                new {Text = "5", Value = "5" },
                new {Text = "6", Value = "6" },
                new {Text = "7", Value = "7" },
                new {Text = "8", Value = "8" },
                new {Text = "9", Value = "9" }
            };

            cb.DataSource = num;

            p.Controls.Add(cb, c, r);
        }


        public String[,] ReadFile()
        {
            String file = @"C:\Users\geardo\Documents\GitHub\Sudoku_GUI\Sudoku\puzzle.txt";
            String[] parsed;

            String line = null;

            String[,] items = new String[9, 9];
            int j = 0;

            using (StreamReader sr = new StreamReader(file))
            {
                while(!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    parsed = line.Split(' ');
                    
                    for(int i = 0; i < items.GetLength(0); i++)
                    {
                        items[j, i] = parsed[i];
                    }
                    j++;
                }
            }
            return items;
        }

        private void PaintPanel(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                Pen pen = new Pen(Color.DimGray, 5);


                g.DrawLine(pen, 212, 0, 212, 630);
                g.DrawLine(pen, 212 * 2, 0, 212 * 2, 630);

                g.DrawLine(pen, 0, 212, 630, 212);
                g.DrawLine(pen, 0, 212 * 2, 630, 212 * 2);
            }
        }

    }
}
