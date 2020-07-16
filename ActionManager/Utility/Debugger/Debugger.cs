using System.Diagnostics;
using DeckAdam.ActionManager.Repo;

namespace DeckAdam.ActionManager
{
	public static partial class Debugger
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
	}
}