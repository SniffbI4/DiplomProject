using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponInMainMenu : MonoBehaviour
{
    [SerializeField] private Text advWeaponName;

    private Button startGameButton;

    private void Awake()
    {
        startGameButton = GetComponent<Button>();
        startGameButton.interactable = false;
    }

    public void NewGame()
    {
        PlayerPrefs.SetString("WEAPON", advWeaponName.text);
    }

    private void Update()
    {
        if (advWeaponName.text != string.Empty & startGameButton.interactable==false)
        {
            startGameButton.interactable = true;
        }
    }
}
