using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ActionButton
	{
		private string _text;
		private Action _actionToProcess;
		internal ActionButton(string text,Action actionToProcess)
		{
			_text = text;
			_actionToProcess = actionToProcess;
		}

		internal void ProcessButton()
		{
			if (GUILayout.Button(_text))
			{
				_actionToProcess?.Invoke();
			}
		}
	}
}