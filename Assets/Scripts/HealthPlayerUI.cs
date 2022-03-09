using UnityEngine;
using RengeGames.HealthBars;

public class HealthPlayerUI : HealthUI
{
    [SerializeField] private UltimateCircularHealthBar ultimateCircular;

    public override void ShowUI (float health)
    {
        ultimateCircular.SetRemovedSegments((1f-health) * ultimateCircular.SegmentCount);
    }
}
