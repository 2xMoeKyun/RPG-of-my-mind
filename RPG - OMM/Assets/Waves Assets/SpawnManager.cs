using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] enemyRef;

    public Text currentWave;

    private bool reload;

    public int wavesCount;

    private int spawnCount;
    public int limitSpawn;

    private void Start()
    {
        UpdateValue();
    }


    public void UpdateValue()
    {
        currentWave.gameObject.SetActive(true);
        currentWave.text = "Wave " + (wavesCount + 1).ToString();
        StartCoroutine(CoolDown());
    }


    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(3f);
        currentWave.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!reload && spawnCount != limitSpawn)
        {
            spawnCount++;
            StartCoroutine(Spawn());
            reload = true;
        }
        else if (reload && spawnCount == limitSpawn)
        {
            wavesCount++;
            spawnCount = 0;
            limitSpawn += 5;
            UpdateValue();
            Debug.Log("UpdateValue");
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
