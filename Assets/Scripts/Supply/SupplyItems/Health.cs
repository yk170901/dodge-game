using Assets.Scripts.Player;
using Assets.Scripts.Supply;
using UnityEngine;

namespace Assets.Scripts.Supply.SupplyItems
{
    public class Health : SupplyItemBase
    {
        protected override void OnPlayerCollectItem(Collider other)
        {
            FindObjectOfType<PlayerHealth>().RecoverHealth();
        }
    }
}