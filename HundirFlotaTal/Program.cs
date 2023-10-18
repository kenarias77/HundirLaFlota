using system;
using System.Collections.Generic;
class Juego{
    static void Main(){
        Mapa mapa=new Mapa();
        Jugador j1=new Jugador();
        IA j2=new Ia(mapa.Menu());
        mapa.MapaInicial(j1);
        j2.setCoordenadasBarcosIA();
        do{
            mapa.MapaAtaque(j1,j2);
            estrategia(j1);
        }while(!j1.comprobarGanar() && !j2.comprobarGanar())
        if(j1.getGanar())
        {
            Console.WriteLine("el jugador 1 ha ganado!");
        }
        else{
            Console.WriteLine("jugador 2 consigue la victoria!");
        }
    }
}