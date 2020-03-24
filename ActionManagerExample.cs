using System.Collections;
using DeckAdam.ActionManager;
using UnityEngine;

public class ActionManagerExample : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		// Call this once for initializing action manager
		// when recalled all subscribed events will be erased
		ActionManager.Init();
		
		//Subscribe to ActionManagerTestEvent with desired Action<> 
		ActionManager.AddAction(TestEvents.ActionManagerTestEvent, TestMethod);
		ActionManager.AddAction(TestEvents.ActionManagerExampleEvent,()=> Debug.Log("Never triggered event"));

		// Start timed event for triggering event
		StartCoroutine(TestTrigger());
	}

	private void TestMethod()
	{
		Debug.Log("Test action triggered");
	}

	private IEnumerator TestTrigger()
	{
		// Schedule event to be triggered one second later after start method is called
		yield return new WaitForSeconds(1f);
		Debug.Log("Event triggered");
		// Trigger event to run subscribed methods
		ActionManager.TriggerAction(TestEvents.ActionManagerTestEvent);
		ActionManager.RemoveListener(TestEvents.ActionManagerTestEvent,TestMethod);
	}
}

// Define Globally reachable event id class
public static partial class TestEvents
{
	// Event id holder to be used through all codebase without coupling the listeners to each other
	public static readonly long ActionManagerTestEvent = ActionManager.NextTriggerIndex;
}

// Sample usage of partial event class for easier access
// with this usage they came under same class name which saves us time from
// searching through code base for event identifiers
public static partial class TestEvents
{
	public static readonly long ActionManagerExampleEvent = ActionManager.NextTriggerIndex;
}