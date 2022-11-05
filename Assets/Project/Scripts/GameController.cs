using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] Transform m_Player;
    [SerializeField] GameObject m_Dog;
    Transform m_TrDog;
    Animator m_AnDog;

    private void Start()
    {
        m_AnDog = m_Dog.GetComponent<Animator>();
        m_TrDog = m_Dog.GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        m_Player.Translate(direction * speed * Time.fixedDeltaTime);
        if (direction.x != 0)
        {
            m_AnDog.SetInteger("Status", 1);
            int a = m_AnDog.GetInteger("Status");
        }
        else
        {
            m_AnDog.SetInteger("Status", 0);
        }

        if (Vector3.Distance(direction, Vector3.zero) > 0)
        {
            float m_angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            m_TrDog.eulerAngles = new Vector3(0, m_angle, 0);
        }
    }
}
