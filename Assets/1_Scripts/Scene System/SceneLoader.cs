using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using AYellowpaper.SerializedCollections;

public enum EScene
{
    MainMenu = 0,
    Level_1 = 1,
}

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<EScene, string> sceneValues = new();
    [SerializeField] private UI_Fade fadingUI;

    public static event Action<EScene> OnLoadSceneWithEnum;
    public static event Action<string> OnLoadSceneWithString;

    private static SceneLoader _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnLoadSceneWithEnum += LoadScene;
        OnLoadSceneWithString += LoadScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        OnLoadSceneWithEnum -= LoadScene;
        OnLoadSceneWithString -= LoadScene;
    }

    public static void Load(EScene scene) => OnLoadSceneWithEnum?.Invoke(scene);
    public static void Load(string scene) => OnLoadSceneWithString?.Invoke(scene);

    public void LoadScene(EScene scene)
    {
        if (!sceneValues.ContainsKey(scene))
        {
            Debug.LogWarning($"No binding scene found from: {scene}");
            return;
        }

        StartCoroutine(FadeAndLoad());

        IEnumerator FadeAndLoad()
        {
            fadingUI.Show(1);
            yield return new WaitForSeconds(fadingUI.Duration);
            SceneManager.LoadScene(sceneValues[scene]);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad());

        IEnumerator FadeAndLoad()
        {
            fadingUI.Show(1);
            yield return new WaitForSeconds(fadingUI.Duration);
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadingUI.Hide(1);
    }
}
