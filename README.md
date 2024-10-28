# URP-2-Workshop

## Lighting Workshop
### Steps

1. **Verify Lighting Settings**  
   Go to the **Lighting** tab: `Window > Rendering > Lighting`.
   - Create a new Lighting Setting if one doesn't exist already.
   - Ensure the following settings are applied:
     - **Mixed Lighting**:
       - Enable **Baked Global Illumination**
       - Set **Lighting Mode** to **Baked Indirect**
     - **Lightmapping Settings**:
       - **Lightmapper**: Progressive GPU
       - **Lightmap Resolution**: 10
       - **Max Lightmap Size**: 512
       - **Lightmap Compression**: Low Quality
     - Confirm **Auto Generate** is unchecked.

> Low quality settings like this will not create the most realistic effects, but they're recommended now so that baking doesn't take too long. That's also why Auto Generate should stay unchecked, to be able to make changes real time.

2. **Open the Lightmaps Scene**  
   - You’ll see a furnished house, but it's lacking a lot of light. Your task will be to add that light.

3. **Add Light Fixtures**  
   - In **Assets**: `Lightmaps > nappin > House Props Softpack > Prefabs > Lights`, select various lamp models.
   - Place them around the house as desired.
     - Customize the light color, intensity, etc., but keep them set as **baked lights**.
> By selecting the lightbulb in the icons on the top of the Scene view, you can see a preview of your lights.

4. **Bake and Save Lightmaps**
   - In `Scenes > Lightmaps`, locate `LightingData` and delete it.
   - Go to the **Lighting** tab and click **Generate Lights** to bake the lights.
   - Once complete:
     - Save the generated lightmaps to a folder named `Mode1`.
     - Delete the `LightingData` file again.
> Deleting LightinData is only necessary when you're going to generate new lightmaps, as otherwise your previous lightmaps would be overwritten. If you enter playmode without this file, lights will not load properly. That is why you won't delete the file again after generating the final lightmaps.

5. **Adjust Lights for a New Mode**
   - Modify lights by changing their colors, placement, or removing some entirely.
   - Bake lights again and save this new lightmap setup in a folder named `Mode2`.

6. **Set Up Light Switching**
   - Find the `LightSwitch` object in the **Hierarchy**.
   - Add each lightmap (Mode1 and Mode2) to the respective slots in `LightSwitch`. Pay attention to which one is the 'dir' file.
   
7. **Test Lightmap Switching**
   - Enter **Play Mode**:
     - Press **Space** to switch between lightmaps when inside the house.
     - When outside, the lights "turn off" using a lightmap with no added lights.

## Technical Applications

- Baked lighting reduces real-time rendering load.
- Light probes provide consistent lights and shadows across different scenarios.
- Allows for rapid, cost-effective, and impactful adjustments to lighting.


## Post Processing
I suggest using post processing in the Lightmaps scene but you can use other scenes if you wish to.
You'll be using both global and local volumes but you get to choose which effects you would like to use.

**Main Camera -> Global volume**
1. Go to the Main Cameras inspector. Click the `Rendering` dropdown in the Camera component and make sure that the `Post Processing` box is checked, otherwise your effects **will not** be rendered in game!
2. In the inspector of the camera add a `Volume` component.
3. In the `Volume` component click `New` on `Profile`. From there you will be able to click `Add Overrides` and add any post-processing effects you'd like. 

**GameObject -> Local volume**
1. Create an empty GameObject and add a `Box Collider`. Move the object into the inside of the house and adjust the size of the collider to fit the inside interior.
2. Repeat the same steps 2-3 as done in the Main Camera.
3. You can also adjust the blend distance in the `Volume` component to make a smoother transition for the effects when entering the house.

## Day Night Cycle
### Why day night cycle?
A day night cycle can add a level of realism to your game, if that's what you're aiming for. This tutorial will include:
- Changing of skyboxes using time
- Changing of light gradients using time
- Rotation of light using time to reflect the suns/moons movement

### Changing of skyboxes using time
#### Starting off
1. First off, I used skyboxes on the asset store.
   Link: https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353
   <br>The ones I used were night, sunrise, sunset and day, all with the sun/moon. I'm using these skyboxes to fade in and out of each other so that it's clear the time of day is changing. The fading part will come later though.
2. In your project, create an empty game object. This object is going to handle the script for handling the day night cycle. Call it TimeManager.
3. To this empty game object, add a script also called TimeManager.

#### Unity Inspector
To start off, a shader is needed so that the skybox can be set and that it can transition from one to the other.
1. Create a standard surface shader in the "Day Night Cycle" folder, name this "Skybox".
2. Double click this shader to edit it in your code editor of choice. Afterwards, paste the following code: https://pastebin.com/1CSJmbYH
3. Select the shader and create a material named "SkyboxMat".
4. When clicking on the material, you can see in the inspector there are 2 textures available:
<br>&nbsp;&nbsp;&nbsp;- Texture 1: texture of the current skybox
<br>&nbsp;&nbsp;&nbsp;- Texture 2: texture of the next skybox
5. Drag the night skybox into texture 1 and the sunrise texture into texture 2.
6. Make sure your blend slider is set to 0. This blend feature is going to help us actually blend from one skybox to the other ;)
#### Code
For the day night cycle to actually work, we first need to keep track of time. For this, create 3 variables for the amount of minutes, hours and days passed. <br>
If you're wondering about the OnMinutesChange() and OnHoursChange() functions, I'll explain this later ;) Also for the scope of this workshop, I decided not to implement an OnDaysChange() function, because it's really arbitrary.
```
public int minutes;

public int Minutes
{ get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

public int hours = 5;

public int Hours
{ get { return hours; } set { hours = value; OnHoursChange(value); } }

public int days;

public int Days
{ get { return days; } set { days = value; } }
```

