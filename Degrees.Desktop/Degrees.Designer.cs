namespace Degrees.Desktop
{
    partial class Degrees
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        //private void InitializeComponent()
        //{
        //    this.components = new System.ComponentModel.Container();
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.ClientSize = new System.Drawing.Size(800, 450);
        //    this.Text = "Form1";
        //}

        #endregion
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._actor1Combo = new System.Windows.Forms.ComboBox();
            this._actor2Combo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._goButton = new System.Windows.Forms.Button();
            this._outputTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _actor1Combo
            // 
            this._actor1Combo.FormattingEnabled = true;
            this._actor1Combo.Location = new System.Drawing.Point(179, 47);
            this._actor1Combo.Name = "_actor1Combo";
            this._actor1Combo.Size = new System.Drawing.Size(460, 33);
            this._actor1Combo.TabIndex = 0;
            // 
            // _actor2Combo
            // 
            this._actor2Combo.FormattingEnabled = true;
            this._actor2Combo.Location = new System.Drawing.Point(179, 157);
            this._actor2Combo.Name = "_actor2Combo";
            this._actor2Combo.Size = new System.Drawing.Size(460, 33);
            this._actor2Combo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Actor 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Actor 2";
            // 
            // _goButton
            // 
            this._goButton.Location = new System.Drawing.Point(96, 261);
            this._goButton.Name = "_goButton";
            this._goButton.Size = new System.Drawing.Size(543, 41);
            this._goButton.TabIndex = 4;
            this._goButton.Text = "GO";
            this._goButton.UseVisualStyleBackColor = true;
            // 
            // _outputTextBox
            // 
            this._outputTextBox.Location = new System.Drawing.Point(96, 366);
            this._outputTextBox.Multiline = true;
            this._outputTextBox.Name = "_outputTextBox";
            this._outputTextBox.ReadOnly = true;
            this._outputTextBox.Size = new System.Drawing.Size(543, 351);
            this._outputTextBox.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 800);
            this.Controls.Add(this._outputTextBox);
            this.Controls.Add(this._goButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._actor2Combo);
            this.Controls.Add(this._actor1Combo);
            this.Name = "Degrees";
            this.Text = "Degrees";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _actor1Combo;
        private System.Windows.Forms.ComboBox _actor2Combo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _goButton;
        private System.Windows.Forms.TextBox _outputTextBox;
    }
}

