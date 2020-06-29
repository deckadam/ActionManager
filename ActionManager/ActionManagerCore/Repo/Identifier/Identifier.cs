using System;
using UnityEngine;

namespace DeckAdam.ActionManager.Core.Repo
{
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