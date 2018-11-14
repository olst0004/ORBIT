using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TargetLight))]
public class TargetLightEditor : Editor 
{
	public override void OnInspectorGUI() 
	{
		DrawDefaultInspector ();
		TargetLight _TargetLight = (TargetLight)target;
	}

	private void OnSceneGUI() 
	{
		TargetLight _TargetLight = (TargetLight)target;
		Transform handleTransform = _TargetLight.transform;
		Quaternion handleRotation = Quaternion.identity;

		Vector3 _Target = _TargetLight._TargetPos;

		Handles.color = _TargetLight.GetLightColor();
		Handles.DrawLine (handleTransform.position, _Target);

		EditorGUI.BeginChangeCheck ();
		_Target = Handles.DoPositionHandle (_Target, handleRotation);

		if (EditorGUI.EndChangeCheck ()) {

			//_TargetLight._TargetPos = _Target;
			//_TargetLight.LookAtTarget ();
			_TargetLight.SetTargetPos(_Target);

			Undo.RecordObject (_TargetLight, "Move Target");
			EditorUtility.SetDirty (_TargetLight);
		}
	}
}
