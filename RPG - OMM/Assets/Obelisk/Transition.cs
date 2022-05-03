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
        ManagerScene.SceneSwitch = true;
        Destroy(this);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !TransitionManager.isTransit)
        {
            TransitionTrigger(loadSceneNumber);
        }
    }
}
