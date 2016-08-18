# Cake.Coveralls Documentation

Cake.Coveralls is an Addin for [Cake](http://cakebuild.net/) which allows the posting of code coverage reports to [Coveralls.io](https://coveralls.io/).

In order to use the commands for this addin, you will need to include either of the following in your build.cake file:

```csharp
#tool coveralls.net
```

```csharp
#tools coveralls.io
```

This will instruct Cake to download the package from NuGet and install it into the Cake Tool's folder, and from there, the executable will be available to your script.

In addition, you will need to include the following:

```csharp
#addin Cake.Coveralls
```

This will instruct Cake to download the [Cake.Coveralls](https://www.nuget.org/packages/Cake.Coveralls/) package from NuGet and install it into the Cake Addin's folder, and from there, load the associated assembly in the execution context for consumption within your script.
