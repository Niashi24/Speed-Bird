using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[SelectionBase]
public class BirdScript : MonoBehaviour
{
    [SerializeField]
    [Required]
    BirdInput _birdInput;

    [SerializeField]
    [Required]
    Rigidbody2D _rbdy2D;

    [SerializeField]
    float _flapVelocity = 20;

    [SerializeField]
    float _swipeVelocity = 80;

    [SerializeField, Min(0)]
    float _glideSpeed = 2.5f;

    [SerializeField]
    float _glideDeceleration = 10;

    public System.Action OnDeath;
    public System.Action OnScore;
    public bool IsGliding {get; private set;}

    void Start()
    {
        OnDeath += this.Reset;
        OnScore += () => Debug.Log("Scored!");
    }

    void OnEnable()
    {
        _birdInput.OnTap += OnFlap;
        _birdInput.OnSwipeDown += OnSwipeDown;
        _birdInput.OnSwipeUp += OnSwipeUp;
    }

    void OnDisable()
    {
        _birdInput.OnTap -= OnFlap;
        _birdInput.OnSwipeDown -= OnSwipeDown;
        _birdInput.OnSwipeUp -= OnSwipeUp;
    }

    void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            OnDeath?.Invoke();
        }

        if (_rbdy2D.velocity.y < -_glideSpeed && _birdInput.isHeld)
        {
            IsGliding = true;
            float speed = Mathf.Max(_glideSpeed, Mathf.Abs(_rbdy2D.velocity.y) - _glideDeceleration * Time.deltaTime);
            _rbdy2D.velocity = Vector2.down * speed;
            _rbdy2D.gravityScale = 0;
        }
        else
        {
            IsGliding = false;
            _rbdy2D.gravityScale = 1;
        }
    }

    void OnFlap()
    {
        _rbdy2D.velocity = Vector2.up * _flapVelocity;
    }

    void OnSwipeUp()
    {
        _rbdy2D.velocity = Vector2.down * _swipeVelocity;
    }

    void OnSwipeDown()
    {
        _rbdy2D.velocity = Vector2.up * _swipeVelocity;
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
        transform.position = new Vector3(transform.position.x, 0, 0);
    }
}
