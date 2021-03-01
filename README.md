# SFMLNet-Collision
Collision detection classes for SFML.Net. Tested with SFML.NET v2.1.5 and v2.5

# About
These classes are a direct C# port of the "Simple Collision Detection" source code found in the [SFML wiki](https://github.com/SFML/SFML/wiki/Source:-Simple-Collision-Detection-for-SFML-2). The code is a translation of the existing C++ code, with the intent of making it easy to integrate with SFML.NET projects, and has been made available as a repository, so others may improve on the code and provide feedback.

# Adding the library to your code
The recommended installation is [via Nuget](nuget.org/packages/SFML.SimpleCollision/). The package name is `SFML.SimpleCollision`.

You may also directly drop in the 3 class files in the solution to your SFML.NET project and use them as is.

# Usage
`CollisionTester` is the static class that contains the collision testing methods supported (`CircleTest()`, `BoundingBoxTest()` and `PixelPerfectTest()`). To use the pixel perfect collision test, make sure to add the texture's bitmask to the `CollisionTester` via `AddBitMask()`.
