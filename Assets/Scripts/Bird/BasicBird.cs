using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[SelectionBase]
public class BasicBird : MonoBehaviour
{
    [SerializeField]
    [Required]
    BirdInput _birdInput;

    [SerializeField]
    [Required]
    Rigidbody2D _rbdy2D;

    [SerializeField]
    float _flapVelocity = 20;

    public System.Action OnDeath;
    public System.Action OnScore;

    void Start()
    {
        OnDeath += this.Reset;
        OnScore += () => Debug.Log("Scored!");
    }

    void OnEnable()
    {
        _birdInput.OnTap += OnFlap;
        _birdInput.OnSwipeDown += OnFlap;
        _birdInput.OnSwipeUp += OnFlap;
    }

    void OnDisable()
    {
        _birdInput.OnTap -= OnFlap;
        _birdInput.OnSwipeDown -= OnFlap;
        _birdInput.OnSwipeUp -= OnFlap;
    }

    void OnFlap()
    {
        _rbdy2D.velocity = Vector2.up * _flapVelocity;
    }

    void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            OnDeath?.Invoke();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag(Tags.Death))
        {
            OnDeath?.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag(Tags.Score))
        {
            OnScore?.Invoke();
        }
    }

    private void Reset()
    {
        _rbdy2D.velocity = Vector2.zero;
        transform.position = transform.position.With(y: 0, z: 0);
    }
}
