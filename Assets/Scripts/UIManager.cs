using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Sprite[] livesSprites;
    
    [SerializeField]
    private Image livesImage;

    private GameManager gameManager;

    [SerializeField]
    private Text waveCompleted;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        waveCompleted.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreText.text = "Score: ";

        if(gameManager == null)
        {
            Debug.LogError("GameManager is null");
        }
    }


    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = livesSprites[currentLives];

        if (currentLives < 1)
            GameOverSequence();
    }

    void GameOverSequence()
    {
        gameOverText.gameObject.SetActive(true);
        gameManager.GameOver();
    }

<<<<<<< Updated upstream
=======
    public void ShowLowAmmo()
    {
        ammoText.gameObject.SetActive(true);
        StartCoroutine(DisableTextRoutine());
    }

    public void ShowThrusterDepleted()
    {
        thrusterDepleted.gameObject.SetActive(true);
        StartCoroutine(DisableTextRoutine());
    }

    public void ShowWaveCompletedText()
    {
        waveCompleted.gameObject.SetActive(true);
        StartCoroutine(DisableWaveCompleteTextRoutine());
    }

    public void HideThrusterText()
    {
        thrusterDepleted.gameObject.SetActive(false);
    }

    IEnumerator DisableWaveCompleteTextRoutine()
    {
        yield return new WaitForSeconds(5);
        waveCompleted.gameObject.SetActive(false);
    }
    IEnumerator DisableTextRoutine()
    {
        yield return new WaitForSeconds(5);
        ammoText.gameObject.SetActive(false);
    }
>>>>>>> Stashed changes
}
