/*
 * Created by SharpDevelop.
 * User: Roman
 * Date: 14/09/2019
 * Time: 06:22 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace act2_V002
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox mostrar_imagen;
		private System.Windows.Forms.Button Seleccionar_imagen;
		private System.Windows.Forms.Button analizar;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button agente;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem agenteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem señueloToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox G_kruskal;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox T_kruskal;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox G_prin;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox T_prin;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ToolStripMenuItem prinToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dijkstraToolStripMenuItem;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.mostrar_imagen = new System.Windows.Forms.PictureBox();
			this.Seleccionar_imagen = new System.Windows.Forms.Button();
			this.analizar = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.agente = new System.Windows.Forms.Button();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.agenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.señueloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.prinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dijkstraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.G_kruskal = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.T_kruskal = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.G_prin = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.T_prin = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.mostrar_imagen)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mostrar_imagen
			// 
			this.mostrar_imagen.Location = new System.Drawing.Point(16, 15);
			this.mostrar_imagen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.mostrar_imagen.Name = "mostrar_imagen";
			this.mostrar_imagen.Size = new System.Drawing.Size(869, 638);
			this.mostrar_imagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.mostrar_imagen.TabIndex = 0;
			this.mostrar_imagen.TabStop = false;
			this.mostrar_imagen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Mostrar_imagenMouseClick);
			// 
			// Seleccionar_imagen
			// 
			this.Seleccionar_imagen.Location = new System.Drawing.Point(889, 15);
			this.Seleccionar_imagen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Seleccionar_imagen.Name = "Seleccionar_imagen";
			this.Seleccionar_imagen.Size = new System.Drawing.Size(136, 46);
			this.Seleccionar_imagen.TabIndex = 1;
			this.Seleccionar_imagen.Text = "Seleccionar";
			this.Seleccionar_imagen.UseVisualStyleBackColor = true;
			this.Seleccionar_imagen.Click += new System.EventHandler(this.Seleccionar_imagenClick);
			// 
			// analizar
			// 
			this.analizar.Location = new System.Drawing.Point(889, 96);
			this.analizar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.analizar.Name = "analizar";
			this.analizar.Size = new System.Drawing.Size(136, 43);
			this.analizar.TabIndex = 2;
			this.analizar.Text = "analizar";
			this.analizar.UseVisualStyleBackColor = true;
			this.analizar.Click += new System.EventHandler(this.AnalizarClick);
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 16;
			this.listBox1.Location = new System.Drawing.Point(889, 161);
			this.listBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(292, 276);
			this.listBox1.TabIndex = 3;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(1187, 15);
			this.treeView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(243, 634);
			this.treeView1.TabIndex = 4;
			
			this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TreeView1MouseClick);
			// 
			// agente
			// 
			this.agente.Location = new System.Drawing.Point(1043, 15);
			this.agente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.agente.Name = "agente";
			this.agente.Size = new System.Drawing.Size(136, 46);
			this.agente.TabIndex = 5;
			this.agente.Text = "Animar";
			this.agente.UseVisualStyleBackColor = true;
			this.agente.Click += new System.EventHandler(this.AgenteClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.agenteToolStripMenuItem,
			this.señueloToolStripMenuItem,
			this.prinToolStripMenuItem,
			this.dijkstraToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(132, 108);
			this.contextMenuStrip1.Click += new System.EventHandler(this.ContextMenuStrip1Click);
			// 
			// agenteToolStripMenuItem
			// 
			this.agenteToolStripMenuItem.Name = "agenteToolStripMenuItem";
			this.agenteToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
			this.agenteToolStripMenuItem.Text = "agente";
			this.agenteToolStripMenuItem.Click += new System.EventHandler(this.AgenteToolStripMenuItemClick);
			// 
			// señueloToolStripMenuItem
			// 
			this.señueloToolStripMenuItem.Name = "señueloToolStripMenuItem";
			this.señueloToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
			this.señueloToolStripMenuItem.Text = "cebo";
			this.señueloToolStripMenuItem.Click += new System.EventHandler(this.SeñueloToolStripMenuItemClick);
			// 
			// prinToolStripMenuItem
			// 
			this.prinToolStripMenuItem.Name = "prinToolStripMenuItem";
			this.prinToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
			this.prinToolStripMenuItem.Text = "Prin";
			this.prinToolStripMenuItem.Click += new System.EventHandler(this.PrinToolStripMenuItemClick);
			// 
			// dijkstraToolStripMenuItem
			// 
			this.dijkstraToolStripMenuItem.Name = "dijkstraToolStripMenuItem";
			this.dijkstraToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
			this.dijkstraToolStripMenuItem.Text = "dijkstra";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(889, 442);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 21);
			this.label1.TabIndex = 6;
			this.label1.Text = "Kruskal";
			// 
			// G_kruskal
			// 
			this.G_kruskal.Location = new System.Drawing.Point(889, 466);
			this.G_kruskal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.G_kruskal.Multiline = true;
			this.G_kruskal.Name = "G_kruskal";
			this.G_kruskal.Size = new System.Drawing.Size(72, 24);
			this.G_kruskal.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(895, 500);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 20);
			this.label2.TabIndex = 8;
			this.label2.Text = "grafos";
			// 
			// T_kruskal
			// 
			this.T_kruskal.Location = new System.Drawing.Point(971, 466);
			this.T_kruskal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.T_kruskal.Multiline = true;
			this.T_kruskal.Name = "T_kruskal";
			this.T_kruskal.Size = new System.Drawing.Size(173, 24);
			this.T_kruskal.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(971, 500);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 20);
			this.label3.TabIndex = 10;
			this.label3.Text = "tiempo";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(893, 535);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 25);
			this.label4.TabIndex = 11;
			this.label4.Text = "Prim";
			// 
			// G_prin
			// 
			this.G_prin.Location = new System.Drawing.Point(889, 564);
			this.G_prin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.G_prin.Multiline = true;
			this.G_prin.Name = "G_prin";
			this.G_prin.Size = new System.Drawing.Size(57, 24);
			this.G_prin.TabIndex = 12;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(889, 592);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 20);
			this.label5.TabIndex = 13;
			this.label5.Text = "grafos";
			// 
			// T_prin
			// 
			this.T_prin.Location = new System.Drawing.Point(956, 564);
			this.T_prin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.T_prin.Multiline = true;
			this.T_prin.Name = "T_prin";
			this.T_prin.Size = new System.Drawing.Size(188, 24);
			this.T_prin.TabIndex = 14;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(951, 592);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 20);
			this.label6.TabIndex = 15;
			this.label6.Text = "tiempo";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1445, 679);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.T_prin);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.G_prin);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.T_kruskal);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.G_kruskal);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.agente);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.analizar);
			this.Controls.Add(this.Seleccionar_imagen);
			this.Controls.Add(this.mostrar_imagen);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "MainForm";
			this.Text = "act2_V002";
			((System.ComponentModel.ISupportInitialize)(this.mostrar_imagen)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
