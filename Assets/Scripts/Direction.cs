using System;

namespace BlackBoxPlus
{
    public enum Direction
    {
        X_POSITIVE,
        X_NEGATIVE,
        Y_POSITIVE,
        Y_NEGATIVE,
        Z_POSITIVE,
        Z_NEGATIVE,
    }

    public class DirectionExtensions
    {
        public static VectorInt3 GetVectorDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.X_POSITIVE:
                    return new VectorInt3(1, 0, 0);
                case Direction.X_NEGATIVE:
                    return new VectorInt3(-1, 0, 0);
                case Direction.Y_POSITIVE:
                    return new VectorInt3(0, 1, 0);
                case Direction.Y_NEGATIVE:
                    return new VectorInt3(0, -1, 0);
                case Direction.Z_POSITIVE:
                    return new VectorInt3(0, 0, 1);
                case Direction.Z_NEGATIVE:
                    return new VectorInt3(0, 0, -1);

                default:
                    throw new ArgumentException("Invalid direction enum: " + direction);
            }

        }

        public static Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.X_POSITIVE:
                    return Direction.X_NEGATIVE;
                case Direction.X_NEGATIVE:
                    return Direction.X_POSITIVE;
                case Direction.Y_POSITIVE:
                    return Direction.Y_NEGATIVE;
                case Direction.Y_NEGATIVE:
                    return Direction.Y_POSITIVE;
                case Direction.Z_POSITIVE:
                    return Direction.Z_NEGATIVE;
                case Direction.Z_NEGATIVE:
                    return Direction.Z_POSITIVE;

                default:
                    throw new ArgumentException("Invalid direction enum: " + direction);
            }
        }

        public static Direction GetEnumDirection(VectorInt3 direction)
        {
            if (direction == VectorInt3.RIGHT)
                return Direction.X_POSITIVE;
            if (direction == VectorInt3.LEFT)
                return Direction.X_NEGATIVE;
            if (direction == VectorInt3.UP)
                return Direction.Y_POSITIVE;
            if (direction == VectorInt3.DOWN)
                return Direction.Y_NEGATIVE;
            if (direction == VectorInt3.FORWARD)
                return Direction.Z_POSITIVE;
            if (direction == VectorInt3.BACKWARD)
                return Direction.Z_NEGATIVE;

            throw new ArgumentException("Invalid direction vector: " + direction.ToString());
            
        }
    }

}
