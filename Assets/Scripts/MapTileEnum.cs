using UnityEngine;
using System.Collections;

public class MapTileEnum : MonoBehaviour {

	static Sprite[] mapTileSprites = Resources.LoadAll<Sprite>("overworldSprites");
	//ERRORS EXIST:
	//Order in mapTileSprites does not follow naming / alphabetical convention.
	public static Sprite tile00 = mapTileSprites[0];
	public static Sprite tile01 = mapTileSprites[1];
	public static Sprite tile02 = mapTileSprites[2];
	public static Sprite tile03 = mapTileSprites[3];
	public static Sprite tile04 = mapTileSprites[4];
	public static Sprite tile05 = mapTileSprites[5];
	public static Sprite tile06 = mapTileSprites[6];
	public static Sprite tile07 = mapTileSprites[7];
	public static Sprite tile08 = mapTileSprites[8];
	public static Sprite tile09 = mapTileSprites[9];
	public static Sprite tile0a = mapTileSprites[10];
	public static Sprite tile0b = mapTileSprites[11];
	public static Sprite tile0c = mapTileSprites[12];
	public static Sprite tile0d = mapTileSprites[13];
	public static Sprite tile0e = mapTileSprites[14];
	public static Sprite tile0f = mapTileSprites[15];

	public static Sprite tile10 = mapTileSprites[16];
	public static Sprite tile11 = mapTileSprites[17];
	public static Sprite tile14 = mapTileSprites[18];
	public static Sprite tile15 = mapTileSprites[19];
	public static Sprite tile16 = mapTileSprites[20];
	public static Sprite tile17 = mapTileSprites[21];
	public static Sprite tile18 = mapTileSprites[22];
	public static Sprite tile19 = mapTileSprites[23];
	public static Sprite tile1a = mapTileSprites[24];
	public static Sprite tile1b = mapTileSprites[25];
	public static Sprite tile1c = mapTileSprites[26];
	public static Sprite tile1d = mapTileSprites[27];
	public static Sprite tile1e = mapTileSprites[28];
	public static Sprite tile1f = mapTileSprites[29];

	public static Sprite tile20 = mapTileSprites[30];
	public static Sprite tile21 = mapTileSprites[31];
	public static Sprite tile22 = mapTileSprites[32];
	public static Sprite tile23 = mapTileSprites[33];
	public static Sprite tile24 = mapTileSprites[34];
	public static Sprite tile25 = mapTileSprites[35];
	public static Sprite tile28 = mapTileSprites[36];
	public static Sprite tile29 = mapTileSprites[37];
	public static Sprite tile2a = mapTileSprites[38];
	public static Sprite tile2b = mapTileSprites[39];
	public static Sprite tile2c = mapTileSprites[40];
	public static Sprite tile2d = mapTileSprites[41];
	public static Sprite tile2e = mapTileSprites[42];
	public static Sprite tile2f = mapTileSprites[43];

	public static Sprite tile30 = mapTileSprites[44];
	public static Sprite tile31 = mapTileSprites[45];
	public static Sprite tile32 = mapTileSprites[46];
	public static Sprite tile33 = mapTileSprites[47];
	public static Sprite tile34 = mapTileSprites[48];
	public static Sprite tile35 = mapTileSprites[49];
	public static Sprite tile36 = mapTileSprites[50];
	public static Sprite tile37 = mapTileSprites[51];
	public static Sprite tile38 = mapTileSprites[52];
	public static Sprite tile39 = mapTileSprites[53];
	public static Sprite tile3c = mapTileSprites[54];
	public static Sprite tile3d = mapTileSprites[55];
	public static Sprite tile3e = mapTileSprites[56];
	public static Sprite tile3f = mapTileSprites[57];

