namespace GameOfLifeClean
{
    public class GameLogic
    {
        static int xLength = 16;
        static int yLength = 16;
        //bools represent alive or dead
        bool[,] currentGrid = new bool[xLength, yLength];
        bool[,] nextGrid = new bool[xLength, yLength];

        //initial clear
        void initalClear()
        {
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    currentGrid[i, j] = false;
                    nextGrid[i, j] = false;
                }
            }
        }

        //on a new click

        //!!!------------------------------------------!!!
        //get x and y from js somehow
        //!!!------------------------------------------!!!

        public void onNewClick(int xClick, int yClick)
        {
            //if the cell was dead, make it alive, otherwise, kill it
            if (currentGrid[xClick, yClick] == false)
            {
                nextGrid[xClick, yClick] = true;
            }
            else
            {
                nextGrid[xClick, yClick] = false;
            }
        }

        //to calculate the next grid
        void getNextGrid()
        {
            //loop through the whole current grid
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    int aliveNeighborCount = 0;
                    //4 special corner cases
                    if (i == 0 && j == 0)
                    {
                        if (checkNeighbor(i + 1, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                        }
                    }
                    else if (i == xLength - 1 && j == 0)
                    {
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i - 1, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                    }
                    else if (i == 0 && j == yLength - 1)
                    {
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                        }
                    }
                    else if (i == xLength - 1 && j == yLength - 1)
                    {
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i - 1, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                    }
                    //now check for the 4 borders, since the corners have been excluded
                    else if (i == 0)
                    {
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                    }
                    else if (i == xLength - 1)
                    {
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i - 1, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i - 1, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                    }
                    else if (j == 0)
                    {
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i - 1, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                        }
                    }
                    else if (j == yLength - 1)
                    {
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i - 1, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                        }
                    }
                    //base case
                    else
                    {
                        if (checkNeighbor(i - 1, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i - 1, j))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i - 1, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j - 1))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j))
                        {
                            aliveNeighborCount++;
                        }
                        if (checkNeighbor(i + 1, j + 1))
                        {
                            aliveNeighborCount++;
                        }
                    }
                    //if the current spot is dead but has 3 living neighbors, it comes alive next round
                    if (currentGrid[i, j] == false)
                    {
                        if (aliveNeighborCount == 3)
                        {
                            nextGrid[i, j] = true;
                        }
                    }
                    //if the current spot is alive but has 1 or 0 or more than 3 neighbors, it dies next round
                    else
                    {
                        if (aliveNeighborCount <= 1 || aliveNeighborCount >= 4)
                        {
                            nextGrid[i, j] = false;
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
                }
            }
        }

        bool checkNeighbor(int x, int y)
        {
            return currentGrid[x, y];
        }

    }
}
