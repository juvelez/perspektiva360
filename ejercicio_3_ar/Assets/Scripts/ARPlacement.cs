using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//using DG.Tweening;

public class ARPlacement : MonoBehaviour {

    /// <summary>
    /// The following properties could be private and serialized fields but because there is not subclassing
    /// it is pretty much economic and faster declaring them public
    /// </summary>
    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    public ARRaycastManager aRRaycastManager;
    public ARPlaneManager planeManager;
    public GameObject HelpIcon;

    private GameObject spawnedObject;
    private Pose PlacementPose;
    private bool placementPoseIsValid = false;

    /// <summary>
    /// In the update cycle we must update the placement indicator, placement pose and spawn object
    /// </summary>
    void Update() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            if (spawnedObject == null) {
                if (placementPoseIsValid) {
                    ARPlaceObject();
                }
            } else {
                // This could be used to further implement model reposition, it is incomplete here
                //Destroy(spawnedObject);
                //spawnedObject = null;
                //HelpIcon.SetActive(true);
            }
        }

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    /// <summary>
    /// Update the current pose position using the first intercepted generated plane by the Plane Manager
    /// If there are more than one hit it is a valid pose, then store it on a global property
    /// </summary>
    void UpdatePlacementPose() {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid) {
            PlacementPose = hits[0].pose;
        }
    }

    /// <summary>
    /// The indicatro graphic game Object must be constantly updated according to the current valid Placment pose
    /// Taking in count that there is not any spawned object yet
    /// </summary>
    void UpdatePlacementIndicator() {
        if (spawnedObject == null && placementPoseIsValid) {
            placementIndicator.SetActive(true);
        #if (UNITY_ANDROID)
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        #endif
        #if (UNITY_IOS)
			placementIndicator.transform.DOMove(PlacementPose.position, 0.25f);
        #endif
        } else {
            placementIndicator.SetActive(false);
        }
    }

    /// <summary>
    /// Finally spawn the prefab assigned and position it
    /// On iOS the given rotation is ignored
    /// </summary>
    void ARPlaceObject() {
        HelpIcon.SetActive(false);
        planeManager.subsystem.Stop();
        #if (UNITY_ANDROID)
            spawnedObject = Instantiate(arObjectToSpawn, PlacementPose.position, PlacementPose.rotation);
        #endif
        #if (UNITY_IOS)
			spawnedObject = Instantiate(arObjectToSpawn, PlacementPose.position, Quaternion.Euler(0, 0, 0));
        #endif
    }

    /// <summary>
    /// Cleanup when this object is destroyed, for experienced prebloms with reusing AR engine, the susbsystem must be stopped and disabled
    /// If this is not going to be used several times, could be not needed
    /// </summary>
    private void OnDestroy() {
        if (planeManager != null) {
            planeManager.subsystem.Stop();
            planeManager.enabled = false;
        }
    }


}

