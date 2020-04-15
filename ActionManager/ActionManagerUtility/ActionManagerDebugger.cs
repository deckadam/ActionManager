using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace DeckAdam.ActionManager
{
	public static class ActionManagerDebugger
	{
		//TODO: Colorized display to editor window, different color for different operations and changeable color option from settings menu
		//TODO: Make txt file output option (Save button to editor window)

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnActionManagerInitialized()
		{
			_logs.Clear();
			Identifiers.Clear();
			Identifiers.Add(long.MaxValue, ActionManagerConstants.Clear);
			CreateNewLog(ActionManagerLogCreator.GetActionManagerInitializedLog(), LogType.OnActionManagerInitialized);
			ActionManagerEditor.Instance?.Initialize();
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearListeners()
		{
			CollectIdentifiers();
			ClearListenerConnections();
			CreateNewLog(ActionManagerLogCreator.GetOnClearListenersLog(), LogType.OnClearListeners);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnRemoveListener(long id, string name)
		{
			CollectIdentifiers();
			SevereListenerConnection(id, name);
			CreateNewLog(ActionManagerLogCreator.GetOnRemoveListenerLog(id, name), LogType.OnRemoveListener);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnActionAdded(long id, string name)
		{
			CollectIdentifiers();
			AddListenerConnection(id, name);
			CreateNewLog(ActionManagerLogCreator.GetOnActionAddedLog(id, name), LogType.OnActionAdded);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearListener(long id)
		{
			CollectIdentifiers();
			SevereIdConnection(id);
			CreateNewLog(ActionManagerLogCreator.GetOnClearListenerLog(id), LogType.OnClearListener);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnTriggerAction(long id)
		{
			CreateNewLog(ActionManagerLogCreator.GetOnTriggerActionLog(id), LogType.OnTriggerAction);
		}

#if UNITY_ASSERTIONS

		internal static Dictionary<long, string> Identifiers = new Dictionary<long, string>();
		internal static Dictionary<long, List<string>> ConnectedListeners = new Dictionary<long, List<string>>();
		private static List<ActionManagerLog> _logs = new List<ActionManagerLog>();

		private static void AppendParagraph(string param, out string result)
		{
			param += ActionManagerConstants.NewLine + CollectStackTrace();
			result = param;
		}

		private static void CreateNewLog(string log, LogType logType)
		{
			AppendParagraph(log, out var result);
			_logs.Add(new ActionManagerLog(logType, result));

			if (ActionManagerEditor.Instance == null) return;
			ActionManagerEditor.Instance.RefreshTabs();
		}

		// TODO: Optimize this part (Probably there is better ways to do this)
		private static string CollectStackTrace()
		{
			var allTrace = ActionManagerConstants.EmptyString;
			var stackTrace = new StackTrace(true);
			var count = stackTrace.FrameCount;

			for (var i = 0; i < count; i++)
			{
				var frame = stackTrace.GetFrame(i);
				var newLog = frame.GetFileName() +
				             ActionManagerConstants.DoubleSpace +
				             frame.GetMethod().Name +
				             ActionManagerConstants.DoubleSpace +
				             frame.GetFileLineNumber();
				newLog = GetCroppedLog(newLog);
				allTrace += newLog + ActionManagerConstants.NewLine;
			}

			return allTrace;
		}


		private static string GetCroppedLog(string logToCrop)
		{
			var splittedText = logToCrop.Split(ActionManagerConstants.Divider);
			var returnString = ActionManagerConstants.EmptyString;
			for (var i = ActionSettings.CurrentSettings.logRemoveIndex; i < splittedText.Length; i++)
			{
				returnString += splittedText[i];
			}

			return returnString;
		}

		private static void CollectIdentifiers()
		{
			var properties = ActionManager.EventClass.GetFields();
			foreach (var prop in properties)
			{
				var value = (long) prop.GetValue(null);
				if (!Identifiers.ContainsKey(value))
				{
					Identifiers[value] = prop.Name;
				}
			}
		}

		private static void SevereListenerConnection(long id, string name)
		{
			if (CheckListenerStatus(id))
				ConnectedListeners[id].Remove(name);
		}

		private static void AddListenerConnection(long id, string name)
		{
			if (CheckListenerStatus(id))
				ConnectedListeners[id].Add(name);

			else
				ConnectedListeners[id] = new List<string> {name};
		}

		private static void ClearListenerConnections()
		{
			var keys = new List<long>(ConnectedListeners.Keys);

			foreach (var key in keys.Where(CheckListenerStatus))
				ConnectedListeners[key].Clear();
		}

		private static void SevereIdConnection(long id)
		{
			if (CheckListenerStatus(id))
				ConnectedListeners[id].Clear();
		}

		private static bool CheckListenerStatus(long id) => (ConnectedListeners.ContainsKey(id) && ConnectedListeners[id] != null);

		internal static List<ActionManagerLog> GetLogs() => _logs;
#endif
	}
}