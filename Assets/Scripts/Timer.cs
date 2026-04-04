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

    // Pour la partie bonus où ça sauvegarde uniquement le meilleur temps, là faut créer une méthode pour pouvoir comparer
    public static long GetFinalTime()
    {
        return stopwatch.ElapsedMilliseconds;
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

        // Charge l'ancien record s'il existe pour pouvoir comparer avec le nouveau score
        long oldRecord = long.MaxValue;

        if (System.IO.File.Exists(filePath))
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);

            if (lines.Length > 0 && long.TryParse(lines[lines.Length - 1], out long lastStep))
            {
                oldRecord = lastStep; // Le dernier step = temps final
            }
        }

        long newRecord = GetFinalTime();

        // Vérifier si le nouveau score est meilleur
        if (newRecord >= oldRecord)
        {
            UnityEngine.Debug.Log("No new record.");
            return;
        }

        UnityEngine.Debug.Log("New record ! Saving...");

        // Crée le dossier s'il n'existe pas
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        // Convertit les steps en texte (un par ligne)
        List<string> linesToSave = new List<string>();
        foreach (long step in steps)
            linesToSave.Add(step.ToString()); // Inspiré de Python

        // Écrit dans le fichier
        System.IO.File.WriteAllLines(filePath, linesToSave);
    }


    public static void Load()
    {
        // Dossier "Saves" à la racine du projet
        string folderPath = System.IO.Path.Combine(UnityEngine.Application.dataPath, "..", "Saves");
        string filePath = System.IO.Path.Combine(folderPath, "score.txt");

        // Si le fichier n'existe pas, on ne fait rien
        if (!System.IO.File.Exists(filePath))
        {
            UnityEngine.Debug.Log("No savefile found, too bad.");
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
        }
    }
}
