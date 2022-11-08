using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject m_Object;
    PlayerController m_Player;

    public virtual void Start()
    {
        //m_Player = FindObjectOfType<PlayerController>();
        //StartCoroutine (Attack());
    }


    IEnumerator Attack()
    {
        yield return new WaitForSeconds(3);
        Spawner();
        StartCoroutine(Attack());
    }
    public void Spawner()
    {
        Vector3 temp = new Vector3(transform.position.x, 0.5f, transform.position.z);
        Instantiate(m_Object, temp, Quaternion.identity);
    }
}
