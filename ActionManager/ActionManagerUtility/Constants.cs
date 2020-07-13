using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal static class Constants
	{
		internal readonly static string IdentifierFilePath = Application.dataPath + "/IdentifierState.json";
		internal readonly static string SettingsFilePath = Application.dataPath + "/Settings.json";
		internal readonly static string LogFilePath = Application.dataPath + "/Logs.json";

		internal readonly static string NewLine = "\n";
		internal readonly static string DoubleSpace = "  ";

		internal readonly static char Divider = '\\';

		internal readonly static string Apply = "Apply";
		internal readonly static string Title = "Action manager debugger";
		internal readonly static string SelectAll = "Select all";
		internal readonly static string DeselectAll = "Deselect all";
		internal readonly static string SavetoFile = "Save";
		internal readonly static string LoadFromFile = "Load";

		internal static string ActionManagerInitializedLog => "Action manager initialized from";

		internal static string OnClearListenersLog => "All subscribers cleared";

		internal static string OnRemoveListenerHeader => "Subscriber removed with id ";
		internal static string OnRemoveListenerBody => " from with name ";

		internal static string OnActionAddedHeader => "Subscribed to event with id ";
		internal static string OnActionAddedBody => " with name ";
		internal static string OnActionAddedClosure => " from";

		internal static string OnClearListenerHeader => "Event listeners cleared with id ";

		internal static string OnTriggerActionHeader => "Event raised with id ";

		internal static string SurrounWithParanthesis(string value) => '(' + value + ')';
		internal static string SurrounWithParanthesis(long value) => '(' + value.ToString() + ')';
	}
}