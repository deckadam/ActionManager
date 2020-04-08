using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class IdContent : ActionContent
	{
		private Dictionary<long, string> _identifiers;
		private ScrollableTextArea _scrollableTextArea;
		private long _lastSelectedLabel = long.MaxValue;

		internal IdContent()
		{
			_scrollableTextArea = new ScrollableTextArea(Style.ScrollableTextAreaStyle);
			Refresh();
		}

		public sealed override void Refresh()
		{
			_identifiers = ActionManagerDebugger.Identifiers;
			GetSelectedIdListeners();
		}

		public sealed override void Display()
		{
			EditorGUILayout.BeginHorizontal();
			DrawTags();
			_scrollableTextArea.Draw();	
			EditorGUILayout.EndHorizontal();
		}

		private void DrawTags()
		{
			EditorGUILayout.BeginVertical(GUILayout.MaxWidth(300));
			foreach (var label in _identifiers.Keys)
			{
				if (GUILayout.Button(_identifiers[label], GUILayout.MinWidth(249), GUILayout.MaxWidth(250)))
				{
					_lastSelectedLabel = label;
					GetSelectedIdListeners();
				}
			}
			EditorGUILayout.EndVertical();
		}

		private void GetSelectedIdListeners()
		{
			if (!ActionManagerDebugger.ConnectedListeners.ContainsKey(_lastSelectedLabel))
			{
				_scrollableTextArea.ClearContext();
				return;
			}

			var connections = ActionManagerDebugger.ConnectedListeners[_lastSelectedLabel];

			if (connections.Count == 0)
			{
				_scrollableTextArea.SetContext("No active connection");
				return;
			}

			_scrollableTextArea.ClearContext();
			foreach (var connection in connections)
			{
				_scrollableTextArea.AppendContext(connection);
			}
		}
	}
}