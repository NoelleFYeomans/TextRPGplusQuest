﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Map
    {
        // ▓ = wall Grey
        // , = floor Grey
        // █ = door Brown
        /*public char[,] map = new char[,]
        {
            {'▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓'},
            {'▓',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ','▓',',',',',',',',',',',',',',',',',',','▓',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ','▓',',',',',',',',',',',',',',',',',',','▓',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ','▓',',',',',',',',',',',',',',',',',',','▓',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ','▓',',',',',',',',',',',',',',',',',',','▓',' ',' ',' ',' ',' ','▓','▓','▓','▓','▓',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ','▓','▓',',','▓','▓','▓','▓',',','▓','▓','▓',' ','▓','▓','▓','▓','▓',',',',',',','▓',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ',' ','▓',',','▓',' ','▓',',',',',',',',','▓',' ','▓',',',',',',',',',',',',',',','▓',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ',' ','▓',',','▓',' ','▓',',',',',',',',','▓','▓','▓',',','▓','▓','▓',',',',',',','▓',' ','▓'},
            {'▓',' ',' ',' ',' ',' ',' ',' ','▓',',','▓',' ','▓',',',',',',',',',',',',',',',',','▓',' ','▓',',',',',',','▓',' ','▓'},
            {'▓',' ',' ',' ',' ',' ','▓','▓','▓',',','▓','▓','▓',',',',',',',',','▓','▓','▓','▓','▓',' ','▓','▓',',','▓','▓',' ','▓'},
            {'▓',' ',' ',' ',' ',' ','▓',',',',',',',',',',','▓','▓','▓',',','▓','▓',' ',' ',' ',' ',' ',' ','▓',',','▓',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ','▓',',',',',',',',',',','▓',' ','▓',',','▓',' ',' ',' ',' ',' ',' ',' ','▓',',','▓',' ',' ','▓'},
            {'▓',' ',' ',' ',' ',' ','▓',',',',',',',',',',','▓','▓','▓',',','▓',' ',' ',' ',' ','▓','▓','▓','▓',',','▓','▓','▓','▓'},
            {'▓',' ',' ',' ',' ',' ','▓',',',',',',',',',',',',',',',',',',','▓',' ',' ',' ',' ','▓',',',',',',',',',',',',',',','▓'},
            {'▓',' ',' ',' ',' ',' ','▓',',',',',',',',',',','▓','▓','▓',',','▓',' ',' ',' ',' ','▓',',',',',',',',',',',',',',','▓'},
            {'▓',' ',' ',' ','▓','▓','▓','▓','▓','▓','▓','▓','▓',' ','▓',',','▓',' ',' ',' ',' ','▓',',',',',',',',',',',',',',','▓'},
            {'▓',' ',' ',' ','▓',',',',',',',',',',',',',',','▓','▓','▓',',','▓','▓','▓','▓','▓','▓',',',',',',',',',',',',',',','▓'},
            {'▓',' ',' ',' ','▓',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',',','▓'},
            {'▓',' ',' ',' ','▓',',',',',',',',',',',',',',','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓',',',',',',',',',',',',',',','▓'},
            {'▓',' ',' ',' ','▓',',',',',',',',',',',',',',','▓',' ',' ',' ',' ',' ',' ',' ',' ','▓',',',',',',',',',',',',',',','▓'},
            {'▓',' ',' ',' ','▓',',',',',',',',',',',',',',','▓',' ',' ',' ',' ',' ',' ',' ',' ','▓',',',',',',',',',',',',',',','▓'},
            {'▓',' ',' ',' ','▓',',',',',',',',',',',',',',','▓',' ',' ',' ',' ',' ',' ',' ',' ','▓',',',',',',',',',',',',',',','▓'},
            {'▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓','▓'}
        };*/

        private Render rend;

        public char[,] map;

        public Map(char[,] grid, Render rend)
        {
            map = grid;
            this.rend = rend;
        }

        public void DrawMap()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] == '▓' || map[i,j] == ',')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }else if (map[i,j] == '█')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    //Console.SetCursorPosition(i, j);
                    //Console.Write(map[i, j]);
                    rend.ScreenChars[i,j] = map[i,j];
                    rend.ScreenColors[i, j] = Console.ForegroundColor;
                }
            }
        }

        public void DrawTile(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            char tile = map[x, y];
            if (tile == '▓' || tile == ',')
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else if (tile == '█')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            Console.Write(tile);
        }

        public bool CheckTile(int x, int y)
        {
            bool isFloor = false;
            if (map[x,y] == ',') isFloor = true;
            return isFloor;
        }


    }
}
