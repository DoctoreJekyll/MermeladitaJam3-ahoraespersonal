using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Paper : MonoBehaviour
{

    [SerializeField] private float endValue;
    [SerializeField] private float duration;
    
    private void Start()
    {
        float targetPositionY = transform.position.y + endValue;
        transform.DOMoveY(targetPositionY, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
