# jkversions
Uses the official LucasArts Star Wars: Dark Forces 2: Jedi Knight 1.0.1 patch to generate three different versions of the main JK.EXE binary suitable for using mods (or bypassing Steam DRM).

This program does the following:

- Downloads the JK 1.0.1 patch (from jkhub.net)
- Extracts 7za.exe (and license) from itself.
- Extracts the full JK.EXE included in the path.
- Downloads two patches from jkhub.net to patch the file to 1.0 and a different patch which applies a community-made changes to the game.
- Applies the patches using the tool included in the patch downloads.
- Verifies resources were patched correctly using SHA1 hashes.
- Backs up your original Steam executable (if you have the Steam version of Jedi Knight).
- Installs the files into your Jedi Knight directory (into patches\, which is where my other Jedi Knight tool Knight can find them to use them).

This project is a Visual Studio 2017 project. It may work with earlier versions but you may have to recreate the project files.

New for version 2.0:

- Recoded in C#!
- .NET Framework version dependancy bumped to 4.5.2. (It should compile in older versions though.)
- Now finds your Jedi Knight directory for you and installs the files there! (If you have the Steam version you need to run it once to set it up first.)