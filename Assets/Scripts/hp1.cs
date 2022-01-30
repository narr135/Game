using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp1 : MonoBehaviour
{
    public Image HealthBar;
    public float curHealth;
    public static float mHealth = 100f;
    Player1Movement Player1;

    public void Start()
    {
        HealthBar = GetComponent<Image>();
        Player1 = FindObjectOfType<Player1Movement>();
    }

    public void Update()
    {
        curHealth = Player1Movement.health;
        HealthBar.fillAmount = curHealth / mHealth;
    }
}
