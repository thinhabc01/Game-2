using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Vector3 Towards, temp;
    PlayerController m_Player;
    void Start()
    {
    	m_Player = FindObjectOfType<PlayerController>();
    	Towards = Vector3.Normalize(m_Player.position - transform.position);
    	temp = new Vector3(Towards.x, 0, Towards.z);
    }
    
    void FixedUpdate()
    {
    	transform.Translate(temp * Time.deltaTime);
    }
}
