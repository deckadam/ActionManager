using System.Collections.Generic;

namespace DeckAdam.ActionManager.Repo
{
	internal static partial class Repository
	{
#if UNITY_ASSERTIONS
		private static Dictionary<long, string> _identifiers;

		internal static void CollectIdentifiers()
		{
			var properties = Core.ActionManager.EventClass.GetFields();
			foreach (var prop in properties)
			{
				var value = (long) prop.GetValue(null);
				if (_identifiers.ContainsKey(value)) continue;
				_identifiers[value] = prop.Name;
				_connectedListeners[value] = new List<string>();
			}

			OnIdentifiersUpdated?.Invoke();
		}

		private static void ReinsertIdentifierNames(long identifier, string name)
		{
			_identifiers[identifier] = name;
		}

		internal static long GetIdentifierCount()
		{
			return _identifiers.Count;
		}

		internal static Dictionary<long, string> GetIdentifiers()
		{
			return _identifiers;
		}

		internal static string GetIdentifierName(long id)
		{
			return _identifiers[id];
		}
#endif
	}
}