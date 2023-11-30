using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class ParticleLoggerPluginManager : MonoBehaviour
{
    #region dll

    private const string DLL_NAME = "ParticleLogger";

    /*[DllImport(DLL_NAME)] private static extern void NewParticleEntry(string entryID);
    [DllImport(DLL_NAME)] private static extern int NewParticleEntrySetAmount(string entryID, int amount);

    [DllImport(DLL_NAME)] private static extern int GetParticleEntry(string entryID);
    [DllImport(DLL_NAME)] private static extern void SetParticleEnrty(string entryID, int amount);

    [DllImport(DLL_NAME)] private static extern int IncrementParticleEntry(string entryID);
    [DllImport(DLL_NAME)] private static extern int IncrementParticleEntrySetAmount(string entryID, int amount);

    [DllImport(DLL_NAME)] private static extern bool Empty();
    [DllImport(DLL_NAME)] private static extern int TotalEntries();
    [DllImport(DLL_NAME)] private static extern bool Contains(string entryID);

    [DllImport(DLL_NAME)] private static extern void RecordStatsToFile(string filePath);
    [DllImport(DLL_NAME)] private static extern void ClearStatFile(string filePath);*/

    [DllImport(DLL_NAME)] private static extern void SetParticleArray(int index, int amount);

    [DllImport(DLL_NAME)] private static extern int GetParticleArray(int index);

    [DllImport(DLL_NAME)] private static extern void RecordArrayToFile(string filePath);

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetParticleArray(0, FindObjectsOfType<Particle>().Count());
            Debug.Log(GetParticleArray(0));
            //RecordArrayToFile(Application.dataPath + "SaveData/SaveData.txt");
        }
    }
}
