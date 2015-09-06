using UnityEngine;
using System.Collections;

public class GPSLoader : MonoBehaviour {
	IEnumerator Start() {
		if (!Input.location.isEnabledByUser) {
			Debug.Log("No permission");
			yield break;
		}
		Input.location.Start();
		int maxWait =  120;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		if (maxWait < 1) {
			print("Timed out");
			yield break;
		}
		if (Input.location.status == LocationServiceStatus.Failed) {
			print("Unable to determine device location");
			yield break;
		} else {
			while(true){
				yield return new WaitForSeconds(0.5f);
				Utils.lat =  Mathf.Lerp((float)Utils.lat,Input.location.lastData.latitude,0.9f);
				Utils.lang = Mathf.Lerp((float)Utils.lang,Input.location.lastData.longitude,0.9f);
				//Utils.alti = Input.location.lastData.altitude;

				 Debug.Log("Location: " + 
				      Input.location.lastData.latitude + " " + 
				      Input.location.lastData.longitude + " " + 
				      Input.location.lastData.altitude + " " + 
				      Input.location.lastData.horizontalAccuracy + " " + 
				      Input.location.lastData.timestamp);
			}
		}
		Input.location.Stop();
	}
}