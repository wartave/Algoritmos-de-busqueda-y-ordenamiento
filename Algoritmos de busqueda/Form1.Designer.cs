﻿namespace Algoritmos_de_busqueda
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
            startBtn = new Button();
            label2 = new Label();
            plotView1 = new OxyPlot.WindowsForms.PlotView();
            label1 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // startBtn
            // 
            startBtn.Location = new Point(12, 573);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(835, 23);
            startBtn.TabIndex = 0;
            startBtn.Text = "Empezar";
            startBtn.UseVisualStyleBackColor = true;
            startBtn.Click += startBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 525);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 2;
            label2.Text = "Tiempo:";
            // 
            // plotView1
            // 
            plotView1.Location = new Point(12, 5);
            plotView1.Name = "plotView1";
            plotView1.PanCursor = Cursors.Hand;
            plotView1.Size = new Size(835, 472);
            plotView1.TabIndex = 11;
            plotView1.Text = "plotView1";
            plotView1.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView1.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView1.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 500);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 12;
            label1.Text = "Tiempo:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 555);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 13;
            label3.Text = "Tiempo:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(859, 604);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(plotView1);
            Controls.Add(label2);
            Controls.Add(startBtn);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startBtn;
        private Label label2;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private Label label1;
        private Label label3;
    }
}