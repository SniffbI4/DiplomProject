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
    [SerializeField] private Weapon[] weapons;

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

        if (PlayerPrefs.HasKey("WEAPON"))
        {
            SpawnCharacter(PlayerPrefs.GetString("WEAPON"));
        }
    }

    public void SpawnCharacter (string weaponName)
    {
        Debug.Log($"Weapon name in reg: {weaponName}");
        int weaponIndex=0;
        for (int i=0; i<weapons.Length; i++)
        {
            Debug.Log($"{weapons[i].weaponName}");
            if (weapons[i].weaponName == weaponName)
            {
                weaponIndex = i;
                break;
            }
        }

        camera.Follow = Player.transform;
        camera.LookAt = Player.transform;
        
        Player.SetActive(false);

        GameObject SpecialWeapon = Instantiate(weapons[weaponIndex].gameObject, weaponParent.transform);

        PlayerShoot playerShoot = Player.GetComponent<PlayerShoot>();
        playerShoot.SpecialWeapon = SpecialWeapon.GetComponent<Weapon>();

        Player.SetActive(true);
        EnemySpawner.instance.SetPlayerPosition(Player.transform);
    }
}
