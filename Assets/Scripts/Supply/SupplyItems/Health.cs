using Assets.Scripts.Player;
using UnityEngine;

public class Health : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement player))
        {
            FindObjectOfType<PlayerHealth>().RecoverHealth();
            Destroy(gameObject);
        }
    }
}
