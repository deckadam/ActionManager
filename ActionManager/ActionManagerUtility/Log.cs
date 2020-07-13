using System;

namespace DeckAdam.ActionManager
{
	[Serializable]
	internal class Log
	{
		public LogType Type;
		public string LogData;
		public float Time;

		internal Log(LogType type, string logData)
		{
			Time = UnityEngine.Time.time - ActionManager.InitializationTime;
			LogData = logData;
			Type = type;
		}


		public override string ToString()
		{
			return Time + "  " + Type + "  " + LogData;
		}
	}
}