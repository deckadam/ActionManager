using System.Collections.Generic;
using System.Linq;
using DeckAdam.ActionManager.UIComponent;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class ActionIdContent : ActionContent
	{
		internal override string ContentName => "Identifiers";

		private ActionScrollableTextArea _actionScrollableTextArea;
		private ActionVerticalArea _verticalTagArea;
		private List<ActionButton> _idButtons;

		private long _lastSelectedLabel = long.MaxValue;

		internal ActionIdContent()
		{
			_actionScrollableTextArea = new ActionScrollableTextArea(ActionStyle.ScrollableTextAreaStyle);
			var scrollBar = new ActionScrollBar();
			_verticalTagArea = new ActionVerticalArea(scrollBar.BeginScrollView, scrollBar.EndScrollView);
			GetSelectedIdListeners();
		}

		internal sealed override void Display(EditorWindow editor)
		{
			DrawTags(editor);
			DrawTextArea(editor);
		}

		//TODO: Make this buttons object
		private void DrawTags(EditorWindow editor)
		{
			_verticalTagArea.BeginVerticalArea(0, 30, 200, (int) editor.position.height - 10);
			foreach (var label in ActionRepo.GetIdentifiers().Keys)
			{
				if (!GUILayout.Button(ActionRepo.GetIdentifierName(label), ActionStyle.ButtonStyle)) continue;
				_lastSelectedLabel = label;
				GetSelectedIdListeners();
			}

			_verticalTagArea.EndVerticalArea();
		}

		private void DrawTextArea(EditorWindow editor)
		{
			GUILayout.BeginArea(new Rect(200, 25, editor.position.width - 205, editor.position.height - 30));
			_actionScrollableTextArea.Draw();
			GUILayout.EndArea();
		}

		private void GetSelectedIdListeners()
		{
			if (!ActionRepo.IsListenerConnectedWithId(_lastSelectedLabel))
			{
				_actionScrollableTextArea.ClearContext();
				return;
			}

			var connections = ActionRepo.GetConnectedListenersWithId(_lastSelectedLabel);

			_actionScrollableTextArea.ClearContext();
			foreach (var connection in connections)
			{
				_actionScrollableTextArea.AppendContext(connection);
			}
		}
	}
}