namespace Panda_Explorer.Controls
{
    partial class ControlButtons
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Layout1 = new System.Windows.Forms.TableLayoutPanel();
            this.MinimizeBtn = new System.Windows.Forms.PictureBox();
            this.MinMaxButn = new System.Windows.Forms.PictureBox();
            this.ExitBtn = new System.Windows.Forms.PictureBox();
            this.Layout1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinMaxButn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutPanel
            // 
            this.Layout1.AutoSize = true;
            this.Layout1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Layout1.ColumnCount = 3;
            this.Layout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.Layout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.Layout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.Layout1.Controls.Add(this.ExitBtn, 0, 0);
            this.Layout1.Controls.Add(this.MinMaxButn, 0, 0);
            this.Layout1.Controls.Add(this.MinimizeBtn, 0, 0);
            this.Layout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Layout1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.Layout1.Location = new System.Drawing.Point(0, 0);
            this.Layout1.Margin = new System.Windows.Forms.Padding(0);
            this.Layout1.Name = "Layout";
            this.Layout1.RowCount = 1;
            this.Layout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Layout1.Size = new System.Drawing.Size(90, 30);
            this.Layout1.TabIndex = 3;
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.BackColor = System.Drawing.SystemColors.Window;
            this.MinimizeBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinimizeBtn.Location = new System.Drawing.Point(0, 0);
            this.MinimizeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Size = new System.Drawing.Size(29, 30);
            this.MinimizeBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.MinimizeBtn.TabIndex = 1;
            this.MinimizeBtn.TabStop = false;
            this.MinimizeBtn.MouseClick += MouseClickMinimizeBtn;
            // 
            // MinMaxButn
            // 
            this.MinMaxButn.BackColor = System.Drawing.SystemColors.Window;
            this.MinMaxButn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinMaxButn.Location = new System.Drawing.Point(29, 0);
            this.MinMaxButn.Margin = new System.Windows.Forms.Padding(0);
            this.MinMaxButn.Name = "MinMaxButn";
            this.MinMaxButn.Size = new System.Drawing.Size(29, 30);
            this.MinMaxButn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.MinMaxButn.TabIndex = 2;
            this.MinMaxButn.TabStop = false;
            this.MinMaxButn.MouseClick += MouseClickMinMaxBtn;
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.SystemColors.Window;
            this.ExitBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExitBtn.Location = new System.Drawing.Point(58, 0);
            this.ExitBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(32, 30);
            this.ExitBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ExitBtn.TabIndex = 3;
            this.ExitBtn.TabStop = false;
            this.ExitBtn.MouseClick += MouseClickExitBtn;
            // 
            // ControlButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.Controls.Add(this.Layout1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ControlButtons";
            this.Size = new System.Drawing.Size(90, 30);
            this.Layout1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinMaxButn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Layout1;
        internal System.Windows.Forms.PictureBox ExitBtn;
        internal System.Windows.Forms.PictureBox MinMaxButn;
        internal System.Windows.Forms.PictureBox MinimizeBtn;
    }
}
