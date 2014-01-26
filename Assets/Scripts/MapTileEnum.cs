using UnityEngine;
using System.Collections;

public class MapTileEnum : MonoBehaviour {

	static string[] solidTiles = { "01", "03", "04", "05", "07", "08", "09", "0a", "0b", "0d", "0f", "10", "11", "15", "16", "17",
									"19", "1b", "1c", "1d", "1e", "1f", "21", "22", "23", "25", "28", "29", "2a", "2b", "2c", "2d",
									"2e", "2f", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3c", "3d", "3e", "3f",
									"41", "42", "43", "44", "45", "47", "48", "49", "4a", "4b", "4d", "96", "9c"};

	static string[] waterTiles = { "14", "1a", "20", "46", "50", "51", "52", "56", "57", "58", "5c", "5d", "5e", "64", "65", "66",
									"6a", "6b", "6c", "70", "71", "72", "78", "79", "7a", "7e", "7f", "80", "84", "85", "86", "90"};

	static Sprite[] mapTileSprites;

	public static Sprite tile00;
	public static Sprite tile01;
	public static Sprite tile02;
	public static Sprite tile03;
	public static Sprite tile04;
	public static Sprite tile05;
	public static Sprite tile06;
	public static Sprite tile07;
	public static Sprite tile08;
	public static Sprite tile09;
	public static Sprite tile0a;
	public static Sprite tile0b;
	public static Sprite tile0c;
	public static Sprite tile0d;
	public static Sprite tile0e;
	public static Sprite tile0f;

	public static Sprite tile10;
	public static Sprite tile11;
	public static Sprite tile14;
	public static Sprite tile15;
	public static Sprite tile16;
	public static Sprite tile17;
	public static Sprite tile18;
	public static Sprite tile19;
	public static Sprite tile1a;
	public static Sprite tile1b;
	public static Sprite tile1c;
	public static Sprite tile1d;
	public static Sprite tile1e;
	public static Sprite tile1f;

	public static Sprite tile20;
	public static Sprite tile21;
	public static Sprite tile22;
	public static Sprite tile23;
	public static Sprite tile24;
	public static Sprite tile25;
	public static Sprite tile28;
	public static Sprite tile29;
	public static Sprite tile2a;
	public static Sprite tile2b;
	public static Sprite tile2c;
	public static Sprite tile2d;
	public static Sprite tile2e;
	public static Sprite tile2f;

	public static Sprite tile30;
	public static Sprite tile31;
	public static Sprite tile32;
	public static Sprite tile33;
	public static Sprite tile34;
	public static Sprite tile35;
	public static Sprite tile36;
	public static Sprite tile37;
	public static Sprite tile38;
	public static Sprite tile39;
	public static Sprite tile3c;
	public static Sprite tile3d;
	public static Sprite tile3e;
	public static Sprite tile3f;

	public static Sprite tile40;
	public static Sprite tile41;
	public static Sprite tile42;
	public static Sprite tile43;
	public static Sprite tile44;
	public static Sprite tile45;
	public static Sprite tile46;
	public static Sprite tile47;
	public static Sprite tile48;
	public static Sprite tile49;
	public static Sprite tile4a;
	public static Sprite tile4b;
	public static Sprite tile4c;
	public static Sprite tile4d;

	public static Sprite tile50;
	public static Sprite tile51;
	public static Sprite tile52;
	public static Sprite tile53;
	public static Sprite tile54;
	public static Sprite tile55;
	public static Sprite tile56;
	public static Sprite tile57;
	public static Sprite tile58;
	public static Sprite tile59;
	public static Sprite tile5a;
	public static Sprite tile5b;
	public static Sprite tile5c;
	public static Sprite tile5d;
	public static Sprite tile5e;
	public static Sprite tile5f;

	public static Sprite tile60;
	public static Sprite tile61;
	public static Sprite tile64;
	public static Sprite tile65;
	public static Sprite tile66;
	public static Sprite tile67;
	public static Sprite tile68;
	public static Sprite tile78;
	public static Sprite tile69;
	public static Sprite tile6a;
	public static Sprite tile6b;
	public static Sprite tile6c;
	public static Sprite tile6d;
	public static Sprite tile6e;
	public static Sprite tile6f;

