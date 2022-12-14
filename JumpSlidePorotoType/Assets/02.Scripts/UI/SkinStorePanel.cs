using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkinStorePanel : MonoBehaviour
{
    const string SkinKey = "Skin";

    [SerializeField] PlayerControl playerControl = null;
    [SerializeField] Button rightButton;
    [SerializeField] Button leftButton;

    [SerializeField] Transform imagesParent;

    public List<RawImage> skins = new List<RawImage>();
    public List<GameObject> skinObj = new List<GameObject>();

    public Button conFirmButton;

    [SerializeField] Transform playerTrm;
    public GameObject normalSkin;

    int index = 0;

    [SerializeField] Button buyButton;

    private void Start()
    {
        //ShowSideSkinImage();
        playerControl = FindObjectOfType<PlayerControl>();

        GameManager.instance.SetPlayerPrefs(SkinKey + 0, 1);

        for (int i = 0; i < imagesParent.childCount; i++)
        {
            skins.Add(imagesParent.GetChild(i).GetComponent<RawImage>());
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
            ConfirmButton();    
        });
    }


    void ConfirmButton()
    {

        if(GameManager.instance.CheckPlayerPrefs($"Skin{index}"))
        {
            normalSkin = skinObj[index];
        }
        else
        {
            if(GameManager.instance.gemCount >= 2000)
            {
                GameManager.instance.SetPlayerPrefs(SkinKey + index, 1);

                CheckHasCurSkin();

                GameManager.instance.gemCount -= 2000;
            }
        }


        CheckHasCurSkin();
    }

    public void CheckHasCurSkin()
    {
        if(GameManager.instance.CheckPlayerPrefs($"Skin{index}"))
        {
            if(normalSkin == skinObj[index])
                buyButton.GetComponentInChildren<Text>().text = "Equipped";
            else
                buyButton.GetComponentInChildren<Text>().text = "Equip";
        }
        else
        {
            buyButton.GetComponentInChildren<Text>().text = "Buy ( 2000 )";
        }
    }

    public void LeftChangeButton()
    {
        if (index <= 0)
            return;

        index--;
        ChangeImage(index);
        PlayerObj();    
        //LeftMove(index);
    }

    public void RightChangeButton()
    {
        if (index >= imagesParent.childCount -1)
            return;

        index++;
        ChangeImage(index);
        PlayerObj();
        //RightMove(index);
    }

    void ChangeImage(int index)
    {
        CheckHasCurSkin();
    }


    void PlayerObj()
    {
        for (int i = 0; i < skinObj.Count; i++)
        {
            skinObj[i].SetActive(false);
        }

        skinObj[index].SetActive(true);
        playerControl.curPlayerObj = skinObj[index].transform;
    }

    /*
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
    */
}
