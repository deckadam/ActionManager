using System.Collections.Generic;
using DeckAdam.ActionManager.Repo;
using UnityEditor;

namespace DeckAdam.ActionManager.UIComponent.SettingsContent
{
	internal class SettingsContent : Content
	{
		private Dictionary<string, int> _labelTable = new Dictionary<string, int>();
		private Dictionary<string, ColorFieldWithLabel> _colorPickers = new Dictionary<string, ColorFieldWithLabel>();
		private Button _applyButton;
		private Toggle _reportInvalidOperation;
		private List<string> _keys;

		internal SettingsContent()
		{
			var counter = 0;
			foreach (var val in Repository.GetLogTypes())
			{
				_labelTable[val.ToString()] = counter++;
			}

			_keys = new List<string>(_labelTable.Keys);
			foreach (var val in _keys)
			{
				_colorPickers[val] = new ColorFieldWithLabel(val, Settings.CurrentSettings.data[GetIndex(val)].GetColor());
			}

			_reportInvalidOperation = new Toggle((val) => Settings.CurrentSettings.reportInvalidOperation = val);
			_applyButton = new Button(Constants.Apply, Settings.SaveSettings);
		}

		internal sealed override void Display(EditorWindow editor)
		{
			DrawColorPickerWithLabel();
			_reportInvalidOperation.DrawToggle(Constants.ReportInvalidOperation);
			_applyButton.ProcessButton();
		}

		private void DrawColorPickerWithLabel()
		{
			foreach (var label in _keys)
			{
				var result = _colorPickers[label].DrawColorField();
				var index = GetIndex(label);
				Settings.CurrentSettings.data[index].SetColor(result);
			}
		}

		private int GetIndex(string label) => _labelTable[label];
	}
}