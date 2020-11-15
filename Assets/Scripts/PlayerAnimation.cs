﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerAnimation : MonoBehaviour {

    private Animator animator;
    private SpriteRenderer rend;
    private PlayerMovement playerMovement;

    private Vector3 vel;

    private void Start() {
        animator = GetComponent<Animator>();
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        vel = playerMovement.GetVelocity();

        // Set facing direction.
        if (vel.x != 0) {
            rend.flipX = vel.x < 0;
        }

        // Set moving.
        animator.SetBool("Moving", vel.x != 0 || vel.z != 0);

        // Set grounded.
        animator.SetBool("Grounded", playerMovement.IsGrounded());
    }
}
