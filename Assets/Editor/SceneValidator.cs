using GameplayDependencies;
using SpawnerAssembly;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SceneValidator 
{ 
    static SceneValidator()
    {
        EditorSceneManager.sceneSaving += OnSceneSaving;
    }

    private static void OnSceneSaving(Scene scene, string path)
    {
        if (scene.name == "SampleScene")
        {
            CheckMissingData();
        }
    }
    
    private static void CheckMissingData()
    {
        var gameplayLoader = Object.FindFirstObjectByType<GameplayLoader>();

        CollectEnemySpawnPoints(gameplayLoader);
    }

    private static void CollectEnemySpawnPoints(GameplayLoader gameplayLoader)
    {
        var enemySpawnPoints = Object.FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None);
        gameplayLoader.CollectSpawnPoint(enemySpawnPoints);
    }
}
