using System;
using System.IO;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	[Serializable]
	internal class ActionIdentifierFile
	{
		internal static ActionIdentifierFile IdentifierFile;
		public Identifier[] identifiers;

		static ActionIdentifierFile()
		{
			IdentifierFile = LoadIdentifierState();
		}

		public ActionIdentifierFile()
		{
			var keys = ActionRepo.GetIdentifiers().Keys;
			var count = keys.Count;
			identifiers = new Identifier[count];
			foreach (var key in keys)
			{
				identifiers[key] = new Identifier(key, ActionRepo.GetIdentifierName(key), ActionRepo.GetConnectedListenersWithId(key));
			}
		}

		internal static void SaveCurrentIdentifierState()
		{
			ActionManagerJSONLoader.SaveData(IdentifierFile);
		}

		internal static ActionIdentifierFile LoadIdentifierState()
		{
			return ActionManagerJSONLoader.LoadData<ActionIdentifierFile>(ActionManagerConstants.IdentifierFilePath);
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