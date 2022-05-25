# CallOfFile

Call of File is a .NET library for reading and writing Call of Duty's XMODEL_EXPORT/XMODEL_BIN/XANIM_EXPORT/XANIM_BIN files.

Call of File currently is only tested on files from Call of Duty 4 and higher.

# Using Call of Duty

Using Call of File is very simple for .NET developers, simply pull the repo:

`git clone --recursive https://github.com/Scobalula/CallOfFile`

Run a restore in NUGET to grab the latest version of K4os.Compression.LZ4 and include in your pojects and you should be good to go.

# Documentation

Documentation is currently in progress, but for now I have included the WIP Unit Test project and an example CLI app that converts files to and from binary and ascii counterparts.

# Bug Reporting

A lot of work and testing has gone into Call of File, but if you find a bug, feel free to open an issue with as much information as possible including files used and any other information.

# Contributing

Contributions are absolutely welcome, if you'd like to contribute code, then you can fork the repo and make edits, then open a PR, don't worry if you think you're code isn't perfect or well documented, as we can make any edits we feel are necessary to bring it up to scratch!

When contributing, ensure to keep your contributions compatible with the MIT license, this includes any libraries used. Also make sure to keep track of credits for any code you use!

# License/Disclaimers

Call of File is based off research done on files provided in Call of Duty: Black Ops III Mod Tools and research done on export2bin itself to gain interoperability with files stored within the Call of Duty: Black Ops III Mod Tools. It is provided AS IS with no warranty.

Call of File is licensed under the MIT License. See the LICENSE file for more information.

# Credits

* Milosz Krajewski - [LZ4 Compression](https://github.com/MiloszKrajewski/K4os.Compression.LZ4)