namespace CatmullRomSplines
{
	partial class CatmullRomSplines
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
            this.components = new System.ComponentModel.Container();
            this.panelCanvas = new Library.FlickerFreePanel();
            this.timerSinus = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // panelCanvas
            // 
            this.panelCanvas.BackColor = System.Drawing.Color.White;
            this.panelCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCanvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this.panelCanvas.Location = new System.Drawing.Point(12, 12);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(268, 246);
            this.panelCanvas.TabIndex = 0;
            // 
            // timerSinus
            // 
            this.timerSinus.Enabled = true;
            this.timerSinus.Tick += new System.EventHandler(this.timerSinus_Tick);
            // 
            // LineLineIntersection2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Controls.Add(this.panelCanvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "CatmullRomSplines";
            this.Text = "Catmull-Rom Splines";
            this.ResumeLayout(false);

		}

		#endregion

		private Library.FlickerFreePanel panelCanvas;
		private System.Windows.Forms.Timer timerSinus;
	}
}

