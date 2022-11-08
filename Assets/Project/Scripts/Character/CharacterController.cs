using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public enum Status { Idle, Run, Attack, Death }
    public Status m_status;

    [SerializeField] Transform m_Transform;
    [SerializeField] GameObject m_Character;

    public float m_speed { get; set; }
    public bool attack;
    private Animator m_AnCharacter;
    private Transform m_TrCharacter;
    public virtual void Start()
    {
        m_AnCharacter = m_Character.GetComponent<Animator>();
        m_TrCharacter = m_Character.GetComponent<Transform>();
    }
    public virtual void Move(Vector3 direction)
    {
        m_Transform.Translate(direction * m_speed * Time.fixedDeltaTime);
        if (Vector3.Distance(direction, Vector3.zero) > 0)
        {
            float m_angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            m_TrCharacter.eulerAngles = new Vector3(0, m_angle, 0);
        }

    }


    public virtual void Attack()
    {

    }
    public virtual void Death()
    {

    }

}