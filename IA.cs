using System;
using System.Collections.Generic;

//clase hija de jugador donde se le asigna la dificultad a la IA
public class IA : Jugador
{
    public int dificultad;
    private int x;
    private int y;
    private string[] z={"A","S","D","W"};
    private Random rnd= new Random();

    //constructor donde se le asigna la dificultad
    public IA(int dificultad)
    {
        this.dificultad=dificultad;
    }

    //switch donde selecciona una estrategia basado en la dificultad seleccionada
    public void estrategia(Jugador j1)
    {
        if(dificultad==1)
        {
            //dificultad 1 todo aleatorio
                x=rnd.Next(65,75);
                y=rnd.Next(48,58);
                AtaqueIA(j1,x,y);
        }
        else{
            //demás dificultades: segun el numero de la dificultad atacara de 1 a 4 veces las posiciones adyacentes a la ultima acertada
            //disponible
            if(coordenadaAtaques.Count>0 && getAtaqueIA())
            {
                x=(int)coordenadaAtaques[coordenadaAtaques.Count-1][0];
                y=(int)coordenadaAtaques[coordenadaAtaques.Count-1][1];
                AtaqueIA(j1,x,y,dificultad);
            }
            else{
                x=rnd.Next(65,75);
                y=rnd.Next(48,58);
                AtaqueIA(j1,x,y);
            }
        }
    }


//metodo para colocar barcos aleatoriamente para la IA utiliza los mismos metodos que se encuentran en la clase 'Mapa.MapaInicial' 
    public void setCoordenadasBarcosIA(Mapa m)
    {
        //string para guardas las coordenadas de los barcos
        string listaTotalBarcos="";
        //posicion del barco a colocar
        int nBarcos=0;
        //string donde se ponen las coordenadas
        string coordBarco;

        //do while donde pone los barcos aleatoriamente hasta que los coloque todos correctamente
        do{
            foreach(Barco b in getBarcos())
            {
                Console.Write(b.getNombre()+" ");
                //segundo foreach que coge la posicion de la lista del barco y la pone en la variable con las coordenadas
                foreach(string p in b.getPosicion())
                {
                    Console.Write(p+" ");
                    listaTotalBarcos+=p;
                }
                Console.WriteLine(" barco:"+nBarcos);
            }
            //variable donde guardaremos cada columna;
            x=rnd.Next(65,75);
            //variable donde guardaremos cada fila
            y=rnd.Next(48,58);
            //variable completa donde le asignaremos la x(A-J) la y(0-9) y la direccion (A,S,D,W)
            coordBarco=""+(char)x+(char)y+z[rnd.Next(0,4)];
            //if donde comprobaremos que el valor creado aleatoriamente es correcto
            if(m.ComprobarCoordenada(coordBarco,nBarcos,listaTotalBarcos))
            {
                setCoordenada(coordBarco,nBarcos);
                nBarcos++;
            }
        }while(nBarcos<10);
    }
    private void AtaqueIA(Jugador j1,int x, int y, int dificultad=1)
    {
        bool salir=false;
        string ataque;
        //comprueba que el ataque elegido al azar no se repita y en caso que lo haga que escoja otra coordenada al azar
        if(dificultad==1)
        {
            ataque=""+(char)x+(char)y;
            while(!salir)
            {
                Console.WriteLine(ataque);
                if(!getAtaque().Contains(ataque+"N") || !getAtaque().Contains(ataque+"Y"))
                {  
                    salir=true;
                    if(j1.Atacado(ataque))
                    {
                        setAtaque(ataque+"Y");
                    }
                    else{
                        setAtaque(ataque+"N");
                    }
                }
                else
                {
                    x=rnd.Next(65,75);
                    y=rnd.Next(48,58);
                    ataque=""+(char)x+(char)y;
                }
            }   
        }
        //comprobacion de que el ataque no sea erroneo o esté ya ocupado  
        else{
            int imposible=0;
            while(!salir && imposible!=4)
            {
                int x1=rnd.Next(-1,2);
                int y1;
                //comprueba que la nueva X no se salga del rango del mapa
                if(x+x1>64 && x+x1<75)
                {

                    //si la nueva X es la misma que la anterior elije la Y al azar entre arriba o abajo
                    if(x1==0)
                    {
                       y1=rnd.Next(2);

                       //los dos if comprueban que la Y no se salga del rango del mapa y que el ataque no se repita llamandose asi mismo para comprobarlo, recursividad!
                       if(y1==0 && y-1>47 && !getAtaque().Contains(""+(char)x+(char)(y-1)))
                       {
                            AtaqueIA(j1,x,y-1);
                            salir=true;
                       }
                       else if(y1==1 && y+1<58 && !getAtaque().Contains(""+(char)x+(char)(y+1)))
                       {
                            AtaqueIA(j1,x,y-1);
                            salir=true;
                       } 
                    }
                    else
                    {
                        if(!getAtaque().Contains(""+(char)(x+x1)+(char)y))
                        {
                            AtaqueIA(j1,x+x1,y);
                            salir=true;
                        }
                    }
                }
                imposible++;
            }
            //si la variable imposible que controla que las 4 posiciones adyacentes estén disponibles llega a 4
            //manda las coordenadas en la que ya atacamos como dificultad 1, provocando que entre en el primer if de la funcion 
            // y como la posicion ya existe en la lista de ataques crea otra aleatoriamente
            if(imposible==4)
            {
                AtaqueIA(j1,x,y);
            }
        }
    }

    //devuelve la racha del ultimo ataque realizado ( mayor que 0 significa que el ultimo ataque ha dado en blanco, 0 significa que ha fallado) 
    private bool getAtaqueIA()
    {
        //la diferencia es para cuando la lista de ataques sea menor que la dificultad, si la dificultad supera este numero 
        //la variable diferencia pasará a ser el resultado de dificultad - la cantidad de ataques en la lista
        int diferencia=0;
        if(getAtaque().Count<dificultad)
        {
            diferencia=dificultad-getAtaque().Count;
        }

        //un for que recorre los ultimos x ataques segun la dificultad seleccionada, si el numero de la dificultad es mayor que la cantidad
        //de ataques en una lista la variable "diferencia" valdra la diferencia entre la dificultad y la cantidad de ataques de la lista
        //EJEMPLO DIFICULTAD=5
        //        LISTA.COUNT=2
        //        CALCULO=DIFERENCIA(5)-1-DIFERENCIA(3)
        for(int i=0;i<(dificultad-1-diferencia);i++)
        {
            //recorre la lista del final hacia el principio por un rango determinado por la dificultad, si encuentra un ataque acertado
            //devuelve true, si no sale del for y devuelve false
            //EJEMPLO si la dificultad es 5 y un ataque ha acertado en la antepenúltima posicion devolvera true
            //pero si no encuentra algun ataque en los ultimos 5 saldra del for devolviendo falso
            if(getAtaque()[getAtaque().Count-(1+i)].Contains("Y"))
            {
                return true;
            }
        }
        return false;
    }
}