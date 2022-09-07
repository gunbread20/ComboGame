using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundManager : MonoBehaviour
{
    static public GroundManager Instance;

    public GroundMove[] grounds;
    public UnityEvent speedUp;
    public UnityEvent speedClear;

    public float speed = 8;
    public float minSpeed = 8;
    public float maxSpeed = 16;
    public float addSpeed = 0.1f;

    public float groundSize;
    public int groundCount = 3;

    [Range(0.01f, 1f)]
    public float gemChance;

    private GenericPool<GroundMove>[] groundPools = null;

    private void Awake()
    {
        groundPools = new GenericPool<GroundMove>[grounds.Length];

        if (Instance == null)
        {
            Instance = this;
        }

        for (int i = 0; i < grounds.Length; i++)
        {
            groundPools[i] = GenericPoolManager.CratePool<GroundMove>($"Ground{i}", grounds[i], transform, 3);
        }

        float temp = gemChance;
        gemChance = 0;

        for (int i = 0; i < groundCount; i++)
        {
            groundPools[0].GetPoolObject(new Vector3(0, 0, groundSize * i), true);
        }

        gemChance = temp;
    }

    void Start()
    {
        speedUp.AddListener(GroundSpeedUp);
        speedClear.AddListener(GroundSpeedClear);
    }

    public void SpawnGround()
    {
        int r = Random.Range(0, grounds.Length);

        groundPools[r].GetPoolObject(new Vector3(0, 0, groundSize * (groundCount - 1)), true);
        //Instantiate(grounds[r], new Vector3(0, 0, groundSize * (groundCount - 1)), Quaternion.identity, transform);
    }

    private void GroundSpeedUp()
    {
        speed += addSpeed;

        if (speed > maxSpeed)
            speed = maxSpeed;
    }

    private void GroundSpeedClear()
    {
        speed = minSpeed;
    }
}
