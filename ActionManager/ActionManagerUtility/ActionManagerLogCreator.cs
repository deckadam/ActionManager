namespace DeckAdam.ActionManager
{
	internal class ActionManagerLogCreator
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
			result += ActionManagerConstants.OpenParanthesis + id + ActionManagerConstants.CloseParanthesis;
			result += OnRemoveListenerBody;
			result += ActionManagerConstants.OpenParanthesis + name + ActionManagerConstants.CloseParanthesis;
			return result;
		}

		private static string OnActionAddedHeader => "Subscribed to event with id ";
		private static string OnActionAddedBody => " with name ";
		private static string OnActionAddedClosure => " from";

		internal static string GetOnActionAddedLog(long id, string name)
		{
			var result = OnActionAddedHeader;
			result += ActionManagerConstants.OpenParanthesis + id + ActionManagerConstants.CloseParanthesis;
			result += OnActionAddedBody;
			result += ActionManagerConstants.OpenParanthesis + name + ActionManagerConstants.CloseParanthesis;
			result += OnActionAddedClosure;
			return result;
		}


		private static string OnClearListenerHeader => "Event listeners cleared with id ";

		internal static string GetOnClearListenerLog(long id)
		{
			var result = OnClearListenerHeader;
			result += ActionManagerConstants.OpenParanthesis + id + ActionManagerConstants.CloseParanthesis;
			return result;
		}

		private static string OnTriggerActionHeader => "Event raised with id ";

		internal static string GetOnTriggerActionLog(long id)
		{
			var result = OnTriggerActionHeader;
			result += ActionManagerConstants.OpenParanthesis + id + ActionManagerConstants.CloseParanthesis;
			return result;
		}
	}
}