	public static Sprite tile40 = mapTileSprites[58];
	public static Sprite tile41 = mapTileSprites[59];
	public static Sprite tile42 = mapTileSprites[60];
	public static Sprite tile43 = mapTileSprites[61];
	public static Sprite tile44 = mapTileSprites[62];
	public static Sprite tile45 = mapTileSprites[63];
	public static Sprite tile46 = mapTileSprites[64];
	public static Sprite tile47 = mapTileSprites[65];
	public static Sprite tile48 = mapTileSprites[66];
	public static Sprite tile49 = mapTileSprites[67];
	public static Sprite tile4a = mapTileSprites[68];
	public static Sprite tile4b = mapTileSprites[69];
	public static Sprite tile4c = mapTileSprites[70];
	public static Sprite tile4d = mapTileSprites[71];

	public static Sprite tile50 = mapTileSprites[72];
	public static Sprite tile51 = mapTileSprites[73];
	public static Sprite tile52 = mapTileSprites[74];
	public static Sprite tile53 = mapTileSprites[75];
	public static Sprite tile54 = mapTileSprites[76];
	public static Sprite tile55 = mapTileSprites[77];
	public static Sprite tile56 = mapTileSprites[78];
	public static Sprite tile57 = mapTileSprites[79];
	public static Sprite tile58 = mapTileSprites[80];
	public static Sprite tile59 = mapTileSprites[81];
	public static Sprite tile5a = mapTileSprites[82];
	public static Sprite tile5b = mapTileSprites[83];
	public static Sprite tile5c = mapTileSprites[84];
	public static Sprite tile5d = mapTileSprites[85];
	public static Sprite tile5e = mapTileSprites[86];
	public static Sprite tile5f = mapTileSprites[87];

	public static Sprite tile60 = mapTileSprites[88];
	public static Sprite tile61 = mapTileSprites[89];
	public static Sprite tile64 = mapTileSprites[90];
	public static Sprite tile65 = mapTileSprites[91];
	public static Sprite tile66 = mapTileSprites[92];
	public static Sprite tile67 = mapTileSprites[93];
	public static Sprite tile68 = mapTileSprites[94];
	public static Sprite tile78 = mapTileSprites[95];
	public static Sprite tile69 = mapTileSprites[96];
	public static Sprite tile6a = mapTileSprites[97];
	public static Sprite tile6b = mapTileSprites[98];
	public static Sprite tile6c = mapTileSprites[99];
	public static Sprite tile6d = mapTileSprites[100];
	public static Sprite tile6e = mapTileSprites[101];
	public static Sprite tile6f = mapTileSprites[102];

	public static Sprite tile70 = mapTileSprites[103];
	public static Sprite tile71 = mapTileSprites[104];
	public static Sprite tile72 = mapTileSprites[105];
	public static Sprite tile73 = mapTileSprites[106];
	public static Sprite tile74 = mapTileSprites[107];
	public static Sprite tile75 = mapTileSprites[108];
	public static Sprite tile8c = mapTileSprites[109];
	public static Sprite tile8d = mapTileSprites[110];
	public static Sprite tile8e = mapTileSprites[111];
	public static Sprite tile8f = mapTileSprites[112];//
	public static Sprite tile90 = mapTileSprites[113];
	public static Sprite tile91 = mapTileSprites[114];
	public static Sprite tile92 = mapTileSprites[115];
	public static Sprite tile93 = mapTileSprites[116];
	public static Sprite tile94 = mapTileSprites[117];
	public static Sprite tile95 = mapTileSprites[118];
	public static Sprite tile96 = mapTileSprites[119];
	public static Sprite tile97 = mapTileSprites[120];
	public static Sprite tile98 = mapTileSprites[121];
	public static Sprite tile99 = mapTileSprites[122];
	public static Sprite tile9a = mapTileSprites[123];
	public static Sprite tile9b = mapTileSprites[124];
	public static Sprite tile9c = mapTileSprites[125];
	public static Sprite tile9d = mapTileSprites[126];

