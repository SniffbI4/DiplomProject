using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Dropdown mesh;
    [SerializeField] private Dropdown weapon;

    private void Awake()
    {
        Time.timeScale = 0;
    }
    public void NewGame()
    {
        //meshInt = mesh.value;
        //weaponInt = weapon.value;

        PlayerPrefs.SetInt("MESH", mesh.value);
        PlayerPrefs.SetInt("WEAPON", weapon.value);
        Time.timeScale = 1;

        //SceneTransition.SwitchToScene(1);

        //loadingOperation = SceneManager.LoadSceneAsync("New Scene");
        //loadingOperation.allowSceneActivation = false;
        //StartCoroutine(Delay());
    }
}
