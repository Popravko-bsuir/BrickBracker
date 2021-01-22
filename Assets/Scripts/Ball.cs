using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _velocity;
    public Transform leftSide;
    public Transform rightSide;
    public Transform startPosition;
    public Manager manager;
    public float speed;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        _velocity = _rb.velocity;
    }

    void Update()
    {
        //_rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            var normal = other.contacts[0].normal;
            _rb.velocity = Vector2.Reflect(_velocity, normal);
            _velocity = _rb.velocity;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var contactPoint = transform.position.x;
            var middlePositionX = other.transform.position.x;
            var leftSidePositionX = leftSide.position.x;
            var rightSidePositionX = rightSide.position.x;
            
            if (contactPoint > middlePositionX)
            {
                var percent = (contactPoint - middlePositionX)/(rightSidePositionX - middlePositionX) * 100;
                var direction = new Vector2(percent, 100 - percent).normalized;
                Reflect(direction);
            }
            else
            {
                var percent = (contactPoint - middlePositionX)/(middlePositionX - leftSidePositionX) * 100;
                var direction = new Vector2(percent, 100 + percent).normalized;
                Reflect(direction);
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<CircleCollider2D>(), gameObject.GetComponent<CircleCollider2D>());
            _rb.velocity = _velocity;
        }
    }

    private void Reflect(Vector2 direction)
    {
        _rb.velocity = direction * speed;
        _velocity = direction * speed;
    }

    public void ResetBall()
    {
        Destroy(gameObject);
        //transform.position = startPosition.position;
    }

    public void SetComponents(Manager niceManager, Transform left, Transform right)
    {
        manager = niceManager;
        leftSide = left;
        rightSide = right;
    }

    private void OnDestroy()
    {
        manager.RemoveBall(this);
        if (manager.balls.Count == 0)
        {
            //gameover
        }
    }

    public void PlusSpeed()
    {
        _rb.velocity = _velocity * 2;
    }
}
