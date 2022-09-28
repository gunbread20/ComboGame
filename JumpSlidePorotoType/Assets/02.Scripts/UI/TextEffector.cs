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

        AnimCharInfo[] animCharArr = new AnimCharInfo[text.textInfo.characterInfo.Length];

        for (int i = 0; i < text.textInfo.characterInfo.Length; i++)
        {
            animCharArr[i] = new AnimCharInfo(text.textInfo.characterInfo[i], text);
        }

        while (timer <= Mathf.Deg2Rad * 360f)
        {
            for (int i = 0; i < text.textInfo.characterInfo.Length; i++)
            {
                animCharArr[i].SetUpdatePos(timer);
            }

            for (int i = 0; i < text.textInfo.meshInfo.Length; i++)
            {
                var meshInfo = text.textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                text.UpdateGeometry(meshInfo.mesh, i);
            }

            timer += Time.deltaTime * 4;

            yield return null;
        }
    }
}

public class AnimCharInfo
{
    bool isMoveOver = false;

    public TMP_CharacterInfo _charInfo;
    public TMP_Text _myText;

    public Vector3[] _verticesOriginPos;
    public Vector3[] _vertices;

    public float _offset;


    public AnimCharInfo(TMP_CharacterInfo charInfo, TMP_Text text)
    {
        _charInfo = charInfo;
        _vertices = text.textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
        _verticesOriginPos = text.textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
        _myText = text;
    }

    public void SetUpdatePos(float time)
    {
        if (!_charInfo.isVisible || isMoveOver)
            return;

        _offset = Mathf.Sin(time + _charInfo.vertexIndex * 0.025f) * 4;

        for (int i = 0; i < 4; i++)
        {
            int curIdx = _charInfo.vertexIndex + i;

            Vector3 origin = _vertices[_charInfo.vertexIndex + i];
            _vertices[curIdx] = origin + Vector3.up * _offset;

            if ((_vertices[curIdx] - _verticesOriginPos[curIdx]).magnitude <= 0.6f || _vertices[curIdx].y < _verticesOriginPos[curIdx].y)
            {
                _vertices[curIdx] = _verticesOriginPos[curIdx];
                isMoveOver = true;
            }
        }
    }
}

public enum TextEffectType
{
    TextShakeOnClear,

}
