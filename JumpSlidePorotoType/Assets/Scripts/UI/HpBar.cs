using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{

    [SerializeField] Slider hpBar;

    void Start()
    {
        hpBar.value = hpBar.maxValue;
    }

    
    public void UpdateHp(float hp , float maxHp)
    {
        hpBar.value = hp / maxHp;

        if (hpBar.value <= 0)
        {
            hpBar.fillRect.gameObject.SetActive(false);
        }
    }
}
