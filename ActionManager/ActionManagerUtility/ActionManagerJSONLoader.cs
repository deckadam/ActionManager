using System.IO;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class ActionManagerJSONLoader
	{
		internal static T LoadData<T>(string dataPath) where T : new()
		{
			if (!File.Exists(dataPath)) return new T();
			var data = File.ReadAllText(ActionManagerConstants.DataPath);
			return JsonUtility.FromJson<T>(data);
		}

		internal static void SaveData<T>(T objectToSave)
		{
			var data = JsonUtility.ToJson(objectToSave);
			File.WriteAllText(ActionManagerConstants.DataPath, data);
		}
	}
}