using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject quitGamePanel;
    [SerializeField] private Button quitGameBtn;

    private void Awake()
    {
        quitGameBtn.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitGamePanel.SetActive(true);
        }
    }

    private void StartGame()
    {
        SceneLoader.Load(EScene.Level_1);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
