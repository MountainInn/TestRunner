using UnityEngine;

public class SplineMoveAlongRoadLane : SplineMove
{
    [SerializeField, HideInInspector]
    SplineRoadLanes roadLanes => TargetSpline.GetComponent<SplineRoadLanes>();

    private int lane = 0;
    private Vector3 laneOffset, targetLaneOffset;
    private bool IsSwitchingLane => laneOffset != targetLaneOffset;

    protected override void Start()
    {
        base.Start();
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

    protected override void UpdateTransform()
    {
        if (targetSpline == null)
        {
            Debug.LogError("Target Spline is null");
            return;
        }

        float t = UpdatePathRatio();

        EvaluatePositionAndRotation(t, out var position, out var rotation);

        UpdateLaneOffset(ref position, rotation);

        transform.position = position;
        transform.rotation = rotation;
    }

    private void UpdateLaneOffset(ref Vector3 position, in Quaternion rotation)
    {
        targetLaneOffset = roadLanes.GetLaneOffset(lane);

        float t = Time.fixedDeltaTime * 10;

        laneOffset = Vector3.Lerp(laneOffset, targetLaneOffset, t);

        position += rotation * laneOffset;
    }

}
