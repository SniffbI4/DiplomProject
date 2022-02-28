using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerMovement), typeof(PlayerShoot))]
public class PlayerInput : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerShoot playerShoot;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    private void Update()
    {
        //Стрельба
        if (Input.GetMouseButton(0))
        {
            playerShoot.Fire();
        }

        //Перезарядка
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerShoot.ReloadWeapon();
        }

        //Смена оружия
        float mouseWheel = Input.GetAxis(GameData.MOUSE_SCROLL);
        if (mouseWheel >= 0.1f || mouseWheel <= -0.1f)
            playerShoot.ChangeWeapon();

        //Перемещение персонажа
        float x = Input.GetAxis(GameData.HORIZONTAL_AXIS);
        float y = Input.GetAxis(GameData.VERTICAL_AXIS);

        //Направление прицела
        Vector3 mousePosition = Input.mousePosition;
        playerMovement.Move(x, y, mousePosition);

        //Проверка (тесты)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnemySpawner.instance.SpawnEnemy();
        }
    }
}
