using System;
using System.Collections.Generic;

//clase donde se dibujara el mapa y el menu del juego
public class Mapa{

    //dificultad que le asignaremos a la IA
    private int Dificultad;

    //string para dibujar las coordenadas del mapa
    private string coordenada="ABCDEFGHIJ";


    //metodo donde se le indica la dificultad de la IA, si es invalida devuelve -1
    public int Menu()
    {
        try{
            Console.WriteLine("Introduzca la dificultad de la IA");
            Console.WriteLine("1-5");
            dificultad=Convert.toInt32(Console.ReadLine());
            if(dificultad<5 && dificultad >0 )
            {
                return dificultad;
            }
            else{
                throw new DificultadInvalida("Error al introducir dificultad");
                return -1;
            }
        }
        catch(exception e)
        {
            Console.WriteLine(e.message);
            return -1;
        }

    }

    //muestra el mapa donde se coloca tus barcos, se actualiza con cada barco que pongas
    public void MapaInicial(Jugador j1)
    {
        //variable para la posicion del barco que va de 0 a 9
        int nBarco=0;

        //coordenada del barco que indicara el usuario con 3 caracteres
        string coorBarco;

        //un string con la lista de todas las posiciones escogidas
        string listaTotalBarcos;
        do{
            Console.WriteLine("introduce las coordenadas del barco empezando por las fragatas, destructores y por ultimo acorazados");
            Console.WriteLine("introduce la coordenada más la direccion que quieres que siga con asdw EJEMPLO (B3S -> que será B3,B4,B5 en caso de acorazado)")
            
            //foreach que primero coge cada barco de la lista de barcos del jugador 1
            foreach(Barco b in j1.getBarcos())
            {
                //segundo foreach que coge la posicion de la lista del barco y la pone en la variable con las coordenadas
                foreach(string p in b.getPosicion())
                {
                    listaTotalBarcos+=p;
                }
            }

            //for que cada vez que introduzcamos una coordenada de barco se actualiza
            for(int i=1;i<11;i++)
            {
                for(int j=0;j<10;j++)
                {
                    //si la posicion a dibujar ya tiene coordenada escogida pone un dibujo en lugar de la coordenada
                    if(listaTotalBarcos.Contains(""+coordenada[i-1]+j));
                    {
                         Console.Write(" <-");
                    }
                    else{
                        Console.Write(" "+coordenada[i-1]+j);
                    }
                }
                Console.WriteLine();
            }
            //lee la coordenada que le indica el usuario, si algo falla no hace nada
            coorBarco=Console.ReadLine().ToUpper();
            if(ComprobarCoordenada(coorBarco,nBarco,listaTotalBarcos))
            {
                j1.setCoordenada(coorBarco,nBarco);
                nBarco+1;
            }
            Console.clear();
        }while(nBarco<10);
    }

//metodo para comprobar que las coordenadas entre barcos no coincidan y que no se salgan del mapa predeterminado
    private bool ComprobarCoordenada(string coorBarco,int nbarco,string total)
    {
        //booleano que confirma que los barcos no se chocan
        bool seguir=false;

        //primer if para los destructores que ocupan 2 espacios (2 vidas)
        if(nbarco>3 && nbarco<7)
        {

            //como funciona: si la lista de las coordenadas de los barcos ya puestas no contienen la primera coordenada que le pasamos 
            //mas la siguiente 'seguir' pasara a ser 'true'

            //esta es para indicar que la coordenada es la que le pasamos más la coordenada a la izquierda
            if(coorBarco[2]=='A')
            {
                if(!total.Contains(coorBarco.Remove(2)) && !total.Contains(""+(char)((int)coorBarco-1)+coorBarco[1])){seguir=true;}
            }

            //lo mismo pero hacia abajo
            else if(coorBarco[2]=='S')
            {
                if(!total.Contains(coorBarco.Remove(2)) && !total.Contains(""+coorBarco[0]+(coorBarco[1]+1))){seguir=true;}
            }

            //lo mismo pero hacia la derecha
            else if(coorBarco[2]=='D')
            {
                if(!total.Contains(coorBarco.Remove(2)) && !total.Contains(""+(char)((int)coorBarco+1)+coorBarco[1])){seguir=true;}
            }

            //lo mismo pero hacia arriba
            else if(coorBarco[2]=='W')
            {
                if(!total.Contains(coorBarco.Remove(2)) && !total.Contains(""+coorBarco+(coorBarco[1]-1))){seguir=true;}
            }
        }

        //este if es para controlar lo mismo pero en los acorazados que ocupan tres espacios
        else if(nbarco>=7)
        {

            //actual a izquierda dos casillas
            if(coorBarco[2]=='A')
            {
                if(!total.Contains(coorBarco.Remove(2)) && !total.Contains(""+(char)((int)coorBarco-1))+coorBarco[1]) && !total.Contains(""+(char)((int)coorBarco-2)+coorBarco[1]){seguir=true;}
            }
            //actual a abajo dos casillas
            else if(coorBarco[2]=='S')
            {
                if(!total.Contains(coorBarco.Remove(2)) && !total.Contains(""+coorBarco[0]+(coorBarco[1]+1)) && !total.Contains(""+coorBarco[0]+(coorBarco[1]+2))){seguir=true;}
            }
            //actual a derecha dos casillas
            else if(coorBarco[2]=='D')
            {
                if(!total.Contains(coorBarco.Remove(2)) && !total.Contains(""+(char)((int)coorBarco+1)+coorBarco[1]) && !total.Contains(""+(char)((int)coorBarco+2)+coorBarco[1])){seguir=true;}
            }
            //actual a arriba dos casillas
            else if(coorBarco[2]=='W')
            {
                if(!total.Contains(coorBarco.Remove(2)) && !total.Contains(""+coorBarco+(coorBarco[1]-1)) && !total.Contains(""+coorBarco+(coorBarco[1]-2))){seguir=true;}
            }
        }

        //en este if controlamos a que las fragatas no esten en una coordenada ya definida y que las demas coordenadas no se repitan
        if(!listaTotalBarcos.Contains(coorBarco.Remove(2)) && seguir)
        {

            //aqui controlamos que la letra de la coordenada es correcta
            if(coordenada.Contains(coorBarco[0]))
            {
                //aqui asignamos el primer espacio a las fragatas
                if(nbarco<4){return true;}

                //aqui asignamos el primer espacio mas el siguiente segun como haya indicado el usuario para los destructores
                else if(nbarco<6)
                {
                    if(coorBarco[2]=='A' && (int)coorBarco[0]>65){return true;}
                    else if(coorBarco[2]=='S' && (int)coorBarco[1]<57){return true;}
                    else if(coorBarco[2]=='D' && (int)coorBarco[0]<74){return true;}
                    else if(coorBarco[2]=='W' && (int)coorBarco[1]>48){return true;}
                }

                //lo mismo que el anterior mas otro espacio para los acorazados
                else{
                    if(coorBarco[2]=='A' && (int)coorBarco[0]>66){return true;}
                    else if(coorBarco[2]=='S' && (int)coorBarco[1]<56){return true;}
                    else if(coorBarco[2]=='D' && (int)coorBarco[0]<73){return true;}
                    else if(coorBarco[2]=='W' && (int)coorBarco[1]>49){return true;}
                }
            }
        }

        //false si algo de lo anterior ha fallado
        return false;
    }

    //muestra el mapa con cada ataque fallido y acertado que hemos hecho
    public void MapaAtaque(Jugador j1,IA j2)
    {
        bool salir=false;
        Console.WriteLine("Indica la coordenada a atacar!");
        for(int i=1;i<11;i++)
        {
            for(int j=0;j<10;j++)
            {
                if(listaFallo.Contains(""+coordenada[i-1]+j));
                {
                    Console.Write(" O");
                }
                else if(ListaAcierto.Contains(""+coordenada[i-1]+j))
                {
                    Console.Write(" <-");
                }
                else{
                    Console.Write(" "+coordenada[i-1]+j);
                }
            }
            Console.WriteLine();
        }
        while(!salir)
        {
            string ataque=Console.ReadLine().ToUpper();
            if(j1.getAtaque().Contains(ataque))
            {
                Console.WriteLine("No puedes atacar al mismo sitio!");
            }
            else{
                salir=true;
                j2.Atacado(ataque,j1);
            }
        }
    }
}