namespace LinePointIntersection
{
	partial class LinePointIntersection
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
			this.panelCanvas = new Library.FlickerFreePanel();
			this.labelInfoBlue = new System.Windows.Forms.Label();
			this.labelDistBlue = new System.Windows.Forms.Label();
			this.labelDistRed = new System.Windows.Forms.Label();
			this.labelInfoRed = new System.Windows.Forms.Label();
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
			// labelInfoBlue
			// 
			this.labelInfoBlue.AutoSize = true;
			this.labelInfoBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelInfoBlue.Location = new System.Drawing.Point(12, 264);
			this.labelInfoBlue.Name = "labelInfoBlue";
			this.labelInfoBlue.Size = new System.Drawing.Size(71, 12);
			this.labelInfoBlue.TabIndex = 1;
			this.labelInfoBlue.Text = "distance to blue: ";
			// 
			// labelDistBlue
			// 
			this.labelDistBlue.AutoSize = true;
			this.labelDistBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDistBlue.Location = new System.Drawing.Point(12, 277);
			this.labelDistBlue.Name = "labelDistBlue";
			this.labelDistBlue.Size = new System.Drawing.Size(37, 12);
			this.labelDistBlue.TabIndex = 2;
			this.labelDistBlue.Text = "<value>";
			// 
			// labelDistRed
			// 
			this.labelDistRed.AutoSize = true;
			this.labelDistRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDistRed.Location = new System.Drawing.Point(209, 277);
			this.labelDistRed.Name = "labelDistRed";
			this.labelDistRed.Size = new System.Drawing.Size(37, 12);
			this.labelDistRed.TabIndex = 4;
			this.labelDistRed.Text = "<value>";
			// 
			// labelInfoRed
			// 
			this.labelInfoRed.AutoSize = true;
			this.labelInfoRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelInfoRed.Location = new System.Drawing.Point(209, 264);
			this.labelInfoRed.Name = "labelInfoRed";
			this.labelInfoRed.Size = new System.Drawing.Size(67, 12);
			this.labelInfoRed.TabIndex = 3;
			this.labelInfoRed.Text = "distance to red: ";
			// 
			// LinePointIntersection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 293);
			this.Controls.Add(this.labelDistRed);
			this.Controls.Add(this.labelInfoRed);
			this.Controls.Add(this.labelDistBlue);
			this.Controls.Add(this.labelInfoBlue);
			this.Controls.Add(this.panelCanvas);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "LinePointIntersection";
			this.Text = "Line-Point Intersection";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Library.FlickerFreePanel panelCanvas;
		private System.Windows.Forms.Label labelInfoBlue;
		private System.Windows.Forms.Label labelDistBlue;
		private System.Windows.Forms.Label labelDistRed;
		private System.Windows.Forms.Label labelInfoRed;
	}
}

