using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal static class Constants
	{
		//TODO: Transfer all strings to constants
		//TODO: Read the strings from json file
		internal readonly static string NewLine = "\n";
		internal readonly static string DoubleSpace = "  ";
		internal readonly static string Apply = "Apply";
		internal readonly static string Title = "Action manager debugger";
		internal readonly static string SettingsFilePath = Application.dataPath + "/Settings.json";
		internal readonly static string IdentifierFilePath = Application.dataPath + "/IdentifierState.json";
		internal readonly static char Divider = '\\';
		internal readonly static string SelectAll = "Select all";
		internal readonly static string DeselectAll = "Deselect all";
		internal readonly static string SavetoFile = "Save";
		internal readonly static string LoadFromFile = "Load";

		internal static string SurrounWithParanthesis(string value) => '(' + value + ')';
		internal static string SurrounWithParanthesis(long value) => '(' + value.ToString() + ')';

		//TODO: Debug template implementation
		#region Debug templates

		#endregion
	}
}