using Assets.Scripts.Player;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            player.ApplySpeedBoost();
            Destroy(gameObject);
        }
    }
}
