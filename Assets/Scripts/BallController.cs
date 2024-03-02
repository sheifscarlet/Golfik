using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public  float currentImpactForce;
    public  float maxImpactForce;
    [SerializeField] private float timeToGetMaxImpact;
    private bool newPositionSetAfterHit = false;
    private Vector2 directionVector; 
    public bool canHit = true;
    private bool isMoving = false;
    private bool isIncreasing = false;
    private float increaseRate; // The rate of increase of  currentImpactForce 
    //коэф замедления, где 1 означает отсутствие замедления, а 0 — мгновенную остановку
    [SerializeField] private float slowDownRate;
    
    //Mouse target
    [SerializeField] private GameObject mouseTarget;
    //COMPONENTS
    private Rigidbody2D rb;
    private HitCounter _hitCounter;
    private CameraShake _cameraShake;
    private FollowMouse _followMouse;

    private void Awake()
    {
        _hitCounter = GameObject.FindWithTag("HitCounter").GetComponent<HitCounter>();
        rb = GetComponent<Rigidbody2D>();
        _cameraShake = GameObject.FindWithTag("Camera").GetComponent<CameraShake>();
        _followMouse = mouseTarget.GetComponent<FollowMouse>();
    }

    // Start is called before the first frame update
    void Start()
    {
        increaseRate = maxImpactForce / timeToGetMaxImpact;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _followMouse.SetPositionToMouse(); 
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            directionVector = (mousePosition - (Vector2)transform.position).normalized;
            newPositionSetAfterHit = true;
        }

        if (Input.GetMouseButtonDown(0) && canHit && newPositionSetAfterHit)
        {
            isIncreasing = true;
            AudioManager.instance.PlaySFX(2);
        }
        else if (Input.GetMouseButtonUp(0) && canHit && newPositionSetAfterHit)
        {
            AudioManager.instance.StopSounds();
            AudioManager.instance.PlaySFX(0);
            UIController.instance.impactForceBar.value = 0;
            _hitCounter.IncreaseHitsCounter();
            isIncreasing = false;
            isMoving = true;
            rb.AddForce(directionVector * currentImpactForce, ForceMode2D.Impulse);
            currentImpactForce = 0;
            newPositionSetAfterHit = false;
            //_followMouse.isFollowMouse = true;
        }

        if (isMoving)
        {
            canHit = false;
            rb.velocity *= slowDownRate;
            if (rb.velocity.magnitude < 0.1f)
            {
                _followMouse.isFollowMouse = true;
                rb.velocity = Vector2.zero;
                rb.rotation = 0;
                isMoving = false;
                canHit = true;
            }
        }

        if (isIncreasing && currentImpactForce < maxImpactForce)
        {
            currentImpactForce += increaseRate * Time.deltaTime;
            currentImpactForce = Mathf.Min(currentImpactForce, maxImpactForce);
            UIController.instance.SetImpactForceBarValue(currentImpactForce);
        }
    }

    public void SetSpeedToZero()
    {
        rb.velocity = Vector2.zero;
    }

    public bool IsStopped()
    {
        if (rb.velocity.magnitude < 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        _cameraShake.ShakeCamera(0.5f,0.1f);
        AudioManager.instance.PlaySFX(3);
    }
}
