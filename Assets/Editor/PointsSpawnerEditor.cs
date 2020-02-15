using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PointsSpawner))]
public class PointsSpawnerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		PointsSpawner enemiesPatrolPoints = (PointsSpawner)target;

		// Button:
		string buttonName = enemiesPatrolPoints.CreateNewPoints ? "Stop creating new points" : "Create new points";
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		if(enemiesPatrolPoints.CreateNewPoints)
		{
			buttonStyle.normal.textColor = Color.red;
			buttonStyle.active.textColor = Color.red;
			buttonStyle.fontStyle = FontStyle.Bold;
		}

		if(GUILayout.Button(buttonName, buttonStyle))
		{
			if(!enemiesPatrolPoints.CreateNewPoints)
			{
				enemiesPatrolPoints.StartCreatingNewPoints();
			}
			else
			{
				enemiesPatrolPoints.StopCreatingNewPoints();
			}
		}

		// Help box:
		if(enemiesPatrolPoints.CreateNewPoints)
		{
			EditorGUILayout.HelpBox("Click a left mouse button on the scene view to set a new point.", MessageType.Info);
		}
	}
}
