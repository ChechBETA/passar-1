using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(StaticDataManager))]
public class StaticDataInspector : Editor
{
	public override void OnInspectorGUI ()
	{
		serializedObject.UpdateIfDirtyOrScript();
		GUILayout.Space (10F);
		DrawDefaultInspector();
		GUILayout.Space (5F);
		
		serializedObject.ApplyModifiedProperties ();
	}
}
