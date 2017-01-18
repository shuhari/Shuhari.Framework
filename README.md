Shuhari's framework library for .Net
========================


According to many C# projects written before, I found that some features are used again and again. So I decided to extract these features to a common library which can be reused by multiple project/solutions.

![Project Status](https://ci.appveyor.com/api/projects/status/eg27hex6c0bl7lrs?svg=true)

Basically, This library contains following components:

__Shuhari.Framework.Common__  Basic language extension and commonly used utility routines, shared by all other components. Features include:
* Verify parameter, environment, and so on
* Utility functions and extension methods for string, collection, assembly, stream, exception, etc
* Helper methods to simplify object comparasion and equality check
* Compression / Decompression helper
* A light-weight ORM implementation (only support Sql server at this time, but architectural provider-independent)
* Text templating based on [NVelocity](http://nvelocity.codeplex.com/)

__Shuhari.Framework.Web.Mvc__  Extensions and controls for ASP.NET MVC programming. Features include:
* Dependency injection based on Ninject
* Open-session-in-view style database connection management
* Globalization support
* Extensions for controller and view

__Shuhari.Framework.Testing.NUnit__ Extension methods to support NUnit testing.


The prebuilt assemblies are published to offical nuget package repository, with same name as each assemblies, thus you can install using package manager:

    install-package Shuhari.Framework.Common [-vesion x.x.x]
    install-package Shuhari.Framework.Web.Mvc [-vesion x.x.x]
    install-package Shuhari.Framework.Testing.NUnit [-vesion x.x.x]

Or use nuget GUI in visual studio to search and install. The nuget build/publish scripts can be found in build.bat.


References:
* [Nuget page for Shuhari.Framework.Common](https://www.nuget.org/packages/Shuhari.Framework.Common/)
* [Nuget page for Shuhari.Framework.Web.Mvc](https://www.nuget.org/packages/Shuhari.Framework.Web.Mvc/)
* [Nuget page for Shuhari.Framework.Testing.NUnit](https://www.nuget.org/packages/Shuhari.Framework.Testing.NUnit/)


Release History:

**v0.1.0.8 (2017-01-18)**
* FIXED: GZip.ComparessFiles renamed to CompressFiles
* Add some commonly-used resource strings

**v0.1.0.3 (2017-01-16)**
Fix NamedQueryDTO.SetQuery as derived
Add IQuery<T>.Set(x => x)
Add EnumUtil.FromFlags / ToFlags

