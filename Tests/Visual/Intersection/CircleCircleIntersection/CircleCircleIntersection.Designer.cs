using MathLib.Visual;

namespace MathLib.Tests.Visual.Intersection.CircleCircleIntersection
{
	partial class CircleCircleIntersection
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
			this.panelCanvas = new FlickerFreePanel();
			this.labelInfoBlue = new System.Windows.Forms.Label();
			this.labelDistBlue = new System.Windows.Forms.Label();
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
			// CircleCircleIntersection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 293);
			this.Controls.Add(this.labelDistBlue);
			this.Controls.Add(this.labelInfoBlue);
			this.Controls.Add(this.panelCanvas);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "CircleCircleIntersection";
			this.Text = "Circle-Circle Intersection";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private FlickerFreePanel panelCanvas;
		private System.Windows.Forms.Label labelInfoBlue;
		private System.Windows.Forms.Label labelDistBlue;
	}
}

