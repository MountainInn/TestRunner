using UnityEngine;
using UnityEngine.Pool;

public class PooledMonoBehaviour : MonoBehaviour
{
    public IObjectPool<GameObject> pool;

    protected void Return()
    {
        pool.Release(gameObject);
    }
}
