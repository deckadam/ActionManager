using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ActionLogContent : ActionContent
	{
		internal override string ContentName => "Logs";

		//TODO: Cache key states
		//TODO: Clear selection and select all buttons
		private Dictionary<string, ActionToggle> _toggles = new Dictionary<string, ActionToggle>();
		private Dictionary<string, bool> _logCondition = new Dictionary<string, bool>();
		private ActionScrollableTextArea _actionScrollableTextArea;
		private ActionVerticalArea _actionVerticalArea;
		private ActionButton _deselectAllButton;
		private ActionButton _selectAllButton;
		private List<string> _keys;

		internal ActionLogContent()
		{
			foreach (var val in ActionRepo.GetLogTypes()) _logCondition[val.ToString()] = true;

			_actionScrollableTextArea = new ActionScrollableTextArea(ActionStyle.ScrollableTextAreaStyle);

			var scrollBar = new ActionScrollBar();
			_actionVerticalArea = new ActionVerticalArea(scrollBar.BeginScrollView, scrollBar.EndScrollView);

			Refresh();

			_keys = new List<string>(_logCondition.Keys);
			foreach (var val in _keys) _toggles[val] = new ActionToggle();

			_selectAllButton = new ActionButton(ActionManagerConstants.SelectAll, () => SetAllKeys(true));
			_deselectAllButton = new ActionButton(ActionManagerConstants.DeselectAll, () => SetAllKeys(false));
		}

		internal sealed override void Display(EditorWindow editor)
		{
			DrawTags(editor);
			DrawTextArea(editor);
		}

		private void DrawTags(EditorWindow editor)
		{
			_actionVerticalArea.BeginVerticalArea(0, 30, 200, (int) editor.position.height - 10);

			foreach (var enumValue in _keys)
			{
				var temp = _logCondition[enumValue];
				_logCondition[enumValue] = _toggles[enumValue].DrawToggle(enumValue);
				if (temp != _logCondition[enumValue]) Refresh();
			}
			_selectAllButton.ProcessButton();
			_deselectAllButton.ProcessButton();
			_actionVerticalArea.EndVerticalArea();
		}

		private void DrawTextArea(EditorWindow editor)
		{
			GUILayout.BeginArea(new Rect(200, 25, editor.position.width - 205, editor.position.height - 30));
			_actionScrollableTextArea.Draw();
			GUILayout.EndArea();
		}

		internal sealed override void Refresh()
		{
			var allLogs = ActionRepo.GetLogs();

			_actionScrollableTextArea.ClearContext();

			foreach (var log in allLogs.Where(log => _logCondition[log.Type.ToString()]))
				_actionScrollableTextArea.AppendContext(log + ActionManagerConstants.NewLine);
		}

		private void SetAllKeys(bool value)
		{
			foreach (var enumValue in _toggles.Keys)
				_toggles[enumValue].SetToggleStatus(value);

			Refresh();
		}
	}
}