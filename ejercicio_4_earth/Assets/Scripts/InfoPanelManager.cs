using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// This behaviour must be attached to the Panel GUI
/// </summary>
public class InfoPanelManager : MonoBehaviour {

    /// <summary>
    /// Public members to store gui references that will be filled depending on the user selections
    /// Because this is NOT a base class there is no need to protect and serialize this members, simplifying development
    /// </summary>
    public RectTransform infoCanvas;
    public Text countryTitleText;
    public Text countryDescriptionText;
    public Image mapPreviewImage;

    /// <summary>
    /// Thsi flag is needed public but is not needed in the Inspector so must be hidden
    /// </summary>
    [HideInInspector]
    public bool isOpen;

    /// <summary>
    /// To store the link to visit
    /// </summary>
    private string visitLink;

    /// <summary>
    /// Reset the scene on excecution start
    /// </summary>
    private void Start() {
        resetScene();
    }

    /// <summary>
    /// Reset the scene to the starting points and values
    /// </summary>
    private void resetScene() {
        isOpen = false;
        infoCanvas.anchoredPosition = new Vector3(900, 0, 0);
        Camera.main.transform.position = new Vector3(0, 0, -6.22f);
    }

    /// <summary>
    /// This method is excecuted by every pin points on the globe
    /// each one has been configured on th einspector with the Country, Info, Link and a Map Preview
    /// </summary>
    /// <param name="countryTitle">A string with the country name for the title</param>
    /// <param name="description">A string with the country description</param>
    /// <param name="link">A string with the Google MAps link to visit</param>
    /// <param name="mapPreview">A Sprite to show as a map preview</param>
    public void ShowInfoPanel(string countryTitle, string description, string link, Sprite mapPreview) {
        Debug.Log("Open Info Panel");

        countryTitleText.text = countryTitle;
        countryDescriptionText.text = description;
        visitLink = link;
        mapPreviewImage.sprite = mapPreview;

        isOpen = true;
        //infoCanvas.anchoredPosition = new Vector3(-32, 0, 0);
        infoCanvas.DOAnchorPosX(-32, 0.9f).SetEase(Ease.OutExpo);

        //Camera.main.transform.position = new Vector3(2, 0, -5.21f);
        Camera.main.transform.DOLocalMove(new Vector3(2, 0, -5.21f), 0.9f).SetEase(Ease.OutExpo);

    }

    /// <summary>
    /// This method is called by the "Cerrar" button to close the information panel
    /// </summary>
    public void HideInfoPanel() {
        Debug.Log("Close Info Panel");
        isOpen = false;
        //infoCanvas.anchoredPosition = new Vector3(900, 0, 0);
        infoCanvas.DOAnchorPosX(902, 0.5f).SetEase(Ease.InExpo);

        //Camera.main.transform.position = new Vector3(0, 0, -6.22f);
        Camera.main.transform.DOLocalMove(new Vector3(0, 0, -6.22f), 1f).SetEase(Ease.InOutExpo);
    }

    /// <summary>
    /// This method is excecuted by the "Visitar" button to navigate on a new Tab to the given country on Google Maps
    /// </summary>
    public void VisitCountry() {
        Debug.Log("Visit Link:" +  visitLink);
        Application.OpenURL(visitLink);
    }

}
