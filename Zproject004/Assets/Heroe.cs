using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Aldeano = NPC.Ally;
using Zombie = NPC.Enemy;
/// <summary>
/// este componente se encarga de asignarle camara,rigidbody (Se desactiva la gravedad y se activan todos los constrains), velocidad al azar e inicia una corrutina para los controladores del heroe al objeto que lo tenga
/// </summary>
public class Heroe : MonoBehaviour
{
    private IEnumerator movCo;

    GameObject[] aldeanos, zombies;
    float distanciaAldeano, distanciaZombie;
    TextMeshProUGUI textAldeano, textZombie;
    float timer1, timer2;

    /// <summary>
    /// Se encarga de asignar los textos, ejecutar la asgnacion de los readonly y de agregar componentes
    /// </summary>
    public void Start()
    {
        textAldeano = GameObject.FindGameObjectWithTag("Aldeanotxt").GetComponent<TextMeshProUGUI>();
        textZombie = GameObject.FindGameObjectWithTag("Zombietxt").GetComponent<TextMeshProUGUI>();
        AsignarReadonlys AsignarVelocidad = new AsignarReadonlys();
        gameObject.AddComponent<Camera>();
        Rigidbody playerRigid = gameObject.AddComponent<Rigidbody>();
        playerRigid.useGravity = false;
        playerRigid.constraints = RigidbodyConstraints.FreezeAll;
        movCo = Acciones(AsignarVelocidad.vel);
        tag = "Heroe";
        StartCoroutine(movCo);
    }

    /// <summary>
    /// Se encarga de detectar la distancia con aldeanos y zombies con el fin de mostrar informacion en UI de la entidad
    /// </summary>
    void Update()
    {
        aldeanos = GameObject.FindGameObjectsWithTag("Aldeano");
        zombies = GameObject.FindGameObjectsWithTag("Zombie");
        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;

        foreach(GameObject aldeano in aldeanos)
        {
            distanciaAldeano = Mathf.Sqrt(Mathf.Pow((aldeano.transform.position.x - transform.position.x), 2) + Mathf.Pow((aldeano.transform.position.y - transform.position.y), 2) + Mathf.Pow((aldeano.transform.position.z - transform.position.z), 2));
            if(distanciaAldeano < 5f)
            {
                timer1 = 0;
                VillagerData villagerData = aldeano.GetComponent<Aldeano.AldeanoScript>().GetData();
                textAldeano.text = "Hola soy " + villagerData.nombre + " y tengo " + villagerData.edad;
            }
        }

        foreach(GameObject zombie in zombies)
        {
            distanciaZombie = Mathf.Sqrt(Mathf.Pow((zombie.transform.position.x - transform.position.x), 2) + Mathf.Pow((zombie.transform.position.y - transform.position.y), 2) + Mathf.Pow((zombie.transform.position.z - transform.position.z), 2));
            if (distanciaZombie < 5f)
            {
                timer2 = 0;
                ZombieData zombieData = zombie.GetComponent<Zombie.ZombieScript>().GetData();
                textZombie.text = "Wraaaaarr quiero " + zombieData.gusto;
            }
        }
        if(timer1 > 5f)
        {
            textAldeano.text = "";
        }

        if (timer1 > 5f)
        {
            textZombie.text = "";
        }

    }

    /// <summary>
    /// Esta corrutina se encarga de asignar las clases movimiento y rotacion, aparte de ejecutarlas cada frame
    /// </summary>
    /// <returns></returns>
    public IEnumerator Acciones(float vel)
    {

        Movimiento movimiento = new Movimiento();
        Rotacion rotacion = new Rotacion();

        while (true)
        {
            movimiento.Mover(this.gameObject, vel);
            rotacion.Girar(this.gameObject, vel);

            yield return new WaitForEndOfFrame();
        }
    }
}
/// <summary>
/// Aigna una velocidad random al Heroe
/// </summary>
class AsignarReadonlys
{
    public readonly int valorMinimo;
    public readonly float vel;

    public AsignarReadonlys()
    {
        vel = Random.Range(0.2f, 0.8f);
        valorMinimo = Random.Range(5, 16);
    }
}

/// <summary>
/// Permite que el objeto que lo tenga se mueva adelante y atras con W y S
/// </summary>
public class Movimiento
{
    public void Mover(GameObject x, float vel)
    {
        if (Input.GetKey(KeyCode.W))
        {
            x.transform.Translate(0, 0, vel / 4);
        }
        if (Input.GetKey(KeyCode.S))
        {
            x.transform.Translate(0, 0, -vel / 4);
        }
    }
}

/// <summary>
/// Permite que el objeto que lo tenga que gire a su mirada a los lados con A y D
/// </summary>
public class Rotacion
{
    public void Girar(GameObject z, float vel)
    {
        if (Input.GetKey(KeyCode.A))
        {
            z.transform.Rotate(0, -vel * 4, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            z.transform.Rotate(0, vel * 4, 0);
        }
    }
}

