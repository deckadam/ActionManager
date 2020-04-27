using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal static class ActionManagerConstants
	{
		//TODO: Transfer all strings to constants
		//TODO: Read the strings from json file
		internal readonly static string NewLine = "\n";
		internal readonly static string DoubleSpace = "  ";
		internal readonly static string Apply = "Apply";
		internal readonly static string Title = "Action manager debugger";
		internal readonly static string DataPath = Application.dataPath + "/Settings.json";
		internal readonly static string IdentifierFilePath = Application.dataPath + "IdentifierState.json";
		internal readonly static string EmptyString = "";
		internal readonly static char Divider = '\\';
		internal readonly static char OpenParanthesis = '(';
		internal readonly static char CloseParanthesis = ')';
		internal readonly static string SelectAll = "Select all";
		internal readonly static string DeselectAll = "Deselect all";

		#region Debug templates

		#endregion
	}
}