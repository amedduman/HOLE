using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    private MeshFilter _groundMesh;
    [SerializeField] private Transform holeCenter;
    [SerializeField] private float radius;

    private List<Vector3> _holeVerts = new List<Vector3>();
    private void Start()
    {
        _groundMesh = GetComponentInChildren<MeshFilter>();
        var verts = _groundMesh.mesh.vertices;

        var colors = _groundMesh.mesh.colors32;

        for (int i = 0, length = verts.Length; i < length; i++)
        {
            if (!IsHoleVert(verts[i]))
                continue;
            _holeVerts.Add(verts[i]);
        }
        
    }

    private void ChangeHolePos()
    {
        // _groundMesh.mesh.vertices = verts;
    }

    private bool IsHoleVert(Vector3 vertPos)
    {
        return Vector3.SqrMagnitude(vertPos - (holeCenter.position)) < radius * radius;
    }
}
