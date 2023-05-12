using UnityEngine;
using UnityEngine.Splines;

public abstract class MySplineComponent : MonoBehaviour
{
    [SerializeField] protected SplineContainer targetSpline;

    public SplineContainer TargetSpline => targetSpline;

    public void EvaluatePositionAndRotation(float t, out Vector3 position, out Quaternion rotation)
    {
        position = targetSpline.EvaluatePosition(t);
        rotation = Quaternion.identity;

        var axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(Vector3.forward, Vector3.up));

        var forward = Vector3.Normalize(targetSpline.EvaluateTangent(t));
        var up = targetSpline.EvaluateUpVector(t);


        rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;
    }
}
