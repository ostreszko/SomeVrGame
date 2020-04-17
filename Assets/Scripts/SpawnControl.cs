using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnControl : MonoBehaviour
{
    public List<SpawnerController> spawnControllers;
    System.Random rand;
    public int maxActiveEnemies = 4;
    int randResult;
    List<int> lastSpawnedEnemies;
    GameMaster gm;

    private void Start()
    {
        rand = new System.Random();
        gm = GameMaster.GM;
        lastSpawnedEnemies = new List<int>();
    }

    void Update()
    {
        if (gm.activeEnemies < maxActiveEnemies)
        {
            if (lastSpawnedEnemies.Count > maxActiveEnemies) lastSpawnedEnemies.RemoveAt(0);
            randResult = rand.Next(spawnControllers.Count);
            if(!lastSpawnedEnemies.Any(x => x == randResult))
            {
                spawnControllers[randResult].SpawnEnemy();
                lastSpawnedEnemies.Add(randResult);
            }
        }
    }
}
