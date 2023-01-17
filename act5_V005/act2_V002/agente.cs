/*
 * Created by SharpDevelop.
 * User: Usuario
 * Date: 27/09/2019
 * Time: 12:34 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace act2_V002
{
	/// <summary>
	/// Description of agente.
	/// </summary>
	public class agente
	{
		int x_act;
		int y_act;
		int id;
		int velocidad;
		int ra = 5;
		int verticesvisitados;
		Circulo v_act;
		distance linea_act;
		List<Circulo> visitados;
		int line_index = 0; // indice que recorre el arreglo de la lista de point
		public agente(Circulo v, int id)
		{
			x_act = v.getx();
			y_act = v.gety();
			this.id = id;
			velocidad = 5;
			v_act = v;
			visitados = new List<Circulo>();
			random();
		}
		
		public List<Circulo> get_visis() {
			return visitados;
		}
		
		public int getx_act() {
			return x_act;
		}
		
		public int gety_act() {
			return y_act;
		}
		
		public int getid() {
			return id;
		}
		
		public int getvelocidad() {
			return velocidad;
		}
		
		public int getR() {
			return ra;
		}
		
		public void sumvel() {
			velocidad *= 2;
		}
		
		public Circulo getv_act() {
			return v_act;
		}
		
		public void setv_act(Circulo d) {
			v_act = d;
		}
		
		public distance get_linea_act() {
			return linea_act;
		}
		
		public int getlineindex() {
			return line_index;
		}
		
		public void setline_index(int a) {
			line_index=a;
		}
		
		public void walk() {
			line_index += velocidad;
			if (line_index >= linea_act.tamañoLinea())
				line_index = linea_act.tamañoLinea()-1;
			
			x_act = linea_act.get_pixforpix(line_index).X;
			y_act = linea_act.get_pixforpix(line_index).Y;
		}
		
		public void random() {
			Random r = new Random();
			linea_act = v_act.getElizabehtAT(r.Next(0, v_act.getConexiones()));
		}
		
		public void mejor_desicion(int i) {
			linea_act = v_act.getElizabehtAT(i);
		}
		
		public int get_verticesvisitados() {
			return verticesvisitados;
		}
		
		public void incres_verticesvisitados() {
			verticesvisitados++;
		}
		
	}
}
