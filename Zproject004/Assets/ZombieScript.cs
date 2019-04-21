using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Aldeno = NPC.Ally;
using Zombie = NPC.Enemy;
namespace NPC
{
    namespace Enemy
    {

        /// <summary>
        /// Esta clase maneja el estado del objeto entre Idle y Mov recalculando estado cada 5 Segundos
        /// </summary>
        public class ZombieScript : MonoBehaviour
        {
            float timer;
            bool infected = false;
            string gusto;
            public ZombieData zombieData;
            int edad;
            float speed;

            enum Gusto
            {
                Piernas, Dedos, Cerebro, Ojos, Lengua,
            }


            /// <summary>
            /// Se encarga de asignar los valores que se usan en la estructura, asignar tag, cambiar color y asignar componentes extras
            /// </summary>
            public void Start()
            {
                GameObject zombie = this.gameObject;
                zombie.tag = "Zombie";
                Renderer zRender = zombie.GetComponent<Renderer>();
                Gusto enumGusto;
                enumGusto = (Gusto)Random.Range(0, 5);
                gusto = enumGusto.ToString();
           
                if (!infected)
                {
                    edad = Random.Range(15, 101);
                    speed = 5f / edad;
                    zombie.transform.position = new Vector3(Random.Range(-20, 21), 0, Random.Range(-20, 21));
                    edad = Random.Range(15, 101);
                    zombie.name = "Zombie";
                    zombie.AddComponent<Rigidbody>().useGravity = false;
                    zombie.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    int numeroColor = Random.Range(0, 3);

                    if (numeroColor == 0)
                        zRender.material.color = Color.cyan;
                    else if (numeroColor == 1)
                        zRender.material.color = Color.green;
                    else
                        zRender.material.color = Color.magenta;

                    zombie.AddComponent<NPC.NpcScript>().speed = speed;
                }
                else
                {
                    zombie.name = "Zombie " + zombieData.nombre;
                    edad = zombieData.edad;
                    zRender.material.color = Color.red;
                }
            }


            /// <summary>
            /// Se encarga de detectar la colision y con que colisiona
            /// </summary>
            /// <param name="other">
            /// Otro objeto con el cual colisiona
            /// </param>
            void OnCollisionEnter(Collision other)
            {
                if(other.gameObject.tag == "Heroe")
                {
                    SceneManager.LoadScene(0);
                }

                if(other.gameObject.tag == "Aldeano")
                {
                    ZombieScript zombie = other.gameObject.AddComponent<Enemy.ZombieScript>();
                    zombie.infected = true;
                    zombie.zombieData = other.gameObject.GetComponent<Ally.AldeanoScript>().GetData();
                    Destroy(other.gameObject.GetComponent<Aldeno.AldeanoScript>());
                }
            }
            

            /// <summary>
            /// Se encarga de dar una estructura de zombie a un aldeano para transformalo
            /// </summary>
            /// <returns>
            /// Estrucutra de aldeano para conversion
            /// </returns>
            public ZombieData GetData()
            {
                ZombieData newzombieData = new ZombieData();
                newzombieData.edad = edad;
                newzombieData.gusto = gusto;
                return newzombieData;
            }

        }
    }
}
