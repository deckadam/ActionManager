using System.Diagnostics;
using DeckAdam.ActionManager.Repo;

namespace DeckAdam.ActionManager
{
	public partial class Debugger
	{
#if UNITY_ASSERTIONS

		private static bool IsReportingOn => (Settings.CurrentSettings.reportInvalidOperation);


		private static void CreateNewLog(string log, LogType logType)
		{
			var result = Constants.NewLine + log + Constants.NewLine + CollectStackTrace();
			Repository.AddLog(new Log(logType, result));
			if (EditoreCore.Instance != null)
			{
				EditoreCore.Instance.RefreshTabs();
			}
		}

		// TODO: Optimize this part (Probably there is better ways to do this)
		private static string CollectStackTrace()
		{
			var allTrace = string.Empty;
			var stackTrace = new StackTrace(true);
			var count = stackTrace.FrameCount;

			for (var i = 4; i < count; i++)
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
				returnString += Constants.ReverseDivider + splittedText[i];

			return returnString;
		}

#endif
	}
}