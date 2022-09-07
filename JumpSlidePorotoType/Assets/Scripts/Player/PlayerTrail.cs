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
        for (int i = maxPoint; i > 0; i--)
        {
            float depth = Mathf.Lerp(transform.position.z, player.position.z, (float)i / maxPoint);

            curTrailPoint = new TrailPoint(transform.position, depth, lastTrailPoint);
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
