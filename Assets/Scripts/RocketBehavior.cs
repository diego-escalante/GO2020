﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RocketBehavior : MonoBehaviour {
    
    private Transform playerTrans;
    private bool startedSequence = false;
    private float vel = 0;
    private float acc = 0.1f;
    private float jerk = 2.5f;
    private float duration = 1f;

    private void Start() {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        if (Vector3.Distance(transform.position, playerTrans.position) < 1.75f && !startedSequence) {
            startedSequence = true;
            StartCoroutine(ExitSequence());
        }
    }

    private IEnumerator ExitSequence() {
        playerTrans.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
        playerTrans.Find("BlobShadowProjector").gameObject.SetActive(false);
        playerTrans.GetComponent<PlayerMovement>().enabled = false;
        playerTrans.GetComponent<PlayerHovering>().enabled = false;
        transform.GetChild(0).GetComponent<ObjectShaker>().Shake(600, 0.05f, 0.01f, false, Vector3.zero, false);
        yield return new WaitForSeconds(3f);

        while(transform.position.y < 50f) {
            vel += Time.deltaTime * acc;
            acc += Time.deltaTime * jerk;
            transform.Translate(new Vector3(0, vel * Time.deltaTime, 0));
            yield return null;
        }

        RawImage fade = GameObject.FindGameObjectWithTag("UI").transform.Find("Fade").GetComponent<RawImage>();
        for(float elapsedTime = 0; elapsedTime <= duration; elapsedTime += Time.deltaTime) {
            fade.color = Color.Lerp(Color.clear, Color.black, elapsedTime/duration);
            yield return null;
        }
        fade.color = Color.black;

        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

}