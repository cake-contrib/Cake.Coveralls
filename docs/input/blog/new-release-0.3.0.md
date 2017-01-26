---
Title: New Release - 0.3.0
Published: 26/1/2017
Category: Release
Author: gep13
---

# It's been a while...

Since the last release, but I hope you will agree that it has been worth waiting for.  This release includes the following:

## Improvements

- [__#10__](https://github.com/cake-contrib/Cake.Coveralls/issues/10) Switch away from using gep13.DefaultBuild and use Cake.Recipe instead
- [__#7__](https://github.com/cake-contrib/Cake.Coveralls/issues/7) Update nuspec with correct information

## Documentation

- [__#17__](https://github.com/cake-contrib/Cake.Coveralls/issues/17) Add Wyam Documentation generation
- [__#6__](https://github.com/cake-contrib/Cake.Coveralls/issues/6) Correct usage documentation

The main one here is the inclusion of [Cake.Recipe](https://github.com/cake-contrib/Cake.Recipe).  For a while now, I have been working on the concept of a convention driven, drop in, build system, that can be applied to any number of projects.  This is very much nearing completion, with all the main parts now in place.

In this most recent version, I have added:

- Execution of GitLink to allow debugging of assemblies generated as part of the build
- Wyam documentation generation, including this blog post that you are reading now.
- Removal of some dependencies in the build process which meant additional nuget packages were required

More details will follow on all of this when the Cake.Recipe project is officially released.  This release of Cake.Coveralls is really just taking it for it's initial spin.

Please do not hesitate to reach out in the [Gitter Channel](https://gitter.im/cake-contrib/Lobby) if you have any issues using this addin.