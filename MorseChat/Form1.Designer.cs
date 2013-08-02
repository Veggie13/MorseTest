namespace MorseChat
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
            this._txtOutput = new System.Windows.Forms.TextBox();
            this._txtMessage = new System.Windows.Forms.TextBox();
            this._btnSendData = new System.Windows.Forms.Button();
            this._chkLetTalk = new System.Windows.Forms.CheckBox();
            this._spnTimeout = new System.Windows.Forms.NumericUpDown();
            this._rdoDevC = new System.Windows.Forms.RadioButton();
            this._rdoDevB = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this._spnTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // _txtOutput
            // 
            this._txtOutput.Enabled = false;
            this._txtOutput.Location = new System.Drawing.Point(13, 13);
            this._txtOutput.Multiline = true;
            this._txtOutput.Name = "_txtOutput";
            this._txtOutput.ReadOnly = true;
            this._txtOutput.Size = new System.Drawing.Size(423, 205);
            this._txtOutput.TabIndex = 0;
            // 
            // _txtMessage
            // 
            this._txtMessage.Location = new System.Drawing.Point(13, 314);
            this._txtMessage.Name = "_txtMessage";
            this._txtMessage.Size = new System.Drawing.Size(339, 20);
            this._txtMessage.TabIndex = 1;
            // 
            // _btnSendData
            // 
            this._btnSendData.Location = new System.Drawing.Point(360, 314);
            this._btnSendData.Name = "_btnSendData";
            this._btnSendData.Size = new System.Drawing.Size(75, 23);
            this._btnSendData.TabIndex = 2;
            this._btnSendData.Text = "Send Data";
            this._btnSendData.UseVisualStyleBackColor = true;
            this._btnSendData.Click += new System.EventHandler(this._btnSendData_Click);
            // 
            // _chkLetTalk
            // 
            this._chkLetTalk.Appearance = System.Windows.Forms.Appearance.Button;
            this._chkLetTalk.Enabled = false;
            this._chkLetTalk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._chkLetTalk.Location = new System.Drawing.Point(13, 225);
            this._chkLetTalk.Name = "_chkLetTalk";
            this._chkLetTalk.Size = new System.Drawing.Size(74, 24);
            this._chkLetTalk.TabIndex = 4;
            this._chkLetTalk.Text = "Let Talk";
            this._chkLetTalk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._chkLetTalk.UseVisualStyleBackColor = true;
            this._chkLetTalk.CheckedChanged += new System.EventHandler(this._chkLetTalk_CheckedChanged);
            // 
            // _spnTimeout
            // 
            this._spnTimeout.Enabled = false;
            this._spnTimeout.Location = new System.Drawing.Point(360, 225);
            this._spnTimeout.Name = "_spnTimeout";
            this._spnTimeout.Size = new System.Drawing.Size(74, 20);
            this._spnTimeout.TabIndex = 5;
            // 
            // _rdoDevC
            // 
            this._rdoDevC.AutoSize = true;
            this._rdoDevC.Location = new System.Drawing.Point(256, 268);
            this._rdoDevC.Name = "_rdoDevC";
            this._rdoDevC.Size = new System.Drawing.Size(32, 17);
            this._rdoDevC.TabIndex = 6;
            this._rdoDevC.Text = "C";
            this._rdoDevC.UseVisualStyleBackColor = true;
            this._rdoDevC.CheckedChanged += new System.EventHandler(this._rdoDevC_CheckedChanged);
            // 
            // _rdoDevB
            // 
            this._rdoDevB.AutoSize = true;
            this._rdoDevB.Checked = true;
            this._rdoDevB.Location = new System.Drawing.Point(154, 268);
            this._rdoDevB.Name = "_rdoDevB";
            this._rdoDevB.Size = new System.Drawing.Size(32, 17);
            this._rdoDevB.TabIndex = 7;
            this._rdoDevB.TabStop = true;
            this._rdoDevB.Text = "B";
            this._rdoDevB.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 346);
            this.Controls.Add(this._rdoDevB);
            this.Controls.Add(this._rdoDevC);
            this.Controls.Add(this._spnTimeout);
            this.Controls.Add(this._chkLetTalk);
            this.Controls.Add(this._btnSendData);
            this.Controls.Add(this._txtMessage);
            this.Controls.Add(this._txtOutput);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this._spnTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _txtOutput;
        private System.Windows.Forms.TextBox _txtMessage;
        private System.Windows.Forms.Button _btnSendData;
        private System.Windows.Forms.CheckBox _chkLetTalk;
        private System.Windows.Forms.NumericUpDown _spnTimeout;
        private System.Windows.Forms.RadioButton _rdoDevC;
        private System.Windows.Forms.RadioButton _rdoDevB;
    }
}

