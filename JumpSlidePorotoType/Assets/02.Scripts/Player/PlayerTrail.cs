using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    [SerializeField] int maxPoint = 100;
    [SerializeField] Transform player = null;

    LineRenderer lr = null;
    List<TrailPoint> playerTrace = new List<TrailPoint>();

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = maxPoint;
        TrailPoint curTrailPoint = null;
        TrailPoint lastTrailPoint = null;

        for (int i = 0; i < maxPoint; i++)
        {
            curTrailPoint = new TrailPoint(transform.position, 0, lastTrailPoint);
            lastTrailPoint = curTrailPoint;

            playerTrace.Add(curTrailPoint);
            lr.SetPosition(i, player.position);
        }
    }

    private void FixedUpdate()
    {
        playerTrace[maxPoint - 1].UpdatePos();
        playerTrace[0]._pos = player.position;
        for (int i = 0; i < maxPoint; i++)
        {
            lr.SetPosition(i, playerTrace[i].Pos);
        }
    }

    private void InitTrail()
    {
        for (int i = 0; i < playerTrace.Count; i++)
        {
            playerTrace[i]._depth = 0;
            playerTrace[i]._pos = player.position;
        }
    }

    public void SetTrail(float blendTime)
    {
        gameObject.SetActive(true);
        InitTrail();
        StartCoroutine(StartTrail(blendTime));
    }

    public void RemoveTrail(float blendTime)
    {
        if (!gameObject.activeSelf)
            return;

        StartCoroutine(StopTrail(blendTime));
    }

    private IEnumerator StartTrail(float blendTime)
    {
        WaitForSeconds wait = new WaitForSeconds(blendTime / maxPoint);

        for (int i = 0; i < maxPoint; i++)
        {
            float depth = Mathf.Lerp(player.position.z, transform.position.z, (float)i / maxPoint);

            playerTrace[i]._depth = depth;
            for (int y = i; y < playerTrace.Count; y++)
            {
                playerTrace[y]._depth = depth;
            }

            yield return wait;
        }
    }

    private IEnumerator StopTrail(float blendTime)
    {
        WaitForSeconds wait = new WaitForSeconds(blendTime / maxPoint);

        for (int i = maxPoint - 1; i >= 0; i--)
        {
            float depth = Mathf.Lerp(player.position.z, transform.position.z, (float)i / maxPoint);

            playerTrace[i]._depth = depth;
            for (int y = playerTrace.Count - 1; y >= i; y--)
            {
                playerTrace[y]._depth = depth;
            }

            yield return wait;
        }
        gameObject.SetActive(false);
    }
}

class TrailPoint
{

    public Vector3 Pos
    {
        get
        {
            return new Vector3(_pos.x, _pos.y, _depth);
        }
    }

    private TrailPoint _backPoint;
    public Vector2 _pos;
    public float _depth;

    public TrailPoint(Vector3 pos, float depth, TrailPoint backPoint)
    {
        _pos = pos;
        _depth = depth;
        _backPoint = backPoint;
    }

    public void UpdatePos()
    {
        if (_backPoint == null)
            return;

        _pos = _backPoint._pos;
        _backPoint.UpdatePos();
    }
}
