# Multiagent-System-X

## Specs
- Unity version 2020.3
- Visual Studio 2019 with `Game development with Unity` components

## Setup

To use path finding algorithm with drones, Mercuna3D unity package is mandatory.   
Please visit *[Mercuna official website](https://mercuna.com/evaluate/)* to download your trial version.  

After downloading your files, head to Unity: `Assets > Import package > Custom Package` and select `Mercuna.unitypackage`.  
Check that you have a **'Mercuna'** folder inside the **'Assets'** folder.

## Scripts
Scripts are inside the folder `Drone_VIS/Assets/Scripts`

> ### bakeNavMesh.cs
> For dynamic generation of the NavMesh component after addition of buildings to the scene.
> 
> Rovers use NavMesh to navigate between buildings

> ### BuildingOnSurface.cs
> For creation of buildings in the scene.  
> 
> By default, minimum value for X,Y,Z is 4, and the maximum is 8. It is not recommended to exceed 10, since drones path finding algorithm does not work well with huge obstacles.


> ### droneMovement.cs
> For manual control of drones.  
> (deactivated when path finding algorithm is active)

> ### dropdownValueDrones.cs  
> For creation of drones

> ### dropdownValueMobileRobots.cs
> For creation of rovers

> ### hideMenu.cs
> Hides the parameters canvas.

> ### planScript.cs
> Allows you to change the size of the plan

> ### playerController.cs
> For controls of the selected agent  
> If the selected agent is a rover, we then use NavMesh.  
> If the selected agent is a drone, we then use Mercuna3DNavigation

> ### receiveINS.cs
> Shows the coordinates and the speed of the object.

> ### roverMovement.cs
> For manual control of rovers.  
> (deactivated when path finding algorithm is active)

> ### selectionHandler.cs
> for selecting an agent and shows its position.

> ### toggleRandomPosition.cs
> Checks the Toggle “Random position” status