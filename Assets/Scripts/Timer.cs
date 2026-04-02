using System.Collections.Generic;
using System.Diagnostics;

public static class Timer
{
    private static readonly Stopwatch stopwatch = new();
    private static List<long> steps = new();

    public static bool IsRunning
    {
        get => stopwatch.IsRunning;
    }

    public static double ElapsedSeconds
    {
        get => stopwatch.ElapsedMilliseconds * 0.001f;
    }

    public static int StepsCount
    {
        get => steps.Count;
    }

    public static double GetStepElapsedSeconds(int index)
    {
        return steps[index] * 0.001f;
    }

    /// <summary>
    /// Reset the timer and remove any steps.
    /// </summary>
    public static void Reset()
    {
        stopwatch.Reset();
        steps.Clear();
    }

    public static void Start()
    {
        stopwatch.Start();
    }

    public static void Stop()
    {
        stopwatch.Stop();
    }

    public static void Step()
    {
        steps.Add(stopwatch.ElapsedMilliseconds);
    }

    public static void Save()
    {
        // Dossier "Saves" à la racine du projet
        string folderPath = System.IO.Path.Combine(UnityEngine.Application.dataPath, "..", "Saves"); // Je répète System.IO au lieu de le mettre au début mais je crois que tu préfères comme ça
        string filePath = System.IO.Path.Combine(folderPath, "score.txt"); // Je savais faire en JSON de base lol

        // Crée le dossier s'il n'existe pas
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        // Convertit les steps en texte (un par ligne)
        List<string> lines = new List<string>();
        foreach (long step in steps)
            lines.Add(step.ToString()); // Inspiré de Python

        // Écrit dans le fichier
        System.IO.File.WriteAllLines(filePath, lines);
    }


    public static void Load()
    {
        // Dossier "Saves" à la racine du projet
        string folderPath = System.IO.Path.Combine(UnityEngine.Application.dataPath, "..", "Saves");
        string filePath = System.IO.Path.Combine(folderPath, "score.txt");

        // Si le fichier n'existe pas, on ne fait rien
        if (!System.IO.File.Exists(filePath))
        {
            UnityEngine.Debug.Log("Aucun fichier de score trouvé dommage.");
            return;
        }

        // Vide les steps actuels
        steps.Clear();

        // Lit toutes les lignes
        string[] lines = System.IO.File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            if (long.TryParse(line, out long value))
            {
                steps.Add(value);
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Ligne invalide dans score.txt : {line}");
            }
        }
    }
}