	public static Sprite tile79 = mapTileSprites[127];
	public static Sprite tile7a = mapTileSprites[128];
	public static Sprite tile7b = mapTileSprites[129];
	public static Sprite tile7c = mapTileSprites[130];
	public static Sprite tile7d = mapTileSprites[131];
	public static Sprite tile7e = mapTileSprites[132];
	public static Sprite tile7f = mapTileSprites[133];

	public static Sprite tile80 = mapTileSprites[134];
	public static Sprite tile81 = mapTileSprites[135];
	public static Sprite tile82 = mapTileSprites[136];
	public static Sprite tile83 = mapTileSprites[137];
	public static Sprite tile84 = mapTileSprites[138];
	public static Sprite tile85 = mapTileSprites[139];
	public static Sprite tile86 = mapTileSprites[140];
	public static Sprite tile87 = mapTileSprites[141];
	public static Sprite tile88 = mapTileSprites[142];
	public static Sprite tile89 = mapTileSprites[143];

	void Awake()
	{
		Debug.Log("FUDGE!");
		Debug.Log(mapTileSprites.Length);
		//Debug.Log(tile00);
		int i = 0;
		foreach( Sprite s in mapTileSprites)
		{
			Debug.Log(i);
			Debug.Log(s);
			i ++;
		}
	}
	void Start () {
	
	}

