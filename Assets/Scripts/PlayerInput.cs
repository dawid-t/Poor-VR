using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAim))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
	[SerializeField]
	private Platform usedPlatform;
	private PlayerAim playerAim;
	private PlayerMovement playerMovement;


	public enum Platform { PC, AndroidVR }


	private void Awake()
	{
		playerAim = GetComponent<PlayerAim>();
		playerMovement = GetComponent<PlayerMovement>();
	}

	private void Update()
	{
		GetInputsFromRightPlatform();
	}

	private void GetInputsFromRightPlatform()
	{
		if(usedPlatform == Platform.PC)
		{
			GetMouseInputs();
			GetKeyboardInputs();
		}
		else
		{
			GetJoystickInputs();
			GetSmartphoneControllerInputs();
		}
	}

	#region PC inputs:
	private void GetMouseInputs()
	{
		playerAim.Zoom = Input.GetMouseButton(1);
		playerAim.MouseX = Input.GetAxis("Mouse X");
		playerAim.MouseY = Input.GetAxis("Mouse Y");
	}

	private void GetKeyboardInputs()
	{
		playerMovement.Jump = Input.GetButtonDown("Jump");
		playerMovement.X = Input.GetAxis("Horizontal");
		playerMovement.Z = Input.GetAxis("Vertical");
	}
	#endregion PC inputs.

	#region Android VR inputs:
	private void GetJoystickInputs()
	{

	}

	private void GetSmartphoneControllerInputs()
	{

	}
	#endregion Android VR inputs.
}
