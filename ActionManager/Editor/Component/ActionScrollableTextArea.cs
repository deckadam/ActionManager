using System.Text;
using UnityEditor;
using UnityEngine;

namespace DeckAdam.ActionManager
{
	internal class ActionScrollableTextArea
	{
		private StringBuilder _data;
		private GUIStyle _style;
		private Vector2 _scroll;

		public ActionScrollableTextArea(GUIStyle style)
		{
			_data = new StringBuilder();
			_scroll = Vector2.zero;
			_style = style;
		}

		internal void Draw()
		{
			_scroll = EditorGUILayout.BeginScrollView(_scroll);
			EditorGUILayout.TextArea(_data.ToString(), _style,GUILayout.ExpandHeight(true),GUILayout.ExpandWidth(true));
			EditorGUILayout.EndScrollView();
		}

		internal void SetContext(string newContext)
		{
			_data.Clear();
			_data.Append(newContext);
		}

		internal void AppendContext(string newContext)
		{
			_data.AppendLine(newContext);
		}

		internal void ClearContext()
		{
			_data.Clear();
		}
	}
}