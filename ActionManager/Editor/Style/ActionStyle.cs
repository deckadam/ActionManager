using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal static class ActionStyle
	{
		internal static readonly GUIStyle ScrollableTextAreaStyle= new GUIStyle(GUI.skin.textArea)
		{
			wordWrap = false,
			stretchWidth = true,
			stretchHeight = true
		};

		internal static readonly  GUIStyle ButtonStyle = new GUIStyle(GUI.skin.button)
		{
			padding = new RectOffset(5,5,5,5),
			margin = new RectOffset(5,5,5,5),
		};
	}
}