using Assets.Scripts.Player;
using Assets.Scripts.Supply;
using UnityEngine;

public class SpeedBoost : SupplyItemBase
{
    protected override void OnPlayerCollectItem(Collider other)
    {
        other.GetComponent<PlayerMovement>().ApplySpeedBoost();
    }

    protected override void PlayDissolveEffect()
    {
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            renderer.materials[i] = dissolveMaterial;
        }
    }
}
