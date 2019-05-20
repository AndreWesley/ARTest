using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppEventListener : MonoBehaviour, IAppEvent
{

	[SerializeField] private ScriptableAppEvent appEvent;
	[SerializeField] private UnityEvent response;

    public void OnEventCalled (){
		response.Invoke();
	}

	private void OnEnable() {
		appEvent.AddListener(this);
	}

	private void OnDisable() {
		appEvent.RemoveListener(this);
	}
}
