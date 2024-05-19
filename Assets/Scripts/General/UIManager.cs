using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    //UI
    [SerializeField] private Text scoreGUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject optionsUI;
    GameManager gameManager;

    //GameOver scores
    [SerializeField] private Text gameOverScoreUI;
    [SerializeField] private Text gameOverHighscoreUI;

    //Options
    [SerializeField] private Slider volumeSlider;
    public AudioMixer audioMixer;

    private void Start()
    {
        scoreGUI.gameObject.SetActive(false);
        gameManager = GameManager.instance;
        gameManager.onGameOver.AddListener(ActivateGameOverUI);
    }

    public void PlayButtonHandler()
    {
        gameManager.StartGame();
        scoreGUI.gameObject.SetActive(true);
    }

    public void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true);

        gameOverScoreUI.text = "Score: " + gameManager.PrettyScore();
        gameOverHighscoreUI.text = "Highscore: " + gameManager.PrettyHighscore();
    }

    public void ActivateOptionsUI()
    {
        optionsUI.SetActive(true);
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }


    private void OnGUI()
    {
        scoreGUI.text = gameManager.PrettyScore();
    }
}
