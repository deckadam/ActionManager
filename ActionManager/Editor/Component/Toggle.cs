using System;
using UnityEditor;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class Toggle
	{
		private bool _value = true;
		private Action<bool> _onValueChanged;

		internal Toggle()
		{
		}

		internal Toggle(Action<bool> callback)
		{
			_onValueChanged = callback;
		}

		internal bool DrawToggle(string param)
		{
			var result = EditorGUILayout.Toggle(param, _value);

			if (result != _value)
			{
				_onValueChanged?.Invoke(result);
			}

			_value = result;

			return _value;
		}

		internal void SetToggleStatus(bool value)
		{
			_value = value;
		}
	}
}