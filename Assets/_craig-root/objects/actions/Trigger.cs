using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public insideai.Action action;
    void OnTriggerEnter(Collider other) {
        if (action != null)
        {
            action(other.gameObject);
        }
    }
}
