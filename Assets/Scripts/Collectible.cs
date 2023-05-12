using UnityEngine;

public class Collectible : PooledMonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out PlayerCube playerCube))
        {
            Return();
        }
    }
}
