using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject textObj;
    [SerializeField] Text comboText;

    public int currentCombo = 0;
    public int giveComboCnt = 1;

    public void IncreaseCombo()
    {
        currentCombo += giveComboCnt;
        if (currentCombo >= 3)
            ShowComboText();
        if (currentCombo % 10 == 1 && currentCombo != 0)
            GroundManager.Instance.speedUp.Invoke();

        UpdateComboText();
    }

    public void  ResetCombo()
    {
        GroundManager.Instance.speedClear.Invoke();

        HideComboText();
        currentCombo = 0;
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