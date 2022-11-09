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
            switch (type)
            {
                case "Bullet":
                    float m_angle = Vector3.SignedAngle(Vector3.forward, temp, Vector3.up);
                    transform.eulerAngles = new Vector3(0, m_angle, 0);
                    transform.Translate(Vector3.forward * 10 * Time.deltaTime);
                    break;
                case "Rocket":
                    Vector3 playerPosition = new Vector3(m_Player.position.x, transform.position.y, m_Player.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, playerPosition, 1.5f * Time.fixedDeltaTime);
                    break;
                case "Grenade":
                    if (Vector3.Distance(point, transform.position) > 0.3f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, point, 5f * Time.fixedDeltaTime);
                    }
                    else
                    {
                        Bomb();
                    }
                    break;
                case "Saw":
                    StartCoroutine(Saw());
                    break;
            }

        }
        IEnumerator Saw()
        {
            yield return new WaitForSeconds(0.5f);
            Bomb();
        }
        private void Bomb()
        {
            GameObject impactP = Instantiate(impactParticle, transform.position, Quaternion.identity) as GameObject;
            ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
            for (int i = 1; i < trails.Length; i++)
            {
                ParticleSystem trail = trails[i];

                if (trail.gameObject.name.Contains("Trail"))
                {
                    trail.transform.SetParent(null);
                    Destroy(trail.gameObject, 2f);
                }
            }

            Destroy(projectileParticle, 3f);
            Destroy(impactP, 3.5f);
            Destroy(gameObject);
        }
        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log(collision.gameObject.tag);
            switch (collision.gameObject.tag)
            {
                case "wall":
                    if (type == "Bullet")
                    {
                        if (Mathf.Abs(transform.position.z) >= 0.5f)
                        {
                            temp = new Vector3(temp.x, temp.y, temp.z * -1);
                        }
                        if (Mathf.Abs(transform.position.x) >= 0.5f)
                        {
                            temp = new Vector3(temp.x * -1, temp.y, temp.z);
                        }

                    }
                    else
                    {
                        Bomb();
                    }
                    break;

                case "player":
                    Bomb();
                    break;

                case "enemy":
                    Bomb();
                    break;

                case "ground":
                    Bomb();
                    break;
                case "box":
                    Bomb();
                    Destroy(collision.gameObject);
                    break;
            }
        }
    }
}