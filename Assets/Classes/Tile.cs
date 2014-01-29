using UnityEngine;
using System.Collections;

public class Tile {
	//local coords (coords in room)
	public int xcoord;
	public int ycoord;
	public string tilecode;
	public int index;
	//denote enemy / item placement on tile
	public string code;

	public Tile(int x, int y, string tc, string c)
	{
		xcoord = x;
		ycoord = y;
		tilecode = tc;
		code = c;
	}

	//todo: replace these with get/set c# snippets?
	public void alterTileCode(string tc)
	{
		tilecode = tc;
	}
	public void alterCode(string c)
	{
		code = c;
	}
}
