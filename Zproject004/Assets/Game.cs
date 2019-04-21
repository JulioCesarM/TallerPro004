using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aliado = NPC.Ally;
using Enemigo = NPC.Enemy;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public Text NumAld;
    public Text NumZomb;

    void Start()
    {
        AsignarComponente();
    }

    /// <summary>
    /// Se encarga de mostrar la cantidad total de zombies y aldeanos en UI
    /// </summary>
    void Update()
    {
        GameObject[] Zombies = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject[] Aldeano = GameObject.FindGameObjectsWithTag("Aldeano");
        int NumZombies = 0;
        int NumAldeanos = 0;

        foreach (GameObject x in Zombies)
        {
            NumZombies++;
            NumZomb.text = NumZombies.ToString() + " Zombies";

        }

        foreach (GameObject x in Aldeano)
        {
            NumAldeanos++;
            NumAld.text = NumAldeanos.ToString() + " Aldeanos";

        }
    }

    const int GENERACIONMAXIMA = 26;

    /// <summary>
    /// Se encarga de dar un componente al azar a los cubos que se generan
    /// </summary>
    public void AsignarComponente()
    {
        AsignarReadonlys asignarReadonlys = new AsignarReadonlys();

        GameObject heroe = GameObject.CreatePrimitive(PrimitiveType.Cube);
        heroe.AddComponent<Heroe>();

        for (int i = 0; i < Random.Range(asignarReadonlys.valorMinimo, GENERACIONMAXIMA); i++)
        {
            int valorGeneracion = Random.Range(0, 2);
            if (valorGeneracion == 0)
            {
                GameObject aldeano = GameObject.CreatePrimitive(PrimitiveType.Cube);
                aldeano.name = "Aldeano";
                aldeano.AddComponent<Aliado.AldeanoScript>();
            }
            else
            {
                GameObject zombie = GameObject.CreatePrimitive(PrimitiveType.Cube);
                zombie.name = "Zombie";
                zombie.AddComponent<Enemigo.ZombieScript>();
            }
        }
    }
}