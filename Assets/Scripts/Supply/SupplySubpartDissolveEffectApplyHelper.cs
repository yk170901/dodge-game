using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Supply
{
    class SupplySubpartDissolveEffectApplyHelper : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Material _dissolveMaterial;

        public IEnumerator PlayDissolveEffect(float dissolveSpeed)
        {
            Material mat = new Material(_dissolveMaterial);
            _renderer.material = mat;

            float progress = 0;

            while (progress < 1)
            {
                progress += Time.deltaTime * dissolveSpeed;
                mat.SetFloat("_DissolveProgress", progress);
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
