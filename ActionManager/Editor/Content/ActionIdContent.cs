using System.Collections.Generic;
using DeckAdam.ActionManager.UIComponent;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class ActionIdContent : ActionContent
	{
		internal override string ContentName => "Identifiers";

		private Dictionary<long, string> _identifiers;
		private ActionScrollableTextArea _actionScrollableTextArea;
		private long _lastSelectedLabel = long.MaxValue;
		private ActionScrollBar _tagActionScrollBar;

		internal ActionIdContent()
		{
			_actionScrollableTextArea = new ActionScrollableTextArea(ActionStyle.ScrollableTextAreaStyle);
			_tagActionScrollBar = new ActionScrollBar();
			Refresh();
		}

		internal sealed override void Refresh()
		{
			_identifiers = ActionManagerDebugger.Identifiers;
			GetSelectedIdListeners();
		}

		internal sealed override void Display(EditorWindow editor)
		{
			DrawTags(editor);
			DrawTextArea(editor);
		}

		private void DrawTags(EditorWindow editor)
		{
			GUILayout.BeginArea(new Rect(5, 25, 190, editor.position.height - 5));
			EditorGUILayout.BeginVertical();
			_tagActionScrollBar.BeginScrollView();

			foreach (var label in _identifiers.Keys)
			{
				if (!GUILayout.Button(_identifiers[label])) continue;
				_lastSelectedLabel = label;
				GetSelectedIdListeners();
			}

			_tagActionScrollBar.EndScrollView();
			EditorGUILayout.EndVertical();
			GUILayout.EndArea();
		}

		private void DrawTextArea(EditorWindow editor)
		{
			GUILayout.BeginArea(new Rect(200, 25, editor.position.width - 205, editor.position.height - 30));
			_actionScrollableTextArea.Draw();
			GUILayout.EndArea();
		}

		private void GetSelectedIdListeners()
		{
			if (!ActionManagerDebugger.ConnectedListeners.ContainsKey(_lastSelectedLabel))
			{
				_actionScrollableTextArea.ClearContext();
				return;
			}

			var connections = ActionManagerDebugger.ConnectedListeners[_lastSelectedLabel];

			if (connections.Count == 0)
			{
				_actionScrollableTextArea.SetContext("No active connection");
				return;
			}

			_actionScrollableTextArea.ClearContext();
			foreach (var connection in connections)
			{
				_actionScrollableTextArea.AppendContext(connection);
			}
		}
	}
}