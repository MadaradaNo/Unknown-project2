using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score Elements")]
    public int score;
    public int highscore;
    public Text scoreText;
    public Text highscoreText;

    [Header("GameOver")]
    public GameObject gameOverPanel;
    public Text gameOverPanelTextScore;
    public Text gameOverPanelTextHighScoreText;

    [Header("Sounds")]
    public AudioClip[] sliceSounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameOverPanel.SetActive(false);
        GetHighScore();
    }
   private void GetHighScore()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Best: " + highscore.ToString();
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
        if(score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = score.ToString();
        }
    }

    public void OnBombHit()
    {
        Time.timeScale = 0;

        gameOverPanelTextScore.text = "Score: " + score.ToString();
        gameOverPanelTextHighScoreText.text = "Best: " + highscore.ToString();
        gameOverPanel.SetActive(true);
        Debug.Log("Bomb hit");
    }
    public void RestartGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameOverPanel.SetActive(false);
        
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(g);
        }
        Time.timeScale = 1;
    }
    
    public void PlayRandomSliceSound()
    {
        AudioClip randomSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }
}
