using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject textObj;
    [SerializeField] Text comboText;

    public int currentCombo = 0;
    public int feverComboCnt = 0;
    public int giveComboCnt = 1;

    public void IncreaseCombo()
    {
        currentCombo += giveComboCnt;
        feverComboCnt += giveComboCnt;
        if (currentCombo >= 3)
            ShowComboText();
        
        UpdateComboText();
    }

    public void  ResetCombo()
    {
        HideComboText();
        currentCombo = 0;
        feverComboCnt = 0;
        UpdateComboText();
    }

    void UpdateComboText()
    {
        comboText.text = currentCombo.ToString();
    }

    public void HideComboText()
    {
        textObj.gameObject.SetActive(false);
    }

    public void ShowComboText()
    {
        textObj.gameObject.SetActive(true);
    }

}