	public static Sprite tile70;
	public static Sprite tile71;
	public static Sprite tile72;
	public static Sprite tile73;
	public static Sprite tile74;
	public static Sprite tile75;
	public static Sprite tile8c;
	public static Sprite tile8d;
	public static Sprite tile8e;
	public static Sprite tile8f;
	public static Sprite tile90;
	public static Sprite tile91;
	public static Sprite tile92;
	public static Sprite tile93;
	public static Sprite tile94;
	public static Sprite tile95;
	public static Sprite tile96;
	public static Sprite tile97;
	public static Sprite tile98;
	public static Sprite tile99;
	public static Sprite tile9a;
	public static Sprite tile9b;
	public static Sprite tile9c;
	public static Sprite tile9d;

	public static Sprite tile79;
	public static Sprite tile7a;
	public static Sprite tile7b;
	public static Sprite tile7c;
	public static Sprite tile7d;
	public static Sprite tile7e;
	public static Sprite tile7f;

	public static Sprite tile80;
	public static Sprite tile81;
	public static Sprite tile82;
	public static Sprite tile83;
	public static Sprite tile84;
	public static Sprite tile85;
	public static Sprite tile86;
	public static Sprite tile87;
	public static Sprite tile88;
	public static Sprite tile89;

	void Awake()
	{
		init();
		int i = 0;
		foreach( Sprite s in mapTileSprites)
		{
			i ++;
		}
	}

