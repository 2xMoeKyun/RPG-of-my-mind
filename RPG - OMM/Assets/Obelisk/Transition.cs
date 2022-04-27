using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [Header("Scene index")]
    public int loadSceneNumber;
    [Header("Move GameObjects To Scene")]
    public GameObject[] Objects;




    public void TransitionTrigger(int number)
    {
        SceneManager.LoadScene(number);
        for (int i = 0; i < Objects.Length; i++)
        {
            TransitionManager.gameObjects[i] = Objects[i];
        }
        TransitionManager.isTransit = true;
        Destroy(this);
    }

    public void PushingObjects(GameObject[] gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (TransitionManager.isTransit)
        {
            PushingObjects(TransitionManager.gameObjects);
            TransitionManager.isTransit = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !TransitionManager.isTransit)
        {
            TransitionTrigger(loadSceneNumber);
        }
    }
}
