using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePulse : MonoBehaviour {

    /// <summary>
    /// Speed property to adjust thepusating speed of the placement indicator icon
    /// </summary>
    [Tooltip("Ajustar la velocidad de la pulsación en fracciones de 1.0")]
    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float speed = 1.0f;

    /// <summary>
    /// The material and its colro must be stored to create a transparency pulsating FX
    /// </summary>
    private Material material;
    private Color color;

    // Start is called before the first frame update
    void Start() {
        initializeIcon();
    }

    /// <summary>
    /// Method that must be excecutes as soon as possible on this script startup to initialize animation properties
    /// </summary>
    private void initializeIcon() {
        transform.localScale = new Vector3(0, 0, 0);
        //material = GetComponent<Renderer>().material;
        //color = material.color;
    }

    /// <summary>
    /// This animation is based on regular updates, here are adjusted the scale and alpha according to the current FPS
    /// This generate a pulsating effect on the GameObjet, the material must allow transparency
    /// </summary>
    void Update() {
        float currentScale = transform.localScale.x;
        currentScale += speed * Time.deltaTime;
        if (currentScale >= 1) currentScale = 0;
        transform.localScale = new Vector3(currentScale, 1, currentScale);
        //color.a = 1 - currentScale;
        //material.color = color;
    }
}
