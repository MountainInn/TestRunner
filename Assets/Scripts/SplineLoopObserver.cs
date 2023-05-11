using UnityEngine;
using UnityEngine.Events;

public class SplineLoopObserver : MonoBehaviour
{
    [SerializeField] private SplineMove targetSplineMove;

    [SerializeField] private UnityEvent onLoop;

    private float prevPathRatio;

    private void Update()
    {
        float currentPathRatio = targetSplineMove.PathRatio;

        if (currentPathRatio < prevPathRatio)
        {
            onLoop?.Invoke();
        }

        prevPathRatio = currentPathRatio;
    }
}
