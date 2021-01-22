using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int hP;
    public SpriteRenderer spriteRenderer;
    public Sprite crackedGlass;
    public ParticleSystem particleSystem;
    public BoxCollider2D boxCollider2D;
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ball"))
        {
            return;
        }
        
        TakeDamage();
    }

    private void TakeDamage()
    {
        hP--;
        spriteRenderer.sprite = crackedGlass;
        
        if (hP == 0)
        {
            StartCoroutine(Hide());
        }
    }

    private IEnumerator Hide()
    {
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
        particleSystem.Play(false);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
