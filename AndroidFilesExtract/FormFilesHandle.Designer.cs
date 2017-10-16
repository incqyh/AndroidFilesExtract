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
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.goToAnalyze = new System.Windows.Forms.Button();
            this.copyFiles = new System.Windows.Forms.Button();
            this.fileTree = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.searchedFiles = new System.Windows.Forms.ListBox();
            this.searchPattern = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.currentPath = new System.Windows.Forms.TextBox();
            this.startSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(39, 407);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 32;
            this.label5.Text = "完成进度";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(42, 438);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(835, 33);
            this.progressBar1.TabIndex = 31;
            // 
            // goToAnalyze
            // 
            this.goToAnalyze.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.goToAnalyze.Location = new System.Drawing.Point(847, 348);
            this.goToAnalyze.Name = "goToAnalyze";
            this.goToAnalyze.Size = new System.Drawing.Size(135, 42);
            this.goToAnalyze.TabIndex = 30;
            this.goToAnalyze.Text = "进入文件分析";
            this.goToAnalyze.UseVisualStyleBackColor = true;
            this.goToAnalyze.Click += new System.EventHandler(this.GoToAnalyze_Click);
            // 
            // copyFiles
            // 
            this.copyFiles.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.copyFiles.Location = new System.Drawing.Point(847, 238);
            this.copyFiles.Name = "copyFiles";
            this.copyFiles.Size = new System.Drawing.Size(135, 43);
            this.copyFiles.TabIndex = 29;
            this.copyFiles.Text = "复制选中的文件";
            this.copyFiles.UseVisualStyleBackColor = true;
            this.copyFiles.Click += new System.EventHandler(this.CopyFiles_Click);
            // 
            // fileTree
            // 
            this.fileTree.Location = new System.Drawing.Point(42, 157);
            this.fileTree.Name = "fileTree";
            this.fileTree.Size = new System.Drawing.Size(382, 232);
            this.fileTree.TabIndex = 28;
            this.fileTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FileTree_AfterSelect);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(465, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 27;
            this.label4.Text = "搜索到的文件列表";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(39, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "文件树";
            // 
            // searchedFiles
            // 
            this.searchedFiles.FormattingEnabled = true;
            this.searchedFiles.HorizontalScrollbar = true;
            this.searchedFiles.ItemHeight = 12;
            this.searchedFiles.Location = new System.Drawing.Point(465, 157);
            this.searchedFiles.Name = "searchedFiles";
            this.searchedFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.searchedFiles.Size = new System.Drawing.Size(342, 232);
            this.searchedFiles.TabIndex = 25;
            // 
            // searchPattern
            // 
            this.searchPattern.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.searchPattern.Location = new System.Drawing.Point(465, 73);
            this.searchPattern.Name = "searchPattern";
            this.searchPattern.Size = new System.Drawing.Size(342, 26);
            this.searchPattern.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(462, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "文件搜索通配符表达式";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(39, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "当前路径";
            // 
            // currentPath
            // 
            this.currentPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.currentPath.Location = new System.Drawing.Point(42, 73);
            this.currentPath.Name = "currentPath";
            this.currentPath.ReadOnly = true;
            this.currentPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.currentPath.Size = new System.Drawing.Size(382, 26);
            this.currentPath.TabIndex = 21;
            this.currentPath.Tag = "";
            // 
            // startSearch
            // 
            this.startSearch.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startSearch.Location = new System.Drawing.Point(847, 139);
            this.startSearch.Name = "startSearch";
            this.startSearch.Size = new System.Drawing.Size(135, 38);
            this.startSearch.TabIndex = 20;
            this.startSearch.Text = "开始搜索";
            this.startSearch.UseVisualStyleBackColor = true;
            this.startSearch.Click += new System.EventHandler(this.StartSearch_Click);
            // 
            // FormFilesHandle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 524);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.goToAnalyze);
            this.Controls.Add(this.copyFiles);
            this.Controls.Add(this.fileTree);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchedFiles);
            this.Controls.Add(this.searchPattern);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentPath);
            this.Controls.Add(this.startSearch);
            this.Name = "FormFilesHandle";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button goToAnalyze;
        private System.Windows.Forms.Button copyFiles;
        private System.Windows.Forms.TreeView fileTree;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox searchedFiles;
        private System.Windows.Forms.TextBox searchPattern;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox currentPath;
        private System.Windows.Forms.Button startSearch;
    }
}

