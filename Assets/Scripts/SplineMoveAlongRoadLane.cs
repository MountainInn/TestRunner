using UnityEngine;

public class SplineMoveAlongRoadLane : MonoBehaviour
{
    [SerializeField] private SplineRoadLanes roadLanes;

    private int lane = 0;
    private Vector3 laneOffset, targetLaneOffset;

    private void Start()
    {
        lane = roadLanes.Mid;
    }

    public void SwitchLane(Direction direction)
    {
        lane = direction switch
            {
                Direction.Left => lane -= 1,
                Direction.Right => lane += 1,
                (_) => throw new System.ArgumentException()
            };

        lane = Mathf.Clamp(lane, 0, roadLanes.LaneCount-1);
    }

    private void Update()
    {
        float t = Time.fixedDeltaTime * 10;

        targetLaneOffset = roadLanes.GetLaneOffset(lane);

        laneOffset = Vector3.Lerp(laneOffset, targetLaneOffset, t);

        transform.localPosition = laneOffset;
    }
}
