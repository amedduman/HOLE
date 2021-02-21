using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    private Vector3 _initialPos;
    private Quaternion _initialRot;
    private Transform _tr;

    private void Awake()
    {
        _tr = transform.GetChild(0).transform;
    }

    private void OnEnable()
    {
        _initialPos = _tr.localPosition;
        _initialRot = _tr.rotation;
    }

    private void OnDisable()
    {
        _tr.localPosition = _initialPos;
        _tr.localRotation = _initialRot;
    }
}
