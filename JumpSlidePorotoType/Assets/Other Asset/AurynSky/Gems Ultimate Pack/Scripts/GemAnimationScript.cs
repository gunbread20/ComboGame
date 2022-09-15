using UnityEngine;
using System.Collections;

public class GemAnimationScript : MonoBehaviour {

    public bool isAnimated = false;

    public bool isRotating = false;
    public bool isFloating = false;
    public bool isScaling = false;

    public Vector3 rotationAngle;
    public float rotationSpeed;

    public float floatSpeed;
    private bool goingUp = true;
    public float floatRate;
    private float floatTimer;
   
    public Vector3 startScale;
    public Vector3 endScale;

    private bool scalingUp = true;
    public float scaleSpeed;
    public float scaleRate;
    private float scaleTimer;

    private AudioSource audioSource = null;
    private MeshRenderer mr = null;

    private Vector3 originLocalPos = Vector3.zero;

    private void Awake()
    {
        originLocalPos = transform.localPosition;
        audioSource = GetComponent<AudioSource>();
        mr = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        transform.localPosition = originLocalPos;
        mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, 1);
        isAnimated = true;
    }

    public void SetOff()
    {
        audioSource.volume = GameManager.instance.volume / 100f;
        audioSource.Play();
        isAnimated = false;
        StartCoroutine(OffEffect());
    }

    // Update is called once per frame
    void Update()
    {

        if (isAnimated)
        {
            if (isRotating)
            {
                transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
            }

            if (isFloating)
            {
                floatTimer += Time.deltaTime;
                Vector3 moveDir = new Vector3(0.0f, 0.0f, floatSpeed);
                transform.Translate(moveDir);

                if (goingUp && floatTimer >= floatRate)
                {
                    goingUp = false;
                    floatTimer = 0;
                    floatSpeed = -floatSpeed;
                }

                else if (!goingUp && floatTimer >= floatRate)
                {
                    goingUp = true;
                    floatTimer = 0;
                    floatSpeed = +floatSpeed;
                }
            }

            if (isScaling)
            {
                scaleTimer += Time.deltaTime;

                if (scalingUp)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, endScale, scaleSpeed * Time.deltaTime);
                }
                else if (!scalingUp)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, startScale, scaleSpeed * Time.deltaTime);
                }

                if (scaleTimer >= scaleRate)
                {
                    if (scalingUp) { scalingUp = false; }
                    else if (!scalingUp) { scalingUp = true; }
                    scaleTimer = 0;
                }
            }
        }
    }

    IEnumerator OffEffect()
    {
        float timer = 0f;
        float alphatimer = 0f;
        Color startCol = mr.material.color;
        Vector3 pos = Vector3.zero;
        while (timer <= 1f)
        {
            pos = transform.localPosition;
            pos.y += 10 * Time.deltaTime;
            transform.localPosition = pos;

            startCol.a = 1 - alphatimer;
            mr.material.color = startCol;
            transform.Rotate(rotationAngle * 30 * Time.deltaTime);

            timer += Time.deltaTime;
            alphatimer += Time.deltaTime * 3;

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
