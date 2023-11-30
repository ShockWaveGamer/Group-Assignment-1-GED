using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Logger : MonoBehaviour
{
    #region dll import

    private const string DLL_NAME = "Particle Stats Loger.dll";

    [DllImport(DLL_NAME)] public static extern void NewParticleEntry(string entryID);
    [DllImport(DLL_NAME)] public static extern int NewParticleEntryA(string entryID, int amount);

    [DllImport(DLL_NAME)] public static extern int GetParticleEntry(string entryID);
    [DllImport(DLL_NAME)] public static extern void SetParticleEnrty(string entryID, int amount);

    [DllImport(DLL_NAME)] public static extern int IncrementParticleEntry(string entryID);
    [DllImport(DLL_NAME)] public static extern int IncrementParticleEntryA(string entryID, int amount);

    [DllImport(DLL_NAME)] public static extern bool Empty();
    [DllImport(DLL_NAME)] public static extern int TotalEntries();
    [DllImport(DLL_NAME)] public static extern bool Contains(string entryID);

    [DllImport(DLL_NAME)] public static extern void RecordStatsToFile(string filePath);
    [DllImport(DLL_NAME)] public static extern void ClearStatFile(string filePath);

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            NewParticleEntry("Hi");
        }
    }
}
