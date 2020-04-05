using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class IdContent : IActionContent
	{
		private Dictionary<long, string> _identifiers;

		internal IdContent()
		{
			Refresh();
		}

		public void Display()
		{
			EditorGUILayout.BeginHorizontal();
			DrawTags();
			DrawIdentifierNameList();
			EditorGUILayout.EndHorizontal();
		}

		private void DrawTags()
		{
			EditorGUILayout.BeginVertical();
			foreach (var label in _identifiers.Keys)
			{
				if (GUILayout.Button(_identifiers[label],GUILayout.MinWidth(250),GUILayout.MaxWidth(250)))
				{
					
				}
			}

			EditorGUILayout.EndVertical();
		}

		private void DrawIdentifierNameList()
		{
		}

		public void Refresh()
		{
			_identifiers = ActionManagerDebugger.Identifiers;
		}
	}
}