	public static Sprite getTileSprite(string s_tile)
	{
		if(s_tile == "00") return tile00;
		if(s_tile == "01") return tile01;
		if(s_tile == "02") return tile02;
		if(s_tile == "03") return tile03;
		if(s_tile == "04") return tile04;
		if(s_tile == "05") return tile05;
		if(s_tile == "06") return tile06;
		if(s_tile == "07") return tile07;
		if(s_tile == "08") return tile08;
		if(s_tile == "09") return tile09;
		if(s_tile == "0a") return tile0a;
		if(s_tile == "0b") return tile0b;
		if(s_tile == "0c") return tile0c;
		if(s_tile == "0d") return tile0d;
		if(s_tile == "0e") return tile0e;
		if(s_tile == "0f") return tile0f;

		if(s_tile == "10") return tile10;
		if(s_tile == "11") return tile11;
		if(s_tile == "12") return tile18;
		if(s_tile == "14") return tile14;
		if(s_tile == "15") return tile15;
		if(s_tile == "16") return tile16;
		if(s_tile == "17") return tile17;
		if(s_tile == "18") return tile18;
		if(s_tile == "19") return tile19;
		if(s_tile == "1a") return tile1a;
		if(s_tile == "1b") return tile1b;
		if(s_tile == "1c") return tile1c;
		if(s_tile == "1d") return tile1d;
		if(s_tile == "1e") return tile1e;
		if(s_tile == "1f") return tile1f;

		if(s_tile == "20") return tile20;
		if(s_tile == "21") return tile21;
		if(s_tile == "22") return tile22;
		if(s_tile == "23") return tile23;
		if(s_tile == "24") return tile24;
		if(s_tile == "25") return tile25;
		if(s_tile == "28") return tile28;
		if(s_tile == "29") return tile29;
		if(s_tile == "2a") return tile2a;
		if(s_tile == "2b") return tile2b;
		if(s_tile == "2c") return tile2c;
		if(s_tile == "2d") return tile2d;
		if(s_tile == "2e") return tile2e;
		if(s_tile == "2f") return tile2f;

		if(s_tile == "30") return tile30;
		if(s_tile == "31") return tile31;
		if(s_tile == "32") return tile32;
		if(s_tile == "33") return tile33;
		if(s_tile == "34") return tile34;
		if(s_tile == "35") return tile35;
		if(s_tile == "36") return tile36;
		if(s_tile == "37") return tile37;
		if(s_tile == "38") return tile38;
		if(s_tile == "39") return tile39;
		if(s_tile == "3c") return tile3c;
		if(s_tile == "3d") return tile3d;
		if(s_tile == "3e") return tile3e;
		if(s_tile == "3f") return tile3f;

		if(s_tile == "40") return tile40;
		if(s_tile == "41") return tile41;
		if(s_tile == "42") return tile42;
		if(s_tile == "43") return tile43;
		if(s_tile == "44") return tile44;
		if(s_tile == "45") return tile45;
		if(s_tile == "46") return tile46;
		if(s_tile == "47") return tile47;
		if(s_tile == "48") return tile48;
		if(s_tile == "49") return tile49;
		if(s_tile == "4a") return tile4a;
		if(s_tile == "4b") return tile4b;
		if(s_tile == "4c") return tile4c;
		if(s_tile == "4d") return tile4d;

		if(s_tile == "50") return tile50;
		if(s_tile == "51") return tile51;
		if(s_tile == "52") return tile52;
		if(s_tile == "53") return tile53;
		if(s_tile == "54") return tile54;
		if(s_tile == "55") return tile55;
		if(s_tile == "56") return tile56;
		if(s_tile == "57") return tile57;
		if(s_tile == "58") return tile58;
		if(s_tile == "59") return tile59;
		if(s_tile == "5a") return tile5a;
		if(s_tile == "5b") return tile5b;
		if(s_tile == "5c") return tile5c;
		if(s_tile == "5d") return tile5d;
		if(s_tile == "5e") return tile5e;
		if(s_tile == "5f") return tile5f;

		if(s_tile == "60") return tile60;
		if(s_tile == "61") return tile61;
		if(s_tile == "64") return tile64;
		if(s_tile == "65") return tile65;
		if(s_tile == "66") return tile66;
		if(s_tile == "67") return tile67;
		if(s_tile == "68") return tile68;
		if(s_tile == "69") return tile69;
		if(s_tile == "6a") return tile6a;
		if(s_tile == "6b") return tile6b;
		if(s_tile == "6c") return tile6c;
		if(s_tile == "6d") return tile6d;
		if(s_tile == "6e") return tile6e;
		if(s_tile == "6f") return tile6f;

		if(s_tile == "70") return tile70;
		if(s_tile == "71") return tile71;
		if(s_tile == "72") return tile72;
		if(s_tile == "73") return tile73;
		if(s_tile == "74") return tile74;
		if(s_tile == "75") return tile75;
		if(s_tile == "78") return tile78;
		if(s_tile == "79") return tile79;
		if(s_tile == "7a") return tile7a;
		if(s_tile == "7b") return tile7b;
		if(s_tile == "7c") return tile7c;
		if(s_tile == "7d") return tile7d;
		if(s_tile == "7e") return tile7e;
		if(s_tile == "7f") return tile7f;

		if(s_tile == "80") return tile80;
		if(s_tile == "81") return tile81;
		if(s_tile == "82") return tile82;
		if(s_tile == "83") return tile83;
		if(s_tile == "84") return tile84;
		if(s_tile == "85") return tile85;
		if(s_tile == "86") return tile86;
		if(s_tile == "87") return tile87;
		if(s_tile == "88") return tile88;
		if(s_tile == "89") return tile89;
		if(s_tile == "8c") return tile8c;
		if(s_tile == "8d") return tile8d;
		if(s_tile == "8e") return tile8e;
		if(s_tile == "8f") return tile8f;

		if(s_tile == "90") return tile90;
		if(s_tile == "91") return tile91;
		if(s_tile == "92") return tile92;
		if(s_tile == "93") return tile93;
		if(s_tile == "94") return tile94;
		if(s_tile == "95") return tile95;
		if(s_tile == "96") return tile96;
		if(s_tile == "97") return tile97;
		if(s_tile == "98") return tile98;
		if(s_tile == "99") return tile99;
		if(s_tile == "9a") return tile9a;
		if(s_tile == "9b") return tile9b;
		if(s_tile == "9c") return tile9c;
		if(s_tile == "9d") return tile9d;

		return tile00;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
