using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public void StartGamePlay()
    {
        Time.timeScale = 1f;
    }

    public void StopGamePlay()
    {
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EndGame()
    {
        Application.Quit();
    }
  
}
