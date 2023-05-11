using UnityEngine;

public class SplineMove : MySplineComponent
{
    [SerializeField] protected float speed = 10f;

    protected float coveredPath, totalPath, deltaPath, pathRatio;

    public float PathRatio => pathRatio;

    protected virtual void Start()
    {
        totalPath = targetSpline.CalculateLength();
        deltaPath = Time.fixedDeltaTime * speed;
    }

    protected virtual void FixedUpdate()
    {
        coveredPath += deltaPath;

        UpdateTransform();
    }

    protected virtual void UpdateTransform()
    {
        if (targetSpline == null)
        {
            Debug.LogError("Target Spline is null");
            return;
        }

        UpdatePathRatio();

        EvaluatePositionAndRotation(pathRatio, out var position, out var rotation);

        transform.position = position;
        transform.rotation = rotation;
    }

    protected float UpdatePathRatio()
    {
        pathRatio = coveredPath / totalPath;

        pathRatio %= 1f;

        return pathRatio;
    }

}
