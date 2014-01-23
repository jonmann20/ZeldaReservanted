using UnityEngine;
using System.Collections;

public class EnterName : MonoBehaviour {
	
	public float speed;

	Font font;
	GUIContent cnt;
	GUIStyle style;

	char[,] charArr = new char[,]{
		{'A', 'L', 'W', '0'},
		{'B', 'M', 'X', '1'},
		{'C', 'N', 'Y', '2'},
		{'D', 'O', 'Z', '3'},
		{'E', 'P', '-', '4'},
		{'F', 'Q', '.', '5'},
		{'G', 'R', ',', '6'},
		{'H', 'S', '!', '7'},
		{'I', 'T', '\'', '8'},
		{'J', 'U', '&', '9'},
		{'K', 'V', '.', ' '}
	};
	Vector2 curPos = Vector2.zero;
	int hrtPos = 0; // 0: first, 1: second, 2: third, 3: REGISTER, 4: END
	
	GameObject srcBeep, srcBomb;
	public AudioClip beep, bomb;


	GameObject heartSelector;

	void Awake(){
		GameObject hSelPrefab = Resources.Load<GameObject>("heartSelectorReg");
		heartSelector = Instantiate(hSelPrefab) as GameObject;
		font = Resources.Load<Font>("Fonts/prstartk");
	}

	void Start () {
		cnt = new GUIContent();
		cnt.text = "";
		
		style = new GUIStyle();
		style.font = font;
		style.fontSize = 7;
		style.normal.textColor = Color.white;

		srcBeep = new GameObject("srcBeep");
		srcBeep.AddComponent<AudioSource>();
		srcBeep.audio.clip = beep;

		srcBomb = new GameObject("srcBomp");
		srcBomb.AddComponent<AudioSource>();
		srcBomb.audio.clip = bomb;
	}

	Vector2 mid = new Vector2(Screen.width/2, Screen.height/2);

	void OnGUI(){
		GUI.Label(new Rect(mid.x - 19, mid.y - 60, 100, 100), cnt, style);
	}

	void Update(){
		// heart
		if(Input.GetButtonDown("Select")){

			if(++hrtPos > 3){
				hrtPos = 0;
			}

			switch(hrtPos){
				case 0:
					heartSelector.transform.position = new Vector2(-72, 70);
					break;
				case 1:
					heartSelector.transform.position = new Vector2(-72, 50);
					break;
				case 2:
					heartSelector.transform.position = new Vector2(-72, 30);
					break;
				case 3:
					heartSelector.transform.position = new Vector2(-76, 3);
					break;
			}

			playBeep();
		}

		// blink highlighter
		if(Time.frameCount % 7 == 0){
			renderer.enabled = !renderer.enabled;
		}

		// highlighter movement
		if(Input.GetButtonDown("Left") && transform.position.x > -97){
			transform.position += new Vector3(-speed, 0);
			--curPos.x;
			playBeep();
		}

		if(Input.GetButtonDown("Right") && transform.position.x < 91){
			transform.position += new Vector3(speed, 0);
			++curPos.x;
			playBeep();
		}

		if(Input.GetButtonDown("Up") && transform.position.y < -19){
			transform.position += new Vector3(0, speed);
			--curPos.y;
			playBeep();
		}

		if(Input.GetButtonDown("Down") && transform.position.y > -63){
			transform.position += new Vector3(0, -speed);
			++curPos.y;
			playBeep();
		}

		// add char to name
		if(Input.GetButtonDown("Attack")){
			cnt.text += charArr[(int)curPos.x, (int)curPos.y];
			playBombSet();
		}

		// add a space
		if(Input.GetButtonDown("SpecialAttack")){
			cnt.text += charArr[10, 3];	// space
		}
	}

	void playBeep(){
		if(srcBeep.audio.isPlaying){
			srcBeep.audio.Stop();
		}

		srcBeep.audio.Play();
	}

	void playBombSet(){
		if(srcBomb.audio.isPlaying){
			srcBomb.audio.Stop();
		}
		
		srcBomb.audio.Play();
	}
}
