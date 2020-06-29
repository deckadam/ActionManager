using System;
using DeckAdam.ActionManager.Core.Repo;

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
}