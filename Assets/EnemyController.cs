using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    PlayerController m_Player;

    public override void Start()
    {
        base.Start();
        m_speed = 1f;
        m_Player = FindObjectOfType<PlayerController>();
    }


    private void FixedUpdate()
    {
        if (Vector3.Distance(m_Player.position, transform.position) > 0.7f)
        {
            Vector3 direction = Vector3.Normalize(m_Player.position - transform.position);
            Towards(direction);
            if (m_status == Status.Run)
            {
                Run(direction);
            }
        }
    }
}
