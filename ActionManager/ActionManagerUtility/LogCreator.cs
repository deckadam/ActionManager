namespace DeckAdam.ActionManager
{
	internal class LogCreator
	{
		private static string ActionManagerInitializedLog => "Action manager initialized from";

		internal static string GetActionManagerInitializedLog()
		{
			return ActionManagerInitializedLog;
		}

		private static string OnClearListenersLog => "All subscribers cleared";

		internal static string GetOnClearListenersLog()
		{
			return OnClearListenersLog;
		}

		private static string OnRemoveListenerHeader => "Subscriber removed with id ";
		private static string OnRemoveListenerBody => " from with name ";

		internal static string GetOnRemoveListenerLog(long id, string name)
		{
			var result = OnRemoveListenerHeader;
			result += Constants.SurrounWithParanthesis(id);
			result += OnRemoveListenerBody;
			result += Constants.SurrounWithParanthesis(name);
			return result;
		}

		private static string OnActionAddedHeader => "Subscribed to event with id ";
		private static string OnActionAddedBody => " with name ";
		private static string OnActionAddedClosure => " from";

		internal static string GetOnActionAddedLog(long id, string name)
		{
			var result = OnActionAddedHeader;
			result += Constants.SurrounWithParanthesis(id);
			result += OnActionAddedBody;
			result += Constants.SurrounWithParanthesis(name);
			result += OnActionAddedClosure;
			return result;
		}


		private static string OnClearListenerHeader => "Event listeners cleared with id ";

		internal static string GetOnClearListenerLog(long id)
		{
			var result = OnClearListenerHeader;
			int.Parse(result += Constants.SurrounWithParanthesis(id));
			return result;
		}

		private static string OnTriggerActionHeader => "Event raised with id ";

		internal static string GetOnTriggerActionLog(long id)
		{
			var result = OnTriggerActionHeader;
			result += Constants.SurrounWithParanthesis(id);
			return result;
		}
	}
}