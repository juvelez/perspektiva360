using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimationControl : MonoBehaviour {

    /// <summary>
    /// Swap animation button reference
    /// </summary>
    [SerializeField]
    [Tooltip("Botón usado para activar el intercambio de animaciones")]
    private Button buttonSwapAnimation;

    /// <summary>
    /// Animation control properties
    /// </summary>
    private Animator animController;
    private bool iddleState = true;


    /// <summary>
    /// On start find in children the animator for this object and start the "Idle" animation
    /// </summary>
    void Start() {
        animController = GetComponentInChildren<Animator>();
        animController.Play("Idle");
        buttonSwapAnimation.GetComponentInChildren<Text>().text = "Bailar";
    }

    /// <summary>
    /// Swaps animation, if the current animation is Iddle then start dancing animation, els return to the iddle loop
    /// Is best to use a Flag to control the current state rather than ask for the current animation name
    /// </summary>
    public void SwapAnimation() {
        if (iddleState) {
            iddleState = false;
            animController.Play("Dance");
            buttonSwapAnimation.GetComponentInChildren<Text>().text = "Parar";
        } else {
            iddleState = true;
            animController.Play("Idle");
            buttonSwapAnimation.GetComponentInChildren<Text>().text = "Bailar";
        }
    }

}
