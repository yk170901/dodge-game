using Assets.Scripts.Player;
using Assets.Scripts.Supply;
using System.Collections;
using UnityEngine;

public class TimeSlower : SupplyItemBase
{
    protected override void OnPlayerCollectItem(Collider other)
    {
        GameManager.Instance.ApplyTimeSlower();
    }

}
