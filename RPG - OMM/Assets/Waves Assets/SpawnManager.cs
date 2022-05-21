using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] enemyRef;

    private bool reload;
    private void Update()
    {
        if (!reload)
        {
            StartCoroutine(Spawn());
            reload = true;
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
