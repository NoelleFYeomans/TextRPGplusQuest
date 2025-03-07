﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextRPG
{
    internal class MapGenerator
    {
        // ▓ = wall Grey
        // , = floor Grey
        // █ = door Brown

        private Random rand = Constants.rand;

        private MapChunk[] B = new MapChunk[Constants.RoomsPerCategory];       //0
        private MapChunk[] L = new MapChunk[Constants.RoomsPerCategory];       //1
        private MapChunk[] R = new MapChunk[Constants.RoomsPerCategory];       //2
        private MapChunk[] T = new MapChunk[Constants.RoomsPerCategory];       //3
        private MapChunk[] BL = new MapChunk[Constants.RoomsPerCategory];      //4
        private MapChunk[] BR = new MapChunk[Constants.RoomsPerCategory];      //5
        private MapChunk[] BT = new MapChunk[Constants.RoomsPerCategory];      //6
        private MapChunk[] LR = new MapChunk[Constants.RoomsPerCategory];      //7
        private MapChunk[] LT = new MapChunk[Constants.RoomsPerCategory];      //8
        private MapChunk[] RT = new MapChunk[Constants.RoomsPerCategory];      //9
        private MapChunk[] BLR = new MapChunk[Constants.RoomsPerCategory];     //10
        private MapChunk[] BLT = new MapChunk[Constants.RoomsPerCategory];     //11
        private MapChunk[] BRT = new MapChunk[Constants.RoomsPerCategory];     //12
        private MapChunk[] LRT = new MapChunk[Constants.RoomsPerCategory];     //13
        private MapChunk[] BLRT = new MapChunk[Constants.RoomsPerCategory];    //14
        private MapChunk Empty = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]
        {
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
        });

        private MapChunk[,] TempMap = new MapChunk[Constants.mapHeight, Constants.mapWidth];
        private Tile[,] miniMap = new Tile[Constants.mapHeight,Constants.mapWidth];

        private void GetFiles()
        {
            string[] tempB = File.ReadAllLines("MapChunkCategories/B.txt"); //Grab appropriate text file    //
            for (int i = 0; i < Constants.RoomsPerCategory; i++)                                                                     //
            {                                                                                               //
                B[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]); //makes new chunk                                       //
                B[i].BOpen = true;  //sets chunk's appropriate door bools to true                           //
                for (int j = 0; j < Constants.roomHeight; j++)                         //                                      //  Repeat for every chunk category
                {                                                   //                                      //
                    for (int k = 0; k < Constants.roomWidth; k++)                     //                                      //
                    {                                               //                                      //
                        B[i].roomMap[j, k] = tempB[j + (Constants.roomHeight * i)][k];    //Gets chunk chars from file            //
                    }                                               //                                      //
                }                                                   //                                      //
            }                                                                                               //

            string[] tempL = File.ReadAllLines("MapChunkCategories/L.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                L[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                L[i].LOpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        L[i].roomMap[j, k] = tempL[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempR = File.ReadAllLines("MapChunkCategories/R.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                R[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                R[i].ROpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        R[i].roomMap[j, k] = tempR[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempT = File.ReadAllLines("MapChunkCategories/T.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                T[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                T[i].TOpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        T[i].roomMap[j, k] = tempT[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempBL = File.ReadAllLines("MapChunkCategories/BL.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                BL[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                BL[i].BOpen = true;
                BL[i].LOpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        BL[i].roomMap[j, k] = tempBL[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempBR = File.ReadAllLines("MapChunkCategories/BR.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                BR[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                BR[i].BOpen = true;
                BR[i].ROpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        BR[i].roomMap[j, k] = tempBR[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempBT = File.ReadAllLines("MapChunkCategories/BT.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                BT[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                BT[i].BOpen = true;
                BT[i].TOpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        BT[i].roomMap[j, k] = tempBT[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempLR = File.ReadAllLines("MapChunkCategories/LR.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                LR[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                LR[i].LOpen = true;
                LR[i].ROpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        LR[i].roomMap[j, k] = tempLR[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempLT = File.ReadAllLines("MapChunkCategories/LT.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                LT[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                LT[i].LOpen = true;
                LT[i].TOpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        LT[i].roomMap[j, k] = tempLT[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempRT = File.ReadAllLines("MapChunkCategories/RT.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                RT[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                RT[i].ROpen = true;
                RT[i].TOpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        RT[i].roomMap[j, k] = tempRT[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempBLR = File.ReadAllLines("MapChunkCategories/BLR.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                BLR[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                BLR[i].BOpen = true;
                BLR[i].LOpen = true;
                BLR[i].ROpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        BLR[i].roomMap[j, k] = tempBLR[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempBLT = File.ReadAllLines("MapChunkCategories/BLT.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                BLT[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                BLT[i].BOpen = true;
                BLT[i].LOpen = true;
                BLT[i].TOpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        BLT[i].roomMap[j, k] = tempBLT[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempBRT = File.ReadAllLines("MapChunkCategories/BRT.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                BRT[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                BRT[i].BOpen = true;
                BRT[i].ROpen = true;
                BRT[i].TOpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        BRT[i].roomMap[j, k] = tempBRT[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempLRT = File.ReadAllLines("MapChunkCategories/LRT.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                LRT[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                LRT[i].LOpen = true;
                LRT[i].ROpen = true;
                LRT[i].TOpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        LRT[i].roomMap[j, k] = tempLRT[j + (Constants.roomHeight * i)][k];
                    }
                }
            }

            string[] tempBLRT = File.ReadAllLines("MapChunkCategories/BLRT.txt");
            for (int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                BLRT[i] = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]);
                BLRT[i].BOpen = true;
                BLRT[i].LOpen = true;
                BLRT[i].ROpen = true;
                BLRT[i].TOpen = true;
                for (int j = 0; j < Constants.roomHeight; j++)
                {
                    for (int k = 0; k < Constants.roomWidth; k++)
                    {
                        BLRT[i].roomMap[j, k] = tempBLRT[j + (Constants.roomHeight * i)][k];
                    }
                }
            }
        }

        private MapChunk RandomizeTile(MapChunk[] chunkOptions) //Gets a chunk from a predetermined category
        {
            MapChunk chunk = chunkOptions[rand.Next(0, chunkOptions.Length)];
            return chunk;
        }

        private MapChunk FillTile(int x, int y)
        {
            if (y == 0) // Left Side of map                                                                 //
            {                                                                                               //
                if (x == 0) // Top of Map                                                                    //
                {                                                                                           //
                    if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen)                                     //
                    {                                                                                       //
                        return RandomizeTile(BR);                                                           //
                    }
                    else if (TempMap[x, y + 1].LOpen)                                                        //  Gets a random chunk for the left side of the map
                    {                                                                                       //
                        return RandomizeTile(R);                                                            //  Checks all possible entrance arrangements for the current arrangement
                    }
                    else if (TempMap[x + 1, y].TOpen)                                                        //
                    {                                                                                       //  Gives a random chunk that has the correct arrangement
                        return RandomizeTile(B);                                                            //
                    }                                                                                       //
                }
                else if (x == TempMap.GetLength(0) - 1) //Bottom of map                                      //  Repeat for right side and center
                {                                                                                           //
                    if (TempMap[x, y + 1].LOpen && TempMap[x - 1, y].BOpen)                                     //
                    {                                                                                       //
                        return RandomizeTile(RT);                                                           //
                    }                                                                                       //
                    else if (TempMap[x, y + 1].LOpen)                                                         //
                    {                                                                                       //
                        return RandomizeTile(R);                                                            //
                    }                                                                                       //
                    else if (TempMap[x - 1, y].BOpen)                                                         //
                    {                                                                                       //
                        return RandomizeTile(T);                                                            //
                    }                                                                                       //
                }                                                                                           //
                else //left side of map not top or bottom                                                   //
                {                                                                                           //
                    if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen && TempMap[x - 1, y].BOpen)            //
                    {                                                                                       //
                        return RandomizeTile(BRT);                                                          //
                    }
                    else if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen)                              //
                    {                                                                                       //
                        return RandomizeTile(BR);                                                           //
                    }
                    else if (TempMap[x - 1, y].BOpen && TempMap[x, y + 1].LOpen)                              //
                    {                                                                                       //
                        return RandomizeTile(RT);                                                           //
                    }
                    else if (TempMap[x - 1, y].BOpen && TempMap[x + 1, y].TOpen)                              //
                    {                                                                                       //
                        return RandomizeTile(BT);                                                           //
                    }
                    else if (TempMap[x, y + 1].LOpen)                                                       //
                    {                                                                                       //
                        return RandomizeTile(R);                                                            //
                    }
                    else if (TempMap[x - 1, y].BOpen)                                                       //
                    {                                                                                       //
                        return RandomizeTile(T);                                                            //
                    }
                    else if (TempMap[x + 1, y].TOpen)                                                       //
                    {                                                                                       //
                        return RandomizeTile(B);                                                            //
                    }                                                                                       //
                }                                                                                           //
            }
            else if (y == TempMap.GetLength(1) - 1)    //right side of map
            {
                if (x == 0) //top of map
                {
                    if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                    {
                        return RandomizeTile(BL);
                    }
                    else if (TempMap[x, y - 1].ROpen)
                    {
                        return RandomizeTile(L);
                    }
                    else if (TempMap[x + 1, y].TOpen)
                    {
                        return RandomizeTile(B);
                    }
                }
                else if (x == TempMap.GetLength(0) - 1) // bottom of map
                {
                    if (TempMap[x, y - 1].ROpen && TempMap[x - 1, y].BOpen)
                    {
                        return RandomizeTile(LT);
                    }
                    else if (TempMap[x, y - 1].ROpen)
                    {
                        return RandomizeTile(L);
                    }
                    else if (TempMap[x - 1, y].BOpen)
                    {
                        return RandomizeTile(T);
                    }
                }
                else    // right side of map not top or bottom
                {
                    if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen && TempMap[x - 1, y].BOpen)
                    {
                        return RandomizeTile(BLT);
                    }
                    else if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                    {
                        return RandomizeTile(BL);
                    }
                    else if (TempMap[x - 1, y].BOpen && TempMap[x, y - 1].ROpen)
                    {
                        return RandomizeTile(LT);
                    }
                    else if (TempMap[x - 1, y].BOpen && TempMap[x + 1, y].TOpen)
                    {
                        return RandomizeTile(BT);
                    }
                    else if (TempMap[x, y - 1].ROpen)
                    {
                        return RandomizeTile(L);
                    }
                    else if (TempMap[x - 1, y].BOpen)
                    {
                        return RandomizeTile(T);
                    }
                    else if (TempMap[x + 1, y].TOpen)
                    {
                        return RandomizeTile(B);
                    }
                }
            }
            else if (x == 0)   //Top of Map not left or right
            {
                if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BLR);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(LR);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BR);
                }
                else if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BL);
                }
                else if (TempMap[x, y + 1].LOpen)
                {
                    return RandomizeTile(R);
                }
                else if (TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(L);
                }
                else if (TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(B);
                }
            }
            else if (x == TempMap.GetLength(0) - 1) //bottom of map not left or right
            {
                if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(LRT);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(LR);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(RT);
                }
                else if (TempMap[x, y - 1].ROpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(LT);
                }
                else if (TempMap[x, y + 1].LOpen)
                {
                    return RandomizeTile(R);
                }
                else if (TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(L);
                }
                else if (TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(T);
                }
            }
            else    //not on the edge of the map
            {
                if (TempMap[x - 1, y].BOpen && TempMap[x, y - 1].ROpen && TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BLRT);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BLR);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(LRT);
                }
                else if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(BLT);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(BRT);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(RT);
                }
                else if (TempMap[x, y - 1].ROpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(LT);
                }
                else if (TempMap[x - 1, y].BOpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BT);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(LR);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BR);
                }
                else if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BL);
                }
                else if (TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(T);
                }
                else if (TempMap[x, y + 1].LOpen)
                {
                    return RandomizeTile(R);
                }
                else if (TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(L);
                }
                else if (TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(B);
                }
            }
            return Empty;
        }

        private void MapChecker()
        {
            List<Tuple<int, int>> checkedCoords = new List<Tuple<int, int>>();
            checkedCoords.Add(Tuple.Create(Constants.mapHeight/2, Constants.mapWidth/2));
            bool done = false;
            while (done == false)
            {
                bool addedRooms = false;
                List<Tuple<int, int>> toAdd = new List<Tuple<int, int>>();

                foreach (Tuple<int, int> coords in checkedCoords)
                {
                    if (coords.Item1 > 0 && TempMap[coords.Item1, coords.Item2].TOpen)
                    {
                        if (checkedCoords.Contains(Tuple.Create(coords.Item1 - 1, coords.Item2)) == false)
                        {
                            addedRooms = true;
                            toAdd.Add(Tuple.Create(coords.Item1 - 1, coords.Item2));
                        }
                    }
                    if (coords.Item1 < Constants.mapWidth -1 && TempMap[coords.Item1, coords.Item2].BOpen)
                    {
                        if (checkedCoords.Contains(Tuple.Create(coords.Item1 + 1, coords.Item2)) == false)
                        {
                            addedRooms = true;
                            toAdd.Add(Tuple.Create(coords.Item1 + 1, coords.Item2));
                        }
                    }
                    if (coords.Item2 > 0 && TempMap[coords.Item1, coords.Item2].LOpen)
                    {
                        if (checkedCoords.Contains(Tuple.Create(coords.Item1, coords.Item2 - 1)) == false)
                        {
                            addedRooms = true;
                            toAdd.Add(Tuple.Create(coords.Item1, coords.Item2 - 1));
                        }
                    }
                    if (coords.Item2 < Constants.mapHeight - 1 && TempMap[coords.Item1, coords.Item2].ROpen)
                    {
                        if (checkedCoords.Contains(Tuple.Create(coords.Item1, coords.Item2 + 1)) == false)
                        {
                            addedRooms = true;
                            toAdd.Add(Tuple.Create(coords.Item1, coords.Item2 + 1));
                        }
                    }
                }

                foreach (Tuple<int, int> coords in toAdd)
                {
                    if (checkedCoords.Contains(coords) == false)
                        checkedCoords.Add(coords);
                }

                if (addedRooms == false)
                {
                    done = true;
                }

            }

            for (int i = 0; i < Constants.mapWidth; i++)
            {
                for (int j = 0; j < Constants.mapHeight; j++)
                {

                    if (checkedCoords.Contains(Tuple.Create(i, j)) != true)
                    {
                        TempMap[i, j] = Empty;
                    }

                }
            }

        }

        public char[,] RandomizeMap()
        {
            GetFiles();

            char[,] grid = new char[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];

            for (int i = 0; i < Constants.mapHeight; i++)
            {
                for (int j = 0; j < Constants.mapWidth; j++)
                {
                    if((i+j)%2 == 0)
                    {
                        MapChunk[][] roomArrays;
                        if(i > 0 && i < Constants.mapHeight - 1 && j > 0 && j < Constants.mapWidth - 1)
                        {
                            roomArrays = new MapChunk[][]
                            {
                                BLR,BLT,BRT,LRT,BLRT,
                            };
                            TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                        }
                        else if (i == 0)
                        {
                            if(j > 0 && j < Constants.mapWidth - 1)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                BL,BR,LR,BLR
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                            else if(j == 0)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    B,R,BR
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                            else if(j == Constants.mapWidth - 1)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    B,L,BL
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                        }else if (i == Constants.mapHeight - 1)
                        {
                            if (j > 0 && j < Constants.mapWidth - 1)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    LT,LR,RT
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                            else if (j == 0)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    T,R,RT
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                            else if (j == Constants.mapWidth - 1)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    T,L,LT
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                        }
                        else
                        {
                            if (j == 0)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    BT,BR,RT,BRT
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                            else if (j == Constants.mapWidth - 1)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    BT,BL,LT,BLT
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Constants.mapHeight; i++)
            {
                for (int j = 0; j < Constants.mapWidth; j++)
                {
                    if((i+j)%2 != 0)
                    {
                        TempMap[i, j] = FillTile(i,j);
                    }
                }
            }

            MapChecker();

            for (int i = 0; i < Constants.mapHeight; i++)                           //
            {                                                                       //
                for (int j = 0; j < Constants.mapWidth; j++)                        //
                {                                                                   //
                    for (int k = 0; k < Constants.roomHeight; k++)                  //
                    {                                                               //
                        for (int l = 0; l < Constants.roomWidth; l++)               //
                        {                                                           //
                            grid[i * Constants.roomHeight + k, j * Constants.roomWidth + l] = TempMap[i, j].roomMap[k, l];  //  puts the chunks together in grid
                        }                                                           //
                    }                                                               //
                }                                                                   //
            }                                                                       //

            return grid;
        }

        public char[,] BossRoom()
        {
            string[] tempBossRooms = File.ReadAllLines("MapChunkCategories/BossRooms.txt");
            MapChunk[] BossRooms = new MapChunk[Constants.RoomsPerCategory];
            for(int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                BossRooms[i] = new MapChunk(new char[Constants.BossRoomHeight, Constants.BossRoomWidth]);
                for (int j = 0; j < Constants.BossRoomHeight; j++)
                {
                    for (int k = 0; k < Constants.BossRoomWidth; k++)
                    {
                        BossRooms[i].roomMap[j, k] = tempBossRooms[j + (Constants.BossRoomHeight * i)][k];
                    }
                }
            }
            return BossRooms[rand.Next(BossRooms.Length)].roomMap;
        }

        public Tile[,] makeMiniMap()
        {
            for (int i = 0; i < TempMap.GetLength(0); i++)
            {
                for (int j = 0; j < TempMap.GetLength(1); j++)
                {
                    if (B.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('^', ConsoleColor.White, ConsoleColor.Black);
                    if (L.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('>', ConsoleColor.White, ConsoleColor.Black);
                    if (R.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('<', ConsoleColor.White, ConsoleColor.Black);
                    if (T.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('V', ConsoleColor.White, ConsoleColor.Black);

                    if (BL.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('┐', ConsoleColor.White, ConsoleColor.Black);
                    if (BR.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('┌', ConsoleColor.White, ConsoleColor.Black);
                    if (BT.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                    if (LR.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                    if (LT.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('┘', ConsoleColor.White, ConsoleColor.Black);
                    if (RT.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('└', ConsoleColor.White, ConsoleColor.Black);

                    if (BLR.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('┬', ConsoleColor.White, ConsoleColor.Black);
                    if (BLT.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('┤', ConsoleColor.White, ConsoleColor.Black);
                    if (BRT.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('├', ConsoleColor.White, ConsoleColor.Black);
                    if (LRT.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('┴', ConsoleColor.White, ConsoleColor.Black);

                    if (BLRT.Contains(TempMap[i, j]))
                        miniMap[i, j] = new Tile('┼', ConsoleColor.White, ConsoleColor.Black);
                }
            }

            return miniMap;
        }

    }
}
