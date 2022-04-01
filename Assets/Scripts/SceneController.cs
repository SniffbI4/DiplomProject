using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Dropdown weapon;

    private void Awake()
    {
        Time.timeScale = 0;
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("WEAPON", weapon.value);
        Time.timeScale = 1;
    }
}
