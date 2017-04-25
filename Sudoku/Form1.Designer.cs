namespace Sudoku
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            for (int i = 0; i < 9; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            }
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);

            this.tableLayoutPanel1.RowCount = 9;
            for (int i = 0; i < 9; i++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            }
            this.tableLayoutPanel1.Size = new System.Drawing.Size(900, 900);
            this.tableLayoutPanel1.TabIndex = 0;
        
            this.tableLayoutPanel1.Controls.Add(this.button1, 3, 4);


            this.button1.Text = "button1";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1000, 1000);
            this.Controls.Add(this.tableLayoutPanel1);
         

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
    }
}

