using UnityEngine;
using UnityEngine.AI;
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
        PauseNavMeshAgents(false);
    }

    public void StopGamePlay()
    {
        Time.timeScale = 0f;
        PauseNavMeshAgents(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EndGame()
    {
        Application.Quit();
    }
    private void PauseNavMeshAgents(bool pause)
    {
        NavMeshAgent[] navMeshAgents = FindObjectsOfType<NavMeshAgent>();

        foreach (NavMeshAgent agent in navMeshAgents)
        {
            if (agent.isActiveAndEnabled) // Check if the NavMeshAgent is active and enabled
            {
                agent.isStopped = pause;
            }
        }
    }
}
