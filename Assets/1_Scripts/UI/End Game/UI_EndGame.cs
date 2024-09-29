using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TMP_Text titleText;

    private PlayerMovement player;

    public static UI_EndGame instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        player = FindAnyObjectByType<PlayerMovement>();
    }

    public void ShowWinPanel()
    {
        titleText.color = Color.green;
        titleText.SetText("You Win!");

        player.enabled = false;
        endGamePanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        titleText.color = Color.red;
        titleText.SetText("You Lose!");

        player.enabled = false;
        endGamePanel.SetActive(true);
    }
}
