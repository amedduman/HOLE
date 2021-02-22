using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    private Vector3 _initialPos;
    private Quaternion _initialRot;
    private Transform _myTransform;
    private void Awake()
    {
        _myTransform = gameObject.transform;
        _initialPos = _myTransform.localPosition;
        _initialRot = _myTransform.localRotation;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        _myTransform.localPosition = _initialPos;
        _myTransform.localRotation = _initialRot;
    }
}
