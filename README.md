# SOTT Battlebit voxel fortify magicavoxel terrain and extra's bepinex plugin 

![Ingame terrain](https://github.com/SwatDoge/SOTTBBVFMVTAEBP/blob/master/readme/magicavoxelTerrain.png?raw=true)
![Ingame terrain](https://github.com/SwatDoge/SOTTBBVFMVTAEBP/blob/master/readme/ingameTerrain.png?raw=true)

## This project is mostly intended for code scraping/learning, I do not plan on actively maintaining this.
Special thanks to: Attack Helicopter 4000 & Muj for getting me set up and helping me with deobfuscation. (And generally being awesome people.)\
I also wanna thank OkiDoki, Lorenzo and Julgers for responding to questions and deobfuscating important parts of our game, speeding up our development efforts.\
Also ty for prof & star for creating and testing our voxel maps.

## What is this?
This is a bepinex plugin for battlebits servers. I did a lot of digging into the VoxelFortify gamemode in specific, getting a pretty clear understanding of how the gamemode works. That said, the codebase is every changing and the mono build of battlebits its getting older ever day. I've shelved this project because I got burned out on reversing il2cpp code.

## What can this plugin do?
Right now this project does a few things:
- This project hooks pre generated voxel structures, and loads magica voxel created terains instead.
- This project hooks block placement, allowing it to assign buildHP seperately from maxHP. (You can place blocks with different HP and block data)
- This project hooks block repairs, and offers basic "block upgrading".
- This project hooks RPC packets, allowing you to inspect and modify them.
- This project hooks anti-cheat, so you can use unity inspector on the client.
- This project offers a few chat commands:
    - `voxel place <x> <y> <z>`
    - `voxel destroy <x> <y> <z>`
    - `getpos <steamId of target>`
    - `buildHP <set HP of blocks when placed>`
    - `clear`

## Other resources and discoveries
- [My list of il2cpp code deobfuscations](https://docs.google.com/spreadsheets/d/19AFtVbYBXFsKqKwyGkTO7b2kFNa4_ScB7-tH4vGhlBc/edit?usp=sharing)
- Steps to download Battlebits' unobfuscated mono build (latest: 2706145631362632842):
    - go to steam://nav/console in your browser, this should open console in steam.
    - run download_depot 671860 671861 2706145631362632842 in your console to download the build.
- Packet layout of blocks (will change over time, but still useful)\
![Ingame terrain](https://github.com/SwatDoge/SOTTBBVFMVTAEBP/blob/master/readme/blockPackets.png?raw=true)