using UnityEngine;
using System.Collections;
using System;

public class Room {
	public int xcoord;
	public int ycoord;
	public Tile[,] tiles = new Tile[16, 11];
	public string exitNorth = "00";
	public string exitEast = "00";
	public string exitSouth = "00";
	public string exitWest = "00";
	public string name = "noname";

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
				tiles[i, j].xcoord = i;
				tiles[i, j].ycoord = j;
			}
		}

		name = "Room (" + x.ToString() + "," + y.ToString() + ")";
	}

	//hexVals and spawnVals are 16x11 2d arrays of strings.
	public Room(int x, int y, string[,] hexVals, string[,] spawnVals)
	{
		xcoord = x;
		ycoord = y;
		//COPY TILE ARRAY
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 11; j++)
			{
				Tile t = new Tile(i, j, hexVals[i, j], spawnVals[i, j]);
				tiles[i, j] = t;
			}
		}
	}

	public void setExit(char exitDirection, string val)
	{
		if(exitDirection == 'n') exitNorth = val;
		else if(exitDirection == 'e') exitEast = val;
		else if(exitDirection == 's') exitSouth = val;
		else if(exitDirection == 'w') exitWest = val;
	}

	public void setName(string s)
	{
		name = s;
	}

	public Tile findTileWithCode(string code)
	{
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 11; j++)
			{
				if(tiles[i,j].code == code)
				   return tiles[i,j];
			}
		}

		Tile errorTile = new Tile(-1, -1, "FIND TILE ERROR", "FIND TILE ERROR");
		return errorTile;
	}
}
