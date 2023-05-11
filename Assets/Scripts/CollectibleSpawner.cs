using UnityEngine;

public class CollectibleSpawner : MySplineComponent
{
    [SerializeField] private SplineRoadLanes roadLanes;
    [SerializeField] private float spawnSegmentLength;
    [SerializeField] private int collectiblesPerSegment;

    [SerializeField] private GameObject collectiblePrefab;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        float totalLength = TargetSpline.CalculateLength();

        float coveredLength = spawnSegmentLength;

        float t = coveredLength / totalLength;

        while (t < 1)
        {
            int lane = UnityEngine.Random.Range(0, roadLanes.LaneCount);

            Vector3 offset = roadLanes.EvaluateLaneOffset(t, lane);

            EvaluatePositionAndRotation(t, out Vector3 position, out Quaternion rotation);

            position += offset;

            GameObject.Instantiate(collectiblePrefab, position, rotation);

            coveredLength += spawnSegmentLength;
            t = coveredLength / totalLength;
        }
    }
}
