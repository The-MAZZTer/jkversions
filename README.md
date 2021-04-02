# jkversions
Uses the official LucasArts Star Wars: Dark Forces 2: Jedi Knight 1.0.1 patch to generate three different versions of the main JK.EXE binary suitable for using mods (or bypassing Steam DRM).

This program can do the following:

- Downloads the JK 1.0.1 patch (from jkhub.net or a custom location, or use a locally provided file).
- Extracts 7za.exe (and license) from itself.
- Extracts the full JK.EXE included in the patch.
- Downloads two patches (from jkhub.net or a custom location, or using locally provided files) to patch the file to 1.0 and a different patch which applies community-made changes to the game.
- Applies the patches using the tool included in the patch downloads.
- Verifies resources were patched correctly using file hashes.
- Backs up your original Steam executable (if you have the Steam version of Jedi Knight).
- Installs the files into your Jedi Knight directory (into patches\, which is where my other Jedi Knight tool Knight can find them to use them).

This project is a Visual Studio 2019 project, for .NET Framework 4.6.2 (.NET 5 WinForms is too unstable to develop with).

New for version 3.0:

- .NET Framework version dependency bumped to 4.6.2 for .NET Standard 2.0 compatibility.
- Added settings file for customizing download URLs and hashes
- Added form UI for customizing download URLs, which patches get applied, and where the resulting files get placed.

New for version 2.0:

- Recoded in C#!
- .NET Framework version dependency bumped to 4.5.2. (It should compile in older versions though.)
- Now finds your Jedi Knight directory for you and installs the files there! (If you have the Steam version you need to run it once to set it up first.)
