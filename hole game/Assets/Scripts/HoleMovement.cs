using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    private Mesh _groundMesh;
    private Vector3[] meshVerts;
    [SerializeField] private Transform holeCenter;
    [SerializeField] private float radius;

    struct VertData
    {
        public Vector3 Position;
        public bool IsHole;
    }

    private VertData[] _vertsData;
    
    private void Start()
    {
        SetHoleVerts();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    private void MoveHole(Vector3 pos)
    {
        ChangeHoleVertsPos(pos);
        ChangeHolePos();
    }

    private void SetHoleVerts()
    {
        _groundMesh = GetComponentInChildren<MeshFilter>().mesh;
        meshVerts = _groundMesh.vertices;
        _vertsData = new VertData[meshVerts.Length];

        for (int i = 0, length = meshVerts.Length; i < length; i++)
        {
            if (IsHoleVert(meshVerts[i]))
            {
                _vertsData[i].Position = meshVerts[i];
                _vertsData[i].IsHole = true;
            }
            else
            {
                _vertsData[i].Position = meshVerts[i];
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
                meshVerts[i] = _vertsData[i].Position;
            }
        }

        _groundMesh.vertices = meshVerts;
    }

    private void ChangeHoleVertsPos(Vector3 pos)
    {
        for (int i = 0, length = _vertsData.Length; i < length; i++)
        {
            if (_vertsData[i].IsHole)
            {
                _vertsData[i].Position = pos;
            }
        }
    }
}
