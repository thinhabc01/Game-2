using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class LvController : MonoBehaviour
{
    [SerializeField] GameObject[] m_Enemys;
    public float m_NumEnemy;
    GameController m_GameController;
    bool lv_check;
    private void Start()
    {
        lv_check = true;
        m_GameController = FindObjectOfType<GameController>();
        m_NumEnemy = m_Enemys.Length;
        Spawn();
    }

    public void Spawn()
    {

    }
    public void EnemyDeath()
    {
        m_NumEnemy--;
        if (m_NumEnemy < 2)
        {
            if (lv_check)
            {
                lv_check = false;
                m_GameController.WinGame();
            }
            //for (int i = 0; i < m_Enemys.Length; i++)
            //{
            //    Destroy(m_Enemys[i]);
            //}
        }
    }
    public void PlayerDeath()
    {
        if (lv_check)
        {
            lv_check = false;
            m_GameController.OverGame();
        }
        for (int i = 0; i < m_Enemys.Length; i++)
        {
            Destroy(m_Enemys[i]);
        }
    }
}
