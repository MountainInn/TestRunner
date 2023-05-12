using UnityEngine;
using UnityEngine.Pool;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private SplineRoadLanes roadLanes;
    [SerializeField] private GameObject collectiblePrefab;
    [SpaceAttribute]
    [SerializeField] private float spawnSpacing;

    private IObjectPool<GameObject>
        pool;

    void Awake()
    {
        pool = new ObjectPool<GameObject>(CreateCollectible,
                                                     OnGetCollectible,
                                                     OnReleaseCollectible,
                                                     OnDestroyCollectible,
                                                     collectionCheck: true,
                                                     defaultCapacity: 30,
                                                     maxSize: 60);
    }

    void Start()
    {
        Spawn();
    }

    GameObject CreateCollectible()
    {
        var newCollectible = GameObject.Instantiate(collectiblePrefab);

        newCollectible.gameObject.GetComponent<Collectible>().pool = pool;

        return newCollectible;
    }

    void OnGetCollectible(GameObject coin)
    {
        coin.SetActive(true);
    }

    void OnReleaseCollectible(GameObject coin)
    {
        coin.SetActive(false);
    }

    void OnDestroyCollectible(GameObject coin)
    {
        Destroy(coin.gameObject);
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
