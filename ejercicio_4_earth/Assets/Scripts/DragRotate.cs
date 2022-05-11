using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The globe parent GameObject has this behaviour attached to it
/// </summary>
public class DragRotate : MonoBehaviour {

    /// <summary>
    /// Propeties used for rotation calculations
    /// </summary>
    private Quaternion yRotation;
    private float multiplier = 2f;

    /// <summary>
    /// This roperty is used to store a rotation help icon reference
    /// </summary>
    [SerializeField]
    [Tooltip("The UI image with the Rotation Help Icon Assigned")]
    private GameObject rotateHelpIcon;

    /// <summary>
    /// In the update cycle the mouse movement is verified
    /// Hide the rotation help icon, if is visible
    /// Calculate an Euler angle for the rotation ammount according to the mouse position in the X Axis with a sensitivity multiplier
    /// </summary>    
    void Update() {
        if (Input.GetMouseButton(0)) {
            hideRotateIcon();
            float deltaX = Input.GetAxis("Mouse X");
            yRotation = Quaternion.Euler(0f, -deltaX * multiplier, 0f);
            transform.rotation = yRotation * transform.rotation;
        }
    }

    /// <summary>
    /// Hides the rotate help icon if it is visible
    /// </summary>
    private void hideRotateIcon() {
        if (rotateHelpIcon.activeSelf) {
            rotateHelpIcon.SetActive(false);
        }
    }
}
