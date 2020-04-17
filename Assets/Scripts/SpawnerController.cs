using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    GameMaster gm;
    ObjectPooler objectPooler;

    void Start()
    {
        gm = GameMaster.GM;
        objectPooler = ObjectPooler.Instance;
    }

    public void SpawnEnemy()
    {
            if (gm.killedEnemies % 5 == 0 && gm.killedEnemies > 0)
            {
                gm.activeEnemies++;
                objectPooler.SpawnFromPool("BigEnemy", transform.position, transform.rotation);
            }
            else
            {
                gm.activeEnemies++;
                objectPooler.SpawnFromPool("EnemyHuman", transform.position, transform.rotation);
            }
        }
}
