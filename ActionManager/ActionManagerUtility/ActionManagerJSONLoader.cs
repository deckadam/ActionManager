using System.IO;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class ActionManagerJSONLoader
	{
		internal static T LoadData<T>(string filePath) where T : new()
		{
			if (!File.Exists(filePath)) return new T();
			var data = File.ReadAllText(filePath);
			return JsonUtility.FromJson<T>(data);
		}

		internal static void SaveData<T>(T objectToSave, string filePath)
		{
			var data = JsonUtility.ToJson(objectToSave, true);
			File.WriteAllText(filePath, data);
		}
	}
}