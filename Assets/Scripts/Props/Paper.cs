using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Paper : MonoBehaviour
{

    [SerializeField] private float endValue;
    [SerializeField] private float duration;

    private Tween myTween;
    private UIPaper uiPaper;
    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        uiPaper = FindObjectOfType<UIPaper>();
        float targetPositionY = transform.position.y + endValue;
        myTween = transform.DOMoveY(targetPositionY, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiPaper.AddPaper();
            myTween.Kill();
            StartCoroutine(SoundMovida());
        }
    }

    private IEnumerator SoundMovida()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;
        Collider2D coll = GetComponent<Collider2D>();
        coll.enabled = false;
        audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(1.5f);
        
        Destroy(this.gameObject);
    }
}
