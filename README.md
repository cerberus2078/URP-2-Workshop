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

