using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class Button
	{
		private string _text;
		private Action _actionToProcess;
		internal Button(string text,Action actionToProcess)
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