using System;
using System.IO;

class Game
{
    static void Main()
    {
        char[,] map = ReadMap("level01", out int performerX, out int performerY);
        DrawMap(map);
        PlaceEnemies(map);

        int health = 100;

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            int newPerformerX = performerX;
            int newPerformerY = performerY;

            switch (key.Key)
            {
                case ConsoleKey.W:
                    newPerformerX--;
                    break;
                case ConsoleKey.S:
                    newPerformerX++;
                    break;
                case ConsoleKey.A:
                    newPerformerY--;
                    break;
                case ConsoleKey.D:
                    newPerformerY++;
                    break;
            }

            if (newPerformerX >= 0 && newPerformerX < map.GetLength(0) && newPerformerY >= 0 && newPerformerY < map.GetLength(1))
            {
                if (map[newPerformerX, newPerformerY] != '#')
                {  
                    map[performerX, performerY] = ' ';

                    performerX = newPerformerX;
                    performerY = newPerformerY; 
                    
                    if (map[newPerformerX, newPerformerY] == 'D')
                    {
                        Console.WriteLine("Level Complete!");
                        break;
                    }

                    if (map[performerX, performerY] == 'e')
                    {
                        health -= 20;
                    }
                   
                    map[performerX, performerY] = '@';

                    Console.Clear();
                    DrawMap(map);
                    DrawHealthBar(health);

                    if (health <= 0)
                    {
                        Console.WriteLine("Game Over!");
                        break;
                    }
                }
            }
        }
    }

    static char[,] ReadMap(string mapName, out int performerX, out int performerY)
    {
        performerX = 0;
        performerY = 0;

        string[] newFile = File.ReadAllLines($"maps/{mapName}.txt");
        char[,] map = new char[newFile.Length, newFile[0].Length];

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (j < newFile[i].Length)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == '@')
                    {
                        performerX = i;
                        performerY = j;
                    }
                }
            }
        }

        return map;
    }

    static void DrawMap(char[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine();
        }
    }

    static void PlaceEnemies(char[,] map)
    {
        Random random = new Random();

        int numEnemies = 6;

        for (int i = 0; i < numEnemies; i++)
        {
            int x = random.Next(2, map.GetLength(0) - 1);
            int y = random.Next(2, map.GetLength(1) - 1);

            if (map[x, y] == ' ')
            {
                map[x, y] = 'e';
            }
        }
    }
    
    static void DrawHealthBar(int health)
    {
        int healthPercentage = health / 10;
        Console.WriteLine($"Health: [{new string('#', healthPercentage)}{new string('_', 10 - healthPercentage)}]");
    }
}
