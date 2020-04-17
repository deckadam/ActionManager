using System;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ActionVerticalArea
	{
		private Action _onBegin;
		private Action _onEnd;

		internal ActionVerticalArea(Action onBegin = null, Action onEnd = null)
		{
			_onBegin = onBegin;
			_onEnd = onEnd;
		}

		internal void BeginVerticalArea(int x, int y, int z, int w)
		{
			GUILayout.BeginArea(new Rect(x, y, z, w));
			EditorGUILayout.BeginVertical();
			_onBegin?.Invoke();
		}

		internal void EndVerticalArea()
		{
			_onEnd?.Invoke();
			EditorGUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}
}