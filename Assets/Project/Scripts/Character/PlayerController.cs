using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] FloatingJoystick floatingJoystick;
    [SerializeField] float speedRun;
    [SerializeField] GameObject Shield;

    public Vector3 position;
    public override void Start()
    {
        base.Start();
        m_status = Status.Run;
        m_speed = speedRun;
    }

    private void FixedUpdate()
    {
        position = transform.position;
        if (m_status == Status.Run)
        {
            Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
            Vector3 point = direction + position;
            Move(direction);
            if (point.x >= -5f && point.x <= 5f && point.z >= -7f && point.z <= 7f)
            {

            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Missile":
                if (gameObject.CompareTag("player"))
                {
                    m_status = Status.Death;
                    Death();
                }
                break;
            case "enemy":
                if (attack == true)
                {
                    collision.gameObject.GetComponent<CharacterController>().Death();
                }
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "item1":
                StartCoroutine(ItemSpeed());
                Destroy(other.gameObject);
                break;
            case "item2":
                StartCoroutine(ItemShield());
                Destroy(other.gameObject);
                break;
        }
    }
    IEnumerator ItemSpeed()
    {
        attack = true;
        m_speed = speedRun * 2;
        yield return new WaitForSeconds(3);
        m_speed = speedRun;
        attack = false;
    }
    IEnumerator ItemShield()
    {
        //Debug.Log("item shield");
        Shield.SetActive(true);
        gameObject.tag = "wall";
        yield return new WaitForSeconds(10);
        Shield.SetActive(false);
        gameObject.tag = "player";
    }

    public override void Death()
    {
        base.Death();
        gameObject.tag = "wall";
        m_gameController.PlayerDeath();
    }
}
