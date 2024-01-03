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

    protected override IEnumerator PlayDissolveEffect()
    {
        SupplySubpartDissolveEffectApplyHelper[] helpers = GetComponentsInChildren<SupplySubpartDissolveEffectApplyHelper>();
        
        foreach(SupplySubpartDissolveEffectApplyHelper helper in helpers)
        {
            StartCoroutine(helper.PlayDissolveEffect(dissolveSpeed));
        }

        yield return null;
    }
}
