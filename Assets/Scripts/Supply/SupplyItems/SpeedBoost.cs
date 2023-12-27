﻿using Assets.Scripts.Player;
using Assets.Scripts.Supply;
using UnityEngine;

public class SpeedBoost : SupplyItemBase
{
    protected override void OnPlayerCollectItem(Collider other)
    {
        other.TryGetComponent(out PlayerMovement playerMovement);
        playerMovement.ApplySpeedBoost();
    }
}
