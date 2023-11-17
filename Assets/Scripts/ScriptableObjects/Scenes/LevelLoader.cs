using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelLoader", menuName = "Custom/LevelLoader")]
public class LevelLoader : ScriptableObject
{

    public LevelData[] baseLevels; // Levels that are not boss levels
    public string[] bossScenes;    // Boss scenes

    private int currentLevelIndex = -1;
    private int[] levelIndices;
    public int bossAppearcanceIndex;

    public void LoadNextLevel()
    {
       
        currentLevelIndex++;
        if (currentLevelIndex % bossAppearcanceIndex == (bossAppearcanceIndex-1))
        {
            // If all base levels are completed, load a random boss scene
            int randomBossIndex = Random.Range(0, bossScenes.Length);
            SceneManager.LoadScene(bossScenes[randomBossIndex]);
        }
        else
        {
            
            int levelIndex = levelIndices[currentLevelIndex];
            SceneManager.LoadScene(baseLevels[levelIndex].sceneName);
        }
    }

    public void LevelCompleted()
    {
        LoadNextLevel();
    }

    

    public void StartGame()
    {
        GenerateRandomLevelIndices();
        LoadNextLevel();
    }

    private void GenerateRandomLevelIndices()
    {
        levelIndices = new int[baseLevels.Length];
        for (int i = 0; i < baseLevels.Length; i++)
        {
            levelIndices[i] = i;
        }

        // Shuffle the level indices using Fisher-Yates algorithm
        for (int i = 0; i < baseLevels.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, baseLevels.Length);
            int temp = levelIndices[i];
            levelIndices[i] = levelIndices[randomIndex];
            levelIndices[randomIndex] = temp;
        }

        currentLevelIndex = -1; // Reset the current level index
    }
}
