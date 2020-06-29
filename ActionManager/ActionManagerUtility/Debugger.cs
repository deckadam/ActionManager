using System.Diagnostics;
using DeckAdam.ActionManager.Core.Repo;

namespace DeckAdam.ActionManager
{
	public static class Debugger
	{
		//TODO: Colorized display to editor window, different color for different operations and changeable color option from settings menu
		//TODO: Make txt file output option (Save button to editor window)

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnActionManagerInitialized()
		{
			CreateNewLog(LogCreator.GetActionManagerInitializedLog(), LogType.OnActionManagerInitialized);
		}

		// TODO: Collect identifiers optimize
		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearListeners()
		{
			Repository.CollectIdentifiers();
			Repository.ClearListenerConnections();
			CreateNewLog(LogCreator.GetOnClearListenersLog(), LogType.OnClearListeners);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnRemoveListener(long id, string name)
		{
			Repository.CollectIdentifiers();
			Repository.SevereListenerConnection(id, name);
			CreateNewLog(LogCreator.GetOnRemoveListenerLog(id, name), LogType.OnRemoveListener);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnActionAdded(long id, string name)
		{
			Repository.CollectIdentifiers();
			Repository.AddListenerConnection(id, name);
			CreateNewLog(LogCreator.GetOnActionAddedLog(id, name), LogType.OnActionAdded);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearListener(long id)
		{
			Repository.CollectIdentifiers();
			Repository.SevereIdConnection(id);
			CreateNewLog(LogCreator.GetOnClearListenerLog(id), LogType.OnClearListener);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnTriggerAction(long id)
		{
			CreateNewLog(LogCreator.GetOnTriggerActionLog(id), LogType.OnTriggerAction);
		}

#if UNITY_ASSERTIONS

		private static void CreateNewLog(string log, LogType logType)
		{
			var result = Constants.NewLine + CollectStackTrace();
			Repository.AddLog(new Log(logType, result));
			EditoreCore.Instance?.RefreshTabs();
		}

		// TODO: Optimize this part (Probably there is better ways to do this)
		private static string CollectStackTrace()
		{
			var allTrace = string.Empty;
			var stackTrace = new StackTrace(true);
			var count = stackTrace.FrameCount;

			for (var i = 0; i < count; i++)
			{
				var frame = stackTrace.GetFrame(i);
				var newLog = frame.GetFileName() +
				             Constants.DoubleSpace +
				             frame.GetMethod().Name +
				             Constants.DoubleSpace +
				             frame.GetFileLineNumber();
				newLog = GetCroppedLog(newLog);
				allTrace += newLog + Constants.NewLine;
			}

			return allTrace;
		}


		private static string GetCroppedLog(string logToCrop)
		{
			var splittedText = logToCrop.Split(Constants.Divider);
			var returnString = string.Empty;

			for (var i = Settings.CurrentSettings.logRemoveIndex; i < splittedText.Length; i++)
				returnString += splittedText[i];

			return returnString;
		}

#endif
	}
}