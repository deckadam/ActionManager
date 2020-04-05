using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class ActionManagerLog
	{
		private string _log;
		private float _time;

		internal ActionManagerLog(LogType type, string log)
		{
			_time = Time.time - ActionManager.InitializationTime;
			_log = log;
			Type = type;
		}

		internal LogType Type { get; }

		public override string ToString()
		{
			return _time + "  " + Type + "  " + _log;
		}
	}
}