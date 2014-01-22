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

	void Start () {
		font = Resources.Load<Font>("Fonts/prstartk");
		
		cnt = new GUIContent();
		cnt.text = "";
		
		style = new GUIStyle();
		style.font = font;
		style.fontSize = 7;
		style.normal.textColor = Color.white;
	}
	
	void OnGUI(){
		GUI.Label(new Rect(10, 10, 100, 100), cnt, style);
	}

	void Update () {
		if(Input.GetButtonDown("Left") && transform.position.x > -97){
			transform.position += new Vector3(-speed, 0);
			--curPos.x;
		}

		if(Input.GetButtonDown("Right") && transform.position.x < 92){
			transform.position += new Vector3(speed, 0);
			++curPos.x;
		}

		if(Input.GetButtonDown("Up") && transform.position.y < 12){
			transform.position += new Vector3(0, speed);
			--curPos.y;
		}

		if(Input.GetButtonDown("Down") && transform.position.y > -43){
			transform.position += new Vector3(0, -speed);
			++curPos.y;
		}

		if(Input.GetKeyDown(KeyCode.Return)){
			cnt.text += charArr[(int)curPos.x, (int)curPos.y];
		}

	}
}
