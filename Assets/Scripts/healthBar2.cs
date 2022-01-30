using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar2 : MonoBehaviour
{
    public static Slider slider;

    public static void setMaxHealth2(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public static void setHealth2(int health)
    {
        slider.value = health;
    }
}
