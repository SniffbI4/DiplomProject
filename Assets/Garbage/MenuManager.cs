using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneTransition.SwitchToScene(0);
        }
    }

    public void GoToGame ()
    {
        SceneTransition.SwitchToScene(1);
    }
}
