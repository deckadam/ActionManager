using System.Diagnostics;
using System.Text;
using Debug = UnityEngine.Debug;

namespace DeckAdam.ActionManager
{
	public static class ActionManagerDebugger
	{
		private static StringBuilder _completeDebug = new StringBuilder();
		private static StackTrace _stackTrace = new StackTrace(true);
		private const string Tab = "   ";
		private const string DentedLine = "-----------------------------------------------------------------";

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnActionManagerInitialized()
		{
			AppendLine("Action manager initialized from : ");
			AppendParagraphEnding();
			Debug.Log(_completeDebug);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearListeners()
		{
			AppendLine("All listeners cleared");
			AppendParagraphEnding();
			Debug.Log(_completeDebug);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnRemoveListener(long id, string name)
		{
			AppendLine("Listener removed with id (" + id + ") from");
			AppendParagraphEnding();
			Debug.Log(_completeDebug);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnActionAdded(long id)
		{
			AppendLine("Action subscribed with id (" + id + ")  from");
			AppendParagraphEnding();
			Debug.Log(_completeDebug);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearListener()
		{
			AppendLine("Event listeners cleared");
			AppendParagraphEnding();
			Debug.Log(_completeDebug);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnTriggerAction(long id)
		{
			AppendLine("Event raised with id (" + id + ")");
			AppendParagraphEnding();
			Debug.Log(_completeDebug);
		}

		private static void AppendParagraphEnding()
		{
			_completeDebug.AppendLine(CollectStackTrace().ToString());
			_completeDebug.AppendLine(DentedLine);
		}

		private static void AppendLine(string text)
		{
			_completeDebug.AppendLine(text);
		}

		private static StringBuilder CollectStackTrace()
		{
			var allTrace = new StringBuilder();
			var count = _stackTrace.FrameCount;

			for (var i = 1; i < count; i++)
			{
				var frame = _stackTrace.GetFrame(i);
				allTrace.AppendLine(frame.GetFileName() + Tab + frame.GetMethod().Name + Tab + frame.GetFileLineNumber());
			}

			return allTrace;
		}
	}
}