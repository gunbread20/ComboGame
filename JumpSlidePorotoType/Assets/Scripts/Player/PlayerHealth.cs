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
        // 체력 감소
        _hp -= 1f;

        hpBar.UpdateHp(_hp, _maxHp);

        // 이펙트 및 카메라 효과

        ShakeCamera(3f);

        // 게임오버 조건 추가
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
