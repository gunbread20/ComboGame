using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    public float _maxHp;
    public float _hp;

    HpBar hpBar;
    ScoreManager scoreManager;
    ComboManager comboManager;

    [SerializeField] CinemachineVirtualCamera cam;

    float timer;

    private void Start()
    {
        hpBar = FindObjectOfType<HpBar>();
        scoreManager = FindObjectOfType<ScoreManager>();
        comboManager = FindObjectOfType<ComboManager>();

        _maxHp = _hp;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }
    }

    public void Damaged()
    {
        // ü�� ����
        _hp -= 1f;

        hpBar.UpdateHp(_hp, _maxHp);

        // ����Ʈ �� ī�޶� ȿ��

        ShakeCamera(3f);

        // ���ӿ��� ���� �߰�
        if (_hp <= 0)
        {
            scoreManager.GameOverScore();
            GameManager.instance.GameOver();

        }

        comboManager.ResetCombo();
    }

    void ShakeCamera(float power)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = power;

        timer = 0.3f;
    }
}
