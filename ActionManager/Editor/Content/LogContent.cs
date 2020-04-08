using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	public class LogContent : ActionContent
	{
		private Dictionary<string, bool> _logCondition = new Dictionary<string, bool>();
		private ScrollableTextArea _scrollableTextArea;

		public LogContent()
		{
			var temp = Enum.GetValues(typeof(LogType));
			foreach (var val in temp) _logCondition[val.ToString()] = true;
			_scrollableTextArea = new ScrollableTextArea(Style.ScrollableTextAreaStyle);
			Refresh();
		}

		public sealed override void Display()
		{
			EditorGUILayout.BeginHorizontal();
			DrawLabels();
			_scrollableTextArea.Draw();
			EditorGUILayout.EndHorizontal();
		}

		public sealed override void Refresh()
		{
			var allLogs = ActionManagerDebugger.GetLogs();

			_scrollableTextArea.ClearContext();

			foreach (var log in allLogs)
				if (_logCondition[log.Type.ToString()])
					_scrollableTextArea.AppendContext(log + "\n");
		}

		private void DrawLabels()
		{
			EditorGUILayout.BeginVertical(GUILayout.MaxWidth(300));
			var keys = new List<string>(_logCondition.Keys);
			foreach (var enumValue in keys)
				_logCondition[enumValue] = EditorGUILayout.Toggle(enumValue, _logCondition[enumValue], GUILayout.MinWidth(249), GUILayout.MaxWidth(250));
			EditorGUILayout.EndVertical();
		}
	}
}