using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    public Transform platformStartPoint;
    public GameObject ballPrefab;
    public GameObject platformPrefab;
    private GameObject _platform;
    private Plathorm _platformScript;
    private Transform _ballStartPoint;
    public List<Ball> balls = new List<Ball>(); 
    void Start()
    { 
        _platform = Instantiate(platformPrefab, platformStartPoint.position, platformStartPoint.rotation);
        _platformScript = _platform.GetComponent<Plathorm>();
        _platformScript.SetManager(this);
        _ballStartPoint = _platform.transform.GetChild(2).gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            var ballCloneScript = Instantiate(ballPrefab, _ballStartPoint.position, _ballStartPoint.rotation).GetComponent<Ball>();
            ballCloneScript.SetComponents(this, _platform.transform.GetChild(0).gameObject.GetComponent<Transform>(), 
                _platform.transform.GetChild(1).gameObject.GetComponent<Transform>());
            balls.Add(ballCloneScript);

        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            foreach (var ball in balls)
            {
                ball.PlusSpeed();
            }
        }
    }

    public void RemoveBall(Ball nice)
    {
        balls.Remove(nice);
    }
}
