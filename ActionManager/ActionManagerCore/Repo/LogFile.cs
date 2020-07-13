//TODO: Log file implementation

using System;

namespace DeckAdam.ActionManager.UIComponent.LogContent
{
	[Serializable]
	internal class LogFile
	{
		public Log[] SavedLog;

		public static LogFile LoadLogFile()
		{
			return JsonLoader.LoadData<LogFile>(Constants.LogFilePath);
		}

		public static void SaveLogFile(Log[] logsToSave)
		{
			JsonLoader.SaveData(new LogFile(){SavedLog = logsToSave}, Constants.LogFilePath);
		}
	}
}