using System;
using DeckAdam.ActionManager.Repo;

namespace DeckAdam.ActionManager
{
	[Serializable]
	internal class IdentifierFile
	{
		internal static IdentifierFile identifierFile;
		
		public ActionIdentifier[] identifiers;

		private void CollectData()
		{
			var count = Repository.GetIdentifierCount();
			identifiers = new ActionIdentifier[count];

			var loopCount = 0;
			foreach (var val in Repository.GetIdentifiers())
			{
				identifiers[loopCount++] = new ActionIdentifier(val.Key,
					val.Value,
					Repository.GetConnectedListenersWithId(val.Key));
			}
		}

		internal static void SaveCurrentIdentifierState()
		{
			identifierFile = new IdentifierFile();
			identifierFile.CollectData();
			JsonLoader.SaveData(identifierFile, Constants.IdentifierFilePath);
		}

		internal static void LoadIdentifierState()
		{
			identifierFile = JsonLoader.LoadData<IdentifierFile>(Constants.IdentifierFilePath);
		}
	}
	
	[Serializable]
	internal class ActionIdentifier
	{
		public long id;
		public string name;
		public string[] connectedIdentifiers;

		public ActionIdentifier(long id, string name, string[] connectedIdentifiers)
		{
			this.id = id;
			this.connectedIdentifiers = connectedIdentifiers;
			this.name = name;
		}
	}
}