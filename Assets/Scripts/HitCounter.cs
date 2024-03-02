using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitCounter : MonoBehaviour
{
    [Header("Hit Counter")]
    [SerializeField] private int maxHits;
    [SerializeField] private Transform startPosition;
    [SerializeField] private GameObject gameOverPanel;
    public int MaxHits
    {
        get { return maxHits; }
    }
    [SerializeField] private int hitCount;
    public int HitCount
    {
        get { return hitCount; }
    }   
    private GameObject _ball;
    private BallController _ballController;
    private HoleHitCheck _holeHitCheck;
    private TimerController _timerController;
    private void Awake()
    {
        _ball = GameObject.FindWithTag("Player");
        _timerController = GameObject.FindWithTag("Timer").GetComponent<TimerController>();
        _ballController = _ball.GetComponent<BallController>();
        _holeHitCheck = _ball.GetComponent<HoleHitCheck>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCount > maxHits)
        {
            if (_ballController.IsStopped())
            {
                if (!_holeHitCheck.IsHitHole)
                {
                    GameOver();
                }
               
            }
            
        }
    }

    public void GameOver()
    {
        _ballController.SetSpeedToZero();
        _ballController.enabled = false;
        gameOverPanel.SetActive(true);
    }
    public void ToMainMenu()
    {
        AudioManager.instance.PlaySFX(1);
        _ballController.enabled = false;
        gameOverPanel.SetActive(true);
        SceneManager.LoadScene("Menu");
    }

    public void TryAgain()
    {
        _timerController.ResetTimer();
        _ball.transform.position = startPosition.position;
        AudioManager.instance.PlaySFX(1);
        _ballController.enabled = true;
        hitCount = 0;
        _holeHitCheck.winScreenPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        
    }

    public void NextLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void IncreaseHitsCounter()
    {
        hitCount += 1;
    }
    
    
    
}
