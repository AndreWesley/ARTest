using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ContentDownloader : MonoBehaviour {

	[SerializeField] private string uri = "https://whatsnextdigital.com.br/cdn/Shared/videos/whatsnextdigital_video.mp4";

	[SerializeField] private ScriptableAppEvent onDownloadFail;
	[SerializeField] private ScriptableAppEvent onContentInCache;
	[SerializeField] private ScriptableFloat progress;

	private string dirPath;
	private string filePath;

	void Awake () {
		//setup path names
		dirPath = Application.persistentDataPath + "/" + Constants.DIRECTORY_NAME;
		filePath = dirPath + "/" + Constants.FILE_NAME;

		//create directory		
		if (!Directory.Exists (dirPath)) {
			Directory.CreateDirectory (dirPath);
			PlayerPrefs.SetString (Constants.DIRECTORY_PATH_KEY, dirPath);
			PlayerPrefs.Save ();
		}
	}

	private IEnumerator DownloadContent () {
		UnityWebRequest webRequest = new UnityWebRequest (uri);
		webRequest.downloadHandler = new DownloadHandlerBuffer ();

		UnityWebRequestAsyncOperation asyncOp = webRequest.SendWebRequest ();

		while (!asyncOp.isDone) {
			progress.value = asyncOp.progress;
			yield return null;
		}

		if (webRequest.isNetworkError || webRequest.isHttpError) {
			onDownloadFail.Call ();
		} else {
			progress.value = asyncOp.progress;
			StoreContentAndPath (webRequest.downloadHandler.data);
			onContentInCache.Call ();
		}
	}

	public void StoreContentAndPath (byte[] content) {
		File.WriteAllBytes (filePath, content);
		PlayerPrefs.SetString (Constants.FILE_PATH_KEY, filePath);
		PlayerPrefs.Save ();
	}

	public void DownloadOrCacheContent () {
		if (!File.Exists (filePath)) {
			StartCoroutine (DownloadContent ());
		} else {
			onContentInCache.Call ();
		}
	}

}