/*
 *Gerardo Rodriguez 
 * COMP 585
 * 4/26/2017
 * Covington
 * Sudoku Project
 */

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
        TableLayoutPanel p = new TableLayoutPanel();

        //2D Array will hold sudoku puzzle
        int[,] puzzle;


        public SudokuTLP()
        {

            //Number of rows and columns for TableLayoutPanel
            p.ColumnCount = 9; 
            p.RowCount = 9;

            //Initialize panel rows and columns
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

            //This will call function which will call to read sudoku file, and add labels and combo boxes accordingly
            SetUpPuzzle(p);
        
            p.Size = new System.Drawing.Size(630, 630); //Size of panel

            this.ClientSize = new System.Drawing.Size(700, 700);//Size of Window
            this.Controls.Add(p);
            p.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;//Lines out all cells in panel

            p.Paint += new PaintEventHandler(PaintPanel);//Paints partition to seperate cells into 9 subgroups
        } //Set up TableLayoutPanel and call functions

        public void SetUpPuzzle(TableLayoutPanel p)
        {
            puzzle = ReadFile(); //Call to read file

            /*This loop will determine if a combobox (if value is 0) or label should be
            place in respective cell*/
            for (int row = 0; row < p.RowCount; row++)
            {
                for(int col = 0; col < p.ColumnCount; col++)
                {
                    if (puzzle[row, col] == 0)
                        AddComboBox(col, row);
                    else
                        AddLabel(col, row);
                }
            }
        }//Calls ReadFile and calls function to add either label or ComboBox

        public void AddLabel(int c, int r)
        {
            Label lbl = new Label(); //Initialize new lable

            //Set label properties
            lbl.Height = 40;
            lbl.Width = 45;
            lbl.Anchor = (AnchorStyles.None);
            lbl.Padding = new Padding(10);//Centers label in cell
            lbl.Font = new Font(FontFamily.GenericSansSerif, 15.0f, FontStyle.Regular);
            lbl.Text = puzzle[r, c].ToString();

            p.Controls.Add(lbl, c, r);

        } //Adds label to cell
        public void AddComboBox(int c, int r)
        {
            //initialize ComboBox
            ComboBox cb = new ComboBox();

            //set ComboBox properties
            cb.Height = 40;
            cb.Width = 45; 
            cb.Anchor = (AnchorStyles.None);
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Font = new Font(FontFamily.GenericSansSerif, 15.0f, FontStyle.Regular);

            cb.DisplayMember = "Text";
            cb.ValueMember = "Value";

            //Index values
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

            cb.MouseDown += new MouseEventHandler(ConstraintsMessage); //When ComboBox is right-clicked we get to see constraints
            cb.SelectedValueChanged += new EventHandler(UpdateItems); //Updates puzzle value for corresponding index

            p.Controls.Add(cb, c, r);
        } //Adds ComboBox to cell

        private void UpdateItems(Object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender; //Case object to ComboBox
            int r = p.GetRow(cb);
            int c = p.GetColumn(cb);

            try
            {
                puzzle[r, c] = (int)cb.SelectedIndex;//Places changed value into indexed array

            }catch(InvalidCastException ex)
            {
                Console.WriteLine(ex);
            }  
        } //Updates array when ComboBox value is changed

        private void ConstraintsMessage(Object sender, MouseEventArgs e)
        {
            Point mouseDownLocation = new Point(e.X, e.Y);

            if(e.Button == MouseButtons.Right) //Right-button click
            {
                String result = "Contstraints: " + ConstraintsChecker((ComboBox)sender); //Displayes constraints
                MessageBox.Show(result, "Constraint Hints");

            }
                    
        } //Diplays message box with Constraints

        private String ConstraintsChecker(ComboBox cb)
        {
            int row = p.GetRow(cb);
            int col = p.GetColumn(cb);

            String s = ""; //String with constraints to be returned

            //bool values to determine what numbers are used by giving them value of false
            bool[] rvalues = new bool[9];
            bool[] cvalues = new bool[9];
            bool[] gvalues = new bool[9];

            //Initialize all bool values to true
            for (int i = 0; i < 9; i++)
            {
                rvalues[i] = true;
                cvalues[i] = true;
                gvalues[i] = true;
            }

            //gives false value to respected index based on what numbers are in column
            for(int i = 0; i < 9; i++)
            {
                if(i != col) 
                {
                    int v = puzzle[row, i];

                    if(v != 0)
                    {
                        if (rvalues[v - 1])
                            rvalues[v - 1] = false;
                    }
                }
            }

            //gives false value to respected index based on what numbers are in row

            for (int i = 0; i < 9; i++)
            {
                if(i != row)
                {
                    int v = puzzle[i, col];

                    if(v != 0)
                    {
                        if (cvalues[v - 1])
                            cvalues[v - 1] = false;
                    }
                }
            }

            int ulr = (row/3)*3;
            int ulc = (col/3)*3;

            //gives false value to respected index based on what numbers are in subgrid

            for (int i = ulr; i < ulr + 3; i++)
            {
                for(int j = ulc; j < ulc+3; j++)
                {
                    if (i == row && j == col) continue;

                    int v = puzzle[i, j];
                    if (v != 0)
                        if (gvalues[v - 1])
                            gvalues[v - 1] = false;                  
                }
            }

            /*Checks all constraints and adds them to string
              this will prevent duplicates*/
            for(int i = 0; i < 9; i++)
            {
                if (rvalues[i] && cvalues[i] && gvalues[i]) s += " " + (i + 1);
            }

                return s;
        } //Looks for all constraints, called by ConstraintsMessage

        public int[,] ReadFile()
        {
            String file = @"..\..\puzzle.txt";
            String[] parsed;

            String line = null;

            int[,] items = new int[9, 9]; //2D array to be returned and populated in puzzle array
            int j = 0;
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        parsed = line.Split(' ');//String places into string array

                        for (int i = 0; i < items.GetLength(0); i++)
                        {
                            try
                            {
                                items[j, i] = Int32.Parse(parsed[i]);//String converted to int

                            }
                            catch (IndexOutOfRangeException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        j++;
                    }
                }
                return items;
            }catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        } //Reads sudoku puzzle file

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
        } //paints subdivision lines

    }
}
