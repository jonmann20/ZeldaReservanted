using UnityEngine;
using System.Collections;

public class EnterName : MonoBehaviour {

	public float speed;
	public float letterSpacing = 140f;

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

	public GameObject namePosHighlightPrefab;
	GameObject heartSelector, namePosHighlight;

	void Awake(){
		GameObject hSelPrefab = Resources.Load<GameObject>("heartSelectorReg");
		heartSelector = Instantiate(hSelPrefab) as GameObject;
		font = Resources.Load<Font>("Fonts/prstartk");

		namePosHighlight = Instantiate(namePosHighlightPrefab) as GameObject;
	}

	void Start () {
		cnt = new GUIContent();
		cnt.text = "";
		
		style = new GUIStyle();
		style.font = font;
		style.fontSize = 30;
		style.normal.textColor = Color.white;

		srcBeep = new GameObject("srcBeep");
		srcBeep.AddComponent<AudioSource>();
		srcBeep.GetComponent<AudioSource>().clip = beep;

		srcBomb = new GameObject("srcBomp");
		srcBomb.AddComponent<AudioSource>();
		srcBomb.GetComponent<AudioSource>().clip = bomb;
	}

	void OnGUI(){
		GUI.Label(new Rect(Screen.width/2.6f, Screen.height/5.6f, 100, 100), cnt, style);

		// strike #2 and #3
		GUI.Label(new Rect(Screen.width/2.6f, Screen.height/3.4f, 100, 100), "---", style);
		GUI.Label(new Rect(Screen.width/2.6f, Screen.height/2.5f, 100, 100), "---", style);
	}

	void Update(){
		// heart
		if(Input.GetButtonDown("Select")){

			if(++hrtPos > 1){//3
				hrtPos = 0;
			}

			switch(hrtPos){
				case 0:
					heartSelector.transform.position = new Vector2(-72, 75);
					break;
				case 1:
					//heartSelector.transform.position = new Vector2(-72, 50);
					heartSelector.transform.position = new Vector2(-76, 3);
					break;
//				case 2:
//					heartSelector.transform.position = new Vector2(-72, 25);
//					break;
//				case 3:
//					heartSelector.transform.position = new Vector2(-76, 3);
//					break;
			}

			playBeep();
		}

		// blink highlighter
		if(Time.frameCount % 7 == 0){
			GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
			namePosHighlight.GetComponent<Renderer>().enabled = !namePosHighlight.GetComponent<Renderer>().enabled;
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
		if(hrtPos == 0 && Input.GetButtonDown("Attack") && cnt.text.Length < 8){
			cnt.text += charArr[(int)curPos.x, (int)curPos.y];
			playBombSet();

			if(cnt.text.Length != 8){
				namePosHighlight.transform.position += new Vector3(Screen.width/letterSpacing, 0);
			}
		}

		// add a space
		if(hrtPos == 0 && Input.GetButtonDown("SpecialAttack") && cnt.text.Length < 8){
			cnt.text += charArr[10, 3];	// space

			if(cnt.text.Length != 8){
				namePosHighlight.transform.position += new Vector3(Screen.width/letterSpacing, 0);
			}
		}

		if(hrtPos == 1 && Input.GetButtonDown("Enter")){
			PlayerPrefs.SetString("name", cnt.text);
			PlayerPrefs.SetInt("numHearts", 3);
			PlayerPrefs.SetInt("hasBomb", 0);
			PlayerPrefs.SetInt("numBomb", 0);
			PlayerPrefs.SetInt ("hasSword", 0);
			PlayerPrefs.SetInt("numDeath", 0);

			Application.LoadLevel("loadSave");
		}
	}

	void playBeep(){
		if(srcBeep.GetComponent<AudioSource>().isPlaying){
			srcBeep.GetComponent<AudioSource>().Stop();
		}

		srcBeep.GetComponent<AudioSource>().Play();
	}

	void playBombSet(){
		if(srcBomb.GetComponent<AudioSource>().isPlaying){
			srcBomb.GetComponent<AudioSource>().Stop();
		}
		
		srcBomb.GetComponent<AudioSource>().Play();
	}
}
