
namespace Gmap.net_test
{
    partial class Form1
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
            this.gMapTest = new GMap.NET.WindowsForms.GMapControl();
            this.SuspendLayout();
            // 
            // gMapTest
            // 
            this.gMapTest.AllowDrop = true;
            this.gMapTest.Bearing = 0F;
            this.gMapTest.CanDragMap = true;
            this.gMapTest.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapTest.GrayScaleMode = false;
            this.gMapTest.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapTest.LevelsKeepInMemmory = 5;
            this.gMapTest.Location = new System.Drawing.Point(12, 12);
            this.gMapTest.MarkersEnabled = true;
            this.gMapTest.MaxZoom = 2;
            this.gMapTest.MinZoom = 2;
            this.gMapTest.MouseWheelZoomEnabled = true;
            this.gMapTest.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapTest.Name = "gMapTest";
            this.gMapTest.NegativeMode = false;
            this.gMapTest.PolygonsEnabled = true;
            this.gMapTest.RetryLoadTile = 0;
            this.gMapTest.RoutesEnabled = true;
            this.gMapTest.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapTest.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapTest.ShowTileGridLines = false;
            this.gMapTest.Size = new System.Drawing.Size(1037, 647);
            this.gMapTest.TabIndex = 0;
            this.gMapTest.Zoom = 0D;
            this.gMapTest.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gMapTest_OnMarkerEnter);
            this.gMapTest.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.gMapTest_OnMarkerLeave);
            this.gMapTest.Load += new System.EventHandler(this.gMapTest_Load);
            this.gMapTest.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMapTest_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 671);
            this.Controls.Add(this.gMapTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapTest;
    }
}

