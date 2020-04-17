﻿using System.Collections.Generic;
using DeckAdam.ActionManager.UIComponent;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class ActionSettingsContent : ActionContent
	{
		internal override string ContentName => "Settings";

		private Dictionary<string, int> _labelTable = new Dictionary<string, int>();
		private Dictionary<string, ActionColorFieldWithLabel> _colorPickers = new Dictionary<string, ActionColorFieldWithLabel>();
		private ActionButton _applyButton;
		private List<string> _keys;

		internal ActionSettingsContent()
		{
			var counter = 0;
			foreach (var val in ActionRepo.GetLogTypes())
			{
				_labelTable[val.ToString()] = counter++;
			}

			_keys = new List<string>(_labelTable.Keys);
			foreach (var val in _keys)
			{
				_colorPickers[val] = new ActionColorFieldWithLabel(val, ActionSettings.CurrentSettings.data[GetIndex(val)].GetColor());
			}

			_applyButton = new ActionButton(ActionManagerConstants.Apply);
		}

		internal sealed override void Display(EditorWindow editor)
		{
			DrawColorPickerWithLabel();
			ApplyButton();
		}

		private void ApplyButton()
		{
			if (!_applyButton.DrawButton()) return;
			ActionSettings.SaveSettings(ActionSettings.CurrentSettings);
		}

		private void DrawColorPickerWithLabel()
		{
			foreach (var label in _keys)
			{
				var result = _colorPickers[label].DrawColorField();
				ActionSettings.CurrentSettings.data[GetIndex(label)].SetColor(result);
			}
		}

		private int GetIndex(string label) => _labelTable[label];
	}
}