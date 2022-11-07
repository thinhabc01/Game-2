using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    Vector3 Point;
    PlayerController m_Player;
    void Start()
    {
    	m_Player = FindObjectOfType<PlayerController>();
    	Point = new Vector3(m_Player.position.x, 1f, m_Player.position.z);
    }
    
    void FixedUpdate()
    {
    	transform.position = Vector3.MoveTowards(transform.position, Point, 2f*Time.fixedDeltaTime);
    }
}
