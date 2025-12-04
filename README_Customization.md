
# Customization: RobotArmHelix Update

The RobotArmHelix project has been updated such that it can be built and run  again, using these tools and actions:

* customization of the [iglibmodules container repository](https://github.com/ajgorhoe/iglibmodules) on the branch `swrepos/GrLib/repoMain`
  * this also inclludes custoization of the [codedoc repository](https://github.com/ajgorhoe/IGLib.workspace.doc.codedoc) on the branch with the same name, which can be used for automatic generation of Doxygen code deocumentation
* a fork of the [Helix Toolkit](https://github.com/helix-toolkit/helix-toolkit), with a branch `00IGLib/25_12_03_CustomizingOldCommitForRobotArm_7049fa` created from an old commit compatible with the [RobotArmHelix]() project and customized such that it ues supported .NET framework and works with RobotArmHelix
* a fork of the RobotArmHelix project with a customization branch `https://github.com/Gabryxx7/RobotArmHelix` for upgrading the project and making it work

Usage:

* Clone the [iglibmodules container repository](https://github.com/ajgorhoe/iglibmodules) and checkout the branch `swrepos/GrLib/repoMain`
* Run scripts that will clone individual components checked out at appropriate references (branches):

  * `GrLibUpdateRepo_HelixToolkitForRobotArm.ps1` - clones the `Helix Toolkit` and checks out the appropriate branch
  * `GrLibUpdateRepo_RobotArmHelix.ps1` - clones the RoboArmHelix project and checks out the appropriate customization branch
  * Open the `RobotArmHelx` solution in Visual Studio, build it, and run the application. The solution is located at `RobotArmHelix\RobotArmHelix\RobotArmHelix.sln`

Expected path of the robot model:

> RobotArmHelix\RobotArmHelix\RobotArmHelix\bin\3D_Models\IRB6700-MH3_245-300_IRC5_rev02_LINK01_CAD.stl

