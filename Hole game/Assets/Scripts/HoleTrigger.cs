using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collectable"))
        {
            Debug.Log("collect");
        }
        else if (other.gameObject.CompareTag("damaging"))
        {
            Debug.Log( "damage");
        }
    }
}
