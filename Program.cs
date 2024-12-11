using System;
using System.Collections.Generic;
using System.Diagnostics;

public class DFS
{
    
    public static void RecursiveDFS(Dictionary<int, List<int>> graph, int node, HashSet<int> visited)
    {
        if (visited.Contains(node))
            return;

        visited.Add(node);
        foreach (var neighbor in graph[node])
        {
            RecursiveDFS(graph, neighbor, visited);
        }
    }

    
    public static void IterativeDFS(Dictionary<int, List<int>> graph, int startNode)
    {
        var stack = new Stack<int>();
        var visited = new HashSet<int>();

        stack.Push(startNode);

        while (stack.Count > 0)
        {
            int current = stack.Pop();
            if (!visited.Contains(current))
            {
                visited.Add(current);
                foreach (var neighbor in graph[current])
                {
                    stack.Push(neighbor);
                }
            }
        }
    }

    
    public static Dictionary<int, List<int>> GenerateLargeGraph(int nodes, int edgesPerNode)
    {
        var graph = new Dictionary<int, List<int>>();
        var random = new Random();

        for (int i = 0; i < nodes; i++)
        {
            graph[i] = new List<int>();
            for (int j = 0; j < edgesPerNode; j++)
            {
                int neighbor = random.Next(0, nodes);
                if (neighbor != i && !graph[i].Contains(neighbor))
                {
                    graph[i].Add(neighbor);
                }
            }
        }
        return graph;
    }

    
    public static void Benchmark()
    {
        int nodes = 10000; 
        int edgesPerNode = 10; 
        var graph = GenerateLargeGraph(nodes, edgesPerNode);

        int startNode = 0;

        Console.WriteLine("Тестирование рекурсивного DFS...");
        var stopwatch = Stopwatch.StartNew();
        var visitedRecursive = new HashSet<int>();
        RecursiveDFS(graph, startNode, visitedRecursive);
        stopwatch.Stop();
        Console.WriteLine($"Рекурсивный DFS завершён за {stopwatch.ElapsedMilliseconds} мс");

        Console.WriteLine("Тестирование итеративного DFS...");
        stopwatch.Restart();
        IterativeDFS(graph, startNode);
        stopwatch.Stop();
        Console.WriteLine($"Итеративный DFS завершён за {stopwatch.ElapsedMilliseconds} мс");
    }

    
    public static void Main()
    {
        Benchmark();
    }
}
