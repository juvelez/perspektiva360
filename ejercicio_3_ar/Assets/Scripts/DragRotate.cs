using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotate : MonoBehaviour {

    /// <summary>
    /// Propeties used for rotation calculations
    /// </summary>
    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion yRotation;
    private float multiplier = 0.5f;

    /// <summary>
    /// This roperty is used to store a rotation help icon reference
    /// </summary>
    [SerializeField]
    [Tooltip("The UI image with the Rotation Help Icon Assigned")]
    private GameObject rotateHelpIcon;

    /// <summary>
    /// In the update cycle the touch events are verified if the first one has moved then
    /// Hide the rotation help icon, is it is visible
    /// Calculate an Euler angle for the rotation ammount according to the finger position in the X Axis with a speed multiplier
    /// </summary>    
    void Update() {
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) {
                hideRotateIcon();
                yRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * multiplier, 0f);
                transform.rotation = yRotation * transform.rotation;
            }
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
