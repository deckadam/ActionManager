using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ActionScrollBar
	{
		private Vector2 _scrollAmount;

		internal void BeginScrollView()
		{
			_scrollAmount = EditorGUILayout.BeginScrollView(_scrollAmount);
		}

		internal void EndScrollView()
		{
			EditorGUILayout.EndScrollView();
		}
	}
}