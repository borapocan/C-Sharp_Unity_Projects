using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config parameters

    [SerializeField] AudioClip breakSound;

    [SerializeField] GameObject blockSparklesVFX;

    //[SerializeField] int maxHits;

    [SerializeField] Sprite[] hitSprites;

    //cached references

    Level level;

    //state variables

    [SerializeField] int timesHit; // TODO only serialized for debug purposes

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();

        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {

            HandleHit();

        }

    }

    private void HandleHit()
    {
        timesHit++;

        int maxHits = hitSprites.Length + 1;

        if (timesHit >= maxHits)
        {
            BlockDestroy();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;

        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.Log("Block sprite is missing from array." + gameObject.name);
        }
    }

    private void BlockDestroy()
    {
        PlayBlockDestroySFX();

        Destroy(gameObject);

        level.BlocksDestroyed();

        TriggerSparklesVFX();

        //Debug.Log(collision.gameObject.name); //what destroys

        //Debug.Log(collision.otherCollider.name); //what get destroyed
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();

        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);

        Destroy(sparkles, 1f);
    }
}
