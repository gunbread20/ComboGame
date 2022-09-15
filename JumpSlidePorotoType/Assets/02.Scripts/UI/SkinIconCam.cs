using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinIconCam : MonoBehaviour
{
    public GameObject skinIconCam;

    public Transform camParent;
    public Transform iconParent;

    void Start()
    {

    }

    void Update()
    {


    }

    void CreateRenderCam()
    {
        GameObject[] Skins = Resources.LoadAll<GameObject>("Skins/Player");

        for (int i = 0; i < iconParent.childCount; i++)
        {
            GameObject obj = Instantiate(skinIconCam, camParent);

            RenderTexture renderTexture = new RenderTexture(256, 256, 24, RenderTextureFormat.Default);

            obj.GetComponent<Camera>().targetTexture = renderTexture;

            //Skins[i] 

            SetSkinImage(obj);
        }
    }

    void SetSkinImage(GameObject camObj)
    {
        for (int i = 0; i < iconParent.childCount; i++)
        {
            iconParent.GetChild(i).GetComponent<RawImage>().texture = camParent.GetChild(i).GetComponent<Camera>().targetTexture;
        }
    }
}


