/*
 * Created by SharpDevelop.
 * User: Roman
 * Date: 14/09/2019
 * Time: 06:36 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace act2_V002
{
	/// <summary>
	/// Description of Circulo.
	/// </summary>
	
	public class distance {
		
		List<Point> pixforpix = new List<Point>();
		Circulo C_sig = new Circulo();
		Circulo C_ori = new Circulo();
		double distancia;
		bool conexion;
		int peso;
		
		public distance(double i, bool c, Circulo d, Circulo e) {
			distancia = i;
			conexion = c;
			if (c == true) {
				C_sig = d;
				C_ori = e;
			}
		}
		
		public double getdist () {
			return distancia;
		}
		
		public bool getanexo() {
			return conexion;
		}
		
		public Circulo getV_d() {
			return C_sig;
		}
		
		public void add_pixforpix (List<Point> p) {
			pixforpix = p;
			
		}
		
		public Point get_pixforpix (int a) {
			return pixforpix[a];
		}
		
		public int tamañoLinea() {
			return pixforpix.Count;
		}
		
		public void aum_peso() {
			peso++;
		}
		
		public int get_peso() {
			return peso;
		}
		
		public Circulo get_Cori() {
			return C_ori;
		}
		
		
		
	}
	
	public class Circulo
	{
		List<distance> elizabeth = new List<distance>();
		List<int> hijos = new List<int>();
		
		int r;
		int id;
		int centroy;
		int centrox;
		int posicion_pixel;
		int conexiones=0;
		bool hay_agente = false;
		bool meta = false;
		int peso_acumlado;
		int origen;
		
		bool prim;
		bool kruskal;
		public Circulo()
		{
		
		}
		
		public void alta (int ra, int centroiy,int centroix) {
			
			r = ra;
			centroy = centroiy;
			centrox = centroix;
			posicion_pixel = centroiy + centroix;
			origen = -1;
			prim = false;
			kruskal = false;
			
		}
		
		public override string ToString()
		{
			return string.Format("[Circulo R={0}, Centrox={1}, Centroy={2}]", r, centrox, centroy);
		}
		
		public void sethay_agente(bool a){
			hay_agente = a;
		}
		
		public bool gethay_agente() {
			return hay_agente;
		}
		
		public int getx () {
			return centrox;
		}
		
		public int gety () {
			return centroy;
		}
		
		public int get_ra() {
			return r;
		}
		
		public void to_distan(double i, bool c, Circulo d, Circulo e) {
			if (c == true) conexiones++;
			elizabeth.Add(new distance(i, c, d, e));
		}
		
		public double getdistan(int i) {
			return elizabeth[i].getdist();
		}
		
		public bool getcos(int i) {
			return elizabeth[i].getanexo();
		}
		
		public void add_point (int i, List<Point> p, int j) {
			elizabeth[i].add_pixforpix(p);
			hijos.Add(j);
		}
		
		public int get_hijos (int i) {
			return hijos[i];
		}
		
		public int max_hijos() {
			return hijos.Count;
		}
		
		public Point get_point (int i, int indice) {
			return elizabeth[i].get_pixforpix(indice);
		}
		
		public int tamLinea(int i) {
			return elizabeth[i].tamañoLinea();
		}
		
		public int getConexiones() {
			return conexiones;
		}
		
		public distance getElizabehtAT(int i) {
			return elizabeth[i];
		}
		
		public void set_meta(bool i) {
			meta = i;
		}
		
		public bool get_meta() {
			return meta;
		}
		
		public int get_id() {
			return id;
		}
		
		public void set_id(int i) {
			id =i;
		}
		
		public int get_pesoacum() {
			return peso_acumlado;
		}
		
		public void set_pesoacum(int i) {
			peso_acumlado = i;
		}
		
		public int get_origen() {
			return origen;
		}
		
		public void set_origen(int i) {
			origen = i;
		}
		
		public void set_prim() {
			prim = true;
		}
		
		public bool get_prim() {
			return prim;
		}
		
		public void removedista(int i) {
			elizabeth.RemoveAt(i);
		}
		
		public int max_elizabeth() {
			return elizabeth.Count;
		}
		
	}
}
