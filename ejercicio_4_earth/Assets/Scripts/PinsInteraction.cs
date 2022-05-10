using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinsInteraction : MonoBehaviour {

    public string countryName;
    public string description;
    public string link;
    public Sprite mapPreviewSprite;
    public InfoPanelManager infoPanel;

    private Vector3 currentScale;

    private void Start() {
        currentScale = gameObject.transform.localScale;
    }

    /// <summary>
    /// This allow the this GameObject Always faces camera creating a Billboarding Effect
    /// </summary>
    void Update() {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    private void OnMouseEnter() {
        if (!infoPanel.isOpen) gameObject.transform.localScale = currentScale * 1.2f;
    }

    private void OnMouseExit() {
        if (!infoPanel.isOpen) gameObject.transform.localScale = currentScale;
    }

    private void OnMouseDown() {
        if (!infoPanel.isOpen) {
            Debug.Log("Clicked Pin: " + gameObject.name);
            infoPanel.ShowInfoPanel(countryName, description, link, mapPreviewSprite);
        }
    }

}


