using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
