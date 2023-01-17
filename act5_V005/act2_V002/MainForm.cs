/*
 * Created by SharpDevelop.
 * User: Roman
 * Date: 14/09/2019
 * Time: 06:22 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace act2_V002
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		Circulo sim_origin;
		Circulo sim_desti;
		Circulo sim_focus;
		
		bool agenteflag = false;
		bool señalflag = false;
		
		List<Circulo> lista;
		List<Circulo> lista_prim;
		
		List<agente> lista_agente;
		
		List<Point> temp_pix;
		
		Bitmap grafodraw;
		Bitmap animadraw;
		Bitmap provisional;
		
		
		Point p0, pf;
		int contClick =0;
		int suma_total = 0;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			sim_origin = new Circulo(/*null, -1*/);
			sim_desti = new Circulo (/*null, -1*/);
			
			lista = new List<Circulo>();
			lista_prim = new List<Circulo>();
			lista_agente = new List<agente>();
			
			mostrar_imagen.BackgroundImageLayout = ImageLayout.Zoom;
			p0 = new Point();
			pf = new Point();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Seleccionar_imagenClick(object sender, EventArgs e)
		{
			
			openFileDialog1.ShowDialog();
			mostrar_imagen.Image = Image.FromFile(openFileDialog1.FileName);
			lista.Clear();
			lista_agente.Clear();
			listBox1.DataSource = null;
			listBox1.Items.Clear();
			treeView1.Nodes.Clear();
			
			sim_origin = new Circulo(/*null, -1*/);
			sim_desti = new Circulo (/*null, -1*/);
			sim_focus = new Circulo (/*null, -1*/);
	
		}
		void AnalizarClick(object sender, EventArgs e)
		{
			Bitmap pixi = (Bitmap)mostrar_imagen.Image;
			grafodraw = (Bitmap)mostrar_imagen.Image;
			animadraw = new Bitmap(pixi.Width, pixi.Height);
			provisional = new Bitmap(pixi.Width, pixi.Height);
			
			
			
			double cercanos = 1000;
			int cercano_A=0, cercano_B=0;
			hacer_lista_circulos(pixi);
			
			
			for (int x = 0; x < lista.Count; x++) {
				int temp =0;
				for (int z = 0; z < lista.Count; z++) {
					
						bool conex = lineaDDA(x,z, lista[x].getx(), lista[x].gety(), lista[z].getx(), lista[z].gety(), pixi);
						double dis = Math.Sqrt(Math.Pow(lista[z].getx() - lista[x].getx(),2) + Math.Pow(lista[z].gety() - lista[x].gety(),2));
						
						if (x == z) conex = false;
						
						if (conex == true) {
							lista[x].to_distan(dis, conex, lista[z], lista[x]);
							lista[x].add_point(temp, temp_pix, z);
							suma_total += (int)dis;
							temp++;
						}
						
						/*prueba fall = new prueba();
						fall.recreacion(pixi);
						fall.ShowDialog();*/
						
					
				}
				
			}
			
			suma_total += 1;
			
			/*for (int x = 0; x < lista.Count; x++) {
				for (int z = 0; z < lista[x].getConexiones() ; z++) {
				
					if (lista[x].getdistan(z) < cercanos && x != z) {	
						cercanos = lista[x].getdistan(z);
						cercano_A = x;
						cercano_B = z;
					}
				}
			}
			
			dibujarCirculo(lista[cercano_A].getx(), lista[cercano_A].gety(), pixi, Color.Yellow);
			dibujarCirculo(lista[cercano_B].getx(), lista[cercano_B].gety(), pixi, Color.Yellow);*/
			
			for (int x = 0; x < lista.Count; x++) {
				for (int z = 0; z < lista[x].getConexiones(); z++) {
					hacerlineas(x,lista[x].get_hijos(z), pixi);
				}
			}
			
			treeView1.BeginUpdate();
			for (int x =0; x < lista.Count; x++) {
				treeView1.Nodes.Add(x.ToString());
				for (int z = 0; z < lista[x].getConexiones() ; z++) {
					treeView1.Nodes[x].Nodes.Add(lista[x].get_hijos(z).ToString());
				}
			}
			treeView1.EndUpdate();
			
			Graphics g = Graphics.FromImage(pixi);
			for (int i=0; i < lista.Count; i++) {
				lista[i].set_id(i);
				g.DrawString(i.ToString(), new Font(i.ToString(), 15), Brushes.Aqua, (lista[i].getx() - (lista[i].get_ra()*2)), (lista[i].gety() - (lista[i].get_ra()*2) ) );
			}
			
			empezar_dijkstra();
			
			provisional = pixi;
			
			mostrar_imagen.BackgroundImage = pixi;
			
			mostrardatos ();
	
		}
		
		void empezar_dijkstra() {
			for (int i = 0; i < lista.Count; i++) {
				lista[i].set_pesoacum(suma_total);
			}
		}
		
		void hacer_lista_circulos (Bitmap pixi) {
			
			
			for (int y=0; y < pixi.Height; y++) {
				for (int x = 0; x < pixi.Width; x++) {
					
					if (pixi.GetPixel(x,y) == Color.FromArgb(0,0,0) ) {
						calcular_circulo(x,y,pixi);
					}
					if (pixi.GetPixel(x,y) == Color.FromArgb(192,192,192)) {
					    	pixi.SetPixel(x, y, Color.White);
					}
					if (pixi.GetPixel(x,y) == Color.FromArgb(128,128,128)) {
						pixi.SetPixel(x, y, Color.Black);
					}
				
				}
			}
			
			
		}
		
		void calcular_circulo(int x, int y, Bitmap pixi) {
			Circulo nueva = new Circulo();
			
			int ini_x, fin_x, centrox;
			int ysup, yinf, centroy;
			int r,mitad;
			
			ysup = yinf = y;
			ini_x = fin_x = x;
			
			while (pixi.GetPixel(fin_x, y) != Color.FromArgb(255,255,255)) {
				fin_x++;
				//if (pixi.GetPixel(fin_x, y) != Color.FromArgb(255,255,255)) break;
			}
			fin_x--;
			mitad = (ini_x + fin_x)/2;
			
			while (pixi.GetPixel(mitad,yinf) != Color.FromArgb(255,255,255)) {
				yinf++;
				//if (pixi.GetPixel(mitad,yinf) != Color.FromArgb(255,255,255)) break;
			}
			
			/*prueba fall = new prueba();
			fall.recreacion(pixi);
			fall.ShowDialog();*/
			
			centroy = (ysup + yinf)/2;
			r = (yinf - ysup)/2;
			
			ini_x = fin_x = mitad;
			while (pixi.GetPixel(ini_x,y) != Color.FromArgb(255,255,255)) {
				ini_x--;	
			}
			ini_x++;
			
			while (pixi.GetPixel(fin_x,y) != Color.FromArgb(255,255,255)) {
				fin_x++;	
			}
			fin_x++;
			
			centrox = (ini_x + fin_x)/2;
			
			if (!esovalo(centrox,centroy,r,pixi)) {
				
				dibujarCirculo(mitad, centroy, pixi, Color.Gray);
				
				/*int k = 4;
				for (int i = centroy - k; i < k + centroy; i++) {
					for (int j = centrox - k; j < k + mitad; j++) {
							pixi.SetPixel(j, i, Color.Aqua);
					}
				}
				
				pixi.SetPixel(centrox, centroy, Color.Aqua);*/
				nueva.alta(r, centroy, centrox);
				
				lista.Add(nueva);
			}	
			
		}
		
		void mostrardatos () {
			Circulo b = new Circulo();
			/*for (int x = 0; x < lista.Count; x++) {
				for (int z = 0; z < lista.Count-1; z++) {
					if (lista[z].get_ra() < lista[z+1].get_ra()) {
						b = lista[z];
						lista[z] = lista[z+1];
						lista[z+1] = b;
					}
				}
			}*/
			//lista  = lista.OrderBy(func => func.get_ra()).ToList();
			listBox1.DataSource = lista;
		}
		
		bool esovalo (int x, int y, int radioy, Bitmap pixi) {
			int xin, radiox=1;
			xin = x;
			while (pixi.GetPixel(xin,y) != Color.FromArgb(255,255,255)) {
				xin--;
				radiox++;
			}
			xin++;
			
			if (radiox > (radioy+10) || radioy > (radiox+10)) {
				dibujarCirculo(x, y, pixi, Color.Silver);
				return true;
			}
			else {
				return false;
			}
			
		}
		
		void dibujarCirculo(int x, int y,  Bitmap pixi, Color colore) {
			
			int xbeta = x;
			int ybeta, yaux;
			
			ybeta = yaux = y;
			
			while (pixi.GetPixel(xbeta,ybeta) != Color.FromArgb(255,255,255)) {
				xbeta--;
				//if (pixi.GetPixel(xbeta,ybeta) != Color.FromArgb(255,255,255)) break;
			}
			xbeta++;
			
			while (pixi.GetPixel(xbeta, yaux) != Color.FromArgb(255,255,255)) {
				ybeta = y;
				
				while (pixi.GetPixel(xbeta, ybeta) != Color.FromArgb(255,255,255)) {
					pixi.SetPixel(xbeta, ybeta, colore);
					ybeta--;
				}
				ybeta++;
				while (pixi.GetPixel(xbeta, ybeta) != Color.FromArgb(255,255,255)) {
					pixi.SetPixel(xbeta, ybeta, colore);
					ybeta++;
				}
				ybeta--;
				
				xbeta++;
			}
			
			
		}
		
		void dibujaragente(agente a, Color c) {
			Graphics g  = Graphics.FromImage(animadraw);
			//g.Clear(Color.Transparent);
			int x = a.getx_act();
			int y = a.gety_act();
			int ra = a.getR();
			Brush b = new SolidBrush(c);
			
			/*for (int i = y - ra; i < ra + y; i++) {
					for (int j = x - ra; j < ra + x; j++) {
							animadraw.SetPixel(j, i, Color.Green);
					}
				}*/
			
			
			g.FillEllipse(b, x-(ra+ra),y-(ra+ra), (ra*ra), (ra*ra) );
		}
		
		int decision_angulo(agente a) {
			
			double x_0, y_0, x_f,x_f2, y_f, y_f2, dx, dy;
			x_0 = a.getx_act();
			y_0 = a.gety_act();
			x_f = sim_focus.getx();
			y_f = sim_focus.gety();
			dx = x_f - x_0;
			dy = y_f - y_0;
			
			
			double theta_original = Math.Atan2(dy, dx);
			double theta_vet, theta_ant = 1000, theta_nuevo;
			int hijo=0;;
			
			Circulo aux = new Circulo();
			Circulo temp = new Circulo();
			aux = a.getv_act();
			
			for (int i =0; i < aux.max_hijos(); i++) {
				temp = lista[aux.get_hijos(i)];
				
				x_f = temp.getx();
				y_f = temp.gety();
				dx = x_f - x_0;
				dy = y_f - y_0;
				
				theta_vet = Math.Atan2(dy, dx);
				
				if (theta_original > theta_vet) {
					theta_nuevo = theta_original - theta_vet;
				}
				else {
					theta_nuevo = theta_vet - theta_original;
				}
				
				if (theta_nuevo < theta_ant) {
					theta_ant = theta_nuevo;
					hijo = i;
				}
				
			}
			
			return hijo;
		}
		
		int buscar(Circulo busc, List<List <Circulo>> overlista) {
			
			int retorname_esta = -1;
			for (int i =0; i < overlista.Count; i++) {
				for (int j=0; j < overlista[i].Count; j++) {
					if (busc.get_id() == overlista[i][j].get_id()) {
						retorname_esta = i;
						return retorname_esta;
					}
				}
			}
			
			return retorname_esta;
		}
		
		List<List <Circulo>> lista_kruskal = new List<List<Circulo>>();
		
		void kruskal () {
			
			List<List <Circulo>> overlista = new List<List<Circulo>>();
			/*int i_overrise=0;
			
			for (int i=0; i < lista.Count; i++) {
				List<Circulo> sub_overrise = new List<Circulo>();
				sub_overrise.Add(lista[i_overrise]);
				i_overrise++;
				overlista.Add(sub_overrise);
			}*/
			
			List <distance> dista = new List<distance>();
			
			for (int i=0; i < lista.Count; i++) {
				for (int j =0; j < lista[i].max_hijos(); j++) {
					dista.Add(lista[i].getElizabehtAT(j));
				}
			}
			
			for (int i=0; i < dista.Count; i++) {
				for (int j = 0; j < dista.Count; j++) {
					if (dista[i].get_Cori().get_id() == dista[j].getV_d().get_id()) {
						if (dista[j].get_Cori().get_id() == dista[i].getV_d().get_id()) {
							dista.RemoveAt(j);
							break;
						}
					}
				}
			}
			
			distance aux;
			for (int i=0; i < dista.Count; i++) {
				for (int j=0; j < dista.Count -1; j++) {
					
					if (dista[j].getdist() > dista[j+1].getdist()) {
						aux=dista[j];
		                dista[j]=dista[j+1];
		                dista[j+1]=aux;
					}
				}
			}
			
			int decisi = 0;
			int aux_J = 0, aux_j2 =0;
			for (int i=0; i < dista.Count; i++) {
				
				bool enc_ori = false;
				for (int j = 0; j < overlista.Count; j++) {
					for (int h =0; h < overlista[j].Count; h++) {
						if (dista[i].get_Cori().get_id() == overlista[j][h].get_id()) {
							decisi++; 
							aux_J = j;//1
							enc_ori = true;
						}
							
						if (dista[i].getV_d().get_id() == overlista[j][h].get_id()) {
							decisi++; //2
							aux_j2 = j;
						}
							
					}
				}
				
				if (decisi == 0) {
					List<Circulo> sub_overrise = new List<Circulo>();
					sub_overrise.Add(dista[i].get_Cori() );
					sub_overrise.Add(dista[i].getV_d() );
					overlista.Add(sub_overrise);
					
					Graphics gra = Graphics.FromImage(grafodraw);
					Pen pe = new Pen(Color.Red, 3);
					gra.DrawLine(pe, dista[i].get_Cori().getx(), dista[i].get_Cori().gety(),
					             dista[i].getV_d().getx(), dista[i].getV_d().gety());
					
				}
				if (decisi == 1) {
					if (enc_ori == true) {
						overlista[aux_J].Add(dista[i].getV_d() );
					}
					else {
						overlista[aux_j2].Add(dista[i].get_Cori() );
					}
					
					Graphics gra = Graphics.FromImage(grafodraw);
					Pen pe = new Pen(Color.Red, 3);
					gra.DrawLine(pe, dista[i].get_Cori().getx(), dista[i].get_Cori().gety(),
					             dista[i].getV_d().getx(), dista[i].getV_d().gety());
				}
				
				if (decisi == 2) {
					int xD = buscar(dista[i].get_Cori(), overlista);
					int xd = buscar(dista[i].getV_d(), overlista);
					
					if (xD != xd && xD != -1 && xd != -1) {
						if (overlista[xD].Count > overlista[xd].Count) {
							
							for (int k=0; k < overlista[xd].Count; k++) {
								overlista[xD].Add(overlista[xd][k]);
							}
							
							overlista.RemoveAt(xd);
						}
						else {
							
							for (int k=0; k < overlista[xD].Count; k++) {
								overlista[xd].Add(overlista[xD][k]);
							}
							
							overlista.RemoveAt(xD);
						}
						
						Graphics gra = Graphics.FromImage(grafodraw);
						Pen pe = new Pen(Color.Red, 3);
						gra.DrawLine(pe, dista[i].get_Cori().getx(), dista[i].get_Cori().gety(),
						             dista[i].getV_d().getx(), dista[i].getV_d().gety());
					}
				}
				
				decisi = 0;
			}
			
			mostrar_imagen.Image = grafodraw;
			mostrar_imagen.Refresh();
			int cant_g = overlista.Count;
			G_kruskal.Text= cant_g.ToString();
			lista_kruskal = overlista;
			
		}
		
		
		
		void prin(Circulo v_i) {
			
			List<Circulo> pasos = new List<Circulo>();
			int can_g=1;
			Circulo rempla = new Circulo();
			v_i.set_prim();
			rempla = v_i;
			pasos.Add(rempla);
			
			while (lista.Count != pasos.Count) {
				List<distance> suns = new List<distance>();
				
				for (int j = 0; j < pasos.Count; j++) {
					for (int i=0; i < pasos[j].max_hijos(); i++) {
						suns.Add(pasos[j].getElizabehtAT(i));
					}
				}
				
				
				distance aux;
				for (int i=0; i < suns.Count; i++) {
					for (int j=0; j < suns.Count -1; j++) {
						
						if (suns[j].getdist() > suns[j+1].getdist()) {
							aux=suns[j];
			                suns[j]=suns[j+1];
			                suns[j+1]=aux;
						}
					}
				}
				
				int pos_sun = 0;
				for (int i = 0; i < pasos.Count; i++) {
					if (pasos[i].get_id() == suns[pos_sun].getV_d().get_id()) {
						i =-1;
						pos_sun++;
						if (pos_sun == suns.Count) {
							pos_sun = -1;
							break;
						}
					}
				}
				
				if (pos_sun != -1) {
					suns[pos_sun].getV_d().set_prim();
					Circulo esta = new Circulo();
					esta = suns[pos_sun].getV_d();
					pasos.Add(esta);
					
					
					Graphics gra = Graphics.FromImage(grafodraw);
					Pen pe = new Pen(Color.GreenYellow, 5);
					gra.DrawLine(pe, suns[pos_sun].get_Cori().getx(), suns[pos_sun].get_Cori().gety(),
						             suns[pos_sun].getV_d().getx(),  suns[pos_sun].getV_d().gety() );
				}
				else {
					if (lista.Count != pasos.Count) {
						int aux_lista =0;
						
						for (int i = 0; i < pasos.Count; i++) {
							if (lista[aux_lista].get_id() == pasos[i].get_id()) {
								i =-1;
								aux_lista++;
								if (aux_lista == lista.Count) {
									aux_lista = -1;
									break;
								}
							}
						}
						
						if (aux_lista != -1) {
							pasos.Add(lista[aux_lista]);
							can_g++;
						}
					}
				}
			}
			mostrar_imagen.Image = grafodraw;
			mostrar_imagen.Refresh();
			
			
			/*for (int i = 0; i < pasos.Count; i++) {
				
				for (int j = 0; j < pasos[i].max_elizabeth(); j++) {
					
					int mitad = pasos[i].getElizabehtAT(j).tamañoLinea() / 3;
					Color cas = grafodraw.GetPixel(pasos[i].getElizabehtAT(j).get_pixforpix(mitad).X, pasos[i].getElizabehtAT(j).get_pixforpix(mitad).Y);
					
					if (cas == Color.FromArgb(255,255,255)) {
						pasos[i].removedista(j);
						j = -1;
						
					}
					
				}
			}*/
			
			G_prin.Text = can_g.ToString();
			lista_prim = pasos;
		}
		
		
		
		void dijkstra (Circulo v_i) {
			v_i.set_pesoacum(0);
			
			List<Circulo> definitivo = new List<Circulo>();
			Circulo opcion = new Circulo();
			
			opcion = v_i;
			definitivo.Add(opcion);
			
			while (true) {
				List<distance> aris = new List<distance>();
				
				for (int i = 0; i < definitivo.Count; i++) {
					for (int j = 0; j < definitivo[i].max_hijos(); j++) {
						aris.Add(definitivo[i].getElizabehtAT(j));
					}
				}
				
				for (int i = 0; i < aris.Count; i++) {
					bool search = false;
					
					for (int j = 0; j < definitivo.Count; j++) {
						if (definitivo[j].get_id() == aris[i].getV_d().get_id()) {
							search = true;
							break;
						}
					}
					
					if (search == false) {
						double sum = aris[i].get_Cori().get_pesoacum() + aris[i].getdist();
						if (sum < aris[i].getV_d().get_pesoacum()) {
							aris[i].getV_d().set_pesoacum((int)sum);
							aris[i].getV_d().set_origen(aris[i].get_Cori().get_id());
						}
						
					}
				}
				
				distance aux;
				for (int i=0; i < aris.Count; i++) {
					for (int j=0; j < aris.Count -1; j++) {
						
						if (aris[j].getV_d().get_pesoacum() > aris[j+1].getV_d().get_pesoacum()) {
							aux=aris[j];
			                aris[j]=aris[j+1];
			                aris[j+1]=aux;
						}
					}
				}
				
				int pos = 0;
				for (int i = 0; i < definitivo.Count; i++) {
					if (aris[pos].getV_d().get_id() == definitivo[i].get_id()) {
						pos++;
						i = -1;
						if (pos == aris.Count) {
							pos = -1;
							break;
						}
					}
				}
				
				if (pos != -1) {
					definitivo.Add(aris[pos].getV_d());
				}
				else {
					break;
				}
				
				
			}
			
			
			/*while (true) {
				List<distance> aris = new List<distance>();
				
				
				for (int i =0; i < opcion.max_hijos(); i++) {
					aris.Add(opcion.getElizabehtAT(i));
				}
				
				
				
				List<int> repe = new List<int>();
				
				for (int i = 0; i < aris.Count; i++) {
					bool search = false;
					
					for (int j = 0; j < definitivo.Count; j++) {
						if (definitivo[j].get_id() == aris[i].getV_d().get_id()) {
							search = true;
							repe.Add(definitivo[j].get_id());
							break;
						}
					}
					
					if (search == false) {
						double sum = aris[i].get_Cori().get_pesoacum() + aris[i].getdist();
						if (sum < aris[i].getV_d().get_pesoacum()) {
							aris[i].getV_d().set_pesoacum((int)sum);
							aris[i].getV_d().set_origen(aris[i].get_Cori().get_id());
						}
						
					}
				}
				
				distance aux;
				for (int i=0; i < aris.Count; i++) {
					for (int j=0; j < aris.Count -1; j++) {
						
						if (aris[j].getV_d().get_pesoacum() > aris[j+1].getV_d().get_pesoacum()) {
							aux=aris[j];
			                aris[j]=aris[j+1];
			                aris[j+1]=aux;
						}
					}
				}
				
				int apo =0;
				for (int i =0; i < repe.Count; i++) {
					if (repe[i] == aris[apo].getV_d().get_id()) {
						i = -1;
						apo++;
						if (apo == aris.Count) {
							apo = -1;
							break;
						}
					}
				}
				
				if (apo != -1) {
					definitivo.Add(aris[apo].get_Cori());
					opcion = aris[apo].getV_d();
				}
				else {
					break;
				}
				
			}*/
		}
		
		void todos_a_uno(Circulo v_i) {
			
			int arial_12 = 5;
			
					
					
			
			for (int i = 0; i < lista.Count; i++) {
				Circulo opc = new Circulo();
				opc = lista[i];
				while (v_i.get_id() != opc.get_id()) {
					if (opc.get_pesoacum() == suma_total) {
						break;
					}
					for (int j = 0; j < opc.max_hijos(); j++) {
						if (opc.get_origen() == opc.getElizabehtAT(j).getV_d().get_id() ) {
							
							Graphics gra = Graphics.FromImage(animadraw);
							Pen pe = new Pen(Color.GreenYellow, arial_12);
							
							gra.DrawLine(pe, opc.getx(), opc.gety(),
						             opc.getElizabehtAT(j).getV_d().getx(),  opc.getElizabehtAT(j).getV_d().gety() );
							
							opc = opc.getElizabehtAT(j).getV_d();
							break;
						}
					}
				}
				//arial_12--;
			}
		}
		
		bool uno_a_uno(Circulo v_d) {
			int arial_12 = 5;
			
			
			Circulo opc = new Circulo();
			opc = v_d;
			
			while (lista_agente[0].getv_act().get_id() != opc.get_id()) {
				if (opc.get_pesoacum() == suma_total) {
						DialogResult result = MessageBox.Show("Imposible llegar",
								    "Detalle importante",
								    MessageBoxButtons.OK);
						return false;
				}
				for (int j = 0; j < opc.max_hijos(); j++) {
					if (opc.get_origen() == opc.getElizabehtAT(j).getV_d().get_id() ) {
						
						Graphics gra = Graphics.FromImage(animadraw);
						Pen pe = new Pen(Color.Violet, arial_12);
						
						gra.DrawLine(pe, opc.getx(), opc.gety(),
					             opc.getElizabehtAT(j).getV_d().getx(),  opc.getElizabehtAT(j).getV_d().gety() );
						
						opc = opc.getElizabehtAT(j).getV_d();
						break;
					}
				}
			}
			return true;
		}
		
		int modo_dfs(agente a) {
			int aux_hijos = 0;
			
			
			for (int i =0; i < a.get_visis().Count; i++) {
				if (a.get_visis()[i].get_id() == a.getv_act().getElizabehtAT(aux_hijos).getV_d().get_id()) {
					i =-1;
					aux_hijos++;
					if (aux_hijos >= a.getv_act().max_hijos()) {
						aux_hijos = -1;
						break;
					}
				}
				
				int mitad = a.getv_act().getElizabehtAT(aux_hijos).tamañoLinea() / 3;
				Color cas = grafodraw.GetPixel(a.getv_act().getElizabehtAT(aux_hijos).get_pixforpix(mitad).X, a.getv_act().getElizabehtAT(aux_hijos).get_pixforpix(mitad).Y);
				if (cas == Color.FromArgb(255,255,255)) {
					aux_hijos++;
					i = -1;
					if (aux_hijos >= a.getv_act().max_hijos()) {
						aux_hijos = -1;
						break;
					}
				}
			}
			
			if (aux_hijos != -1) {
				a.get_visis().Add(a.getv_act().getElizabehtAT(aux_hijos).getV_d());
				return aux_hijos;
			}
			else {
				int atras = 0;
				for (int i =0; i < a.get_visis().Count; i++) {
					if (a.getv_act().get_id() == a.get_visis()[i].get_id()) {
						atras = i-1;
						break;
					}
				}
				
				if (atras == -1) {
					return -1;
				}
				
				for (int i =0; i < a.getv_act().max_hijos(); i++) {
					if (a.getv_act().getElizabehtAT(i).getV_d().get_id() == a.get_visis()[atras].get_id()) {
						a.get_visis().Add(a.get_visis()[atras]);
						return i;
					}
				}
			}
			
			return -1;
		}
		
		int modo_dijkstra(agente a) {
			int camino =-1;
			
			Circulo opc = new Circulo();
			opc = sim_focus;
			
			List<Circulo> pasos = new List<Circulo>();
			
			
			while (a.getv_act().get_id() != opc.get_id()) {
				pasos.Add(opc);
				
				if (opc.get_pesoacum() == suma_total) {
						DialogResult result = MessageBox.Show("Imposible llegar",
								    "Detalle importante",
								    MessageBoxButtons.OK);
						break;
				}
				for (int j = 0; j < opc.max_hijos(); j++) {
					if (opc.get_origen() == opc.getElizabehtAT(j).getV_d().get_id() ) {
						opc = opc.getElizabehtAT(j).getV_d();
						camino = j;
						break;
					}
				}
			}
			
			int auxi = pasos.Count -1;
			if (auxi > -1) {
				for(int i = 0; i < a.getv_act().max_hijos(); i++) {
					if (a.getv_act().getElizabehtAT(i).getV_d().get_id() == pasos[auxi].get_id()) {
						return i;
					}
				}
			}
			
			
			return -1;
		}
		
		int hacer_desiciones(agente a) {
			Circulo aux = a.getv_act();
			List<int> beta = new List<int>();
			bool gorda=false;
			
			for (int i =0; i < aux.max_hijos(); i++) {	
				beta.Add(aux.getElizabehtAT(i).get_peso());
			}
			
			for (int i =0; i < beta.Count; i++) {
				for (int e=0; e < beta.Count; e++) {
					if (beta[i] != beta[e]) {
						gorda = true;
					}
				}
			}
			
			if (gorda == false) {
				return decision_angulo(a);
			}
			else {
				
				int inferior = beta[0];
				int pos=0;
				for (int i=0; i < beta.Count; i++) {
					if (beta[i] < inferior) {
						inferior = beta[i];
						pos = i;
					}
				}
				return pos;
				
			}
			
		}
		
		void dibujar_direcction(agente a, Color c) {
			Graphics g  = Graphics.FromImage(animadraw);
			double x_0, y_0, x_f,x_f2, y_f, y_f2, dx, dy;
			x_0 = a.getx_act();
			y_0 = a.gety_act();
			x_f = sim_focus.getx();
			y_f = sim_focus.gety();
			dx = x_f - x_0;
			dy = y_f - y_0;
			
			
			double theta = Math.Atan2(dy, dx);
			
			x_f2 = Math.Cos(theta) * 40 + x_0;
			y_f2 = Math.Sin(theta) * 40 + y_0;
			
			Pen p1 = new Pen(c, 4);
			g.DrawLine(p1, (int)x_0, (int)y_0, (int)x_f2, (int)y_f2);
		}
		
		bool lineaDDA(int circulA, int circulB, int x1, int y1, int x2, int y2, Bitmap pixi) {
		
            int dx, dy, pasos, k, flag = 0;
            float x_inc, y_inc, x, y;
            temp_pix = new List<Point>();
            
            dx = x2 - x1;
            dy = y2 - y1;
            if (Math.Abs(dx) > Math.Abs(dy))
                pasos = Math.Abs(dx);
            else
                pasos = Math.Abs(dy);
            x_inc = (float)dx / pasos;
            y_inc = (float)dy / pasos;
            x = x1;
            y = y1;
            
            //pixi.SetPixel((int)x, (int)y, Color.Red);
            for (k = 1; k < pasos + 1; k++)
            {
                x = x + x_inc;
                y = y + y_inc;
                
                
                if (circulA != circulB) {
                	Point p = new Point((int)x, (int)y);
                	temp_pix.Add(p);
                }
                
                if (pixi.GetPixel((int)x, (int)y) == Color.FromArgb(255, 255, 255) && flag == 0) {
                	flag = 1;
                }
                if (pixi.GetPixel((int)x, (int)y) != Color.FromArgb(255, 255, 255) && flag == 1) {
                	flag = 2;
                }
                if (pixi.GetPixel((int)x, (int)y) == Color.FromArgb(255, 255, 255) && flag == 2) {
                	return false;
                }
                
                /*if (pixi.GetPixel((int)x, (int)y) != Color.FromArgb(255, 255, 255)) {
                	if (pixi.GetPixel((int)x, (int)y) != Color.FromArgb(0, 255, 255)) {
                		return false;
                	}
                }*/
                //pixi.SetPixel((int)x, (int)y, color);
            }
            
            return true;
	            
	   }
		void hacerlineas (int punto_A, int punto_B, Bitmap pixi) {
			Graphics gra = Graphics.FromImage(pixi);
			Pen pe = new Pen(Color.Transparent, 3);
			//if (lista[punto_A].getcos(punto_B) == true) {
			gra.DrawLine(pe, lista[punto_A].getx(), lista[punto_A].gety(), lista[punto_B].getx(), lista[punto_B].gety());
			
			
		}
		void AgenteClick(object sender, EventArgs e)
		{
			/*if (agenteflag == true && señalflag == true) {
				
			}*/
			//agente a = new agente(lista[0], 0);
			animationBegins();
				
		}
		
		void limpiarbitmap() {
			Graphics g  = Graphics.FromImage(animadraw);
			g.Clear(Color.Transparent);
		}
		
		void limpiarbitmap_grafo() {
			Graphics g  = Graphics.FromImage(grafodraw);
			g.Clear(Color.Transparent);
		}
		
		
		
		void animationBegins() {
			bool completeflag  =true;
			int mejor;
			
			do {
				
				limpiarbitmap();
				completeflag = true;
				
				foreach(agente a in lista_agente) {
					
				
					if (a.getv_act().get_meta() == true) {
						completeflag = false;
						a.getv_act().set_meta(false);
						
						/*a.get_visis().Clear();
						a.get_visis().Add(a.getv_act());*/
						
						empezar_dijkstra();
						dijkstra(a.getv_act());
						//limpiarbitmap_grafo();
						
						
						grafodraw = provisional;
						
						//todos_a_uno(a.getv_act());
						sim_origin = a.getv_act();
						
						
						
						/*mejor = modo_dijkstra(a);
						a.mejor_desicion(mejor);*/
					}
					distance e_act = a.get_linea_act();
					
					
						
						//int x_des = e_act.getV_d().getCircle().getx();	
						//int y_des = e_act.getV_d().getCircle().gety();
						if (a.getlineindex() < e_act.tamañoLinea()-1) {
						
								a.walk();
								
								if (uno_a_uno(sim_focus)) {
									dibujaragente(a, Color.Green);
									dibujar_direcction(a, Color.Green);
								}
								
								
							}
							else {
								e_act.aum_peso();
								a.setv_act(e_act.getV_d());	
								mejor = modo_dijkstra(a);
								if (mejor != -1) {
									/*a.setv_act(a.get_visis()[0]);
									a.get_visis().Clear();
									a.get_visis().Add(a.getv_act());
									mejor = modo_dfs(a);
									a.mejor_desicion(mejor);
									DialogResult result = MessageBox.Show("Imposible llegar",
								    "Detalle importante",
								    MessageBoxButtons.OK);
									return;*/
									
									a.mejor_desicion(mejor);
									a.setline_index(0);
									a.incres_verticesvisitados();
								}
								
							}
						} 
						mostrar_imagen.Image = animadraw;
						mostrar_imagen.Refresh();
					
					
			} while (completeflag == true);
			
			
		}
		
		void TreeView1MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right) {
				TreeNode node_here = treeView1.GetNodeAt(e.X, e.Y);
				treeView1.SelectedNode = node_here;
				
				if (node_here == null) return;
				
				contextMenuStrip1.Show(this.PointToScreen(e.Location).X + treeView1.Location.X, this.PointToScreen(e.Location).Y + treeView1.Location.Y);
			}
			
	
		}
		void ContextMenuStrip1Click(object sender, EventArgs e)
		{
			
			/*ToolStripMenuItem selected = sender as ToolStripMenuItem;
			if (selected.Text == "agente") {
				AgenteToolStripMenuItemClick(sender, e); //devuelve un circulo
			}
			else if (selected.Text == "cebo") {
				SeñueloToolStripMenuItemClick(sender, e); //devuelve un circulo
			}*/
	
		}
		void AgenteToolStripMenuItemClick(object sender, EventArgs e)
		{
			/*DialogResult result = MessageBox.Show("Pizza con piña o si piña?",
		    "Cuestion importante",
		    MessageBoxButtons.YesNo);
			if (result == DialogResult.Yes) {
				MessageBox.Show("Vergato");
			}*/
			
			int mejor=0;
			//MenuItem selected = sender as MenuItem;
			/*int id = 0;
			for (int i = 0; i < lista_prim.Count; i++) {
				if (lista_prim[i].get_id() == treeView1.SelectedNode.Index) {
					id = i;
					break;
				}
			}*/
			dijkstra(lista[treeView1.SelectedNode.Index]);
			
			
			agente nws = new act2_V002.agente(lista[treeView1.SelectedNode.Index], lista_agente.Count);
			nws.get_visis().Add(lista[treeView1.SelectedNode.Index]);
			sim_origin = lista[treeView1.SelectedNode.Index];
			/*mejor = modo_dijkstra(nws);
			nws.mejor_desicion(mejor);*/
			
			lista_agente.Add(nws);
			if (treeView1.SelectedNode.Index != -1) 
				agenteflag = true;
			
			
			
			
			
			dibujaragente(nws, Color.Green);
			
			mostrar_imagen.Image = animadraw;
			mostrar_imagen.Refresh();
			
			
		}

		void SeñueloToolStripMenuItemClick(object sender, EventArgs e)
		{
			int x, y, ra, mejor = 0;
			Graphics g  = Graphics.FromImage(grafodraw);
			Brush b = new SolidBrush(Color.RoyalBlue);
			if (sim_focus != null) {
				 limpiarbitmap();
				 x = sim_focus.getx();
				 y = sim_focus.gety();
				 ra = sim_focus.get_ra();
				 b = new SolidBrush(Color.Black);
				g.FillEllipse(b, x-ra, y-ra, (ra+ra), (ra+ra) );
				//todos_a_uno(lista_agente[0].getv_act());
			}
			
			
			lista[treeView1.SelectedNode.Index].set_meta(true);
			if (uno_a_uno(lista[treeView1.SelectedNode.Index])) {
				sim_focus = lista[treeView1.SelectedNode.Index];
				if (treeView1.SelectedNode.Index != -1) 
					señalflag = true;
				
				
				 x = sim_focus.getx();
				 y = sim_focus.gety();
				 ra = sim_focus.get_ra();
				 b = new SolidBrush(Color.RoyalBlue);
				g.FillEllipse(b, x-ra, y-ra, (ra+ra), (ra+ra) );
				
				
				//dibujarCirculo(sim_focus.getx(), sim_focus.gety(), grafodraw, Color.RoyalBlue);
				
				mejor = modo_dijkstra(lista_agente[0]);
				lista_agente[0].mejor_desicion(mejor);
				lista_agente[0].setline_index(0);
				
				mostrar_imagen.Image = animadraw;
				mostrar_imagen.Refresh();
				}
			
		}
		
		
		
		void PrinToolStripMenuItemClick(object sender, EventArgs e)
		{
			Stopwatch ws = new Stopwatch();
			ws.Start();
			prin(lista[treeView1.SelectedNode.Index]);
			ws.Stop();
			T_prin.Text = ws.Elapsed.ToString();
			
			Stopwatch sw = new Stopwatch();
			sw.Start();
			kruskal();
			sw.Stop();
			T_kruskal.Text = sw.Elapsed.ToString();
			
		}

		void Mostrar_imagenMouseClick(object sender, MouseEventArgs e)
		{
			double wp,wb, hp, hb, kw, kh,k, incx, incy;
			wp = mostrar_imagen.Width;
			wb = animadraw.Width;
			hp = mostrar_imagen.Height;
			hb = animadraw.Height;
			kw = wp/wb;
			kh = hp/hb;
			incx = 0;
			incy = 0;
			
			if (kw < kh) {
				k = kw;
				incy = (hp - hb * k)/2;
			}
			else {
				k = kh;
				incx = (wp - wb * k)/2;
			}
			
			if (e.Button == MouseButtons.Left) {
				int pos = 0;
				if (pos  != 1) {
					agente a = new agente(lista[pos], pos);
					lista_agente.Add(a);
				}
				
			}
			else {
				int pos = 0;
				if (pos  != 1) {
					agente a = new agente(lista[pos], pos);
					lista_agente.Add(a);
				}
			}
			
			/*if (contClick == 0) {
				contClick++;
				p0.X = (e.X - incx)/k;
				p0.Y = (e.Y - incy)/k;
			}
			else {
				contClick = 0;
				pf.X = (e.X - incx)/k;
				pf.Y = (e.Y - incy)/k;
				
				mostrar_imagen.Image = animadraw;
			}*/
		
		}

		
	}
}
