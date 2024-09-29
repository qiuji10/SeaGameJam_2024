using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class CameraManager : MonoBehaviour
{
    [SerializeField] public GameObject currentActiveCamera;
    [SerializeField] private SerializedDictionary<ColliderCallback, GameObject> _camerasDict;

    private void Awake()
    {
        foreach (var item in _camerasDict)
        {
            if (currentActiveCamera == item.Value)
                item.Value.SetActive(true);
            else
                item.Value.SetActive(false);

            var callback = item.Key;
            callback.OnEnter += OnEnterCameraRegion;
        }
    }

    private void OnEnterCameraRegion(ColliderCallback info)
    {
        if (currentActiveCamera == _camerasDict[info])
            return;

        var prevActiveCamera = currentActiveCamera;
        currentActiveCamera = _camerasDict[info];
        currentActiveCamera.SetActive(true);
        prevActiveCamera.SetActive(false);
    }
}
