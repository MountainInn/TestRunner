using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SplineRoadLanes : MySplineComponent
{
    [SerializeField] private LoftRoadBehaviour loftRoad;
    [SerializeField] private int laneCount;
    [SerializeField] private float roadPadding;

    public int LaneCount => laneCount;

    private int _mid;
    public int Mid
    {
        get => _mid;
        set => _mid = value;
    }

    private List<Vector3> laneOffsets;


    private void Awake()
    {
        laneOffsets = new List<Vector3>();

        float laneWidth = ( loftRoad.RoadWidth * 2 - roadPadding * 2) / laneCount;

        bool isOdd = laneCount % 2 == 1;

        if (isOdd)
        {
            laneOffsets.Add(Vector3.zero);
        }

        Mid = laneCount / 2;

        foreach (var sign in new []{ -1, 1 })
        {
            for (int i = 1; i <= Mid; i++)
            {
                float x = sign * i * laneWidth;
                laneOffsets.Add(new Vector3(x , 0, 0 ));
            }
        }

        laneOffsets = laneOffsets.OrderBy(v => v.x).ToList();
    }

    public Vector3 GetLaneOffset(int lane)
    {
        return laneOffsets[lane];
    }
   
    public Vector3 EvaluateLaneOffset(float t, int lane)
    {
        EvaluatePositionAndRotation(t, out Vector3 position, out Quaternion rotation);

        Vector3 offset = rotation * GetLaneOffset(lane);

        return offset;
    }
}
