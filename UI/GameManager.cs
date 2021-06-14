using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public bool gameOver;
    public GameObject gameOverPanel;
    public int bricksLeft;
    public Transform[] levels;
    public int currentLevelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives : " + lives;
        scoreText.text = "Score : " + score;
        bricksLeft = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void UpdateLives(int live)
    {
        lives += live;
        
        if(lives < 0)
        {
            lives = 0;
            GameOver();
        }
        livesText.text = "Lives : " + lives;
    }

    public void UpdateScoreGain(int value)
    {
        score += value;
        scoreText.text = "Score : " + score;
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game quitted");
    }

    public void UpdateBricksLeft()
    {
        bricksLeft--;
        if(bricksLeft <= 0)
        {
            if (currentLevelIndex >= levels.Length - 1)
            {
                GameOver();
            }
            else
            {
                gameOver = true;
                Invoke("LoadLevel", 3);
            }
        }
    }

    public void LoadLevel()
    {
        currentLevelIndex++;
        lives = 3;
        score = 0;
        Transform nextLevel = Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        bricksLeft = GameObject.FindGameObjectsWithTag("Brick").Length;
        gameOver = false;
    }

}
