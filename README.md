# MathBox

Try out Unity game dev platform with C# and learn math.

## Get the tools
1. [Install Unity](https://store.unity.com/#plans-individual)
2. [Install Visual Studio](https://visualstudio.microsoft.com/downloads/)  
3. [Configure Visual Studio for Unity](https://docs.microsoft.com/en-us/visualstudio/gamedev/unity/get-started/getting-started-with-visual-studio-tools-for-unity?pivots=windows)  
*Make sure you* [Set Visual Studio as default code editor in Unity](https://docs.unity3d.com/Manual/VisualStudioIntegration.html)

## Unity and git

- https://forum.unity.com/threads/which-folders-should-i-add-to-git.608149/

- https://learn.unity.com/tutorial/project-architecture-unity-project-folder-structure#

## Starting a Unity project after cloning this repo

After you clone this repo you need to rebuild the Unity Library-folder for each project project as that folder is not committed to git.  
Navigate to the scene-file in windows explorer and open it.

Example:  
Double-click on `...\MathBox\Graph1\Assets\Scenes\SampleScene.unity`

**NB** Opening the project folder from the Unity UI don't rebuild the Library folder correct the first time and you risk losing assets.
