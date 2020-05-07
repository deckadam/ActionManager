using System;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	[Serializable]
	internal class ActionSettings
	{
		internal static ActionSettings CurrentSettings;
		public ActionTypeColorData[] data;
		public int logRemoveIndex = 0;

		static ActionSettings()
		{
			CurrentSettings = LoadSettings();
		}

		public ActionSettings()
		{
			var values = Enum.GetValues(typeof(LogType));
			InitializeColors(values);
		}

		private void InitializeColors(Array values)
		{
			data = new ActionTypeColorData[values.Length];
			foreach (var val in values)
			{
				data[val.GetHashCode()] = new ActionTypeColorData(val.ToString(), Color.black);
			}
		}

		internal static ActionSettings LoadSettings()
		{
			return ActionManagerJSONLoader.LoadData<ActionSettings>(ActionManagerConstants.SettingsFilePath);
		}

		internal static void SaveSettings()
		{
			ActionManagerJSONLoader.SaveData(CurrentSettings, ActionManagerConstants.SettingsFilePath);
		}
	}

	[Serializable]
	internal class ActionTypeColorData
	{
		public string name;
		public Color color;
		private string _colorHexCode;

		public string GetName() => name;
		public string GetColorCode() => _colorHexCode;
		public Color GetColor() => color;

		public void SetName(string value) => name = value;

		public void SetColor(Color value)
		{
			color = value;
			_colorHexCode = ColorUtility.ToHtmlStringRGB(value);
		}

		public ActionTypeColorData(string name, Color color)
		{
			this.name = name;
			this.color = color;
		}
	}
}