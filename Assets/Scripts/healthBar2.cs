using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar2 : MonoBehaviour
{
    public static Slider healtSlider2;

    public static void setMaxHealth2(int health)
    {
        healtSlider2.maxValue = health;
        healtSlider2.value = health;
    }
    
    public static void setHealth2(int health)
    {
        healtSlider2.value = health;
    }
}
