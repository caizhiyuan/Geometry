﻿using System.Numerics;
using Aptacode.Geometry.Collision.Circles;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public record Rectangle(Vector2 TopLeft, Vector2 TopRight, Vector2 BottomRight, Vector2 BottomLeft) : Polygon(new[]
    {
        TopLeft, TopRight, BottomRight, BottomLeft
    })
    {
        #region Construction

        public static Rectangle Create(Vector2 position, Vector2 size) =>
            new(position,
                new Vector2(position.X + size.X, position.Y + size.Y),
                position + size,
                new Vector2(position.X, position.Y + size.Y));

        #endregion

        #region Properties

        public Vector2 Position => TopLeft;

        private Vector2? _size;

        public Vector2 Size
        {
            get => _size ?? BottomRight - TopLeft;
            init => _size = value;
        }
        
        #endregion

        #region Transformations

        public override Rectangle Translate(Vector2 delta) =>
            new(TopLeft + delta, TopRight + delta, BottomRight + delta, BottomLeft + delta)
            {
                BoundingCircle = BoundingCircle.Translate(delta),
                Size = Size
            };

        public override Rectangle Rotate(float delta) => this;

        public override Rectangle Scale(Vector2 delta) => this;

        public override Rectangle Skew(Vector2 delta) => this;

        #endregion
    }
}