using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal static class ActionRepo
	{
#if UNITY_ASSERTIONS
		internal static Action OnIdentifiersUpdated;

		private static Dictionary<long, List<string>> _connectedListeners;
		private static Dictionary<long, string> _identifiers;
		private static List<ActionManagerLog> _logs;
		private static Array _logTypes;

		static ActionRepo()
		{
			_connectedListeners = new Dictionary<long, List<string>>();
			_identifiers = new Dictionary<long, string>();
			_logs = new List<ActionManagerLog>();
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
				if (_identifiers.ContainsKey(value)) continue;
				_identifiers[value] = prop.Name;
				_connectedListeners[value] = new List<string>();
			}

			OnIdentifiersUpdated?.Invoke();
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

		internal static void SaveLogStatus()
		{
		}

		internal static void LoadLogStatus()
		{
		}

		internal static void SaveIdentifierStatus()
		{
			ActionIdentifierFile.SaveCurrentIdentifierState();
		}

		internal static void LoadIdentifierStatus()
		{
			ActionIdentifierFile.LoadIdentifierState();
			var oldData = ActionIdentifierFile.IdentifierFile.identifiers;

			foreach (var val in oldData)
			{
				ReinsertIdentifierNames(val.Id, val.Name);
				ReinsertConnections(val.Id, val.ConnectedIdentifiers);
			}
		}

		private static void ReinsertIdentifierNames(long identifier, string name)
		{
			_identifiers[identifier] = name;
		}

		private static void ReinsertConnections(long identifier, IEnumerable<string> connectedListener)
		{
			_connectedListeners[identifier] = connectedListener.ToList();
		}

		internal static string[] GetConnectedListenersWithId(long id)
		{
			Debug.Log(_connectedListeners == null);
			if (_connectedListeners[id] == null) return null;
			return _connectedListeners[id].ToArray();
		}

		internal static bool IsListenerConnectedWithId(long id) => _connectedListeners.ContainsKey(id);

		internal static Dictionary<long, string> GetIdentifiers() => _identifiers;
		internal static string GetIdentifierName(long id) => _identifiers[id];
		internal static long GetIdentifierCount() => _identifiers.Count;

		internal static IEnumerable<ActionManagerLog> GetLogs() => _logs.ToArray();

		internal static Array GetLogTypes() => _logTypes;

		private static bool CheckListenerStatus(long id) => (_connectedListeners.ContainsKey(id) && _connectedListeners[id] != null);


#endif
	}
}