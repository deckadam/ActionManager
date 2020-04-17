using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckAdam.ActionManager
{
	internal static class ActionRepo
	{
#if UNITY_ASSERTIONS

		private static Dictionary<long, string> _identifiers = new Dictionary<long, string>();
		private static Dictionary<long, List<string>> _connectedListeners = new Dictionary<long, List<string>>();
		private static List<ActionManagerLog> _logs = new List<ActionManagerLog>();
		private static Array _logTypes;

		internal static void InitializeRepo()
		{
			_identifiers.Clear();
			_logs.Clear();
			_logTypes = Enum.GetValues(typeof(LogType));
		}

		internal static void AddLog(ActionManagerLog newLog)
		{
			_logs.Add(newLog);
		}

		internal static void CollectIdentifiers()
		{
			var properties = ActionManager.EventClass.GetFields();
			foreach (var prop in properties)
			{
				var value = (long) prop.GetValue(null);
				if (!_identifiers.ContainsKey(value))
					_identifiers[value] = prop.Name;
			}
		}

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

		private static bool CheckListenerStatus(long id) => (_connectedListeners.ContainsKey(id) && _connectedListeners[id] != null);

		internal static IEnumerable<ActionManagerLog> GetLogs() => _logs.ToArray();
		internal static IEnumerable<string> GetConnectedListenersWithId(long id) => _connectedListeners[id];
		
		internal static Dictionary<long, string> GetIdentifiers() => _identifiers;
		internal static Array GetLogTypes() => _logTypes;
		
		internal static bool IsListenerConnectedWithId(long id) => _connectedListeners.ContainsKey(id);
		internal static string GetIdentifierName(long id) => _identifiers[id];
#endif
	}
}