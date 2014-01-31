using UnityEngine;
using System.Collections;

public class HudFull : MonoBehaviour {

	bool isGamePaused = false;
	bool isFullHud = false, fromFull = false;

	GameObject mainCamera;
	AudioSource bgMusic;

	void Start(){
		mainCamera = GameObject.Find("MainCamera");
		bgMusic = mainCamera.GetComponent<AudioSource>();
	}

	void Update () {
		if(Input.GetButtonDown("Select")){
			if(!isFullHud){
				pauseTheGame();
			}
		}

		if(Input.GetButtonDown("Enter")){
			//pauseTheGame();
		
			Vector3 newPos;
			if(isFullHud){
				fromFull = true;
				newPos = new Vector3(0, -11.4f);
			}
			else {
				fromFull = false;
				isFullHud = true;
				newPos = new Vector3(0, 11.4f);
			}

			StartCoroutine(MoveToPosition(
				mainCamera.transform, 
				mainCamera.transform.position + newPos,
				1.8f
			));
		}
	}

	void pauseTheGame(){
		if(isGamePaused){
			Time.timeScale = 1;
			bgMusic.audio.Play();
		}
		else {
			Time.timeScale = 0;
			bgMusic.audio.Pause();
		}
		
		isGamePaused = !isGamePaused;
	}

	IEnumerator MoveToPosition(Transform tForm, Vector3 newPos, float time){
		float elapsedTime = 0;
		Vector3 startingPos = tForm.position;
		
		while (elapsedTime < time){
			tForm.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
			elapsedTime += Time.deltaTime;

			if(elapsedTime >= time){
				if(fromFull){
					isFullHud = false;
				}
			}
			
			yield return null;
		}
	}
}
