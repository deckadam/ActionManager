using System;
using System.Collections.Generic;
using DeckAdam.ActionManager.UIComponent;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class ActionSettingsContent : ActionContent
	{
		internal override string ContentName => "Settings";

		private static ActionSettings _currentActionSettings;
		private Dictionary<string, int> _labelTable = new Dictionary<string, int>();
		private ActionButton _applyButton;

		internal ActionSettingsContent()
		{
			var temp = Enum.GetValues(typeof(LogType));

			var counter = 0;
			foreach (var val in temp)
			{
				_labelTable[val.ToString()] = counter++;
			}

			_applyButton = new ActionButton(ActionManagerConstants.Apply);
		}

		internal sealed override void Display(EditorWindow editor)
		{
			EditorGUILayout.BeginVertical();
			DrawColorPickerWithLabel();
			ApplyButton();
			EditorGUILayout.EndVertical();
		}

		private void ApplyButton()
		{
			if (!_applyButton.DrawButton()) return;
			ActionSettings.SaveSettings(ActionSettings.CurrentSettings);
		}

		private void DrawColorPickerWithLabel()
		{
			EditorGUILayout.BeginVertical();

			var keys = new List<string>(_labelTable.Keys);
			foreach (var label in keys)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(label, GUILayout.MinWidth(250), GUILayout.MaxWidth(250));
				ActionSettings.CurrentSettings.data[GetIndex(label)].SetColor(EditorGUILayout.ColorField(ActionSettings.CurrentSettings.data[GetIndex(label)].GetColor()));
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndVertical();
		}

		private int GetIndex(string label) => _labelTable[label];
	}
}