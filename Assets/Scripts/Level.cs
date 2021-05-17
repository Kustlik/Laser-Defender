using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] int loadDelayInSec = 2;

    public void LoadGameOver()
    {
        StartCoroutine(LoadDelay());
    }

    private IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(loadDelayInSec);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
        if (FindObjectOfType<GameSession>())
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
