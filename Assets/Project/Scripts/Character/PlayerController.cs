using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] FloatingJoystick floatingJoystick;
    [SerializeField] float speedRun;

    public Vector3 position;
    public override void Start()
    {
        base.Start();
        m_speed = speedRun;
    }

    private void FixedUpdate()
    {
        position = transform.position;
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        Move(direction);
    }
}
