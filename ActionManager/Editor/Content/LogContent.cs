using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	public class LogContent : IActionContent
	{
		private Dictionary<string, bool> _logCondition = new Dictionary<string, bool>();
		private StringBuilder _printedLogs;

		public LogContent()
		{
			var temp = Enum.GetValues(typeof(LogType));
			foreach (var val in temp) _logCondition[val.ToString()] = true;
			_printedLogs = new StringBuilder();
			Refresh();
		}

		public void Display()
		{
			EditorGUILayout.BeginHorizontal();
			DrawLabels();
			DrawTextArea();
			EditorGUILayout.EndHorizontal();
		}

		public void Refresh()
		{
			var allLogs = ActionManagerDebugger.GetLogs();

			_printedLogs.Clear();

			foreach (var log in allLogs)
				if (_logCondition[log.Type.ToString()])
					_printedLogs.AppendLine(log + "\n");
		}

		private void DrawLabels()
		{
			EditorGUILayout.BeginVertical();

			var keys = new List<string>(_logCondition.Keys);
			foreach (var enumValue in keys)
				_logCondition[enumValue] = EditorGUILayout.Toggle(enumValue, _logCondition[enumValue],GUILayout.MinWidth(250),GUILayout.MaxWidth(250));

			EditorGUILayout.EndVertical();
		}

		private void DrawTextArea()
		{
			EditorGUILayout.TextArea(_printedLogs.ToString(), GUILayout.MinHeight(999), GUILayout.MinWidth(999));
		}
	}
}