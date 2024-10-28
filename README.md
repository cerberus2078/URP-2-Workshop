# URP-2-Workshop
URP-2-workshop








# Post Processing
I suggest using post processing in the Lightmaps scene but you can use other scenes if you wish to.
You'll be using both global and local volumes but you get to choose which effects you would like to use.

**Main Camera -> Global volume**
1. Go to the Main Cameras instpector. Click the Rendering dropdown in the Camera component and make sure that the Post Processing box is checked, otherwise your effects **will not** be rendered in game!
2. In the inspector of the camera add a Volume component.
3. Click New on Profile under the Volume component. From there you will be able to click Add Overrides and add any post-processing effects you'd like. 

**GameObject -> Local volume**
1. Create an empty GameObject and add a box collider. Move the object into the inside of the house and adjust the size of the box collider to fit the inside.
2. Repeat the same steps 2-3 as done in the Main Camera.
3. You can also adjust the blend distance in the Volume component to make a smoother transition for the effects when entering the house.
