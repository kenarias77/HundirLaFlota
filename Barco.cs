using System;
using System.Collections.Generic;

//clase de los barcos, donde se le asigna la vida y nombre
public class Barco{
    private int vida;
    private string nombre;

    //lista de la posicion que ocupa el barco
    private List<string> posicionBarco=new List<string>();

    //constructor de barcos
    public Barco(int vida,string nombre)
    {
        this.vida=vida;
        this.nombre=nombre;
    }

    //get para ver la vida restante del barco
    public int getVida()
    {
        return vida;
    }

    public void RestaVida()
    {
        vida-=1;
    }

    //get para ver el nombre del barco
    public string getNombre()
    {
        return nombre;
    }

    //get para ver la posicion del barco
    public List<string> getPosicion()
    {
        return posicionBarco;
    }

    //set para colocar al barco
    public void setPosicion(string coordenadaBarco)
    {
        //variable donde guardar el caracter que necesitemos a ascii
        int ascii;
        //switch que selecciona el 3 caracter del string 
        switch(coordenadaBarco[2])
        {
            //seleccionamos la primera letra de la coordenada y la convertimos a ascii restandole 1 por cada vida
            //que tenga el barco ya que la coordenada va hacia la izquierda
            case 'A':
            ascii=(int)coordenadaBarco[0];
                for(int i=0;i<vida;i++)
                {
                    posicionBarco.Add(""+(char)+coordenadaBarco[1]);
                    ascii--;
                }
            break;
            //seleccionamos la segunda letra de la coordenada y la convertimos a ascii sumandole 1 por cada vida
            //que tenga el barco ya que la coordenada va hacia abajo
            case 'S':
            ascii=(int)coordenadaBarco[1];
                for(int i=0;i<vida;i++)
                {
                    posicionBarco.Add(""+coordenadaBarco[0]+(char)ascii);
                    ascii++;
                }
            break;
            //seleccionamos la primera letra de la coordenada y la convertimos a ascii sumandole 1 por cada vida
            //que tenga el barco ya que la coordenada va hacia la derecha
            case 'D':
            ascii=(int)coordenadaBarco[0];
                for(int i=0;i<vida;i++)
                {
                    posicionBarco.Add(""+(char)ascii+coordenadaBarco[1]);
                    ascii++;
                }
            break;
            //seleccionamos la segunda letra de la coordenada y la convertimos a ascii restandole 1 por cada vida
            //que tenga el barco ya que la coordenada va hacia arriba
            case 'W':
            ascii=(int)coordenadaBarco[1];
                for(int i=0;i<vida;i++)
                {
                    posicionBarco.Add(""+coordenadaBarco[0]+(char)ascii);
                    ascii--;
                }
            break;
        }
    }
}