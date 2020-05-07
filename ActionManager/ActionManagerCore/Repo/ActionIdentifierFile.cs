using System;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	[Serializable]
	internal class ActionIdentifierFile
	{
		internal static ActionIdentifierFile IdentifierFile;
		public Identifier[] identifiers;

		private void CollectData()
		{
			var count = ActionRepo.GetIdentifierCount();
			identifiers = new Identifier[count];

			var loopCount = 0;
			foreach (var val in ActionRepo.GetIdentifiers())
			{
				identifiers[loopCount++] = new Identifier(val.Key,
					val.Value,
					ActionRepo.GetConnectedListenersWithId(val.Key));
			}
		}

		internal static void SaveCurrentIdentifierState()
		{
			IdentifierFile = new ActionIdentifierFile();
			IdentifierFile.CollectData();
			ActionManagerJSONLoader.SaveData(IdentifierFile, ActionManagerConstants.IdentifierFilePath);
		}

		internal static void LoadIdentifierState()
		{
			IdentifierFile = ActionManagerJSONLoader.LoadData<ActionIdentifierFile>(ActionManagerConstants.IdentifierFilePath);
			Debug.Log("Saved");
		}

		[Serializable]
		internal class Identifier
		{
			public long Id;
			public string Name;
			public string[] ConnectedIdentifiers;

			public Identifier(long id, string name, string[] connectedIdentifiers)
			{
				Id = id;
				ConnectedIdentifiers = connectedIdentifiers;
				Name = name;
			}
		}
	}
}