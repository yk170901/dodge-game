using System;
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

        public void PlayDissolveEffect()
        {
            Debug.Log("helper changing mat");
            _renderer.material = _dissolveMaterial;
        }
    }
}
