using UnityEngine;

public class MouseBirdInput : BirdInput
{
    [SerializeField]
    float _minDistance = 0.1f;
    [SerializeField]
    float _minSpeed = 1;
    [SerializeField]
    float _maxTapTime = 0.5f;
    [SerializeField]
    Camera _camera;

    private Vector2 previousPosition = Vector2.zero;
    private float startTime;

    void Update()
    {
        if (this.isHeld)
        {
            if (Input.GetMouseButtonUp(0))
            {
                this.isHeld = false;

                float dT = Time.time - startTime;
                if (dT == 0) return;

                Vector2 currentPosition = GetWorldMousePosition();
                float dS = Vector2.Distance(currentPosition, previousPosition);
                if (dS < _minDistance)
                {
                    if (dT > _maxTapTime) return;
                    // Debug.Log($"Didn't go far enough: {dS}");
                    OnTap?.Invoke();
                    return;
                }

                float spd = dS/dT;
                if (spd < _minSpeed) 
                {
                    if (dT > _maxTapTime) return;
                    // Debug.Log($"Didn't go fast enough: {spd}");
                    OnTap?.Invoke();
                    return;
                }

                // Debug.Log($"Swiped: Speed: {spd}, dS: {dS}");

                Vector2 dir = (currentPosition - previousPosition).normalized;
                if (dir.y > 0.5f)
                    OnSwipeUp?.Invoke();
                else if (dir.y < -0.5f)
                    OnSwipeDown?.Invoke();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.isHeld = true;
                previousPosition = GetWorldMousePosition();
                startTime = Time.time;
            }
        }
    }

    Vector2 GetWorldMousePosition()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}