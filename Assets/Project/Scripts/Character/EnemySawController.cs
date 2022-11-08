using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySawController : CharacterController
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
                    yield return null;
                    m_status = Status.Run;
                    break;

                case Status.Attack:
                    attack = true;
                    yield return new WaitForSeconds(1f);
                    m_status = Status.Run;
                    break;

                case Status.Run:
                    if (Vector3.Distance(m_Player.position, transform.position) > 0.8f)
                    {
                        m_status = Status.Run;
                        Vector3 direction = Vector3.Normalize(m_Player.position - transform.position);
                        Move(direction);
                        yield return null;

                    }

                    else
                    {
                        m_status = Status.Attack;
                        yield return new WaitForSeconds(1f);

                    }
                    break;

                default:
                    break;
            }
        }
    }
}
