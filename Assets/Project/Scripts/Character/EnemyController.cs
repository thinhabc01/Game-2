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
    bool death;
    public override void Start()
    {
        base.Start();
        death = false;
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
                    m_status = Status.Run;
                    yield return new WaitForSeconds(1.5f);
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
        switch (collision.gameObject.tag)
        {
            case "Missile":
                if (!death)
                {
                    death = true;
                    m_status = Status.Death;
                    Death();
                }
                break;
        }
    }
    public override void Death()
    {
        base.Death();
        m_gameController.EnemyDeath();
    }
}
