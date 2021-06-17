
namespace Monogame_Sokobon {
    partial class LevelEditor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.SizeY = new System.Windows.Forms.TextBox();
            this.SizeX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelBox = new System.Windows.Forms.Label();
            this.LevelName = new System.Windows.Forms.TextBox();
            this.Export = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Load = new System.Windows.Forms.Button();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Closing);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Level Size:";
            // 
            // SizeY
            // 
            this.SizeY.Location = new System.Drawing.Point(49, 27);
            this.SizeY.Name = "SizeY";
            this.SizeY.Size = new System.Drawing.Size(23, 23);
            this.SizeY.TabIndex = 2;
            this.SizeY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SizeY_KeyDown);
            this.SizeY.Leave += new System.EventHandler(this.SizeY_TextChanged);
            // 
            // SizeX
            // 
            this.SizeX.Location = new System.Drawing.Point(12, 27);
            this.SizeX.Name = "SizeX";
            this.SizeX.Size = new System.Drawing.Size(23, 23);
            this.SizeX.TabIndex = 1;
            this.SizeX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SizeX_KeyDown);
            this.SizeX.Leave += new System.EventHandler(this.SizeX_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "X";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(100, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 426);
            this.panel1.TabIndex = 4;
            // 
            // labelBox
            // 
            this.labelBox.AutoSize = true;
            this.labelBox.Location = new System.Drawing.Point(8, 58);
            this.labelBox.Name = "labelBox";
            this.labelBox.Size = new System.Drawing.Size(42, 15);
            this.labelBox.TabIndex = 5;
            this.labelBox.Text = "Name:";
            // 
            // LevelName
            // 
            this.LevelName.Location = new System.Drawing.Point(12, 76);
            this.LevelName.Name = "LevelName";
            this.LevelName.Size = new System.Drawing.Size(82, 23);
            this.LevelName.TabIndex = 6;
            this.LevelName.TextChanged += new System.EventHandler(this.LevelName_TextChanged);
            this.LevelName.Leave += new System.EventHandler(this.LevelName_TextChanged);
            // 
            // Export
            // 
            this.Export.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Export.FlatAppearance.BorderSize = 0;
            this.Export.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Export.Location = new System.Drawing.Point(12, 105);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(82, 82);
            this.Export.TabIndex = 7;
            this.Export.Text = "Export\r\nLevel\r\n";
            this.Export.UseVisualStyleBackColor = false;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.Location = new System.Drawing.Point(8, 395);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 43);
            this.button1.TabIndex = 8;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Load
            // 
            this.Load.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Load.FlatAppearance.BorderSize = 0;
            this.Load.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Load.Location = new System.Drawing.Point(12, 193);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(82, 82);
            this.Load.TabIndex = 9;
            this.Load.Text = "Load\r\nLevel\r\n(No Save)\r\n";
            this.Load.UseVisualStyleBackColor = false;
            this.Load.Click += new System.EventHandler(this.Load_Click);
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 450);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Export);
            this.Controls.Add(this.LevelName);
            this.Controls.Add(this.labelBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SizeX);
            this.Controls.Add(this.SizeY);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MinimumSize = new System.Drawing.Size(554, 489);
            this.Name = "LevelEditor";
            this.Text = "Level Editor";
            this.SizeChanged += new System.EventHandler(this.LevelEditor_ResizeEnd);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SizeY;
        private System.Windows.Forms.TextBox SizeX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelBox;
        private System.Windows.Forms.TextBox LevelName;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Load;
    }
}