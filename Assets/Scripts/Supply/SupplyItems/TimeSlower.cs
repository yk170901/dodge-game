using Assets.Scripts.Player;
using UnityEngine;

public class TimeSlower : MonoBehaviour
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
