# Editor
Simple RTF Editor written in C#

![RTFEditor Jpeg](/img/RTFEditor.jpg)

![RTFEditor Jpeg](/img/FileMenu.png)

![RTFEditor Jpeg](/img/EditMenu.png)

![RTFEditor Jpeg](/img/SettingsMenu.png)


## Getting Started
I used Visual Studio Community 2017 and .NET Framework 4 to build this app. Font, fontDialog.Font, Window Position, and Window Size are saved on exit to %APPDATA%\RTFEditor\settings.xml.  These settings are loaded on the start of the application as well.

You can cut, copy, and paste as well as insert images into your documents.  Font color is not selective but when set changes the color of the entire document.  This is because font color is set through the ForeColor property.

### Prerequisites
```
.NET Framework 5 and C# 9
```
### Installing
```
Run Setup.exe and then launch the app from the shortcut on your desktop
```
## Built With
[VS Enterprise 2019](https://visualstudio.microsoft.com/downloads/) - The build software
## Versioning
We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/norton287/Editor/tags).

## Authors
**John Norton** - *Initial work* - [norton287](https://github.com/norton287)
See also the list of [contributors](https://github.com/norton287/Editor/contributors) who participated in this project.
## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
## Acknowledgments
* C# for being an easy to learn language.
* Inspired by my need to learn to code in c#
