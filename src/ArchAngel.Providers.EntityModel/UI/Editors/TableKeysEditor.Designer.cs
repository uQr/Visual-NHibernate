﻿namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    partial class TableKeysEditor
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
            this.slyceGrid1 = new Slyce.Common.Controls.Diagramming.SlyceGrid.SlyceGrid();
            this.SuspendLayout();
            // 
            // slyceGrid1
            // 
            this.slyceGrid1.DisabledColor = System.Drawing.Color.Gray;
            this.slyceGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slyceGrid1.InvalidColor = System.Drawing.Color.Orange;
            this.slyceGrid1.Location = new System.Drawing.Point(0, 0);
            this.slyceGrid1.Name = "slyceGrid1";
            this.slyceGrid1.Size = new System.Drawing.Size(287, 98);
            this.slyceGrid1.TabIndex = 2;
            // 
            // TableKeysEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.slyceGrid1);
            this.Name = "TableKeysEditor";
            this.Size = new System.Drawing.Size(287, 98);
            this.ResumeLayout(false);

        }

        #endregion

        private Slyce.Common.Controls.Diagramming.SlyceGrid.SlyceGrid slyceGrid1;
    }
}
