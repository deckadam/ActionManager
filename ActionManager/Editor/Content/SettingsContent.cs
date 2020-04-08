using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DeckAdam.ActionManager;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class SettingsContent : ActionContent
	{
		internal static Settings CurrentSettings;
		private Dictionary<string, int> _labelTable = new Dictionary<string, int>();

		internal SettingsContent()
		{
			var temp = Enum.GetValues(typeof(LogType));

			var counter = 0;
			foreach (var val in temp)
			{
				_labelTable[val.ToString()] = counter++;
			}

			CurrentSettings = Settings.LoadSettings();
		}

		public sealed override void Display()
		{
			EditorGUILayout.BeginVertical();
			DrawColorPickerWithLabel();
			ApplyButton();
			EditorGUILayout.EndVertical();
		}

		private void ApplyButton()
		{
			if (!GUILayout.Button("Apply")) return;
			Settings.SaveSettings(CurrentSettings);
		}

		private void DrawColorPickerWithLabel()
		{
			EditorGUILayout.BeginVertical();

			var keys = new List<string>(_labelTable.Keys);
			foreach (var label in keys)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(label,GUILayout.MinWidth(250),GUILayout.MaxWidth(250));
				CurrentSettings.data[GetIndex(label)].SetColor(EditorGUILayout.ColorField(CurrentSettings.data[GetIndex(label)].GetColor()));
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndVertical();
		}

		private int GetIndex(string label) => _labelTable[label];
	}
}