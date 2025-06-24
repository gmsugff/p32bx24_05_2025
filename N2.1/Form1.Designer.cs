namespace N2._1
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
            listBoxLog = new TextBox();
            btnStop = new Button();
            btnStart = new Button();
            SuspendLayout();
            // 
            // listBoxLog
            // 
            listBoxLog.Location = new Point(356, 12);
            listBoxLog.Multiline = true;
            listBoxLog.Name = "listBoxLog";
            listBoxLog.Size = new Size(400, 358);
            listBoxLog.TabIndex = 5;
            listBoxLog.TextChanged += listBoxLog_TextChanged;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(40, 138);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(149, 64);
            btnStop.TabIndex = 4;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += this.btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(40, 67);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(149, 46);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += this.btnStart_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBoxLog);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox listBoxLog;
        private Button btnStop;
        private Button btnStart;
    }
}
