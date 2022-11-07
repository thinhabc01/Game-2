using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] float speedRun;
    [SerializeField] float sleepTime;
    [SerializeField] WeaponController m_Weapon;

    PlayerController m_Player;

    public override void Start()
    {
        base.Start();
        m_speed = speedRun;
        m_Player = FindObjectOfType<PlayerController>();
    }


    private void FixedUpdate()
    {
        if (m_status == Status.Run)
            {
            if (Vector3.Distance(m_Player.position, transform.position) > 0.7f)
            {
                Vector3 direction = Vector3.Normalize(m_Player.position - transform.position);
                Move(direction);
            }
        }
        if (m_status == Status.Attack)
        {
            m_Weapon.Spawner();
        }
    }


    
}
