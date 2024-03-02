using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimationController : MonoBehaviour
{
    private Animator _animator;
    private HoleHitCheck _holeHitCheck;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _holeHitCheck = GetComponent<HoleHitCheck>();
    }

    

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("isHitHole",_holeHitCheck.IsHitHole);
    }
}
