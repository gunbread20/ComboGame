using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinStorePanel : MonoBehaviour
{
    [SerializeField] Image skinImage;
    [SerializeField] Image nextSkinImage;
    [SerializeField] Image beforeSkinImage;

    public Image[] skins;

    int index = 0;

    private void Start()
    {
        ShowSideSkinImage();
    }

    public void LeftChangeButton()
    {
        index--;
        ChangeImage(index);
    }

    public void RightChangeButton()
    {
        index++;
        ChangeImage(index);
    }

    void ChangeImage(int index)
    {
        skinImage = skins[index];
        ShowSideSkinImage();
    }

    void ShowSideSkinImage()
    {
        if (index - 1 < 0)
        {
            nextSkinImage = skins[index + 1];
            return;
        }
        if (skins[index + 1] == null)
        {
            beforeSkinImage = skins[index - 1];
            return;
        }

        nextSkinImage = skins[index + 1];
        beforeSkinImage = skins[index - 1];
    }
}
