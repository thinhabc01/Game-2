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


        void Update()
        {
            if (enemy.attack)
            {
                enemy.attack = false;
                GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
                switch (m_type)
                {
                    case TypeWeapon.Bullet:
                        projectile.GetComponent<ETFXProjectileScript>().type = "Bullet";
                        break;

                    case TypeWeapon.Rocket:
                        projectile.GetComponent<ETFXProjectileScript>().type = "Rocket";
                        break;

                    case TypeWeapon.Grenade:
                        projectile.GetComponent<ETFXProjectileScript>().type = "Grenade";
                        break;
                    case TypeWeapon.Saw:
                        projectile.GetComponent<ETFXProjectileScript>().type = "Saw";
                        break;
                }

            }
        }
    }
}