namespace N3
{
    partial class Form1
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
        private void InitializeComponent()
        {
            rtbMessages = new RichTextBox();
            btnStart = new Button();
            txtPort = new TextBox();
            SuspendLayout();
            // 
            // rtbMessages
            // 
            rtbMessages.Location = new Point(460, 50);
            rtbMessages.Name = "rtbMessages";
            rtbMessages.Size = new Size(305, 241);
            rtbMessages.TabIndex = 0;
            rtbMessages.Text = "";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(27, 28);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(135, 67);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(145, 136);
            txtPort.Multiline = true;
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(259, 155);
            txtPort.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtPort);
            Controls.Add(btnStart);
            Controls.Add(rtbMessages);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbMessages;
        private Button btnStart;
        private TextBox txtPort;
    }
}
