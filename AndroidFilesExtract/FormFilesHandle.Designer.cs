namespace AndroidFilesExtract
{
    partial class FormFilesHandle
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.testView = new System.Windows.Forms.ListBox();
            this.test = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testView
            // 
            this.testView.FormattingEnabled = true;
            this.testView.ItemHeight = 12;
            this.testView.Location = new System.Drawing.Point(93, 33);
            this.testView.Name = "testView";
            this.testView.Size = new System.Drawing.Size(261, 244);
            this.testView.TabIndex = 38;
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(93, 329);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(132, 57);
            this.test.TabIndex = 39;
            this.test.Text = "test";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // FormFilesHandle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 595);
            this.Controls.Add(this.test);
            this.Controls.Add(this.testView);
            this.Name = "FormFilesHandle";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox testView;
        private System.Windows.Forms.Button test;
    }
}

