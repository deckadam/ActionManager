using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal static class Style
	{
		public static readonly GUIStyle ScrollableTextAreaStyle= new GUIStyle(GUI.skin.textArea)
		{
			fontSize = 12,
			padding = new RectOffset(5,5,5,5),
			margin = new RectOffset(15,15,15,15),
			wordWrap = false
		};
	}
}