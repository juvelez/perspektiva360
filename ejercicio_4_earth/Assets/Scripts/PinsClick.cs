using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an alternative to the current CLICK implementation using RayCasting
/// Because this app is only for Web the current implementation could more suitable
/// But this works fine too and relies on Physics and RayCasting
/// It could be better to use this implementation if this app is going to be Mobile 
/// and need to implement Touching instead of Clicking
/// This must be attached to the Globe parent
/// </summary>

public class PinsClick : MonoBehaviour {

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                doPointerAction(hit.transform.gameObject);
            }
        }
    }

    private void doPointerAction(GameObject target) {
        Debug.Log(target.name);
        switch (target.name) {
            case "PinColombia":
                break;
            case "PinBrazil":
                break;
            case "PinChina":
                break;
        }
    }
}
