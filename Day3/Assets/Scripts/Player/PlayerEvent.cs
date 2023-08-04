using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEvent : MonoBehaviour
{
    public Collider2D colliderPlayer;
    public void ColliderOn(int i)
    {
        if (i == 1)
        {
            colliderPlayer.gameObject.SetActive(true);
        }
        else if (i == 0)
        {
            colliderPlayer.gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Load()
    {
        SceneManager.LoadScene(0);
    }
}
