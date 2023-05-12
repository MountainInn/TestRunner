using UnityEngine;
using UnityEngine.Pool;

abstract public class PooledMonoBehaviour : MonoBehaviour
{
    protected IObjectPool<GameObject> pool;

    public void SetPool(IObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }

    protected void Return()
    {
        pool.Release(gameObject);
    }
}
