using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelLoader", menuName = "Custom/LevelLoader")]
public class LevelLoader : ScriptableObject
{
    public LevelData[] levels;
    public string[] bossScenes;
    private int currentLevelIndex = -1;
    private int[] levelIndices;

    public void LoadRandomLevel()
    {
        if (levelIndices == null || levelIndices.Length == 0)
        {
            GenerateRandomLevelIndices();
        }

        currentLevelIndex++;
        if (currentLevelIndex >= levelIndices.Length)
        {
            int randomBossIndex = Random.Range(0, bossScenes.Length);
            SceneManager.LoadScene(bossScenes[randomBossIndex]);
        }
        else
        {
            int levelIndex = levelIndices[currentLevelIndex];
            SceneManager.LoadScene(levels[levelIndex].sceneName);
        }
    }

    public void LevelCompleted()
    {
        LoadRandomLevel();
    }

    private void GenerateRandomLevelIndices()
    {
        levelIndices = new int[levels.Length];
        for (int i = 0; i < levels.Length; i++)
        {
            levelIndices[i] = i;
        }

        // Shuffle the level indices using Fisher-Yates algorithm
        for (int i = 0; i < levels.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, levels.Length);
            int temp = levelIndices[i];
            levelIndices[i] = levelIndices[randomIndex];
            levelIndices[randomIndex] = temp;
        }

        currentLevelIndex = -1; // Reset the current level index
    }
}
