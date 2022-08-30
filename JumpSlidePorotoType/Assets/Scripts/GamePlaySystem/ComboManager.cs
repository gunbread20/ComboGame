using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public int currentCombo = 0;

    public void IncreaseCombo()
    {
        currentCombo += 1;
    }

    public void  ResetCombo()
    {
        currentCombo = 0;
    }
}