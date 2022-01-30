using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp2 : MonoBehaviour
{
    public Image HealthBar;
    public float curHealth;
    public static float mHealth = 100f;
    Player2Movement Player2;

    public void Start()
    {
        HealthBar = GetComponent<Image>();
        Player2 = FindObjectOfType<Player2Movement>();
    }

    public void Update()
    {
        curHealth = Player2Movement.health2;
        HealthBar.fillAmount = curHealth / mHealth;
    }
}
