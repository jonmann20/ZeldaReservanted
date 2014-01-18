using UnityEngine;
using System.Collections;
using System;

public class Room {
	int xcoord, ycoord;
	Tile[,] tiles = new Tile[16, 11];

	//t is a 16x11 2d array of Tiles
	public Room(int x, int y, Tile[,] t)
	{
		xcoord = x;
		ycoord = y;
		Array.Copy(t, 0, tiles, 0, 16);
	}
}
