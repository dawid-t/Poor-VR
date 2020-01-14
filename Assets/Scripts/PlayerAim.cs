using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
	private bool zoom = false;
	[SerializeField] [Range(60, 120)]
	private int clampingUp = 90;
	[SerializeField] [Range(-60, -120)]
	private int clampingDown = -90;
	[SerializeField]
	private int mouseSensitivity = 150;
	private float mouseX = 0, mouseY = 0, xRotation = 0, originalFieldOfView;
	[SerializeField]
	private Transform playerCamera;
	private Camera playerCameraComponent;


	public bool Zoom { get => zoom; set => zoom = value; }
	public float MouseX { get => mouseX; set => mouseX = value; }
	public float MouseY { get => mouseY; set => mouseY = value; }


	private void Awake()
	{
		playerCameraComponent = playerCamera.GetComponent<Camera>();
		originalFieldOfView = playerCameraComponent.fieldOfView;
		LockCursor();
	}

	private void Update()
	{
		Aim();
		ZoomAim();
	}

	private void OnValidate()
	{
		if(mouseSensitivity < 0)
		{
			mouseSensitivity = 0;
		}
	}

	private void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Aim()
	{
		mouseX *= mouseSensitivity * Time.deltaTime;
		mouseY *= mouseSensitivity * Time.deltaTime;

		// Horizontal:
		transform.Rotate(Vector3.up, mouseX);

		// Vertical:
		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, clampingDown, clampingUp);
		playerCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
	}

	private void ZoomAim()
	{
		if(zoom)
		{
			if(playerCameraComponent.fieldOfView > originalFieldOfView-30)
			{
				playerCameraComponent.fieldOfView--;
			}
		}
		else
		{
			if(playerCameraComponent.fieldOfView < originalFieldOfView)
			{
				playerCameraComponent.fieldOfView++;
			}
		}
	}
}
