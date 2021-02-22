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

    private void Start()
    {
        // GameManager.Instance.OnWaitingForNextLevel += () =>
        //     GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        // GameManager.Instance.OnLoadNextLevel += () => 
        //     GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    private void OnEnable()
    {
        _initialPos = _tr.localPosition;
        _initialRot = _tr.rotation;
        GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    private void OnDisable()
    {
        _tr.localPosition = _initialPos;
        _tr.localRotation = _initialRot;
    }
}
