using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] float speedRun;
    [SerializeField] float sleepTime;
    [SerializeField] WeaponController m_Weapon;

    PlayerController m_Player;
    public float timer = 0;
    public override void Start()
    {
        base.Start();
        m_speed = speedRun;
        m_Player = FindObjectOfType<PlayerController>();
        Run();
    }
    public virtual void Run()
    {
        StartCoroutine(PlayerBehavior());
    }
    IEnumerator PlayerBehavior()
    {
        while (m_status != Status.Death)
        {
            switch (m_status)
            {
                case Status.Idle:
                    yield return new WaitForSeconds(sleepTime);
                    m_status = Status.Run;
                    break;

                case Status.Attack:
                    attack = true;
                    yield return new WaitForSeconds(1.5f);
                    m_status = Status.Run;
                    break;

                case Status.Run:
                    timer += Time.deltaTime;
                    Vector3 direction = Vector3.Normalize(m_Player.position - transform.position);
                    Move(direction);
                    if (timer > 2f)
                    {
                        m_status = Status.Attack;
                        timer = 0;
                    }
                    yield return null;

                    break;

                default:
                    break;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag.Equals("enemy"))
        //{
        //    m_status = Status.Death;
        //}
        //if (collision.gameObject.tag.Equals("win"))
        //{

        //}
    }

}
