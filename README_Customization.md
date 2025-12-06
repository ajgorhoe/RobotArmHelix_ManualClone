
# Customization: RobotArmHelix Update

You can **skip to [Usage](#usage)** if not interested in details of customization.

## First Stage of Customization: Using Container Repository and Specialized Fork of Helix Toolkit

The RobotArmHelix project has been updated such that it can be built and run. The C# project file (`.csproj`) was changed to the new SDK-style to make changes easier. The project was upgraded to .NET Framework 4.8, which is supported and can easily be targeted on Windows computers. Source code has not been changed except for the part where robot models are locates. This part was modified to be more robust, and finds models through location of executable (motels are copied to the directory of executable during build process).

Instead of direcly referencing Helix Toolkit's DLLs (which needed to be generated and put to specific location), the project now references source projects from the source repository, which must be cloned at the specified relative location (scripts are provided that take care of this). The project currently works only with very old versions of Helix Toolkit (before 2018), which did not support any currently supported version of .NET or .NET Framework. Therefore, the Helix Toolkit sources are [cloned form a fork](https://github.com/ajgorhoe/helix-toolkit) which contains customization of these old versions on the branch `25_12_03_CustomizingOldCommitForRobotArm_7049fa`. This branch adapts the necessary projects from Helix toolkit such that they can be used by the RobotArmHelix project. Note that there is an issue with how Helix Toolkit projects are organized, because of which Helic Toolkit cannot be cloned in the same directory as the RobotArmHelix. Instead, the RobotArmHelix project must be cloned at one level deeper, in a separate directory, that contains copies of some files from Helix Toolkit.

The following components were used in this customization:

* customization of the [iglibmodules container repository](https://github.com/ajgorhoe/iglibmodules) on the branch `swrepos/GrLib/repoMain`
  * this also inclludes custoization of the [codedoc repository](https://github.com/ajgorhoe/IGLib.workspace.doc.codedoc) on the branch with the same name, which can be used for automatic generation of Doxygen code deocumentation
* a fork of the [Helix Toolkit](https://github.com/helix-toolkit/helix-toolkit), with a branch `00IGLib/25_12_03_CustomizingOldCommitForRobotArm_7049fa` created from an old commit compatible with the [RobotArmHelix](https://github.com/Gabryxx7/RobotArmHelix) project and customized such that it ues supported .NET framework and works with RobotArmHelix
* a fork of the RobotArmHelix project with a customization branch `https://github.com/Gabryxx7/RobotArmHelix` for upgrading the project and making it work

## Second Stage of Customization: Self-contained Project, Includes Cloning Scripts and Initialization Project

In this stage, **scripts for cloning of the Helix Toolkit** repository (whic contains project dependencies) were added. Specifically, run the PowerShell script located at `scripts/UpdateRepo_HelixToolkitForRobotArm.ps1`. This script clones the Helix Toolkit from the appropriate fork and checks out the branch that is compatible with the current version of the `RobotArmHelix` project. The repository is cloned into the `helix-toolkit-forRobotArm/` directory beside the directory where `RobotArmHelix` repository is cloned.

Beside providing the cloning scripts, an **initialization project** called `InitModule` is added to the solution. This project is not a code project and does not generate a library or executable, it just runs the script that clone the Helix Toolkit repository, where dependencies are references. The projectt is set to "build" before all other projects in the solution, such that source code of dependency projects is already available when other projects are built.

Of course, the solution `RobotArmHelix.sln` or projects individually can also be built by any other IDE (like `VS Code` or `JetBrains Rider`) or via command-line tools like `dotnet` or `MSBuild`.

## Usage

### Use the Newer Way, Utilizing Cloning & Updating Scripts and Initialization Project

This approach is the most comfortable. You just need to clone the repository and run the RobotArmHelix program in Visual Studio:

* Clone the appropriate [RobotArmHelix fork](#https://github.com/ajgorhoe/RobotArmHelix)
  * After (if) the customized version is merged in, you can also use the [original repository](https://github.com/Gabryxx7/RobotArmHelix)
* Check out the appropriate branch, `swrepos/GrLib/SelfContainedRepository`
* Open the solution (`RobotArmHelix.sln`) in Visual Studio
* Run the `RobotArmHelix` project:
  * In Solution Explorer, right-click the `RobotArmHelix` project and select *"Set as startup project"*
  * Press `Ctrl+F5` or click the run button (empty arrow) or select from the menu Debug / Start Without Debugging

When you run the program for the first time, this will trigger cloning of the Helix Toolkit repository (if not cloned yet), which may take quite some time (half a minute or more) because of the large size of the repository. Subsequent runs will be practically instantaneous, and after modifying the code, build and run will take a couple of seconds.

### Use with Container Repository that Takes Care of Cloning

It is possible to use a Container repository that simplifies cloning of the current repository and of the Helix Toolkit (for dependencies), and also takes care of addressing the compatible repositoriy forks and checking out the compatible branch of the Helix Toolkit repository. Here are the steps:

* Clone the [iglibmodules container repository](https://github.com/ajgorhoe/iglibmodules) and checkout the branch `swrepos/GrLib/repoMain`
* Run the PowerShell script `GrLibUpdateRepoGroup_RobotArmHelix.ps1` that will clone individual components checked out at appropriate references (branches). Alternatively, you can run individual cloning scripts:

  * `GrLibUpdateRepo_HelixToolkitForRobotArm.ps1` - clones the `Helix Toolkit` repository used by RobotArmHelix and checks out the appropriate branch
  * `GrLibUpdateRepo_RobotArmHelix.ps1` - clones the RoboArmHelix project and checks out the appropriate customization branch
* Open the `RobotArmHelx` solution in Visual Studio, build it, and run the application. The solution is located at `RobotArmHelix\RobotArmHelix.sln`

### Manually Clone the Correct (Updated and Compatible) Repositories and Checking out Correct Branches

If you want to do cloning **manually**, make sure to clone both repositories in the same directory, side by side:

* Clone the Helix Toolkit [from this fork](https://github.com/ajgorhoe/helix-toolkit) in the `helix-toolkit-forRobotArm` directory and switch to the `00IGLib/25_12_03_CustomizingOldCommitForRobotArm_7049fa` branch
* Clone the RobotArmHelix project [from this fork](https://github.com/ajgorhoe/RobotArmHelix) (or [from the original repo](https://github.com/Gabryxx7/RobotArmHelix), after changes are pulled in) in the `RobotArmHelix` directory and switch to the `swrepos/GrLib/repoMain` branch (or to the `master`branch, after changes are merged).
