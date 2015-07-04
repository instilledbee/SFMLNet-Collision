# SFMLNet-Collision
Collision detection classes for SFML.Net. Tested with SFML.NET 2.1

# About
These classes are a direct C# port of the "Simple Collision Detection" source code found in the [SFML wiki](https://github.com/SFML/SFML/wiki/Source:-Simple-Collision-Detection-for-SFML-2). The code is a translation of the existing C++ code, with the intent of making it easy to integrate with SFML.NET projects, and has been made available as a repository, so others may improve on the code and provide feedback.

# Usage
`CollisionTester` is the static class that contains the collision testing methods, so you may simply drop it and the other classes (`OrientedBoundingBox` and `BitmaskManager`) into your existing SFML.NET project and use them as is.

It is recommended that you integrate it into your existing code, if you derive from `SFML.Graphics.Sprite` and/or use an object management system in your application.
