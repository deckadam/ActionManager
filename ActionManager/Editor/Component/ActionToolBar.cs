using UnityEngine;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class ActionToolBar
	{
		private string[] _content;
		private int _currentTab = 0;

		internal ActionToolBar(string[] content)
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