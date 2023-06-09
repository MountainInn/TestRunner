using UnityEngine;

public class Collectible : PooledMonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out PlayerCube playerCube))
        {
            Return();
        }
    }
}
