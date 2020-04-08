using System;
using System.IO;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	[Serializable]
	internal class Settings
	{
		private static readonly string SettingsPath = Application.dataPath + "/Settings.json";

		public ActionTypeColorData[] data;

		private Settings()
		{
			var values = Enum.GetValues(typeof(LogType));
			InitializeColors(values);
		}

		private void InitializeColors(Array values)
		{
			data = new ActionTypeColorData[values.Length];
			foreach (var val in values)
			{
				data[val.GetHashCode()] = new ActionTypeColorData(val.ToString(), Color.blue);
			}
		}

		internal static Settings LoadSettings()
		{
			if (!File.Exists(SettingsPath)) return new Settings();
			var data = File.ReadAllText(SettingsPath);
			return JsonUtility.FromJson<Settings>(data);
		}

		internal static void SaveSettings(Settings value)
		{
			var data = JsonUtility.ToJson(value);
			File.WriteAllText(SettingsPath, data.ToString());
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