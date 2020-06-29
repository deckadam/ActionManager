using UnityEditor;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class Toggle
	{
		private bool _value = true;

		internal bool DrawToggle(string param)
		{
			_value = EditorGUILayout.Toggle(param, _value);
			return _value;
		}

		internal void SetToggleStatus(bool value)
		{
			_value = value;
		}
	}
}