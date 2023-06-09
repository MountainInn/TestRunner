using UnityEngine;
using UnityEngine.Events;

public class InputService : MonoBehaviour
{
    [SerializeField]
    private float swipeThreshold;

    [SerializeField]
    private UnityEvent<Direction> onSwipe;


    private Vector3 beginPos, endPos, delta;

    private bool ongoingSwipe;


    private void Update()
    {
        DetectSwipes();
    }

    private void DetectSwipes()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ongoingSwipe = true;

            beginPos = Input.mousePosition;
        }

        if (ongoingSwipe && Input.GetMouseButtonUp(0))
        {
            ongoingSwipe = false;

            endPos = Input.mousePosition;

            delta = endPos - beginPos;

            float deltaMagnitude = delta.magnitude;

            if (deltaMagnitude > swipeThreshold)
            {
                Direction direction = (delta.x < 0) ? Direction.Left : Direction.Right;

                onSwipe?.Invoke(direction);
            }
        }
    }
}
