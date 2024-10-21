using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Transform target;

    [Header("No Lightmaps (Cleared)")]
    public List<Texture2D> lightmapColorClear;
    public List<Texture2D> lightmapDirClear;   

    [Header("Lightmaps (Mode 1)")]
    public List<Texture2D> lightmapColorOne;    
    public List<Texture2D> lightmapDirOne;     

    [Header("Lightmaps (Mode 2)")]
    public List<Texture2D> lightmapColorTwo;  
    public List<Texture2D> lightmapDirTwo;   

    private int currentLightmap = 0; 
    private bool insideTrigger = false;  

    void Update()
    {
        if (insideTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            SwitchLightmap();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            insideTrigger = true;
            ApplyLightmap(lightmapColorOne, lightmapDirOne);
            Debug.Log("Entered trigger, switched to Lightmap 1");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == target)
        {
            insideTrigger = false;
            ClearLightmaps();
            Debug.Log("Exited trigger, cleared lightmaps");
        }
    }

   
    void SwitchLightmap()
    {
        currentLightmap = (currentLightmap + 1) % 2; //Divide by the amount of lightmaps to chose from
        switch (currentLightmap)
        {
            case 0:
                ApplyLightmap(lightmapColorOne, lightmapDirOne);
                Debug.Log("Switched to Lightmap 1");
                break;
            case 1:
                ApplyLightmap(lightmapColorTwo, lightmapDirTwo);
                Debug.Log("Switched to Lightmap 2");
                break;
        }
    }
    
    void ApplyLightmap(List<Texture2D> colorMaps, List<Texture2D> dirMaps)
    {
        LightmapData[] newLightmaps = new LightmapData[colorMaps.Count];
        for (int i = 0; i < colorMaps.Count; i++)
        {
            newLightmaps[i] = new LightmapData();
            newLightmaps[i].lightmapColor = colorMaps[i];
            if (dirMaps != null && dirMaps.Count > i)
            {
                newLightmaps[i].lightmapDir = dirMaps[i];
            }
        }
        LightmapSettings.lightmaps = newLightmaps;
    }
    
    void ClearLightmaps()
    {
        LightmapSettings.lightmaps = new LightmapData[0];
    }
}
