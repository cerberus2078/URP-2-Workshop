using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxSunset;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Gradient gradientNightToSunrise;
    [SerializeField] private Gradient gradientSunriseToDay;
    [SerializeField] private Gradient gradientDayToSunset;
    [SerializeField] private Gradient gradientSunsetToNight;
    [SerializeField] private Light globalLight;

    public int minutes;

    public int Minutes
    { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

    public int hours;

    public int Hours
    { get { return hours; } set { hours = value; OnHoursChange(value); } }

    public int days;

    public int Days
    { get { return days; } set { days = value; } }

    private float actualSeconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        actualSeconds += Time.deltaTime;
        
        if (actualSeconds >= 1)
        {
            Minutes += 30;
            actualSeconds = 0;
        }
    }
    private void OnMinutesChange(int value)
    {
        if (value >= 60)
        {
            Hours++;
            minutes = 0;
        }
        if (Hours >= 24)
        {
            Hours = 0;
            Days++;
        }
    }

    public void OnHoursChange(int value) 
    {
        if (value == 6)
        {
            StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 5f));
            StartCoroutine(LerpLight(gradientNightToSunrise, 10f));
        }
        else if (value == 10)
        {
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 5f));
            StartCoroutine(LerpLight(gradientSunriseToDay, 10f));
        }
        else if (value == 18)
        {
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 5f));
            StartCoroutine(LerpLight(gradientDayToSunset, 10f));
        }
        else if (value == 22)
        {
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 5f));
            StartCoroutine(LerpLight(gradientSunsetToNight, 10f));
        }
    }

    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        RenderSettings.skybox.SetFloat("_Blend", 0);

        for (float i = 0; i < time; i += Time.deltaTime)
        {
            RenderSettings.skybox.SetFloat("_Blend", i / time);
            yield return null;
        }
        RenderSettings.skybox.SetTexture("_Texture1", b);
    }

    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
    for (float i = 0; i < time; i += Time.deltaTime)
    {
        globalLight.color = lightGradient.Evaluate(i / time);
        RenderSettings.fogColor = globalLight.color;
        yield return null;
    }
}
}
