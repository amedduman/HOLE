using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    [SerializeField] private Transform holeCenter;
    [SerializeField] private float radius;
    [SerializeField] [Range(.1f,1)] private float speed = 1;
    
    private Mesh _groundMesh;
    private Vector3[] _meshVerts;
    private Bounds _groundBounds;
    private MeshCollider _meshCollider;
    private Vector3 _initialPos;
    Vector3[] _initialVertsPos;

    struct VertData
    {
        public Vector3 Position;
        public bool IsHole;
    }

    private VertData[] _vertsData;

    private void OnEnable()
    {
        GameManager.Instance.OnLoadNextLevel += ResetHolePos;
    }

    private void Awake()
    {
        _meshCollider = GetComponentInChildren<MeshCollider>();
        _groundBounds = GetComponentInChildren<BoxCollider>().bounds;
        _groundMesh = GetComponentInChildren<MeshFilter>().mesh;
    }

    private void Start()
    {
        _initialPos = holeCenter.position;
        _initialVertsPos = _groundMesh.vertices;
        SetHoleVerts();
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;
        var x = Input.GetAxis("Mouse X") * speed;
        var y = Input.GetAxis("Mouse Y") * speed;
        var move = new Vector3(x,-y);
        if (!IsInBounds(move)) return;
        MoveHoleCenter(move);
        MoveHole(move);
    }

    private void ResetHolePos()
    {
        _groundMesh.vertices = _initialVertsPos;
        holeCenter.position = _initialPos;
        _meshCollider.sharedMesh = _groundMesh;
        
        for (int i = 0, length = _vertsData.Length; i < length; i++)
        {
            _vertsData[i].Position = _initialVertsPos[i];
        }
    }

    private bool IsInBounds(Vector3 move)
    {
        var pos = holeCenter.position + new Vector3(move.x,0,-move.y);

        return _groundBounds.Contains(pos);
    }

    private void MoveHoleCenter(Vector3 move)
    {
        holeCenter.position += new Vector3(move.x,0,-move.y);
    }

    private void MoveHole(Vector3 pos)
    {
        ChangeHoleVertsPos(pos);
        ChangeHolePos();
        _meshCollider.sharedMesh = _groundMesh;
    }

    private void SetHoleVerts()
    {
        _meshVerts = _groundMesh.vertices;
        _vertsData = new VertData[_meshVerts.Length];

        for (int i = 0, length = _meshVerts.Length; i < length; i++)
        {
            if (IsHoleVert(_meshVerts[i]))
            {
                _vertsData[i].Position = _meshVerts[i];
                _vertsData[i].IsHole = true;
            }
            else
            {
                _vertsData[i].Position = _meshVerts[i];
                _vertsData[i].IsHole = false;
            }
        }
    }
    
    private bool IsHoleVert(Vector3 vertPos)
    {
        return Vector3.SqrMagnitude(vertPos - (holeCenter.position)) < radius * radius;
    }

    private void ChangeHolePos()
    {
        for (int i = 0, length = _groundMesh.vertices.Length; i < length; i++)
        {
            if (_vertsData[i].IsHole)
            {
                _meshVerts[i] = _vertsData[i].Position;
            }
        }

        _groundMesh.vertices = _meshVerts;
    }

    private void ChangeHoleVertsPos(Vector3 pos)
    {
        for (int i = 0, length = _vertsData.Length; i < length; i++)
        {
            if (_vertsData[i].IsHole)
            {
                _vertsData[i].Position += pos;
            }
        }
    }
}
