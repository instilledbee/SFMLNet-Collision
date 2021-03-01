using SFML.Graphics;
using SFML.System;

namespace InstilledBee.SFML.SimpleCollision
{
    /// <summary>
    /// Provides methods for testing collisions between Sprites
    /// </summary>
    public static class CollisionTester
    {
        private static BitmaskManager _bitmasks = new BitmaskManager();

        /// <summary>
        /// Add a Texture's bitmap data into the CollisionTester's BitmaskManager
        /// </summary>
        /// <param name="tex"></param>
        public static void AddBitMask(Texture tex)
        {
            _bitmasks.Create(tex);
        }

        /// <summary>
        /// Check if 2 Sprites are colliding based on pixel perfect testing
        /// </summary>
        /// <param name="firstObj">The first Sprite object to compare</param>
        /// <param name="secondObj">The second Sprite object to compare</param>
        /// <param name="alphaLimit">Determines how opaque a pixel needs to be to count as hit</param>
        /// <returns>True if there is a hit between the two Sprites' pixels</returns>
        public static bool PixelPerfectTest(Sprite firstObj, Sprite secondObj, uint alphaLimit)
        {
            FloatRect intersection;
            IntRect firstSubRect, secondSubRect;

            if (firstObj.GetGlobalBounds().Intersects(secondObj.GetGlobalBounds(), out intersection))
            {
                firstSubRect = firstObj.TextureRect;
                secondSubRect = secondObj.TextureRect;

                for (int i = (int)intersection.Left; i < intersection.Left + intersection.Width; ++i)
                {
                    for (int j = (int)intersection.Top; j < intersection.Top + intersection.Height; ++j)
                    {
                        Vector2f firstVector = firstObj.InverseTransform.TransformPoint(i, j);
                        Vector2f secondVector = secondObj.InverseTransform.TransformPoint(i, j);

                        if(firstVector.X > 0 && firstVector.Y > 0
                           && secondVector.X > 0 && secondVector.Y > 0
                           && firstVector.X < firstSubRect.Width && firstVector.Y < firstSubRect.Height
                           && secondVector.X < secondSubRect.Width && secondVector.Y < secondSubRect.Height)
                        {
                            if(_bitmasks.GetPixel(firstObj.Texture, (uint)(firstVector.X + firstSubRect.Left), (uint)(firstVector.Y + firstSubRect.Top)) > alphaLimit
                                && _bitmasks.GetPixel(secondObj.Texture, (uint)(secondVector.X + secondSubRect.Left), (uint)(secondVector.Y + secondSubRect.Top)) > alphaLimit)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check if 2 Sprites are colliding based on their radii
        /// </summary>
        /// <param name="firstObj">The first Sprite object to compare</param>
        /// <param name="secondObj">The second Sprite object to compare</param>
        /// <returns>True if the Sprites' distance is lesser than the sum of their radii</returns>
        public static bool CircleTest(Sprite firstObj, Sprite secondObj)
        {
            Vector2f firstSize = GetSpriteSize(firstObj);
            Vector2f secondSize = GetSpriteSize(secondObj);

            float firstRadius = (firstSize.X + firstSize.Y) / 4;
            float secondRadius = (secondSize.X + secondSize.Y) / 4;

            Vector2f distance = GetSpriteCenter(firstObj) - GetSpriteCenter(secondObj);

            return (distance.X * distance.X + distance.Y * distance.Y) <= ((firstRadius + secondRadius) * (firstRadius + secondRadius));
        }

        /// <summary>
        /// Check if 2 Sprites are colliding based on the Separating Axis Theorem
        /// </summary>
        /// <param name="firstObj">The first Sprite object to compare</param>
        /// <param name="secondObj">The second Sprite object to compare</param>
        /// <returns>True if the Sprites' projections overlap</returns>
        public static bool BoundingBoxTest(Sprite firstObj, Sprite secondObj)
        {
            OrientedBoundingBox firstObb = new OrientedBoundingBox(firstObj);
            OrientedBoundingBox secondObb = new OrientedBoundingBox(secondObj);

            Vector2f[] axes = new Vector2f[4] {
                                    new Vector2f(firstObb.Points[1].X - firstObb.Points[0].X, firstObb.Points[1].Y - firstObb.Points[0].Y),
                                    new Vector2f(firstObb.Points[1].X - firstObb.Points[2].X, firstObb.Points[1].Y - firstObb.Points[2].Y),
                                    new Vector2f(secondObb.Points[0].X - secondObb.Points[3].X, secondObb.Points[0].Y - secondObb.Points[3].Y),
                                    new Vector2f(secondObb.Points[0].X - firstObb.Points[1].X, firstObb.Points[0].Y - firstObb.Points[1].Y)
                               };

            for (int i = 0; i < 4; ++i)
            {
                float firstMinObb, firstMaxObb, secondMinObb, secondMaxObb;

                firstObb.ProjectOntoAxis(axes[i], out firstMinObb, out firstMaxObb);
                secondObb.ProjectOntoAxis(axes[i], out secondMinObb, out secondMaxObb);

                if (!((secondMinObb <= firstMaxObb) && (secondMaxObb >= firstMinObb))) return false;
            }

            return true;
        }

        public static Vector2f GetSpriteCenter(Sprite obj)
        {
            FloatRect aabb = obj.GetGlobalBounds();
            return new Vector2f(aabb.Left + (aabb.Width / 2), aabb.Top + (aabb.Height / 2));
        }

        public static Vector2f GetSpriteSize(Sprite obj)
        {
            IntRect originalSize = obj.TextureRect;
            Vector2f scale = obj.Scale;

            return new Vector2f(originalSize.Width * scale.X, originalSize.Height * scale.Y);
        }
    }
}
