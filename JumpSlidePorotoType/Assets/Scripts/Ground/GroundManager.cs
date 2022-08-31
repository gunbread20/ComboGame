using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    static public GroundManager Instance;

    public GameObject[] grounds;
    public float speed;
    public float groundSize;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Instantiate(grounds[0], new Vector3(0, 0, 0), Quaternion.identity, transform);
        Instantiate(grounds[0], new Vector3(0, 0, (groundSize)), Quaternion.identity, transform);
        Instantiate(grounds[0], new Vector3(0, 0, (groundSize * 2)), Quaternion.identity, transform);
    }

    public void SpawnGround()
    {
        int r = Random.Range(0, grounds.Length);

        Instantiate(grounds[r], new Vector3(0, 0, groundSize * 2), Quaternion.identity, transform);
    }

}
