using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public int loadSceneNumber;

    public void TransitionTrigger(int number)
    {
        SceneManager.LoadScene(number);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TransitionTrigger(loadSceneNumber);
        }
    }
}
