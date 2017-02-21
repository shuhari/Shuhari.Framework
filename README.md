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

**v0.1.4.0 (2017-02-21)**
* NEW: StreamDecorator expose InnerStream
* NEW: AuthenticationResultDTO
* REFACTOR: UserManager.Signin return user info
* REFACTOR: BaseDbContext renamed to FrameworkDbContext

**v0.1.3.0 (2017-02-14)**
* RENAMED: IRepository.ListAll renamed to GetAll
* NEW: IRepository.GetBy
* NEW: BaseDbContext.CreateSessionScope
* REMOVED: FrameworkController.ExecuteAjax
* NEW: FrameworkController.ExecuteJsonResult

**v0.1.2.0 (2017-02-10)**
* REMOVED: IDbEngine.ExecuteCommand
* REMOVED: TextReplacer
* NEW: DbSriptExecuteOptions
* NEW: CommandLine
* FrameworkDatabase use lazy creation of sessionFactory

**v0.1.1.1 (2017-02-09)**
* Add -v parameter to DbManagementCommandOptions
* Add Find/FindIndex to CollectionUtil
* CollectionUTil.FindBy/FindByName marked as obsolute

**v0.1.1.0 (2017-02-09)**
* Add -v parameter to DbManagementCommandOptions
* Add Find/FindIndex to CollectionUtil
* CollectionUTil.FindBy/FindByName marked as obsolute

**v0.1.0.15 (2017-01-22)**
* Refactor IQuery<T> parameters

**v0.1.0.14 (2017-01-22)**
* FIXED: IQuery<T>.Set unified

For full list of release history view [History](https://github.com/shuhari/Shuhari.Framework/blob/master/HISTORY.md)