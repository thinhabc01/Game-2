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
        Point = new Vector3(m_Player.position.x, transform.position.y, m_Player.position.z);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(Point, transform.position) > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Point, 2f * Time.fixedDeltaTime);
        }
        else
        {
            StartCoroutine(GrenadeAttack());
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);


        //if (collision.gameObject.tag.Equals("wall"))
        //{
        //    Destroy(gameObject);
        //}

    }
    IEnumerator GrenadeAttack()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
