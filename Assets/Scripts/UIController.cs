using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [Header("Hit Counter Elements")]
    [SerializeField] TextMeshProUGUI hitStatusTxt;
    [Header("Impact Elements")]
    [SerializeField] TextMeshProUGUI impactText;
    public  Slider impactForceBar;
    [Header("Timer")]
    [SerializeField] TextMeshProUGUI timerText;
    
    //COMPONENTS
    private BallController _ballController;
    private HitCounter _hitCounter;
    private TimerController _timerController;

    private void Awake()
    {
        instance = this;
        _ballController = GameObject.FindWithTag("Player").GetComponent<BallController>();
        _hitCounter = GameObject.FindWithTag("HitCounter").GetComponent<HitCounter>();
        _timerController = GameObject.FindWithTag("Timer").GetComponent<TimerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        impactForceBar.value = 0;
        impactForceBar.maxValue = _ballController.maxImpactForce;
    }

    // Update is called once per frame
    void Update()
    {
        hitStatusTxt.text = _hitCounter.HitCount.ToString() + ":" + _hitCounter.MaxHits.ToString();
        impactText.text = _ballController.currentImpactForce.ToString();
        timerText.text = _timerController.timer.Minutes.ToString("F0") + ":" + _timerController.timer.Seconds.ToString("F0");

    }
    
    public void SetImpactForceBarValue(float value)
    {
        impactForceBar.value = value;
    }
}
