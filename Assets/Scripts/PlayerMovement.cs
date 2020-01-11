﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	private bool isGrounded = false, jump = false;
	private readonly float standardGravity = -9.81f;
	[SerializeField]
	private float gravityMultiplier = 3, speed = 5, jumpHeight = 2;
	private float gravity = -9.81f, x = 0, z = 0;
	private Vector3 velocity = Vector3.zero;
	[SerializeField]
	private Transform groundChecker;
	[SerializeField]
	private LayerMask groundMask;
	private CharacterController characterController;


	public bool Jump { get => jump; set => jump = value; }
	public float X { get => x; set => x = value; }
	public float Z { get => z; set => z = value; }


	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		CheckGround();
		UseGravity();
		Move();
		JumpUp();
	}

	private void OnValidate()
	{
		MultiplyGravity();
	}

	private void MultiplyGravity()
	{
		gravity = standardGravity * gravityMultiplier;
	}

	private void CheckGround()
	{
		isGrounded = Physics.CheckSphere(groundChecker.position, 0.5f, groundMask);
	}

	private void UseGravity()
	{
		float y = gravity * Time.deltaTime;
		velocity.y += y;

		if(isGrounded && velocity.y < -2)
		{
			velocity.y = -2;
		}

		characterController.Move(velocity * Time.deltaTime);
	}

	private void Move()
	{
		Vector3 move = (transform.right*x + transform.forward*z) * speed;
		characterController.Move(move * Time.deltaTime);
	}

	private void JumpUp()
	{
		if(isGrounded && jump)
		{
			jump = false;
			float y = Mathf.Sqrt(jumpHeight * -2 * gravity);

			velocity.y = y;
			characterController.Move(velocity * Time.deltaTime);
		}
	}
}
