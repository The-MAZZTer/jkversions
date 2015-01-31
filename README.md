# jkversions
Uses the official LucasArts Star Wars: Dark Forces 2: Jedi Knight 1.0.1 patch to generate three different versions of the main JK.EXE binary suitable for using mods (or bypassing Steam DRM).

This program does the following (from my memory of it):

- Downloads the JK 1.0.1 patch (from my site, currently. Should probably throw it up on github or something).
- Extracts 7za.exe (and license) from itself.
- Extracts the JK.EXE from that patch (they do not use a patch, it is a full binary).
- Downloads two patches from jkhub.net to patch the file to 1.0 and a different patch which applies a community-made changes to the game.
- Downloads bspatch.exe from jkhub.net and applies the patches.
- Verifies resources were patched correctly using SHA1 hashes.
- Dumps the files into %TEMP% and opens the folder so you can do stuff with them.

The project files were made with Visual Studio 2013 Community Edition (which is free) so earlier versions may not be able to read them.

Pre-compiled binary download at http://www.jkhub.net/project/show.php?projid=511&section=downloads
