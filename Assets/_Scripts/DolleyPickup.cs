using UnityEngine;

public class DolleyPickup : MonoBehaviour
{
    public Transform NearbyBox { get; private set; }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box")) NearbyBox = other.transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == NearbyBox) NearbyBox = null;
    }

    public void Clear() => NearbyBox = null;
}