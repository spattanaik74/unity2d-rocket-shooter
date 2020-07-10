using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    //Config para
    [SerializeField] float delayInSecond = 2f;


    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game Play 1");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGameOver()
    {
       StartCoroutine (LoadGame());
    }
    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(delayInSecond);

        SceneManager.LoadScene("Game Over");
    }

}
