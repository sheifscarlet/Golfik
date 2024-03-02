using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleHitCheck : MonoBehaviour
{
    [SerializeField] private Transform holePosition;
    [SerializeField] private bool enoughSpeedToFinishLevel;
    [SerializeField] private float distanceBetween = 5f;
    [SerializeField] private float normalRequiredSpeed = 3f;
    [SerializeField] private float closeEnoughSpeed = 5f;
    public GameObject winScreenPanel;
    private bool _isHitHole;
    public bool IsHitHole
    {
        get { return _isHitHole; }
    }
    
    private Rigidbody2D rb;
    private BallController _ballController;

    private void Awake()
    {
        _ballController = GetComponent<BallController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _isHitHole = false;
        winScreenPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToHole = Vector2.Distance(transform.position, holePosition.position);
        //Debug.Log(distanceToHole);
        if (distanceToHole <= distanceBetween)
        {
            enoughSpeedToFinishLevel = rb.velocity.magnitude < closeEnoughSpeed;
        }
        else
        {
            enoughSpeedToFinishLevel = rb.velocity.magnitude < normalRequiredSpeed;
        }
    }

    void FinishLevel()
    {
        winScreenPanel.SetActive(true);
        _ballController.enabled = false;
        _isHitHole = true;
        AudioManager.instance.PlaySFX(4);
        rb.velocity = Vector2.zero;
        Pass();
        Debug.Log("Level is Finished");
    }

    void Pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel >= PlayerPrefs.GetInt("UnlockedLevels"))
        {
            PlayerPrefs.SetInt("UnlockedLevels",currentLevel + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hole"))
        {
            if (enoughSpeedToFinishLevel)
            {
                FinishLevel();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hole"))
        {
            _isHitHole = false;

        }
    }
}
