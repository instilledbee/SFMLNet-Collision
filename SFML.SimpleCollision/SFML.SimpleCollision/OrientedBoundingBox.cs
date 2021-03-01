using SFML.Graphics;
using SFML.System;

namespace InstilledBee.SFML.SimpleCollision
{
    /// <summary>
    /// A bounding box that can be projected onto an axis
    /// </summary>
    internal class OrientedBoundingBox
    {
        /// <summary>
        /// The corner points of the OrientedBoundingBox
        /// </summary>
        public Vector2f[] Points { get; private set; }

        public OrientedBoundingBox(Sprite obj)
        {
            Transform trans = obj.Transform;
            IntRect local = obj.TextureRect;

            Points = new Vector2f[4] {
                            trans.TransformPoint(0f, 0f),
                            trans.TransformPoint(local.Width, 0f),
                            trans.TransformPoint(local.Width, local.Height),
                            trans.TransformPoint(0f, local.Height)
                      };
        }

        /// <summary>
        /// Project the bounding box onto the specified axis coordinates
        /// </summary>
        /// <param name="axis">The coordinates of the axis to project to</param>
        /// <param name="min">The smallest projection value obtained</param>
        /// <param name="max">The largest projection value obtained</param>
        public void ProjectOntoAxis(Vector2f axis, out float min, out float max)
        {
            min = (Points[0].X * axis.X) + (Points[1].Y * axis.Y);
            max = min;

            for (int i = 1; i < 4; ++i)
            {
                float projection = (Points[i].X * axis.X) + (Points[i].Y * axis.Y);

                if (projection < min) min = projection;
                if (projection > max) max = projection;
            }
        }
    }
}
