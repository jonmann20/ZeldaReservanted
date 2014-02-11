using UnityEngine;
using System.Collections;

public class HudFull : MonoBehaviour {

	bool isGamePaused = false,
		 isFullHud = false,		// true/false when animation over
		 fromFull = false,		// previous state of transition
		 doHud = false			// true/false on Enter press
	;

	float newCameraY = 0;

	GameObject mainCamera, rectSelectorPrefab, rectSelector;
	AudioSource bgMusic;

	public Sprite[] spr;
	SpriteRenderer sprRend;

	void Start(){
		mainCamera = GameObject.Find("MainCamera");
		bgMusic = mainCamera.GetComponent<AudioSource>();

		rectSelectorPrefab = Resources.Load<GameObject>("hudRectSelector");
	}

	void Update () {
		if(Input.GetButtonDown("Select")){
			if(!isFullHud && (Dungeon.that == null || !Dungeon.that.isAnimating)){
				pauseTheGame();
			}
		}

		if(Input.GetButtonDown("Enter")){
			if(!doHud && !isGamePaused && (GameplayMain.that == null || !GameplayMain.that.screenScrolling) && (Dungeon.that == null || !Dungeon.that.isAnimating)){
				Time.timeScale = 0;
			
				if(isFullHud){
					Destroy(rectSelector);
					
					fromFull = true;
					newCameraY = -11.4f;
				}
				else {
					fromFull = false;
					isFullHud = true;
					newCameraY = 11.4f;


					GameObject.Find("rupeeNum").GetComponent<GUIText>().enabled = false;
					GameObject.Find("keyNum").GetComponent<GUIText>().enabled = false;
					GameObject.Find("bombNum").GetComponent<GUIText>().enabled = false;
					GameObject.Find ("LvlName").GetComponent<GUIText>().enabled = false;

					if(GameObject.Find("OldManTxt(Clone)") != null){
						GameObject.Find ("OldManTxt(Clone)").GetComponent<GUIText>().enabled = false;
					}

					if(GameplayMain.that != null && GameplayMain.that.isSpeech){
						GameplayMain.that.hideSpeech = true;
					}

				}

				doHud = true;
			}
		}

		if(doHud){
			bool flag = false;

			if(newCameraY > 0){
				mainCamera.transform.Translate(0, 0.18f, 0);

				if(mainCamera.transform.position.y >= newCameraY){
					flag = true;
				}
			}
			else if(newCameraY < 0){
				mainCamera.transform.Translate(0, -0.18f, 0);

				if(mainCamera.transform.position.y <= 0){
					mainCamera.transform.position = new Vector3(0, 0, -2);
					flag = true;
				}
			}

			if(flag){
				if(fromFull){
					isFullHud = false;
					Time.timeScale = 1;

					GameObject.Find("rupeeNum").GetComponent<GUIText>().enabled = true;
					GameObject.Find("keyNum").GetComponent<GUIText>().enabled = true;
					GameObject.Find("bombNum").GetComponent<GUIText>().enabled = true;
					GameObject.Find ("LvlName").GetComponent<GUIText>().enabled = true;

					if(GameObject.Find("OldManTxt(Clone)") != null){
						GameObject.Find ("OldManTxt(Clone)").GetComponent<GUIText>().enabled = true;
					}

					if(GameplayMain.that != null && GameplayMain.that.isSpeech){
						GameplayMain.that.hideSpeech = false;
					}

				}
				else {
					rectSelector = Instantiate(rectSelectorPrefab) as GameObject;
					sprRend = rectSelector.GetComponent<SpriteRenderer>();
				}

				doHud = false;
			}
		}


		// selector animation
		if(sprRend != null){
			int idx = (int)(Time.realtimeSinceStartup * 8);
			idx = idx % 2;
			sprRend.sprite = spr[idx];
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
}
