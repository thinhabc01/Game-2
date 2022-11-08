using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SawAttack());
    }
    IEnumerator SawAttack()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
