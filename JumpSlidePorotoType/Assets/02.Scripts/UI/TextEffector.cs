using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffector : MonoBehaviour
{
    TMP_Text text = null;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        SetEffect(TextEffectType.TextShakeOnClear);
    }

    public void SetEffect(TextEffectType type)
    {
        switch(type)
        {
            case TextEffectType.TextShakeOnClear:
                {
                    StartCoroutine(ClearShake());
                    break;
                }
        }
    }

    public IEnumerator ClearShake()
    {
        text.ForceMeshUpdate();

        float timer = 0f;
        float offset = 0f;
        while (timer <= 6.28319f)
        {
            for (int i = 0; i < text.textInfo.characterInfo.Length; i++)
            {
                Debug.Log(offset);

                TMP_CharacterInfo charInfo = text.textInfo.characterInfo[i];
                if (charInfo.isVisible)
                {
                    Vector3[] vertices = text.textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
                    offset = Mathf.Sin(timer + charInfo.vertexIndex * 0.05f);

                    for (int j = 0; j < 4; j++)
                    {
                        Vector3 origin = vertices[charInfo.vertexIndex + j];
                        vertices[charInfo.vertexIndex + j] = origin + Vector3.up * offset;
                    }
                }
            }

            for (int i = 0; i < text.textInfo.meshInfo.Length; i++)
            {
                var meshInfo = text.textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                text.UpdateGeometry(meshInfo.mesh, i);
            }

            timer += Time.deltaTime;

            yield return null;
        }
    }
}

public enum TextEffectType
{
    TextShakeOnClear,

}
