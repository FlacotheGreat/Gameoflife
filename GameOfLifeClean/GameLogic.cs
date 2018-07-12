using System;
using System.Collections.Generic;

namespace GameOfLifeClean
{
    public class GameLogic
    {

        //game of life logic pseudocode

        //overall logic
        //process clicks to new game state
        //process new game state based on current game state
        //save new game state
        //send new game state to users

        //some problems:
        //1. somehow, a clicked square needs to tell a controller or something which x and y were clicked
        //2. this code needs to send out its completed nextGrid to the clients so they will have an updated screen
        //3. this does not account for the need to mix colors

        //data structures

        static int xLength = 16;
        static int yLength = 16;
        //bools represent alive or dead
        public bool[,] currentGrid = new bool[xLength, yLength];
        public bool[,] nextGrid = new bool[xLength, yLength];
        public bool firstLaunch = true;

        //keep track of colors too
        public System.Drawing.Color[,] currentGridColors = new System.Drawing.Color[xLength, yLength];

        public System.Drawing.Color[,] nextGridColors = new System.Drawing.Color[xLength, yLength];

        //initial clear
        void initalClear()
        {
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    currentGrid[i, j] = false;
                    nextGrid[i, j] = false;
                    currentGridColors[i, j] = System.Drawing.Color.FromArgb(255, 255, 255);
                    nextGridColors[i, j] = System.Drawing.Color.FromArgb(255, 255, 255);
                }
            }
        }

        //on a new click

        public void onNewClick(int xClick, int yClick, System.Drawing.Color fillColor)
        {
            //if the cell was dead, make it alive, otherwise, kill it
            if (currentGrid[xClick, yClick] == false)
            {
                nextGrid[xClick, yClick] = true;
                nextGridColors[xClick, yClick] = fillColor;
            }
            else
            {
                nextGrid[xClick, yClick] = false;
                nextGridColors[xClick, yClick] = System.Drawing.Color.FromArgb(255, 255, 255);
            }
        }

        //to calculate the next grid
        public void getNextGrid()
        {
            if(firstLaunch)
            {
                initalClear();
                firstLaunch = false;
            }
            //loop through the whole current grid
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    List<System.Drawing.Color> colorList = new List<System.Drawing.Color>();
                    int aliveNeighborCount = 0;
                    //4 special corner cases
                    if (i == 0 && j == 0)
                    {
                        if (checkNeighbor(i + 1, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j+1]);
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j+1]);
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j]);
                        }
                    }
                    else if (i == xLength - 1 && j == 0)
                    {
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j]);
                        }
                        if (checkNeighbor(i - 1, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j+1]);
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j+1]);
                        }
                    }
                    else if (i == 0 && j == yLength - 1)
                    {
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j-1]);
                        }
                        if (checkNeighbor(i + 1, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j-1]);
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j]);
                        }
                    }
                    else if (i == xLength - 1 && j == yLength - 1)
                    {
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j-1]);
                        }
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j]);
                        }
                        if (checkNeighbor(i - 1, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j-1]);
                        }
                    }
                    //now check for the 4 borders, since the corners have been excluded
                    else if (i == 0)
                    {
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j-1]);
                        }
                        if (checkNeighbor(i + 1, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j-1]);
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j]);
                        }
                        if (checkNeighbor(i + 1, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j+1]);
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j+1]);
                        }
                    }
                    else if (i == xLength - 1)
                    {
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j-1]);
                        }
                        if (checkNeighbor(i - 1, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j-1]);
                        }
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j]);
                        }
                        if (checkNeighbor(i - 1, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j+1]);
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j+1]);
                        }
                    }
                    else if (j == 0)
                    {
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j]);
                        }
                        if (checkNeighbor(i - 1, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j+1]);
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j+1]);
                        }
                        if (checkNeighbor(i + 1, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j+1]);
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j]);
                        }
                    }
                    else if (j == yLength - 1)
                    {
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j]);
                        }
                        if (checkNeighbor(i - 1, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j-1]);
                        }
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j-1]);
                        }
                        if (checkNeighbor(i + 1, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j-1]);
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j]);
                        }
                    }
                    //base case
                    else
                    {
                        if (checkNeighbor(i - 1, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j-1]);
                        }
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j]);
                        }
                        if (checkNeighbor(i - 1, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i-1, j+1]);
                        }
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j-1]);
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i, j+1]);
                        }
                        if (checkNeighbor(i + 1, j - 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j-1]);
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j]);
                        }
                        if (checkNeighbor(i + 1, j + 1))
                        {
                            aliveNeighborCount++;
                            colorList.Add(currentGridColors[i+1, j+1]);
                        }
                    }
                    //if the current spot is dead but has 3 living neighbors, it comes alive next round
                    if (currentGrid[i, j] == false)
                    {
                        if (aliveNeighborCount == 3)
                        {
                            nextGrid[i, j] = true;
                            if(colorList.Count == 2){
                                nextGridColors[i, j] = blendColors(colorList[0], colorList[1]);
                            }else if(colorList.Count == 3){
                                nextGridColors[i, j] = blendColors(colorList[0], colorList[1], colorList[2]);
                            }
                        }
                    }
                    //if the current spot is alive but has 1 or 0 or more than 3 neighbors, it dies next round
                    else
                    {
                        if (aliveNeighborCount <= 1 || aliveNeighborCount >= 4)
                        {
                            nextGrid[i, j] = false;
                            nextGridColors[i, j] = System.Drawing.Color.FromArgb(255, 255, 255);
                        }else{
                            if(colorList.Count == 2){
                                nextGridColors[i, j] = blendColors(colorList[0], colorList[1]);
                            }else if(colorList.Count == 3){
                                nextGridColors[i, j] = blendColors(colorList[0], colorList[1], colorList[2]);
                            }
                        }
                    }
                }
            }
            //!!!------------------------------------------!!!
            //push the nextGrid in such a way that the clients will display it...
            //!!!------------------------------------------!!!

            //maybe something like
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    if (nextGrid[i, j] == true)
                    {
                        // tellAllClientsToExecute(assignColorToSquare(i, j, someColor));
                        //in the game handler we need to use the sendXYColor method
                        
                    }
                    else
                    {
                        //this makes a square white
                        // tellAllClientsToExecute(assignColorToSquare(i, j, #FFFFFF))

                    }
                }
            }

            //now that logic for the next grid is done, copy it to the current grid and clear out the next grid
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    currentGrid[i, j] = nextGrid[i, j];
                    nextGrid[i, j] = false;
                    currentGridColors[i, j] = nextGridColors[i, j];
                    nextGridColors[i, j] = System.Drawing.Color.FromArgb(255, 255, 255);
                }
            }
        }

        public System.Drawing.Color blendColors(System.Drawing.Color color1, System.Drawing.Color color2){
            System.Drawing.Color returnColor = System.Drawing.Color.FromArgb(color1.R/2 + color2.R/2, color1.G/2 + color2.G/2, color1.B/2 + color2.B/2);
            return returnColor;
        }

        public System.Drawing.Color blendColors(System.Drawing.Color color1, System.Drawing.Color color2, System.Drawing.Color color3){
            System.Drawing.Color returnColor = System.Drawing.Color.FromArgb(color1.R/3 + color2.R/3 + color3.R/3, color1.G/3 + color2.G/3 + color3.G/3, color1.B/3 + color2.B/3 + color3.B/3);
            return returnColor;
        }

        bool checkNeighbor(int x, int y)
        {
            return currentGrid[x, y];
        }

        public System.Drawing.Color getIndexColor(int x, int y){
            return currentGridColors[x, y];
        }
    }
}