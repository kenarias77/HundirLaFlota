using System;
using System.Collections.Generic;

//clase padre de jugador 
public class Jugador{

    //array de barcos 
    protected Barco[] barcos=new Barco[10];
    //lista de las coordenadas donde se guardan las posiciones en las que se ataca
    protected List<string> coordenadaAtaques=new List<string>();

    //booleano que indica si el jugador gana la partida
    protected bool ganar=true;

    //constructor de jugador
    public Jugador()
    {
        barcos[0]=new Barco(1,"fragata1");
        barcos[1]=new Barco(1,"fragata2");
        barcos[2]=new Barco(1,"fragata3");
        barcos[3]=new Barco(1,"fragata4");
        barcos[4]=new Barco(2,"destructor1");
        barcos[5]=new Barco(2,"destructor2");
        barcos[6]=new Barco(2,"destructor3");
        barcos[7]=new Barco(3,"acorazado1");
        barcos[8]=new Barco(3,"acorazado2");
        barcos[9]=new Barco(3,"acorazado3");
    }

    //metodo que pasa un string con la coordenada a atacar a la lista de coordenadas y las organiza
    public bool Atacado(string coordenada)
    {
        foreach(Barco b in barcos)
        {
            if(b.getPosicion().Contains(coordenada))
            {
                b.RestaVida();
                if(b.getVida()==0)
                {
                    Console.WriteLine("Hundido!");
                    return true;
                }
                else{
                    Console.WriteLine("Tocado!");
                    return true;
                }
            }
        }
        return false;
    }
    

    //metodo para poner los barcos al empezar la partida
    public void setCoordenada(string coordenada,int posicion)
    {
        barcos[posicion].setPosicion(coordenada);
    }
    //get para coger la lista de barcos
    public Barco[] getBarcos()
    {
        return barcos;
    }
    //set para guardar los ataques
    public void setAtaque(string ataque)
    {
        coordenadaAtaques.Add(ataque);
    }
    //get para obtener la lista de ataques realizados del jugador
    public List<string> getAtaque()
    {
        return coordenadaAtaques;
    }
    //funcion para saber si el jugador ha ganado
    public bool comprobarGanar()
    {
        foreach(Barco b in barcos)
        {
            if(b.getVida() > 0)
            {
                return true;
            }
        }
        ganar=false;
        return ganar;
    }

    //get para el booleano ganar
    public bool getGanar()
    {
        return ganar;
    }
}