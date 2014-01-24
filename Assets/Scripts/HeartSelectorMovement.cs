using UnityEngine;
using System.Collections;

public class HeartSelectorMovement : MonoBehaviour {
	
	int hrtPos = 1;

	GUIContent cnt;
	GUIStyle style;

	string playerName;

	GameObject redHeart;

	void Awake(){
		redHeart = Resources.Load<GameObject>("heartSelectorRegRed");
	}

	void Start(){
		playerName = PlayerPrefs.GetString("name");

		cnt = new GUIContent();
		if(playerName != ""){
			hrtPos = 0;
		}
		cnt.text = playerName;

		updateHrtPos();
		
		style = new GUIStyle();
		style.font = Resources.Load<Font>("Fonts/prstartk");;
		style.fontSize = 7;
		style.normal.textColor = Color.white;

		int n = PlayerPrefs.GetInt("numHearts");
		for(int i=0; i < n; ++i){
			GameObject h = new GameObject();
			h = Instantiate(redHeart) as GameObject;
			h.transform.position = new Vector3(21.7f + i*9, 19.2f);
		}
	}

	void Update () {
		if(Input.GetButtonDown("Select")){
			++hrtPos;

			if(hrtPos > 2){
				if(name != ""){
					hrtPos = 0;
				}
				else {
					hrtPos = 1;
				}
			}

			updateHrtPos();
		}

		if(Input.GetButtonDown("Enter")){
			if(hrtPos == 0){
				Application.LoadLevel("main");
			}
			else if(hrtPos == 1){
				Application.LoadLevel("registerName");
			}
			else if(hrtPos == 2){
				//Application.LoadLevel("eliminationMode");
			}
		}
	}

	void updateHrtPos(){
		switch(hrtPos){
			case 0:
				transform.position = new Vector3(-89f, 29f, 0);
				break;
			case 1:
				transform.position = new Vector3(-89f, -52.5f, 0);
				break;
			case 2: 
				transform.position = new Vector3(-89f, -66.5f, 0);
				break;
		}
	}
	
	void OnGUI(){
		GUI.Label(new Rect(Screen.width/2 - 54f, Screen.height/2 - 29.5f, 100, 100), cnt, style);
	}
}
