using UnityEngine;
using System.Collections;

public class Tile {
	//local coords (coords in room)
	int xcoord, ycoord;
	public string hexval;
	public int index;
	//denote enemy / item placement on tile
	public string spawnval;

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
	public void print()
	{
		Debug.Log("x:" + xcoord + " y: " + ycoord + " hex:" + hexval + " spawn:" + spawnval);
	}
}
