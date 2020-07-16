using System.Diagnostics;

namespace DeckAdam.ActionManager
{
	public partial class Debugger
	{
		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnReinitialization()
		{
			if (!IsReportingOn) return;
			CreateNewLog(Constants.OnReinitialization, LogType.OnInvallidOperation);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnNonInitializedOperation()
		{
			if (!IsReportingOn) return;
			CreateNewLog(Constants.OnNoninitalizedOperation, LogType.OnInvallidOperation);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnInvalidRemoveListener()
		{
			if (!IsReportingOn) return;
			CreateNewLog(Constants.OnNonExistingListenerRemoval, LogType.OnInvallidOperation);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnAddingEmptyAction()
		{
			if (!IsReportingOn) return;
			CreateNewLog(Constants.OnAddingToNonExistingKey, LogType.OnInvallidOperation);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnClearingNonExistingKey()
		{
			if (!IsReportingOn) return;
			CreateNewLog(Constants.OnClearingNonExistingListener, LogType.OnInvallidOperation);
		}

		[Conditional("UNITY_ASSERTIONS")]
		internal static void OnTriggeringNonExistingEvent()
		{
			if (!IsReportingOn) return;
			CreateNewLog(Constants.OnTriggeringNonExistingListener, LogType.OnInvallidOperation);
		}
	}
}