using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ActionToggle
	{
		private bool _value = true;

		internal bool DrawToggle(string param)
		{
			_value = EditorGUILayout.Toggle(param, _value);
			return _value;
		}
	}
}