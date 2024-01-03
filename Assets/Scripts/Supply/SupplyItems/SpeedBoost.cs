using Assets.Scripts.Player;
using Assets.Scripts.Supply;
using System.Collections;
using UnityEngine;

public class SpeedBoost : SupplyItemBase
{
    protected override void OnPlayerCollectItem(Collider other)
    {
        other.GetComponent<PlayerMovement>().ApplySpeedBoost();
    }

    // https://chat.openai.com/c/1e825507-e3b4-4122-9bf0-c1e016d9c419
    protected override IEnumerator PlayDissolveEffect()
    {
        Material[] copy = new Material[dissolveRenderer.materials.Length];
        for (int i = 0; i < copy.Length; i++)
        {
            copy[i] = new Material(dissolveMaterial);
        }
        dissolveRenderer.materials = copy;


        float progress = 0;

        while (progress < 1)
        {
            progress += Time.deltaTime * dissolveSpeed;

            for (int i = 0; i < copy.Length; i++)
            {
                copy[i].SetFloat("_DissolveProgress", progress);
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