	void init()
	{
		mapTileSprites = Resources.LoadAll<Sprite>("overworldSprites");
		tile00 = mapTileSprites[0];
		tile01 = mapTileSprites[1];
		tile02 = mapTileSprites[2];
		tile03 = mapTileSprites[3];
		tile04 = mapTileSprites[4];
		tile05 = mapTileSprites[5];
		tile06 = mapTileSprites[6];
		tile07 = mapTileSprites[7];
		tile08 = mapTileSprites[8];
		tile09 = mapTileSprites[9];
		tile0a = mapTileSprites[10];
		tile0c = mapTileSprites[12];
		tile0d = mapTileSprites[13];
		tile0e = mapTileSprites[14];
		tile0f = mapTileSprites[15];
		
		tile10 = mapTileSprites[16];
		tile11 = mapTileSprites[17];
		tile14 = mapTileSprites[18];
		tile15 = mapTileSprites[19];
		tile16 = mapTileSprites[20];
		tile17 = mapTileSprites[21];
		tile18 = mapTileSprites[22];
		tile19 = mapTileSprites[23];
		tile1a = mapTileSprites[24];
		tile1b = mapTileSprites[25];
		tile1c = mapTileSprites[26];
		tile1d = mapTileSprites[27];
		tile1e = mapTileSprites[28];
		tile1f = mapTileSprites[29];
		
		tile20 = mapTileSprites[30];
		tile21 = mapTileSprites[31];
		tile22 = mapTileSprites[32];
		tile23 = mapTileSprites[33];
		tile24 = mapTileSprites[34];
		tile25 = mapTileSprites[35];
		tile28 = mapTileSprites[36];
		tile29 = mapTileSprites[37];
		tile2a = mapTileSprites[38];
		tile2b = mapTileSprites[39];
		tile2c = mapTileSprites[40];
		tile2d = mapTileSprites[41];
		tile2e = mapTileSprites[42];
		tile2f = mapTileSprites[43];
		
		tile30 = mapTileSprites[44];
		tile31 = mapTileSprites[45];
		tile32 = mapTileSprites[46];
		tile33 = mapTileSprites[47];
		tile34 = mapTileSprites[48];
		tile35 = mapTileSprites[49];
		tile36 = mapTileSprites[50];
		tile37 = mapTileSprites[51];
		tile38 = mapTileSprites[52];
		tile39 = mapTileSprites[53];
		tile3c = mapTileSprites[54];
		tile3d = mapTileSprites[55];
		tile3e = mapTileSprites[56];
		tile3f = mapTileSprites[57];
		
		tile40 = mapTileSprites[58];
		tile41 = mapTileSprites[59];
		tile42 = mapTileSprites[60];
		tile43 = mapTileSprites[61];
		tile44 = mapTileSprites[62];
		tile45 = mapTileSprites[63];
		tile46 = mapTileSprites[64];
		tile47 = mapTileSprites[65];
		tile48 = mapTileSprites[66];
		tile49 = mapTileSprites[67];
		tile4a = mapTileSprites[68];
		tile4b = mapTileSprites[69];
		tile4c = mapTileSprites[70];
		tile4d = mapTileSprites[71];
		
		tile50 = mapTileSprites[72];
		tile51 = mapTileSprites[73];
		tile52 = mapTileSprites[74];
		tile53 = mapTileSprites[75];
		tile54 = mapTileSprites[76];
		tile55 = mapTileSprites[77];
		tile56 = mapTileSprites[78];
		tile57 = mapTileSprites[79];
		tile58 = mapTileSprites[80];
		tile59 = mapTileSprites[81];
		tile5a = mapTileSprites[82];
		tile5b = mapTileSprites[83];
		tile5c = mapTileSprites[84];
		tile5d = mapTileSprites[85];
		tile5e = mapTileSprites[86];
		tile5f = mapTileSprites[87];
		
		tile60 = mapTileSprites[88];
		tile61 = mapTileSprites[89];
		tile64 = mapTileSprites[90];
		tile65 = mapTileSprites[91];
		tile66 = mapTileSprites[92];
		tile67 = mapTileSprites[93];
		tile68 = mapTileSprites[94];
		tile78 = mapTileSprites[95];
		tile69 = mapTileSprites[96];
		tile6a = mapTileSprites[97];
		tile6b = mapTileSprites[98];
		tile6c = mapTileSprites[99];
		tile6d = mapTileSprites[100];
		tile6e = mapTileSprites[101];
		tile6f = mapTileSprites[102];
		
		tile70 = mapTileSprites[103];
		tile71 = mapTileSprites[104];
		tile72 = mapTileSprites[105];
		tile73 = mapTileSprites[106];
		tile74 = mapTileSprites[107];
		tile75 = mapTileSprites[108];
		tile8c = mapTileSprites[109];
		tile8d = mapTileSprites[110];
		tile8e = mapTileSprites[111];
		tile8f = mapTileSprites[112];
		tile90 = mapTileSprites[113];
		tile91 = mapTileSprites[114];
		tile92 = mapTileSprites[115];
		tile93 = mapTileSprites[116];
		tile94 = mapTileSprites[117];
		tile95 = mapTileSprites[118];
		tile96 = mapTileSprites[119];
		tile97 = mapTileSprites[120];
		tile98 = mapTileSprites[121];
		tile99 = mapTileSprites[122];
		tile9a = mapTileSprites[123];
		tile9b = mapTileSprites[124];
		tile9c = mapTileSprites[125];
		tile9d = mapTileSprites[126];
		
		tile79 = mapTileSprites[127];
		tile7a = mapTileSprites[128];
		tile7b = mapTileSprites[129];
		tile7c = mapTileSprites[130];
		tile7d = mapTileSprites[131];
		tile7e = mapTileSprites[132];
		tile7f = mapTileSprites[133];
		
		tile80 = mapTileSprites[134];
		tile81 = mapTileSprites[135];
		tile82 = mapTileSprites[136];
		tile83 = mapTileSprites[137];
		tile84 = mapTileSprites[138];
		tile85 = mapTileSprites[139];
		tile86 = mapTileSprites[140];
		tile87 = mapTileSprites[141];
		tile88 = mapTileSprites[142];
		tile89 = mapTileSprites[143];
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

	public static bool isSolid(string hex)
	{
		foreach(string s in solidTiles)
			if(s == hex) return true;
		return false;
	}

	public static bool isWater(string hex)
	{
		foreach(string s in waterTiles)
			if(s == hex) return true;
		return false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
