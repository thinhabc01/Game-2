using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] FloatingJoystick floatingJoystick;
    public Vector3 position;
    public override void Start()
    {
        base.Start();
        m_speed = 2f;
    }

    private void FixedUpdate()
    {
        position = transform.position;
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        Run(direction);
        Towards(direction);
    }
}
