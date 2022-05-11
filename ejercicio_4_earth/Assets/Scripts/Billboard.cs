using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Each pin on the globe has this behaviour attached to it allowing Billboarding
/// </summary>
public class Billboard : MonoBehaviour {

    /// <summary>
    /// This allow the this GameObject Always faces camera creating a Billboarding Effect
    /// </summary>
    void Update() {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

}
