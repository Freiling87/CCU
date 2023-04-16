REM    This script copies your project's outputs into the game folders. Then it runs the game executable.
ECHO "======= POST-BUILD"

ECHO "    --- Defining directories"
SET destinationBepinex=C:\Program Files (x86)\Steam\steamapps\common\Streets of Rogue\BepInEx\
SET destinationCore="%destinationBepinex%core\"
SET destinationPatchers="%destinationBepinex%patchers\"
SET destinationPlugins="%destinationBepinex%plugins\"
SET originPlugins=%cd%\

ECHO "    --- Defining series"
SET exportsPlugins=CCU.dll

ECHO "    --- Copying files"
FOR %%a IN (%exportsPlugins%) DO (XCOPY /s /y "%originPlugins%%%~a" %destinationPlugins%)

ECHO "    --- Running game"
SET executable=C:\Program Files (x86)\Steam\steamapps\common\Streets of Rogue\StreetsOfRogue.exe
START "" "%executable%"

ECHO "    --- Snapping windows into place"
SET console="BepInEx 5.4.21.0 - Streets of Rogue"
SET game="Streets of Rogue"

PING 127.0.0.1 -n 4 >nul

nircmd.exe win activate stitle %console%
nircmd.exe win setsize stitle %console% -1680 0 1680 1050
nircmd.exe win max stitle %console%

REM nircmd.exe win activate stitle %game%
REM nircmd.exe win setsize stitle %game% 1680 0 1680 1050
REM nircmd.exe win max stitle %game%