using Panda_Explorer.PandaSettings;

namespace Panda_Explorer.Controls
{
    partial class TitleBar
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
            this.LayoutTable = new System.Windows.Forms.TableLayoutPanel();
            this.TitleLbl = new System.Windows.Forms.Label();
            this.Icon = new System.Windows.Forms.PictureBox();
            this.ControlButtons = new Panda_Explorer.Controls.ControlButtons();
            this.LayoutTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutTable
            // 
            this.LayoutTable.AutoSize = true;
            this.LayoutTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LayoutTable.ColumnCount = 4;
            this.LayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.LayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.LayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.LayoutTable.Controls.Add(this.TitleLbl, 1, 0);
            this.LayoutTable.Controls.Add(this.Icon, 0, 0);
            this.LayoutTable.Controls.Add(this.ControlButtons, 3, 0);
            this.LayoutTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutTable.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.LayoutTable.Location = new System.Drawing.Point(0, 0);
            this.LayoutTable.Margin = new System.Windows.Forms.Padding(0);
            this.LayoutTable.Name = "LayoutTable";
            this.LayoutTable.RowCount = 1;
            this.LayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutTable.Size = new System.Drawing.Size(750, 40);
            this.LayoutTable.TabIndex = 0;
            // 
            // TitleLbl
            // 
            this.TitleLbl.AutoSize = true;
            this.TitleLbl.BackColor = System.Drawing.Color.Transparent;
            this.TitleLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleLbl.Location = new System.Drawing.Point(40, 0);
            this.TitleLbl.Margin = new System.Windows.Forms.Padding(0);
            //this.TitleLbl.MaximumSize = new System.Drawing.Size(0, 40);
            this.TitleLbl.Name = "TitleLbl";
            this.TitleLbl.Size = new System.Drawing.Size(59, 40);
            this.TitleLbl.TabIndex = 2;
            this.TitleLbl.Text = "TitleLbl";
            this.TitleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IconPb
            // 
            this.Icon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Icon.Location = new System.Drawing.Point(0, 0);
            this.Icon.Margin = new System.Windows.Forms.Padding(0);
            this.Icon.MaximumSize = new System.Drawing.Size(80, 80);
            this.Icon.MinimumSize = new System.Drawing.Size(20, 20);
            this.Icon.Name = "Icon";
            this.Icon.Size = new System.Drawing.Size(40, 40);
            this.Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Icon.TabIndex = 1;
            this.Icon.TabStop = false;
            // 
            // ControlButtons
            // 
            this.ControlButtons.AutoSize = true;
            this.ControlButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ControlButtons.BackColor = System.Drawing.SystemColors.Window;
            this.ControlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlButtons.Location = new System.Drawing.Point(630, 0);
            this.ControlButtons.Margin = new System.Windows.Forms.Padding(0);
            //this.ControlButtons.MaximumSize = new System.Drawing.Size(0, 40);
            this.ControlButtons.Name = "ControlButtons";
            this.ControlButtons.Size = new System.Drawing.Size(120, 40);
            this.ControlButtons.TabIndex = 5;
            // 
            // TitleBar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.Controls.Add(this.LayoutTable);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(750, 30);
            this.Name = "TitleBar";
            this.Size = new System.Drawing.Size(750, 40);
            this.LayoutTable.ResumeLayout(false);
            this.LayoutTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TableLayoutPanel LayoutTable;
        internal System.Windows.Forms.Label TitleLbl;
        internal System.Windows.Forms.PictureBox Icon;
        internal ControlButtons ControlButtons;
    }
}
