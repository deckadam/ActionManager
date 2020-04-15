using System;
using System.Collections.Generic;
using System.Linq;
using DeckAdam.ActionManager.UIComponent;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ActionLogContent : ActionContent
	{
		internal override string ContentName => "Logs";

		//TODO: Keys cache
		//TODO: Cache key states
		//TODO: Clear selection and select all buttons
		private Dictionary<string, bool> _logCondition = new Dictionary<string, bool>();
		private List<string> _keys;
		private ActionScrollableTextArea _actionScrollableTextArea;
		private ActionScrollBar _tagActionScrollBar;
		private Dictionary<string, ActionToggle> _toggles = new Dictionary<string, ActionToggle>();

		internal ActionLogContent()
		{
			var temp = Enum.GetValues(typeof(LogType));
			foreach (var val in temp) _logCondition[val.ToString()] = true;
			_actionScrollableTextArea = new ActionScrollableTextArea(ActionStyle.ScrollableTextAreaStyle);
			_tagActionScrollBar = new ActionScrollBar();
			Refresh();
			_keys = new List<string>(_logCondition.Keys);
			foreach (var val in _keys) _toggles[val] = new ActionToggle();
		}

		internal sealed override void Display(EditorWindow editor)
		{
			DrawTags(editor);
			DrawTextArea(editor);
		}

		private void DrawTags(EditorWindow editor)
		{
			GUILayout.BeginArea(new Rect(5, 25, 200, editor.position.height - 5));
			EditorGUILayout.BeginVertical();
			_tagActionScrollBar.BeginScrollView();

			foreach (var enumValue in _keys)
			{
				var temp = _logCondition[enumValue];
				_logCondition[enumValue] = _toggles[enumValue].DrawToggle(enumValue);
				if (temp != _logCondition[enumValue]) Refresh();
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

		internal sealed override void Refresh()
		{
			var allLogs = ActionManagerDebugger.GetLogs();

			_actionScrollableTextArea.ClearContext();

			foreach (var log in allLogs.Where(log => _logCondition[log.Type.ToString()]))
				_actionScrollableTextArea.AppendContext(log + ActionManagerConstants.NewLine);
		}
	}
}