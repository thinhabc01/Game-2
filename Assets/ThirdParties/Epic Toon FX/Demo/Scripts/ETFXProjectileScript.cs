using UnityEngine;
using System.Collections;

namespace EpicToonFX
{
    public class ETFXProjectileScript : MonoBehaviour
    {
        public GameObject impactParticle; // Effect spawned when projectile hits a collider
        public GameObject projectileParticle; // Effect attached to the gameobject as child
        public GameObject muzzleParticle; // Effect instantly spawned when gameobject is spawned
        [Header("Adjust if not using Sphere Collider")]
        public float colliderRadius = 1f;
        [Range(0f, 1f)] // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
        public float collideOffset = 0.15f;
        public string type;
        PlayerController m_Player;
        Vector3 point;
        Vector3 temp;
        void Start()
        {
            m_Player = FindObjectOfType<PlayerController>();
            point = m_Player.position;
            Vector3 Towards = Vector3.Normalize(point - transform.position);
            temp = new Vector3(Towards.x, 0, Towards.z);


            projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
            projectileParticle.transform.parent = transform;
            if (muzzleParticle)
            {
                muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
                Destroy(muzzleParticle, 1.5f); // 2nd parameter is lifetime of effect in seconds
            }
        }

        void FixedUpdate()
        {
            if (type == "Bullet")
            {
                float m_angle = Vector3.SignedAngle(Vector3.forward, temp, Vector3.up);
                transform.eulerAngles = new Vector3(0, m_angle, 0);
                transform.Translate(Vector3.forward * 10 * Time.deltaTime);

            }
            else if (type == "Rocket")
            {
                Vector3 temp = new Vector3(m_Player.position.x, transform.position.y, m_Player.position.z);
                transform.position = Vector3.MoveTowards(transform.position, temp, 2f * Time.fixedDeltaTime);
            }
            else if (type == "Grenade")
            {
                if (Vector3.Distance(point, transform.position) > 0.2f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, point, 5f * Time.fixedDeltaTime);
                }
                else
                {
                    Bomb();
                }
            }
            else
            {
                StartCoroutine(Saw());
            }
        }
        IEnumerator Saw()
        {
            yield return new WaitForSeconds(0.5f);
            Bomb();
        }
        private void Bomb()
        {
            GameObject impactP = Instantiate(impactParticle, transform.position, Quaternion.identity) as GameObject; // Spawns impact effect

            ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>(); // Gets a list of particle systems, as we need to detach the trails
                                                                                 //Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++) // Loop to cycle through found particle systems
            {
                ParticleSystem trail = trails[i];

                if (trail.gameObject.name.Contains("Trail"))
                {
                    trail.transform.SetParent(null); // Detaches the trail from the projectile
                    Destroy(trail.gameObject, 2f); // Removes the trail after seconds
                }
            }

            Destroy(projectileParticle, 3f); // Removes particle effect after delay
            Destroy(impactP, 3.5f); // Removes impact effect after delay
            Destroy(gameObject); // Removes the projectile
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("wall"))
            {
                if (type == "Bullet")
                {
                    temp = temp * -1;
                }
                else
                {
                    Bomb();
                }
            }
            if (collision.gameObject.CompareTag("player"))
            {
                Bomb();
            }
            if (collision.gameObject.CompareTag("enemy"))
            {
                Bomb();
            }
        }
    }
}