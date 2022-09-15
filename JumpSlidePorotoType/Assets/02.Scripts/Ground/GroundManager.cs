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

    private int level = 0;
    public List<int> groundCountByLevel;
    public float speed = 8;
    public float timeSpeed = 1;
    public float minTimeSpeed = 1;
    public float maxTimeSpeed = 3;
    public float addTimeSpeed = 0.1f;
    public float feverSlowTime = 1;

    private GameObject lastPiece;

    public float groundSize;
    public int groundCount = 3;

    public bool gemChance;

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

        gemChance = false;

        for (int i = 0; i < groundCount; i++)
        {
            if (i == groundCount - 1)
            {
                GroundMove nm = groundPools[0].GetPoolObject(new Vector3(0, 0, groundSize * i), true);
                lastPiece = nm.gameObject;
            }
            else
            {
                GroundMove nm = groundPools[0].GetPoolObject(new Vector3(0, 0, groundSize * i), true);
            }
                
        }

        gemChance = true;
    }

    void Start()
    {
        level = 0;
        timeSpeed = 1;
        Time.timeScale = timeSpeed;
        speedUp.AddListener(GroundSpeedUp);
        speedClear.AddListener(GroundSpeedClear);
    }

    public void SpawnGround()
    {
        Vector3 lastPos = Vector3.zero;
        int r = 0;
        GroundMove nm = new GroundMove();

        switch (level)
        {
            case 0:
                if (groundCountByLevel.Count < 1)
                {
                    groundCountByLevel.Add(5);
                }
                r = Random.Range(0, groundCountByLevel[0]);
                lastPos = lastPiece.transform.position;
                nm = groundPools[r].GetPoolObject(new Vector3(0, 0, groundSize + lastPos.z - 0.1f), true);
                break;

            case 1:
                if (groundCountByLevel.Count < 2)
                {
                    groundCountByLevel.Add(7);
                }
                r = Random.Range(0, groundCountByLevel[1]);
                lastPos = lastPiece.transform.position;
                nm = groundPools[r].GetPoolObject(new Vector3(0, 0, groundSize + lastPos.z - 0.1f), true);
                break;

            case 2:
                if (groundCountByLevel.Count < 3)
                {
                    groundCountByLevel.Add(9);
                }
                r = Random.Range(0, groundCountByLevel[2]);
                lastPos = lastPiece.transform.position;
                nm = groundPools[r].GetPoolObject(new Vector3(0, 0, groundSize + lastPos.z - 0.1f), true);
                break;

            case 3:
                if (groundCountByLevel.Count < 4)
                {
                    groundCountByLevel.Add(12);
                }
                r = Random.Range(0, groundCountByLevel[3]);
                lastPos = lastPiece.transform.position;
                nm = groundPools[r].GetPoolObject(new Vector3(0, 0, groundSize + lastPos.z - 0.1f), true);
                break;

            case 4:
                if (groundCountByLevel.Count < 5)
                {
                    groundCountByLevel.Add(14);
                }
                r = Random.Range(0, groundCountByLevel[4]);
                lastPos = lastPiece.transform.position;
                nm = groundPools[r].GetPoolObject(new Vector3(0, 0, groundSize + lastPos.z - 0.1f), true);
                break;

            default:
                r = Random.Range(0, grounds.Length);

                lastPos = lastPiece.transform.position;
                nm = groundPools[r].GetPoolObject(new Vector3(0, 0, groundSize + lastPos.z - 0.1f), true);
                nm.gemActive = true;
                break;
        }

        lastPiece = nm.gameObject;
    }

    public void LevelSet(int v)
    {
        if (v < 5 && v > -1)
        {
            level = v;
        }
        else
            return;
    }
    
    private void GroundSpeedUp()
    {
        timeSpeed += addTimeSpeed;
        if (timeSpeed > maxTimeSpeed)
            timeSpeed = maxTimeSpeed;

        Time.timeScale = timeSpeed;
    }

    private void GroundSpeedClear()
    {
        timeSpeed = minTimeSpeed;
        Time.timeScale = timeSpeed;
    }
}
