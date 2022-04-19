using UnityEngine;
using UnityEngine.UI;
using RengeGames.HealthBars;

public class HealthPlayerUI : HealthUI
{
    [SerializeField] private Image progressBar;

    public override void ShowUI (float health)
    {
        progressBar.fillAmount = health;
    }
}
