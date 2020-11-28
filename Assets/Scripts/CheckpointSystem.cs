﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointSystem : MonoBehaviour {

    private Vector3 respawnPosition;
    private BonfireBehavior bonfire;
    private RawImage fade;
    private bool isFading = false;

    private void Start() {
        respawnPosition = transform.position;
        fade = GameObject.FindGameObjectWithTag("UI").transform.Find("Fade").GetComponent<RawImage>();
    }

    private void Update() {
        if (transform.position.y <= -10f && !isFading) {
            StartCoroutine(Fade(0.25f));
        }
    }

    public void UpdateRespawn(BonfireBehavior newBonfire) {
        if (bonfire != null) {
            bonfire.SetState(false);
        }

        bonfire = newBonfire;
        respawnPosition = bonfire.transform.position + new Vector3(0, 0.5f, -1);
    }

    private IEnumerator Fade(float duration) {
        isFading = true;

        // Fade out
        for(float elapsedTime = 0; elapsedTime <= duration; elapsedTime += Time.deltaTime) {
            Color c = fade.color;
            c.a = Mathf.Lerp(0, 1, elapsedTime/duration);
            fade.color = c;
            yield return null;
        }
        fade.color = Color.black;

        // Make the swap.
        transform.position = respawnPosition;

        // Fade in
        for(float elapsedTime = 0; elapsedTime <= duration; elapsedTime += Time.deltaTime) {
            Color c = fade.color;
            c.a = Mathf.Lerp(1, 0, elapsedTime/duration);
            fade.color = c;
            yield return null;
        }
        fade.color = Color.clear;

        isFading = false;
    } 
}