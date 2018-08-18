namespace UiManager
{
    public partial class GameSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.playerTwoCheckBox = new System.Windows.Forms.CheckBox();
            this.playerOneText = new System.Windows.Forms.TextBox();
            this.playerTwoText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.rowNumeric = new System.Windows.Forms.NumericUpDown();
            this.colNumeric = new System.Windows.Forms.NumericUpDown();
            this.Play = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rowNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Players:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Player1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Player2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 186);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 20);
            this.label4.TabIndex = 3;
            // 
            // playerTwoCheckBox
            // 
            this.playerTwoCheckBox.AutoSize = true;
            this.playerTwoCheckBox.Location = new System.Drawing.Point(30, 70);
            this.playerTwoCheckBox.Name = "playerTwoCheckBox";
            this.playerTwoCheckBox.Size = new System.Drawing.Size(15, 14);
            this.playerTwoCheckBox.TabIndex = 3;
            this.playerTwoCheckBox.UseVisualStyleBackColor = true;
            this.playerTwoCheckBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // playerOneText
            // 
            this.playerOneText.Location = new System.Drawing.Point(135, 34);
            this.playerOneText.Name = "playerOneText";
            this.playerOneText.Size = new System.Drawing.Size(142, 26);
            this.playerOneText.TabIndex = 1;
            // 
            // playerTwoText
            // 
            this.playerTwoText.AccessibleName = string.Empty;
            this.playerTwoText.Enabled = false;
            this.playerTwoText.Location = new System.Drawing.Point(135, 66);
            this.playerTwoText.Name = "playerTwoText";
            this.playerTwoText.Size = new System.Drawing.Size(142, 26);
            this.playerTwoText.TabIndex = 2;
            this.playerTwoText.Text = "[computer]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 140);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Board Size:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(186, 186);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Cols:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 186);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "Rows:";
            // 
            // rowNumeric
            // 
            this.rowNumeric.Location = new System.Drawing.Point(86, 180);
            this.rowNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.rowNumeric.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.rowNumeric.Name = "rowNumeric";
            this.rowNumeric.Size = new System.Drawing.Size(40, 26);
            this.rowNumeric.TabIndex = 4;
            this.rowNumeric.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.rowNumeric.ValueChanged += new System.EventHandler(this.Numeric_Change);
            // 
            // colNumeric
            // 
            this.colNumeric.Location = new System.Drawing.Point(237, 180);
            this.colNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.colNumeric.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.colNumeric.Name = "colNumeric";
            this.colNumeric.Size = new System.Drawing.Size(40, 26);
            this.colNumeric.TabIndex = 5;
            this.colNumeric.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.colNumeric.ValueChanged += new System.EventHandler(this.Numeric_Change);
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(17, 252);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(260, 31);
            this.Play.TabIndex = 6;
            this.Play.Text = "Play!";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // GameSettings
            // 
            this.AcceptButton = this.Play;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 295);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.colNumeric);
            this.Controls.Add(this.rowNumeric);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.playerTwoText);
            this.Controls.Add(this.playerOneText);
            this.Controls.Add(this.playerTwoCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameSettings";
            ((System.ComponentModel.ISupportInitialize)(this.rowNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox playerTwoCheckBox;
        private System.Windows.Forms.TextBox playerOneText;
        private System.Windows.Forms.TextBox playerTwoText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown rowNumeric;
        private System.Windows.Forms.NumericUpDown colNumeric;
        private System.Windows.Forms.Button Play;
    }
}