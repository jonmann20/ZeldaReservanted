using UnityEngine;
using System.Collections;

public class Tile {
	//local coords (coords in room)
	int xcoord, ycoord;
	string hexval;

	//denote enemy / item placement on tile
	string spawnval;

	public Tile(int x, int y, string hex, string spawn)
	{
		xcoord = x;
		ycoord = y;
		hexval = hex;
		spawnval = spawn;
	}

	//todo: replace these with get/set c# snippets?
	public void alterTileHex(string hex)
	{
		hexval = hex;
	}
	public void alterTileSpawn(string spawn)
	{
		spawnval = spawn;
	}
}
