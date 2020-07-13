using UnityEditor;

namespace DeckAdam.ActionManager.UIComponent
{
	internal class Content
	{
		internal string ContentName => GetType().Name;

		internal virtual void Display(EditorWindow editor)
		{
		}

		internal virtual void Refresh()
		{
		}
	}
}