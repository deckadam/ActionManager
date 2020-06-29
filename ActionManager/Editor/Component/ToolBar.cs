using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ToolBar
	{
		private string[] _content;
		private int _currentTab = 0;

		internal ToolBar(string[] content)
		{
			_content = content;
		}

		internal int DrawToolbar()
		{
			_currentTab = GUILayout.Toolbar(_currentTab, _content);
			return _currentTab;
		}
	}
}