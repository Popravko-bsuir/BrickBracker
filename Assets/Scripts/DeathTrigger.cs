using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private Ball _ball;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ball"))
        {
            return;
        }

        if (_ball == null)
        {
            _ball = other.gameObject.GetComponent<Ball>();
        }
        
        _ball.ResetBall();
    }
}
