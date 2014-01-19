using UnityEngine;
using System.Collections;
using System;

public class Room {
	int xcoord, ycoord;
	public Tile[,] tiles = new Tile[16, 11];

	//t is a 16x11 2d array of Tiles
	public Room(int x, int y, Tile[,] t)
	{
		xcoord = x;
		ycoord = y;
		//COPY TILE ARRAY
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 11; j++)
			{
				tiles[i, j] = t[i, j];
			}
		}
	}
}
