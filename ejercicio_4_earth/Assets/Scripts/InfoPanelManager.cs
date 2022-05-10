using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoPanelManager : MonoBehaviour {

    public RectTransform infoCanvas;
    public Text countryTitleText;
    public Text countryDescriptionText;
    public Image mapPreviewImage;

    [HideInInspector]
    public bool isOpen;

    private string visitLink;

    private void Start() {
        resetScene();
    }

    private void resetScene() {
        isOpen = false;
        infoCanvas.anchoredPosition = new Vector3(900, 0, 0);
        Camera.main.transform.position = new Vector3(0, 0, -6.22f);
    }

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

    public void HideInfoPanel() {
        Debug.Log("Close Info Panel");
        isOpen = false;
        //infoCanvas.anchoredPosition = new Vector3(900, 0, 0);
        infoCanvas.DOAnchorPosX(902, 0.5f).SetEase(Ease.InExpo);

        //Camera.main.transform.position = new Vector3(0, 0, -6.22f);
        Camera.main.transform.DOLocalMove(new Vector3(0, 0, -6.22f), 1f).SetEase(Ease.InOutExpo);
    }

    public void VisitCountry() {
        Debug.Log("Visit Link:" +  visitLink);
        Application.OpenURL(visitLink);
    }

}
