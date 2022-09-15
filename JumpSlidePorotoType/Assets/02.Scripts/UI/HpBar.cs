using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{

    [SerializeField] Image hpBar;


    private void Update()
    {
        if(GameManager.instance.state == GameState.RUNNING)
        {
            hpBar.gameObject.SetActive(true);
        }
    }
    public void UpdateHp(float hp , float maxHp)
    {
        hpBar.transform.localScale = new Vector3(hp / maxHp,1,1);

        if (hp / maxHp <= 0)
        {
            hpBar.transform.gameObject.SetActive(false);
        }
    }
}
