using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ActionButton
	{
		private string _text;

		internal ActionButton(string text)
		{
			_text = text;
		}

		internal bool DrawButton()
		{
			return GUILayout.Button(_text);
		}
	}
}