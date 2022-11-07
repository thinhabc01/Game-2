using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
	PlayerController m_Player;
	void Start()
    {
        m_Player = FindObjectOfType<PlayerController>();
    }
    void FixedUpdate()
    {
    	Vector3 temp = new Vector3(m_Player.position.x, transform.position.y, m_Player.position.z);
    	transform.position = Vector3.MoveTowards(transform.position, temp, 2f*Time.fixedDeltaTime);
    }
}
