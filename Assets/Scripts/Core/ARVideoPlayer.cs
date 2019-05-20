using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;

[RequireComponent (typeof (TrackableBehaviour), typeof (VideoPlayer))]
public class ARVideoPlayer : DefaultTrackableEventHandler {

	[SerializeField] private Camera cam;
	private VideoPlayer player;
	private bool pausedByUser;
	private bool pausedByTrackingEvent;

	new void Start () {
		base.Start ();
		player = GetComponent<VideoPlayer> ();
		pausedByTrackingEvent = false;
		pausedByUser = false;
		player.url = PlayerPrefs.GetString(Constants.FILE_PATH_KEY);
	}

	void Update () {
		// if (Input.touchCount > 0) {
		// 	Touch touch = Input.GetTouch (0);
		// 	if (touch.phase == TouchPhase.Began) {
		// 		Ray ray = cam.ScreenPointToRay (touch.position);
		// 		if (Physics.Raycast (ray)) {
		// 			PauseOrResume ();
		// 		}
		// 	}
		// }
	}

	protected override void OnTrackingFound () {
		base.OnTrackingFound ();
		if (!pausedByUser) {
			player.Play ();
		}
		pausedByTrackingEvent = false;
	}

	protected override void OnTrackingLost () {
		base.OnTrackingLost ();
		if (player && player.isPlaying) {
			player.Pause ();
		}
		pausedByTrackingEvent = true;
	}

	public void PauseOrResume () {
		if (pausedByTrackingEvent) {
			return;
		}
		pausedByUser = !pausedByUser;
		if (pausedByUser)
			player.Pause ();
		else
			player.Play ();
	}
}