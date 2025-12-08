<#
    .SYNOPSIS
    Updates or clones a specific repository by calling UpdateOrCloneRepository.ps1 
    with parameters set through global variables.

    .DESCRIPTION
    This script defines a set of variables named with the 'CurrentRepo_' prefix in 
    the same order as UpdateOrCloneRepository.ps1 parameters. Then, it invokes 
    UpdateOrCloneRepository.ps1 *without* passing parameters, relying on 
    -DefaultFromVars to pick up the values from these global variables.
	
	When repository directory is specified as relative path, path is
	resolved with the scrit directory as base path.

    .NOTES
    Make sure UpdateOrCloneRepository.ps1 is accessible at the path specified in 
    $UpdatingScriptPath (absolute or relative).
#>

Write-Host "`n`n======================================================="
Write-Host "Updating/cloning a specific repository..."

########################################################################
# Custom section (USER DEFINED):

# Path to UpdateOrCloneRepository.ps1
$UpdatingScriptPath = "./UpdateOrCloneRepository.ps1"

# Define parameter variables for UpdateOrCloneRepository.ps1
#    in the same order as that script's parameters:

$global:CurrentRepo_Directory = "../../helix-toolkit-forRobotArm/"
$global:CurrentRepo_Ref = version_3.1.2

# Commit that works with the adapted original version of RobotArmHelix:
# "00IGLib/25_12_03_CustomizingOldCommitForRobotArm_7049fa"

# Commit 7049faf49a52a5455c4dbfd0fd0bc2d08db09b11: August 18, 2017.

# "a2d5a244112a392b7570c8be9eaf1245f5112fc9"
# commit a2d5a244112a392b7570c8be9eaf1245f5112fc9:
# Author: Lunci <holance@users.noreply.github.com> Date: 8/7/2017 3:20:10 AM
# Message: Merge pull request #528 from holance/develop

$global:CurrentRepo_Address = "https://github.com/ajgorhoe/helix-toolkit"
$global:CurrentRepo_Remote = "origin"
$global:CurrentRepo_AddressSecondary = "https://github.com/helix-toolkit/helix-toolkit"
$global:CurrentRepo_RemoteSecondary = "remoteUpstream"
$global:CurrentRepo_AddressTertiary = 
$global:CurrentRepo_RemoteTertiary = $null
$global:CurrentRepo_ThrowOnErrors = $false

# End of custom section
########################################################################

# $global:CurrentRepo_DefaultFromVars = $true # params set from variables above
$global:CurrentRepo_BaseDirectory = $null   # base dir will be set to script dir 

# Set CurrentRepo_BaseDirectory to the directory containing this script:
$scriptPath = $MyInvocation.MyCommand.Path
$scriptDir = Split-Path $scriptPath -Parent
$scriptFilename = [System.IO.Path]::GetFileName($scriptPath)

# Set base directory for relative paths to the current script's directory:
$global:CurrentRepo_BaseDirectory = $scriptDir

# If $UpdatingScriptPath is a relative path, convert it to absolute
if (-not [System.IO.Path]::IsPathRooted($UpdatingScriptPath)) {
    $UpdatingScriptPath = Join-Path $scriptDir $UpdatingScriptPath
}

# Write-Host "`n${scriptFilename}:"
# Write-Host "  CurrentRepo_Directory: $CurrentRepo_Directory"
# Write-Host "  CurrentRepo_Address: $CurrentRepo_Address"
# Write-Host "  CurrentRepo_Ref: $CurrentRepo_Ref"
# # Write-Host "  UpdatingScriptPath: $UpdatingScriptPath"
# # Write-Host "  CurrentRepo_BaseDirectory: $CurrentRepo_BaseDirectory `n"

# Print all variables used as settings for updating / cloning repositories:
Write-Host "-------------------------------------------------------"
Write-Host "Variables for repository updating / cloning scripts:"
Write-Host "  CurrentRepo_Directory: $CurrentRepo_Directory"
Write-Host "  CurrentRepo_Ref:       $CurrentRepo_Ref"
Write-Host "  CurrentRepo_Address:   $CurrentRepo_Address"
Write-Host "  CurrentRepo_Remote:    $CurrentRepo_Remote"
Write-Host "  CurrentRepo_AddressSecondary: $CurrentRepo_AddressSecondary"
Write-Host "  CurrentRepo_RemoteSecondary:  $CurrentRepo_RemoteSecondary"
Write-Host "  CurrentRepo_AddressTertiary:  $CurrentRepo_AddressTertiary"
Write-Host "  CurrentRepo_RemoteTertiary:   $CurrentRepo_RemoteTertiary"
Write-Host "  CurrentRepo_ThrowOnErrors:    $CurrentRepo_ThrowOnErrors"
#
Write-Host "  CurrentRepo_DefaultFromVars:  $CurrentRepo_DefaultFromVars"
Write-Host "  CurrentRepo_BaseDirectory   : $CurrentRepo_BaseDirectory"
#
Write-Host "---------------------------------------------------------"

# # Uncomment the line below only when the print script exists!
# & (Join-Path $scriptDir PrintSettingsUpdateOrClone.ps1)

# Invoke UpdateOrCloneRepository.ps1 with no parameters, 
#    so it uses the global variables defined above:
Write-Host "`nCalling update script without parameters; it will use global variables..."
& $UpdatingScriptPath -Execute -DefaultFromVars

Write-Host "`nUpdating or cloning the repository completed."
Write-Host "---------------------------------------------------------`n`n"
