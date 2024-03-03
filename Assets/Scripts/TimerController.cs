using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public class Timer
    {
        private float seconds;
        public float Seconds
        {
            get { return seconds; }
            set { seconds = Mathf.Max(0, value); } 
        }

        private float minutes;
        public float Minutes
        {
            get { return minutes; }
            set { minutes = Mathf.Max(0, value); } 
        }
    }

    [SerializeField] private float minutesToBeat;
    [SerializeField] private float secondsToBeat;
    public Timer timer = new Timer();

    private HoleHitCheck _holeHitCheck;
    private HitCounter _hitCounter;

    private void Awake()
    {
        _hitCounter = GameObject.FindWithTag("HitCounter").GetComponent<HitCounter>();
        _holeHitCheck = GameObject.FindWithTag("Player").GetComponent<HoleHitCheck>();
    }

    void Start()
    {
        timer.Minutes = minutesToBeat;
        timer.Seconds = secondsToBeat;
    }

    void Update()
    {
        if (!_holeHitCheck.IsHitHole)
        {
            
            timer.Seconds -= Time.deltaTime;

            
            if (timer.Seconds <= 0)
            {
                if (timer.Minutes > 0)
                {
                    timer.Minutes--;
                    timer.Seconds += 59;
                }
                else
                {
                    
                    timer.Seconds = 0; 
                    Debug.Log("Time's up!");
                    _hitCounter.GameOver();
                    
                }
            }

            
        }
        
    }
    public void ResetTimer()
    {
        timer.Minutes = minutesToBeat;
        timer.Seconds = secondsToBeat;
    }
}
