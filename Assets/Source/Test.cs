using Source.Player;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth) == false)
            return;

        playerHealth.TryTakeDamage(10);
    }
}