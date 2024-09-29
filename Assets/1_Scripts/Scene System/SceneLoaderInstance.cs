using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderInstance : MonoBehaviour
{
    [SerializeField] private EScene sceneToLoad;
    public void LoadSpecifiedScene() => SceneLoader.Load(sceneToLoad);
}
