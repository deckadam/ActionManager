using System;
using System.Collections.Generic;
using System.Linq;
using DeckAdam.ActionManager.UIComponent.LogContent;

namespace DeckAdam.ActionManager.Repo
{
	internal static partial class Repository
	{
#if UNITY_ASSERTIONS
		internal static Action OnIdentifiersUpdated;

		internal static void InitializeRepository()
		{
			_connectedListeners = new Dictionary<long, List<string>>();
			_identifiers = new Dictionary<long, string>();
			_logs = new List<Log>();
			_logTypes = Enum.GetValues(typeof(LogType));
		}

		internal static void SaveIdentifierStatus()
		{
			IdentifierFile.SaveCurrentIdentifierState();
		}

		internal static void LoadIdentifierStatus()
		{
			IdentifierFile.LoadIdentifierState();
			var oldData = IdentifierFile.identifierFile.identifiers;

			foreach (var val in oldData)
			{
				ReinsertIdentifierNames(val.id, val.name);
				ReinsertConnections(val.id, val.connectedIdentifiers);
			}
		}

		internal static void SaveLogStatus(Dictionary<string, bool> logCondition)
		{
			var selectedLogs = GetLogs().Where(log => logCondition[log.Type.ToString()]).ToList();
			LogFile.SaveLogFile(selectedLogs.ToArray());
		}

		internal static void LoadLogStatus()
		{
			var logs = LogFile.LoadLogFile().SavedLog;
			foreach (var log in logs)
			{
				AddLog(log);
			}
		}
#endif
	}
}