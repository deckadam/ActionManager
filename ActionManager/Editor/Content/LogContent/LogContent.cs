using System.Collections.Generic;
using System.Linq;
using DeckAdam.ActionManager.Core.Repo;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent.LogContent
{
	internal class LogContent : Content
	{
		internal override string ContentName => "Logs";

		//TODO: Cache key states
		//TODO: Clear selection and select all buttons
		private Dictionary<string, Toggle> _toggles = new Dictionary<string, Toggle>();
		private Dictionary<string, bool> _logCondition = new Dictionary<string, bool>();
		private ScrollableTextArea _actionScrollableTextArea;
		private VerticalArea _actionVerticalArea;
		private Button _deselectAllButton;
		private Button _selectAllButton;
		private List<string> _keys;

		internal LogContent()
		{
			foreach (var val in Repository.GetLogTypes()) _logCondition[val.ToString()] = true;

			_actionScrollableTextArea = new ScrollableTextArea(new GUIStyle(GUI.skin.textArea)
			{
				wordWrap = false,
				stretchWidth = true,
				stretchHeight = true
			});

			var scrollBar = new ScrollBar();
			_actionVerticalArea = new VerticalArea(scrollBar.BeginScrollView, scrollBar.EndScrollView);

			Refresh();

			_keys = new List<string>(_logCondition.Keys);
			foreach (var val in _keys) _toggles[val] = new Toggle();

			_selectAllButton = new Button(Constants.SelectAll, () => SetAllKeys(true));
			_deselectAllButton = new Button(Constants.DeselectAll, () => SetAllKeys(false));
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
			var allLogs = Repository.GetLogs();

			_actionScrollableTextArea.ClearContext();

			foreach (var log in allLogs.Where(log => _logCondition[log.Type.ToString()]))
				_actionScrollableTextArea.AppendContext(log + Constants.NewLine);
		}

		private void SetAllKeys(bool value)
		{
			foreach (var enumValue in _toggles.Keys)
				_toggles[enumValue].SetToggleStatus(value);

			Refresh();
		}
	}
}