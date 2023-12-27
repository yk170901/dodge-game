using Assets.Scripts.Player;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            //
            Destroy(gameObject);
        }
    }
}
