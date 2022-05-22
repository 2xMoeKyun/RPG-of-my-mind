using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] enemyRef;

    private bool reload;

    public int wavesCount;

    public int spawnCount;
    public int limitSpawn;
    private void Update()
    {
        if (!reload && spawnCount != limitSpawn)
        {
            spawnCount++;
            StartCoroutine(Spawn());
            reload = true;
        }
        else
        {
            wavesCount++;
            spawnCount = 0;
            limitSpawn += 10;
        }
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(4f);
        GameObject copy = Instantiate(enemyRef[(int)Random.Range(0, (float)(enemyRef.Length ))]);
        copy.transform.position = spawnPoints[(int)Random.Range(0, (float)(spawnPoints.Length))].transform.position;
        reload = false;
    }
}
