using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour
{
    public Text gemText;

    private void Start()
    {
        gemText.text = "" + GameManager.instance.gemCount;
    }

    public bool TryBuying(int price)
    {
        if (GameManager.instance.gemCount > price)
            return true;
        else
            return false;
    }

}
