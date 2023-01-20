using UnityEngine;
using UnityAtoms.BaseAtoms;

public class PipeScript : MonoBehaviour
{
    [SerializeField]
    FloatReference _speed;

    [SerializeField]
    Transform _topPipe;
    [SerializeField]
    Transform _bottomPipe;

    [SerializeField]
    Transform _topPosition;
    [SerializeField]
    Transform _bottomPosition;

    [SerializeField]
    Transform _respawnThreshold;

    public System.Action<PipeScript> OnHitEdge;

    void OnGizmosDrawSelected()
    {
        if (_respawnThreshold == null) return;
        Gizmos.DrawLine(_respawnThreshold.position.With(y: 10), _respawnThreshold.position.With(y: -10));
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * _speed.Value * Time.deltaTime);

        if (transform.position.x < _respawnThreshold.position.x)
        {
            OnHitEdge?.Invoke(this);
        }
    }

    public void Randomize(float maxDeviationFromCenter, float gapHeight)
    {
        float topY = _topPosition.position.y;
        float bottomY = _bottomPosition.position.y;

        float height = topY - bottomY;

        float centerY = (topY + bottomY) / 2;
        // set max gap height as the max height of the pipes
        gapHeight = Mathf.Min(gapHeight, height);
        // can only deviate from the center as much as half the height - the gapHeight
        maxDeviationFromCenter = Mathf.Min(maxDeviationFromCenter, topY - centerY - gapHeight / 2);

        float dYfC = Random.Range(-maxDeviationFromCenter, maxDeviationFromCenter);

        SetTopBottom(_topPipe, topY, centerY + dYfC + gapHeight / 2);
        SetTopBottom(_bottomPipe, centerY + dYfC - gapHeight / 2, bottomY);

    }

    void SetTopBottom(Transform transform, float topY, float bottomY)
    {
        float height = topY - bottomY;

        transform.localScale = transform.localScale.With(y: height);
        transform.position = transform.position.With(y: bottomY + height / 2);
    }
}