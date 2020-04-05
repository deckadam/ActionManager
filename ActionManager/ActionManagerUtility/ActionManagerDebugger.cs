using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace DeckAdam.ActionManager
{
	public static class ActionManagerDebugger
	{
		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnActionManagerInitialized()
		{
			_logs.Clear();
			Identifiers.Clear();
			CreateNewLog("Action manager initialized from", LogType.OnActionManagerInitialized);
			ActionManagerEditor.Instance?.Initialize();
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearListeners()
		{
			CollectIdentifiers();
			CreateNewLog("All subscribers cleared", LogType.OnClearListeners);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnRemoveListener(long id, string name)
		{
			CollectIdentifiers();
			CreateNewLog("Subscriber removed with id (" + id + ") from with name (" + name + ")", LogType.OnRemoveListener);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnActionAdded(long id)
		{
			CollectIdentifiers();
			CreateNewLog("Subscribed to event with id (" + id + ")  from", LogType.OnActionAdded);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearListener(long id)
		{
			CollectIdentifiers();
			CreateNewLog("Event listeners cleared with id (" + id + ")", LogType.OnClearListener);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnTriggerAction(long id)
		{
			CreateNewLog("Event raised with id (" + id + ")", LogType.OnTriggerAction);
		}

#if UNITY_ASSERTIONS

		private const string Tab = "   ";
		internal static Dictionary<long, string> Identifiers = new Dictionary<long, string>();
		internal static Dictionary<long,List<string>> ConnectedListeners = new Dictionary<long, List<string>>();
		private static List<ActionManagerLog> _logs = new List<ActionManagerLog>();

		private static void AppendParagraph(string param, out string result)
		{
			param += "\n" + CollectStackTrace();
			result = param;
		}

		private static void CreateNewLog(string log, LogType logType)
		{
			AppendParagraph(log, out var result);
			_logs.Add(new ActionManagerLog(logType, result));

			if (ActionManagerEditor.Instance != null)
			{
				ActionManagerEditor.Instance.RefreshTabs();
			}
			else Debug.Log("Listener not found");
		}

		// TODO: Optimize this part (Probably there is better ways to do this)
		private static string CollectStackTrace()
		{
			var allTrace = "";
			var stackTrace = new StackTrace(true);
			var count = stackTrace.FrameCount;

			for (var i = 1; i < count; i++)
			{
				var frame = stackTrace.GetFrame(i);
				allTrace += frame.GetFileName() + Tab + frame.GetMethod().Name + Tab + frame.GetFileLineNumber() + "\n";
			}

			return allTrace;
		}

		private static void CollectIdentifiers()
		{
			var properties = ActionManager.EventClass.GetFields();
			foreach (var prop in properties)
			{
				var value = (long) prop.GetValue(null);
				if (!Identifiers.ContainsKey(value))
					Identifiers[value] = prop.Name;
			}
		}

		internal static List<ActionManagerLog> GetLogs() => _logs;
#endif
	}
}