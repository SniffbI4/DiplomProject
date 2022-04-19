using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{
    public static CharacterCreator instance = null;

    [SerializeField] private GameObject Player;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera camera;

    [SerializeField] private GameObject weaponParent;
    [SerializeField] private GameObject[] weapons;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        if (Player == null)
        {
            Debug.LogError("NO CHARACTER");
            return;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("WEAPON"))
        {
            SpawnCharacter(PlayerPrefs.GetInt("WEAPON"));
        }
    }

    public void SpawnCharacter (int weaponDrop)
    {
        int weaponIndex = weaponDrop;

        camera.Follow = Player.transform;
        camera.LookAt = Player.transform;
        
        Player.SetActive(false);

        GameObject SpecialWeapon = Instantiate(weapons[weaponIndex], weaponParent.transform);

        PlayerShoot playerShoot = Player.GetComponent<PlayerShoot>();
        playerShoot.SpecialWeapon = SpecialWeapon.GetComponent<Weapon>();

        Player.SetActive(true);
        EnemySpawner.instance.SetPlayerPosition(Player.transform);
    }
}
