using SFML.Graphics;
using System.Collections.Generic;

namespace Collision
{
    /// <summary>
    /// Stores bitmask data for use with pixel perfect testing
    /// </summary>
    internal class BitmaskManager
    {
        private Dictionary<Texture, uint[]> _bitmasks = new Dictionary<Texture,uint[]>();

        /// <summary>
        /// Get pixel bitmask data of a given Texture within the specified coordinates
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public uint GetPixel(Texture tex, uint x, uint y)
        {
            if (x > tex.Size.X || y > tex.Size.Y)
            {
                return 0;
            }
            else
            {
                return Get(tex)[x + y * tex.Size.X];
            }
        }

        /// <summary>
        /// Get bitmask data of a given Texture. Creates the bitmask data if it doesn't exist yet.
        /// </summary>
        /// <param name="tex"></param>
        /// <returns></returns>
        public uint[] Get(Texture tex)
        {
            uint[] mask;

            if (!_bitmasks.TryGetValue(tex, out mask))
            {
                mask = Create(tex);
            }

            return mask;
        }

        /// <summary>
        /// Create bitmask data for a given Texture, and adds it to the BitmaskManager's internal cache
        /// </summary>
        /// <param name="tex"></param>
        /// <returns>The created bitmask data</returns>
        public uint[] Create(Texture tex)
        {
            Image img = tex.CopyToImage();
            uint[] mask = new uint[tex.Size.Y * tex.Size.X];

            for (uint y = 0; y < tex.Size.Y; ++y)
            {
                for (uint x = 0; x < tex.Size.X; ++x)
                {
                    mask[x + y * tex.Size.X] = img.GetPixel(x, y).A;
                }
            }

            _bitmasks.Add(tex, mask);

            return mask;
        }
    }
}
