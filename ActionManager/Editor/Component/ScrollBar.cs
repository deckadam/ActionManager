using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ScrollBar
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