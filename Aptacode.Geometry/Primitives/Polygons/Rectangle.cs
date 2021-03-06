﻿using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public record Rectangle : Polygon
    {
        #region Construction

        public Rectangle(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft) : base(
            VertexArray.Create(topLeft, topRight, bottomRight, bottomLeft)) { }

        protected Rectangle(VertexArray vertices, BoundingCircle? boundingCircle, BoundingRectangle? boundingRectangle)
            : base(vertices, boundingCircle, boundingRectangle) { }


        public Rectangle(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft,
            BoundingCircle? boundingCircle, BoundingRectangle? boundingRectangle) : base(
            VertexArray.Create(topLeft, topRight, bottomRight, bottomLeft), boundingCircle, boundingRectangle) { }

        public static readonly Rectangle Zero = Create(Vector2.Zero, Vector2.Zero);

        public static Rectangle Create(Vector2 position, Vector2 size) =>
            new(position,
                new Vector2(position.X + size.X, position.Y),
                position + size,
                new Vector2(position.X, position.Y + size.Y));

        public static Rectangle Create(float x, float y, float width, float height) =>
            new(new Vector2(x, y),
                new Vector2(x + width, y),
                new Vector2(x + width, y + height),
                new Vector2(x, y + height));

        #endregion

        #region Properties

        public Vector2 Position => Vertices[0];
        public Vector2 Size => BottomRight - TopLeft;
        public float Width => Size.X;
        public float Height => Size.Y;
        public Vector2 TopLeft => Vertices[0];
        public Vector2 TopRight => Vertices[1];
        public Vector2 BottomRight => Vertices[2];
        public Vector2 BottomLeft => Vertices[3];

        #endregion
    }
}