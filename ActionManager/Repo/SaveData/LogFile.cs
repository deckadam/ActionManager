using System;

namespace DeckAdam.ActionManager.UIComponent.LogContent
{
	[Serializable]
	internal class LogFile
	{
		public Log[] SavedLog;

		internal static LogFile LoadLogFile()
		{
			return JsonLoader.LoadData<LogFile>(Constants.LogFilePath);
		}

		internal static void SaveLogFile(Log[] logsToSave)
		{
			JsonLoader.SaveData(new LogFile() {SavedLog = logsToSave}, Constants.LogFilePath);
		}
	}
}