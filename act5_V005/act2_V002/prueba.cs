/*
 * Created by SharpDevelop.
 * User: Roman
 * Date: 19/09/2019
 * Time: 01:22 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace act2_V002
{
	/// <summary>
	/// Description of prueba.
	/// </summary>
	public partial class prueba : Form
	{
		public prueba()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void recreacion (Bitmap pixi) {
			pictureBox1.Image = pixi;
			
		}
	}
}
