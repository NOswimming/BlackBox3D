using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BlackBoxPlus
{
    public class BlackBox
    {
        int[,,] grid;
        List<Ray> rays;
        public BlackBox(VectorInt3 size)
        {
            grid = new int[size.x, size.y, size.z];
            rays = new List<Ray>();
        }

        public void AddAtom(VectorInt3 position)
        {
            grid[position.x, position.y, position.z] = 1;
        }

        public void RemoveAtom(VectorInt3 position)
        {
            grid[position.x, position.y, position.z] = 0;
        }

        public Ray FireRay(Ray ray)
        {
            throw new NotImplementedException();

            while (true)
            {
                // Follow the ray through the grid
                var immediateGrid = GetImmediateGridForRay(ray);
                var nextRay = GetNextRay(ray, immediateGrid);

                /// If out of bounds then return

            }


        }

        private object GetNextRay(Ray ray, int[,] immediateGrid)
        {
            // Absorb
            if (immediateGrid[1, 1] == 1)
                return null;

            // Reflect
            if (immediateGrid[0, 1] + immediateGrid[1, 0] + immediateGrid[1, 2] + immediateGrid[2, 1] >= 2)
                return new Ray(ray.position, ray.direction * -1);

            // Refract
            if (immediateGrid[0, 1] + immediateGrid[1, 0] + immediateGrid[1, 2] + immediateGrid[2, 1] == 1)
            {
                // Refract right
                if (immediateGrid[0, 1] == 1)
                    return null; //new Ray(ray.position, GetRelative)

            }

            // Pass straight through
            return new Ray(ray.position + ray.direction, ray.direction);


        }

        public int[,] GetImmediateGridForRay(Ray ray)
        {
            var target = ray;
            var slice = new int[3, 3];

            // X Direction
            if (ray.directionEnum == Direction.X_POSITIVE)
            {
                /* Relative positions
                 * 2 | [( 0, 1, 1),( 0, 1, 0),( 0, 1,-1)]
                 * 1 | [( 0, 0, 1),( 0, 0, 0),( 0, 0,-1)]
                 * 0 | [( 0,-1, 1),( 0,-1, 0),( 0,-1,-1)]
                 * Y      ________   ________   ________
                 *     Z     0          1          2
                 */
                // Top row
                slice[0, 2] = grid[ray.target.x, ray.target.y + 1, ray.target.z + 1];
                slice[1, 2] = grid[ray.target.x, ray.target.y + 1, ray.target.z];
                slice[2, 2] = grid[ray.target.x, ray.target.y + 1, ray.target.z - 1];
                // Middle row
                slice[0, 1] = grid[ray.target.x, ray.target.y, ray.target.z + 1];
                slice[1, 1] = grid[ray.target.x, ray.target.y, ray.target.z];
                slice[2, 1] = grid[ray.target.x, ray.target.y, ray.target.z - 1];
                // Bottom row
                slice[0, 0] = grid[ray.target.x, ray.target.y - 1, ray.target.z + 1];
                slice[1, 0] = grid[ray.target.x, ray.target.y - 1, ray.target.z];
                slice[2, 0] = grid[ray.target.x, ray.target.y - 1, ray.target.z - 1];
            }
            if (ray.directionEnum == Direction.X_NEGATIVE)
            {
                /* Relative positions
                 * 2 | [( 0, 1,-1),( 0, 1, 0),( 0, 1, 1)]
                 * 1 | [( 0, 0,-1),( 0, 0, 0),( 0, 0, 1)]
                 * 0 | [( 0,-1,-1),( 0,-1, 0),( 0,-1, 1)]
                 * Y      ________   ________   ________
                 *     Z     0          1          2
                 */
                // Top row
                slice[0, 2] = grid[ray.target.x, ray.target.y + 1, ray.target.z - 1];
                slice[1, 2] = grid[ray.target.x, ray.target.y + 1, ray.target.z];
                slice[2, 2] = grid[ray.target.x, ray.target.y + 1, ray.target.z + 1];
                // Middle row
                slice[0, 1] = grid[ray.target.x, ray.target.y, ray.target.z - 1];
                slice[1, 1] = grid[ray.target.x, ray.target.y, ray.target.z];
                slice[2, 1] = grid[ray.target.x, ray.target.y, ray.target.z + 1];
                // Bottom row
                slice[0, 0] = grid[ray.target.x, ray.target.y - 1, ray.target.z - 1];
                slice[1, 0] = grid[ray.target.x, ray.target.y - 1, ray.target.z];
                slice[2, 0] = grid[ray.target.x, ray.target.y - 1, ray.target.z + 1];
            }

            // Y Direction
            if (ray.directionEnum == Direction.Y_POSITIVE)
            {
                /* Relative positions
                 * 2 | [( 1, 0,-1),( 1, 0, 0),( 1, 0, 1)]
                 * 1 | [( 0, 0,-1),( 0, 0, 0),( 0, 0, 1)]
                 * 0 | [(-1, 0,-1),(-1, 0, 0),(-1, 0, 1)]
                 * X      ________   ________   ________
                 *     Z     0          1          2
                 */
                // Top row
                slice[0, 2] = grid[ray.target.x + 1, ray.target.y, ray.target.z - 1];
                slice[1, 2] = grid[ray.target.x + 1, ray.target.y, ray.target.z];
                slice[2, 2] = grid[ray.target.x + 1, ray.target.y, ray.target.z + 1];
                // Middle row
                slice[0, 1] = grid[ray.target.x, ray.target.y, ray.target.z - 1];
                slice[1, 1] = grid[ray.target.x, ray.target.y, ray.target.z];
                slice[2, 1] = grid[ray.target.x, ray.target.y, ray.target.z + 1];
                // Bottom row
                slice[0, 0] = grid[ray.target.x - 1, ray.target.y, ray.target.z - 1];
                slice[1, 0] = grid[ray.target.x - 1, ray.target.y, ray.target.z];
                slice[2, 0] = grid[ray.target.x - 1, ray.target.y, ray.target.z + 1];
            }
            if (ray.directionEnum == Direction.Y_NEGATIVE)
            {
                /* Relative positions
                 * 2 | [( 1, 0, 1),( 1, 0, 0),( 1, 0,-1)]
                 * 1 | [( 0, 0, 1),( 0, 0, 0),( 0, 0,-1)]
                 * 0 | [(-1, 0, 1),(-1, 0, 0),(-1, 0,-1)]
                 * X      ________   ________   ________
                 *     Z     0          1          2
                 */
                // Top row
                slice[0, 2] = grid[ray.target.x + 1, ray.target.y, ray.target.z + 1];
                slice[1, 2] = grid[ray.target.x + 1, ray.target.y, ray.target.z];
                slice[2, 2] = grid[ray.target.x + 1, ray.target.y, ray.target.z - 1];
                // Middle row
                slice[0, 1] = grid[ray.target.x, ray.target.y, ray.target.z + 1];
                slice[1, 1] = grid[ray.target.x, ray.target.y, ray.target.z];
                slice[2, 1] = grid[ray.target.x, ray.target.y, ray.target.z - 1];
                // Bottom row
                slice[0, 0] = grid[ray.target.x - 1, ray.target.y, ray.target.z + 1];
                slice[1, 0] = grid[ray.target.x - 1, ray.target.y, ray.target.z];
                slice[2, 0] = grid[ray.target.x - 1, ray.target.y, ray.target.z - 1];
            }

            // Z Direction
            if (ray.directionEnum == Direction.Z_POSITIVE)
            {
                /* Relative positions
                 * 2 | [(-1, 1, 0),( 0, 1, 0),( 1, 1, 0)]
                 * 1 | [(-1, 0, 0),( 0, 0, 0),( 1, 0, 0)]
                 * 0 | [(-1,-1, 0),( 0,-1, 0),( 1,-1, 0)]
                 * Y      ________   ________   ________
                 *     X     0          1          2
                 */
                // Top row
                slice[0, 2] = grid[ray.target.x - 1, ray.target.y + 1, ray.target.z];
                slice[1, 2] = grid[ray.target.x, ray.target.y + 1, ray.target.z];
                slice[2, 2] = grid[ray.target.x + 1, ray.target.y + 1, ray.target.z];
                // Middle row
                slice[0, 1] = grid[ray.target.x - 1, ray.target.y, ray.target.z];
                slice[1, 1] = grid[ray.target.x, ray.target.y, ray.target.z];
                slice[2, 1] = grid[ray.target.x + 1, ray.target.y, ray.target.z];
                // Bottom row
                slice[0, 0] = grid[ray.target.x - 1, ray.target.y - 1, ray.target.z];
                slice[1, 0] = grid[ray.target.x, ray.target.y - 1, ray.target.z];
                slice[2, 0] = grid[ray.target.x + 1, ray.target.y - 1, ray.target.z];
            }
            if (ray.directionEnum == Direction.Z_NEGATIVE)
            {
                /* Relative positions
                 * 2 | [( 1, 1, 0),( 0, 1, 0),(-1, 1, 0)]
                 * 1 | [( 1, 0, 0),( 0, 0, 0),(-1, 0, 0)]
                 * 0 | [( 1,-1, 0),( 0,-1, 0),(-1,-1, 0)]
                 * Y      ________   ________   ________
                 *     X     0          1          2
                 */
                // Top row
                slice[0, 2] = grid[ray.target.x + 1, ray.target.y + 1, ray.target.z];
                slice[1, 2] = grid[ray.target.x, ray.target.y + 1, ray.target.z];
                slice[2, 2] = grid[ray.target.x - 1, ray.target.y + 1, ray.target.z];
                // Middle row
                slice[0, 1] = grid[ray.target.x + 1, ray.target.y, ray.target.z];
                slice[1, 1] = grid[ray.target.x, ray.target.y, ray.target.z];
                slice[2, 1] = grid[ray.target.x - 1, ray.target.y, ray.target.z];
                // Bottom row
                slice[0, 0] = grid[ray.target.x + 1, ray.target.y - 1, ray.target.z];
                slice[1, 0] = grid[ray.target.x, ray.target.y - 1, ray.target.z];
                slice[2, 0] = grid[ray.target.x - 1, ray.target.y - 1, ray.target.z];
            }

            return slice;
        }
    }
    

    public class Ray
    {
        public VectorInt3 position;
        public VectorInt3 target { get { return position + direction; } }
        public VectorInt3 direction;
        public Direction directionEnum { get { return DirectionExtensions.GetEnumDirection(direction); } }

        public Ray(VectorInt3 position, VectorInt3 direction)
        {
            this.position = position;
            this.direction = direction;
            
            
        }

        public static Ray CreateRayWithTarget(VectorInt3 target, Direction directionEnum)
        {
            var direction = DirectionExtensions.GetVectorDirection(directionEnum);
            var position = target - direction;
            return new Ray(position, direction);
        }

    }

}
