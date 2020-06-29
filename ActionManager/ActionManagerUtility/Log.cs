using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class Log
	{
		private string _log;
		private float _time;

		internal Log(LogType type, string log)
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