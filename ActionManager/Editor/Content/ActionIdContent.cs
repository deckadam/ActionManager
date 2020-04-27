using System.Collections.Generic;
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

		internal ActionIdContent()
		{
			_actionScrollableTextArea = new ActionScrollableTextArea(ActionStyle.ScrollableTextAreaStyle);
			var scrollBar = new ActionScrollBar();
			_verticalTagArea = new ActionVerticalArea(scrollBar.BeginScrollView, scrollBar.EndScrollView);
			GetSelectedIdListeners(long.MinValue);
			_idButtons = new List<ActionButton>();
			ActionRepo.OnIdentifiersUpdated += AdjustTagButtons;
		}

		internal sealed override void Display(EditorWindow editor)
		{
			DrawTags(editor);
			DrawTextArea(editor);
		}

		private void DrawTags(EditorWindow editor)
		{
			_verticalTagArea.BeginVerticalArea(0, 30, 200, (int) editor.position.height - 10);

			foreach (var label in _idButtons)
				label.ProcessButton();

			_verticalTagArea.EndVerticalArea();
		}

		private void DrawTextArea(EditorWindow editor)
		{
			GUILayout.BeginArea(new Rect(200, 25, editor.position.width - 205, editor.position.height - 30));
			_actionScrollableTextArea.Draw();
			GUILayout.EndArea();
		}

		private void GetSelectedIdListeners(long id)
		{
			_actionScrollableTextArea.ClearContext();

			if (!ActionRepo.IsListenerConnectedWithId(id)) return;

			var connections = ActionRepo.GetConnectedListenersWithId(id);

			foreach (var connection in connections)
				_actionScrollableTextArea.AppendContext(connection);
		}

		private void AdjustTagButtons()
		{
			_idButtons.Clear();

			foreach (var label in ActionRepo.GetIdentifiers().Keys)
			{
				_idButtons.Add(new ActionButton(ActionRepo.GetIdentifierName(label), () => GetSelectedIdListeners(label)));
			}
		}
	}
}