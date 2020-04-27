using System.Diagnostics;

namespace DeckAdam.ActionManager
{
	public static class ActionManagerDebugger
	{
		//TODO: Colorized display to editor window, different color for different operations and changeable color option from settings menu
		//TODO: Make txt file output option (Save button to editor window)

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnActionManagerInitialized()
		{
			CreateNewLog(ActionManagerLogCreator.GetActionManagerInitializedLog(), LogType.OnActionManagerInitialized);
		}
		
		// TODO: Collect identifiers optimize
		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearListeners()
		{
			ActionRepo.CollectIdentifiers();
			ActionRepo.ClearListenerConnections();
			CreateNewLog(ActionManagerLogCreator.GetOnClearListenersLog(), LogType.OnClearListeners);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnRemoveListener(long id, string name)
		{
			ActionRepo.CollectIdentifiers();
			ActionRepo.SevereListenerConnection(id, name);
			CreateNewLog(ActionManagerLogCreator.GetOnRemoveListenerLog(id, name), LogType.OnRemoveListener);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnActionAdded(long id, string name)
		{
			ActionRepo.CollectIdentifiers();
			ActionRepo.AddListenerConnection(id, name);
			CreateNewLog(ActionManagerLogCreator.GetOnActionAddedLog(id, name), LogType.OnActionAdded);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearListener(long id)
		{
			ActionRepo.CollectIdentifiers();
			ActionRepo.SevereIdConnection(id);
			CreateNewLog(ActionManagerLogCreator.GetOnClearListenerLog(id), LogType.OnClearListener);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnTriggerAction(long id)
		{
			CreateNewLog(ActionManagerLogCreator.GetOnTriggerActionLog(id), LogType.OnTriggerAction);
		}

#if UNITY_ASSERTIONS

		private static void CreateNewLog(string log, LogType logType)
		{
			var result = ActionManagerConstants.NewLine + CollectStackTrace();
			ActionRepo.AddLog(new ActionManagerLog(logType, result));
			ActionManagerEditor.Instance?.RefreshTabs();
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
				returnString += splittedText[i];

			return returnString;
		}

#endif
	}
}