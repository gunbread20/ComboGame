using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkinStorePanel : MonoBehaviour
{
    [SerializeField] Button rightButton;
    [SerializeField] Button leftButton;

    [SerializeField] Transform imagesParent;

    public List<Image> skins = new List<Image>();
    public List<GameObject> skinObj = new List<GameObject>();

    public Button conFirmButton;

    [SerializeField] Transform playerTrm;
    [SerializeField] GameObject normalSkin;

    int index = 0;

    private void Start()
    {
        //ShowSideSkinImage();

        for (int i = 0; i < imagesParent.childCount; i++)
        {
            skins.Add(imagesParent.GetChild(i).GetComponent<Image>());
        }
        skins.Reverse();

        rightButton.onClick.AddListener(() =>
        {
            RightChangeButton();
        });

        leftButton.onClick.AddListener(() =>
        {
            LeftChangeButton();
        });

        conFirmButton.onClick.AddListener(() =>
        {
            Destroy(normalSkin);
            GameObject skin =  Instantiate(skinObj[index], playerTrm);
            normalSkin = skin;
        });
    }

    public void LeftChangeButton()
    {
        if (index <= 0)
            return;

        index--;
        ChangeImage(index);
        LeftMove(index);
    }

    public void RightChangeButton()
    {
        if (index >= imagesParent.childCount -2)
            return;

        index++;
        ChangeImage(index);
        RightMove(index);
    }

    void ChangeImage(int index)
    {
        
        Debug.Log(index);
    }


    void LeftMove(int index)
    {
        skins[index].transform.SetSiblingIndex(imagesParent.childCount - 1);
        skins[index].transform.DOScale(1f, 0.3f);
        skins[index].rectTransform.DOAnchorPos(new Vector2(0, 0), 0.3f);

        skins[index].color = Color.black;

        if(index >= 1)
        {
            skins[index - 1].transform.DOScale(0.7f, 0.3f);
            skins[index - 1].rectTransform.DOAnchorPos(new Vector2(-150f, 0), 0.3f);
            skins[index - 1].color = Color.gray;
        }

         skins[index + 1].transform.DOScale(0.7f, 0.3f);
         skins[index + 1].rectTransform.DOAnchorPos(new Vector2(150f, 0), 0.3f);
         skins[index + 1].DOFade(1f, 0.3f);


         skins[index + 2].transform.DOScale(0f, 0.3f);
         skins[index + 2].rectTransform.DOAnchorPos(new Vector2(300f, 0), 0.3f);
         skins[index + 2].DOFade(0f, 0.3f);
    }
    
    void RightMove(int index)
    {
        skins[index].transform.SetSiblingIndex(imagesParent.childCount - 1);
        skins[index].transform.DOScale(1f, 0.3f);
        skins[index].rectTransform.DOAnchorPos(new Vector2(0, 0), 0.3f);

        skins[index].color = Color.black;

        skins[index - 1].transform.DOScale(0.7f, 0.3f);
        skins[index - 1].rectTransform.DOAnchorPos(new Vector2(-150f, 0), 0.3f);

        skins[index - 1].color = Color.gray;


        if (index < imagesParent.childCount - 2)
        {
            skins[index + 1].transform.DOScale(0.7f, 0.3f);
            skins[index + 1].rectTransform.DOAnchorPos(new Vector2(150f, 0), 0.3f);
            skins[index + 1].DOFade(1f, 0.3f);
        }


        if (index > 1)
        {
            skins[index - 2].transform.DOScale(0f, 0.3f);
            skins[index - 2].rectTransform.DOAnchorPos(new Vector2(-300f, 0), 0.3f);
            skins[index - 2].DOFade(0f, 0.3f);
        }

    }

    
}
