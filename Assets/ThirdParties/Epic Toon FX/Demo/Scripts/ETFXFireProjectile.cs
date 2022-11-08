using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace EpicToonFX
{
    public class ETFXFireProjectile : MonoBehaviour
    {
        [SerializeField] CharacterController enemy;
        public enum TypeWeapon { Bullet, Grenade, Rocket, Saw }
        public TypeWeapon m_type;

        [SerializeField]
        public GameObject[] projectiles;
        [Header("Missile spawns at attached game object")]
        public Transform spawnPosition;
        //[HideInInspector]
        public int currentProjectile = 0;
        public float speed = 500;
        PlayerController m_Player;
        //    MyGUI _GUI;
        //ETFXButtonScript selectedProjectileButton;

        void Start()
        {
            m_Player = FindObjectOfType<PlayerController>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextEffect();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                nextEffect();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                previousEffect();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                projectile.transform.LookAt(m_Player.position); //Sets the projectiles rotation to look at the point clicked
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
            }

            if (enemy.attack) //On left mouse down-click
            {
                enemy.attack = false;
                if (m_type == TypeWeapon.Bullet)
                {
                    //m_Player = FindObjectOfType<PlayerController>();
                    //Vector3 Towards = Vector3.Normalize(m_Player.position - transform.position);
                    //projectiles[currentProjectile].GetComponent<ETFXProjectileScript>().temp = new Vector3(Towards.x, 0, Towards.z);
                    GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile

                    projectile.GetComponent<ETFXProjectileScript>().type = "Bullet";
                    //projectile.transform.LookAt(m_Player.position); //Sets the projectiles rotation to look at the point clicked
                    //projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
                }
                else if (m_type == TypeWeapon.Rocket)
                {
                    GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<ETFXProjectileScript>().type = "Rocket";
                }
                else if (m_type == TypeWeapon.Grenade)
                {
                    GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<ETFXProjectileScript>().type = "Grenade";
                }
                else
                {
                    GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<ETFXProjectileScript>().type = "Saw";
                }
            }
        }

        public void nextEffect() //Changes the selected projectile to the next. Used by UI
        {
            if (currentProjectile < projectiles.Length - 1)
                currentProjectile++;
            else
                currentProjectile = 0;
            //selectedProjectileButton.getProjectileNames();
        }

        public void previousEffect() //Changes selected projectile to the previous. Used by UI
        {
            if (currentProjectile > 0)
                currentProjectile--;
            else
                currentProjectile = projectiles.Length - 1;
            //selectedProjectileButton.getProjectileNames();
        }

        public void AdjustSpeed(float newSpeed) //Used by UI to set projectile speed
        {
            speed = newSpeed;
        }
    }
}