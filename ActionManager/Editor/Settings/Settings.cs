using System;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	[Serializable]
	internal class Settings
	{
		internal static Settings CurrentSettings;
		public ActionTypeColorData[] data;
		public int logRemoveIndex = 0;
		public bool reportInvalidOperation = true;

		static Settings()
		{
			CurrentSettings = LoadSettings();
		}

		public Settings()
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

		internal static Settings LoadSettings()
		{
			return JsonLoader.LoadData<Settings>(Constants.SettingsFilePath);
		}

		internal static void SaveSettings()
		{
			JsonLoader.SaveData(CurrentSettings, Constants.SettingsFilePath);
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