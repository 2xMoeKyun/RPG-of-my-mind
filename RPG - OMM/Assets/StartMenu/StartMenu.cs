using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject control;
    public Camera camera;
    public GameObject player;

    private bool start;
    private float k;
    private void Update()
    {
        if (start)
        {
            k += Time.deltaTime;
            Debug.Log(k);
            
            camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10) * k;
            if (k >= 1)
            {
                start = false;
                Move.playerAnimator.SetTrigger("Use");
                StartCoroutine(Load());
            }
            if (camera.orthographicSize > 2)
            {
                camera.orthographicSize = camera.orthographicSize - Time.deltaTime*3;
            }
            
        }
    }

    public void StartButton()
    {
        player.GetComponent<Move>().DisablePlayer();
        transform.parent.GetComponent<Canvas>().enabled = false;
        start = true;
        
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(0);
    }

    public void ShowControl()
    {
        control.SetActive(true);
    }

    public void HideControl()
    {
        control.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
