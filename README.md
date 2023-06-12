# Github Repository Template for Valheim modding

Create your own mod using this template as a starting point for your mod.

## Features

* You will need to rename the solution and .csproj, same as namespace and classes and files to your own names
* Has debug build feature that puts the dll into BepInEx installed inside Valheim
* Has run the game feature for running your debug version locally
* Has release build feature to package everything ready to upload to ThunderStore as zip file in your local user home
  Downloads folder
  * Prepared contents inside ThunderStorePackage folder
  * replace the icon.png with any other 256x256 pixel png file
  * edit the manifest.json file
  * remove or replace the empty files inside config and plugins folders
  * it will put the README.md (this content) and CHANGELOG.md from root folder also into the zip

# Credits

* I did follow some hints by [AzumattDev](https://github.com/AzumattDev) from his
  [YouTube session on how to create a template](https://www.youtube.com/watch?v=gSL31r2AgrI).
* Having had a look at some people's setups for .csproj
