using System;
using System.Collections.Generic;

namespace DeckAdam.ActionManager.Repo
{
	internal static partial class Repository
	{
#if UNITY_ASSERTIONS
		private static List<Log> _logs;
		private static Array _logTypes;

		internal static void AddLog(Log newLog)
		{
			_logs.Add(newLog);
		}

		internal static IEnumerable<Log> GetLogs()
		{
			return _logs.ToArray();
		}

		internal static Array GetLogTypes()
		{
			return _logTypes;
		}
#endif	
	}
}