To actually keep track of time, we need to have a variable that stores the actual seconds passed in real life. For this, add another variable called actualSeconds.

```
private float actualSeconds;
```

Great! Now that we've gotten the time variables written down, let's start by actually having the variables store something. We'll start off by incrementing actualSeconds with Time.deltaTime in the unity Update() method. If a second has passed, a minute in game has passed.
<br>In game, this is very slow, so for now I'm saying 30 in-game minutes have passed for every second.
```
public void Update()
{
    actualSeconds += Time.deltaTime;
    
    if (actualSeconds >= 1)
    {
        Minutes += 30;
        actualSeconds = 0;
    }
}
```

Next up, the OnMinuteChange() function you saw earlier. This function is there to add hours and days to their respective variables.
```
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
```

Afterwards we have the OnHoursChange() function. This function is responsible for changing the skybox and the gradients whenever Hours is a specific value. You can change the values if you want, in my opinion this is the best combination.
```
public void OnHoursChange(int value) 
{
    if (value == 6)
    {
        StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 5f));
        //StartCoroutine(LerpLight(graddientNightToSunrise, 10f));
    }
    else if (value == 10)
    {
        StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 5f));
        //StartCoroutine(LerpLight(graddientSunriseToDay, 10f));
    }
    else if (value == 18)
    {
        StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 5f));
        //StartCoroutine(LerpLight(graddientDayToSunset, 10f));
    }
    else if (value == 22)
    {
        StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 5f));
        //StartCoroutine(LerpLight(graddientSunsetToNight, 10f));
    }
}
```

Whoa! Coroutines?! Don't worry, I'll help you out. First off, let's talk about LerpSkybox(). This coroutine is responsible for changing the skybox with a nice blend effect. We do this by setting the different textures we have in the unity editor and setting the blend to 0. Afterwards, we blend in the skybox with the next skybox with the amount we set at float time. After this is done, the skybox is set to the next skybox.

```
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
```

Now that you've done this, we need to add variables for the skybox textures. Add this code at the top of the TimeManager class.
```
[SerializeField] private Texture2D skyboxNight;
[SerializeField] private Texture2D skyboxSunrise;
[SerializeField] private Texture2D skyboxSunset;
[SerializeField] private Texture2D skyboxDay;
```

#### Back to the unity editor
1. After you set up the LerpSkybox() coroutine, you can already notice you can put your skyboxes in the TimeManager empty game object. Do this accordingly, i.e. night in Skybox night etc.
2. In the scene view, drag the skybox onto the sky, this will set the skybox.
3. Do a test run to see if the skybox is changing!

Congratulations, you've made a functioning skybox! This covers the part "Changing the skybox with time".

### Changing of light gradients using time
What we now want is to change the light accordingly to the current skybox. We first create a variable to get the global light.

```
[SerializeField] private Light globalLight;
```

Next up, create the function "LerpLight()". This function is responsible for changing the light of global light and fog.
```
private IEnumerator LerpLight(Gradient lightGradient, float time)
{
    for (float i = 0; i < time; i += Time.deltaTime)
    {
        globalLight.color = lightGradient.Evaluate(i / time);
        RenderSettings.fogColor = globalLight.color;
        yield return null;
    }
}
```

To have a smooth transition between the 4 colours, we will now make 4 gradients that correspond to their skybox. Create 4 gradient variables.
```
[SerializeField] private Gradient gradientNightToSunrise;
[SerializeField] private Gradient gradientSunriseToDay;
[SerializeField] private Gradient gradientDayToSunset;
[SerializeField] private Gradient gradientSunsetToNight;
```

#### Let's go to the unity editor again!
1. To actually set the gradients, you need to pick a colour code for each gradient. I used the following colour codes for the gradients, but feel free to experiment on your own.
- Night: 202950
- Sunrise: F19E35
- Day: F3F1CF
- Sunset: FF5A00

How to set up a gradient: double click on the gradient field on the empty game object and you can see an arrow underneath the white bar, click on the left most arrow to set the first colour and on the utmost right arrow to set the colour to blend to. Feel free to ask for help here if you need any!
2. After you've down this step, drag and drop the directional light into the Global Light field in the TimeManager object.
3. Adjust the starting colour of the global light with the night colour. (202950)
4. Add fog to the scene. This is done by going to Window>Rendering>Lighting>Environment. Check the box for fog.
5. Set the starting colour of the fog to the night colour (202950) and set the density to 0.05.
6. Go for another test run and see if it works!

Congratulations, now we've added gradients for realistic lighting, this covers the part "Changing of light gradients using time"

### Rotation of light using time to reflect the suns/moons movement
Now this is actually very simple, because we want to rotate the directional light every time a second passes. Use the following code at the top of the OnMinutesChange() function.
```
globalLight.transform.Rotate(Vector3.up, (1f / (1440f / 4f)) * 360f, Space.World);
```

Go for another test run, and if you've managed to implement this, congratulations, you have officially made a functioning skybox! I really hoped you learned something out of this and had fun! I noted some things that could be improved upon below if you want to work further on this :)

### Issues with this implementation
- Whenever you stop the game when a skybox is not night, the colour and the fog is still set to the night value, but the skybox isn't.
- The rotation of the light is not smooth. 

