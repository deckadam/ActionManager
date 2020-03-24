using System.Collections;
using System.Collections.Generic;
using DeckAdam.ActionManager;
using UnityEngine;
using UnityEditor;

public class ActionManagerEditor : EditorWindow
{
	[MenuItem("ActionManager/Debug options")]
	private static void DebugOptions()
	{
		var window = (ActionManagerEditor) EditorWindow.GetWindow(typeof(ActionManagerEditor));
		window.Show();
	}
}