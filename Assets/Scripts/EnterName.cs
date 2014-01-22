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

	bool isVisible = true;

	void Start () {
		font = Resources.Load<Font>("Fonts/prstartk");
		
		cnt = new GUIContent();
		cnt.text = "";
		
		style = new GUIStyle();
		style.font = font;
		style.fontSize = 7;
		style.normal.textColor = Color.white;
	}

	Vector2 mid = new Vector2(Screen.width/2, Screen.height/2);

	void OnGUI(){
		GUI.Label(new Rect(mid.x - 19, mid.y - 60, 100, 100), cnt, style);
	}

	void Update () {
		print (Time.frameCount);

		if(Time.frameCount % 7 == 0){
			isVisible = !isVisible;
			print (isVisible);
			if(!isVisible){
				renderer.enabled = false;
			}
			else {
				renderer.enabled = true;
			}
		}



		if(Input.GetButtonDown("Left") && transform.position.x > -97){
			transform.position += new Vector3(-speed, 0);
			--curPos.x;
		}

		if(Input.GetButtonDown("Right") && transform.position.x < 91){
			transform.position += new Vector3(speed, 0);
			++curPos.x;
		}

		if(Input.GetButtonDown("Up") && transform.position.y < -19){
			transform.position += new Vector3(0, speed);
			--curPos.y;
		}

		if(Input.GetButtonDown("Down") && transform.position.y > -63){
			transform.position += new Vector3(0, -speed);
			++curPos.y;
		}

		if(Input.GetButtonDown("Attack")){
			cnt.text += charArr[(int)curPos.x, (int)curPos.y];
		}

		if(Input.GetButtonDown("SpecialAttack")){
			cnt.text += charArr[10, 3];	// space
		}

	}
}
