using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField] Text ammoInCase;
    [SerializeField] Text allAmmo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }

    public void ShowAmmo (int ammoInCase, int allAmmo)
    {
        this.ammoInCase.text = ammoInCase.ToString();
        this.allAmmo.text = allAmmo.ToString();
    }
}
