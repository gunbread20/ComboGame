using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    static public GroundManager Instance;

    public GameObject[] grounds;
    public float speed;
    public float groundSize;
    public int groundCount = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < groundCount; i++)
        {
            Instantiate(grounds[0], new Vector3(0, 0, (groundSize * i)), Quaternion.identity, transform);
        }
    }

    public void SpawnGround()
    {
        int r = Random.Range(0, grounds.Length);

        Instantiate(grounds[r], new Vector3(0, 0, groundSize * groundCount), Quaternion.identity, transform);
    }

}
