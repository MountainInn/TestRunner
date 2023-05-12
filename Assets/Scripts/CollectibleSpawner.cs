using UnityEngine;
using UnityEngine.Pool;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private SplineRoadLanes roadLanes;
    [SerializeField] private GameObject collectiblePrefab;
    [SpaceAttribute]
    [SerializeField] private float spawnSpacing;

    private IObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(CreateCollectible,
                                          OnGetCollectible,
                                          OnReleaseCollectible,
                                          OnDestroyCollectible,
                                          collectionCheck: true,
                                          defaultCapacity: 30,
                                          maxSize: 60);
    }

    private void Start()
    {
        Spawn();
    }

    private GameObject CreateCollectible()
    {
        var newCollectible = GameObject.Instantiate(collectiblePrefab);

        newCollectible.gameObject.GetComponent<Collectible>().SetPool(pool);

        return newCollectible;
    }

    private void OnGetCollectible(GameObject collectible)
    {
        collectible.SetActive(true);
    }

    private void OnReleaseCollectible(GameObject collectible)
    {
        collectible.SetActive(false);
    }

    private void OnDestroyCollectible(GameObject collectible)
    {
        Destroy(collectible);
    }


    public void Spawn()
    {
        float totalLength = roadLanes.TargetSpline.CalculateLength();

        float coveredLength = spawnSpacing;

        float t = coveredLength / totalLength;

        while (t < 1)
        {
            int lane = UnityEngine.Random.Range(0, roadLanes.LaneCount);

            Vector3 offset = roadLanes.EvaluateLaneOffset(t, lane);

            roadLanes.EvaluatePositionAndRotation(t, out Vector3 position, out Quaternion rotation);

            position += offset;

            var collectible = pool.Get();
            collectible.transform.position = position;
            collectible.transform.rotation = rotation;

            coveredLength += spawnSpacing;
            t = coveredLength / totalLength;
        }
    }
}
