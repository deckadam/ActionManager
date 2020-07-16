namespace DeckAdam.ActionManager
{
	internal class LogCreator
	{
		internal static string GetActionManagerInitializedLog()
		{
			return Constants.ActionManagerInitializedLog;
		}


		internal static string GetOnClearListenersLog()
		{
			return Constants.OnClearListenersLog;
		}


		internal static string GetOnRemoveListenerLog(long id, string name)
		{
			var result = Constants.OnRemoveListenerHeader;
			result += Constants.SurrounWithParanthesis(id);
			result += Constants.OnRemoveListenerBody;
			result += Constants.SurrounWithParanthesis(name);
			return result;
		}


		internal static string GetOnActionAddedLog(long id, string name)
		{
			var result = Constants.OnActionAddedHeader;
			result += Constants.SurrounWithParanthesis(id);
			result += Constants.OnActionAddedBody;
			result += Constants.SurrounWithParanthesis(name);
			result += Constants.OnActionAddedClosure;
			return result;
		}


		internal static string GetOnClearListenerLog(long id)
		{
			var result = Constants.OnClearListenerHeader;
			result += Constants.SurrounWithParanthesis(id);
			return result;
		}


		internal static string GetOnTriggerActionLog(long id)
		{
			var result = Constants.OnTriggerActionHeader;
			result += Constants.SurrounWithParanthesis(id);
			return result;
		}
	}
}