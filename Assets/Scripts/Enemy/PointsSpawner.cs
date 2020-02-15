using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[ExecuteInEditMode]
public class PointsSpawner : MonoBehaviour
{
	private bool createNewPoints;
	private GameObject pointToInstantiate;


	public bool CreateNewPoints => createNewPoints;


	private void OnEnable()
	{
		SceneView.duringSceneGui += CreatePoint;
	}

	private void OnDisable()
	{
		SceneView.duringSceneGui -= CreatePoint;
	}

	public void StartCreatingNewPoints()
	{
		createNewPoints = true;
		OnCreatingNewPoints(createNewPoints);

		CreateNewPointToInstantiate(transform.position);
	}

	public void StopCreatingNewPoints()
	{
		createNewPoints = false;
		OnCreatingNewPoints(createNewPoints);

		DestroyImmediate(pointToInstantiate);
	}

	private void OnCreatingNewPoints(bool isLocked)
	{
		ActiveEditorTracker.sharedTracker.isLocked = isLocked; // Lock/unlock the inspector.
		Selection.activeGameObject = gameObject; // Select this gameObject.

		string modeStatus = isLocked ? "ON" : "OFF";
		Debug.Log("<color=red>Create enemies patrol points mode: <b>"+modeStatus+"</b></color>");
	}

	private void CreatePoint(SceneView scene)
	{
		if(!createNewPoints)
		{
			return;
		}

		// Get a current event:
		Event e = Event.current;

		// Show a point to instantiate:
		if(e.type == EventType.MouseMove)
		{
			// Get a current mouse position on the scene view:
			Vector3 mousePos = e.mousePosition;
			float ppp = EditorGUIUtility.pixelsPerPoint;
			mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
			mousePos.x *= ppp;

			// Raycast on the scene view:
			Ray ray = scene.camera.ScreenPointToRay(mousePos);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
			{
				pointToInstantiate.transform.position = hit.point;
			}
		}

		// Create the new point to instantiate (the current point will stay):
		if(e.type == EventType.MouseDown && e.button == 0)
		{
			CreateNewPointToInstantiate(pointToInstantiate.transform.position);
			EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
			e.Use();
		}
	}

	private void CreateNewPointToInstantiate(Vector3 firstPosition)
	{
		pointToInstantiate = new GameObject((transform.childCount+1)+"");
		pointToInstantiate.transform.parent = transform;
		pointToInstantiate.transform.position = firstPosition;
		DrawPointIcon(pointToInstantiate);
	}

	private void DrawPointIcon(GameObject go)
	{
		GUIContent icon = EditorGUIUtility.IconContent("sv_label_6");
		Type egu = typeof(EditorGUIUtility);

		BindingFlags flags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
		object[] args = new object[] { go, icon.image };

		egu.InvokeMember("SetIconForObject", flags, null, null, args);
	}
}
