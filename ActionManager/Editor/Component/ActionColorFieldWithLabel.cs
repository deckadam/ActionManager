using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ActionColorFieldWithLabel
	{
		private Color _color;
		private string _label;

		internal ActionColorFieldWithLabel(string label, Color initialColor)
		{
			_label = label;
			_color = initialColor;
		}

		internal Color DrawColorField()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(_label, GUILayout.MinWidth(250), GUILayout.MaxWidth(250));
			_color = EditorGUILayout.ColorField(_color);
			EditorGUILayout.EndHorizontal();
			return _color;
		}
	}
}