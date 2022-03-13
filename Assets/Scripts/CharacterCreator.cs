using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterMeshes
{
    public GameObject mesh;
    public Avatar avatar;
}

public class CharacterCreator : MonoBehaviour
{
    public static CharacterCreator instance = null;

    [SerializeField] private GameObject MainCharacter;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera camera;

    [SerializeField] private GameObject[] weapons;
    [SerializeField] private CharacterMeshes[] meshes;

    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        if (MainCharacter == null)
        {
            Debug.LogError("NO CHARACTER");
            return;
        }
    }

    private void Start()
    {
        int mesh = PlayerPrefs.GetInt("MESH");
        int weapon = PlayerPrefs.GetInt("WEAPON");

        SpawnCharacter(mesh, weapon);
    }

    public void SpawnCharacter (int meshDrop, int weaponDrop)
    {
        Debug.Log("SPAWN");
        int meshIndex = meshDrop;
        int weaponIndex = weaponDrop;

        GameObject Player = Instantiate(MainCharacter);
        camera.Follow = Player.transform;
        camera.LookAt = Player.transform;
        
        Player.SetActive(false);

        animator = Player.GetComponent<Animator>();
        animator.avatar = meshes[meshIndex].avatar;
        GameObject Mesh = Instantiate(meshes[meshIndex].mesh, Player.transform);
        GameObject SpecialWeapon = Instantiate(weapons[weaponIndex], Player.transform);

        PlayerShoot playerShoot = Player.GetComponent<PlayerShoot>();
        playerShoot.SpecialWeapon = SpecialWeapon.GetComponent<Weapon>();

        Player.SetActive(true);
        EnemySpawner.instance.SetPlayerPosition(Player.transform);
    }
}
