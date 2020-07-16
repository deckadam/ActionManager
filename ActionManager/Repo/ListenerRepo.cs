using System.Collections.Generic;
using System.Linq;

namespace DeckAdam.ActionManager.Repo
{
	internal static partial class Repository
	{
		private static Dictionary<long, List<string>> _connectedListeners;

		internal static void SevereListenerConnection(long id, string name)
		{
			if (CheckListenerStatus(id))
				_connectedListeners[id].Remove(name);
		}

		internal static void AddListenerConnection(long id, string name)
		{
			if (CheckListenerStatus(id))
				_connectedListeners[id].Add(name);

			else
				_connectedListeners[id] = new List<string> {name};
		}

		internal static void ClearListenerConnections()
		{
			var keys = new List<long>(_connectedListeners.Keys);

			foreach (var key in keys.Where(CheckListenerStatus))
				_connectedListeners[key].Clear();
		}

		internal static void SevereIdConnection(long id)
		{
			if (CheckListenerStatus(id))
				_connectedListeners[id].Clear();
		}

		private static void ReinsertConnections(long identifier, IEnumerable<string> connectedListener)
		{
			_connectedListeners[identifier] = connectedListener.ToList();
		}

		internal static string[] GetConnectedListenersWithId(long id)
		{
			return _connectedListeners[id] == null ? null : _connectedListeners[id].ToArray();
		}

		internal static bool IsListenerConnectedWithId(long id)
		{
			return _connectedListeners.ContainsKey(id);
		}

		private static bool CheckListenerStatus(long id)
		{
			return _connectedListeners.ContainsKey(id) && _connectedListeners[id] != null;
		}
	}
}