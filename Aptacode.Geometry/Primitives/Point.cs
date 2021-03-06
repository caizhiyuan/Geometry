﻿using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record Point : Primitive
    {
        #region Properties

        public Vector2 Position => Vertices[0];

        #endregion

        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Ctor

        public Point(Vector2 position) : base(VertexArray.Create(position)) { }
        protected Point(VertexArray vertexArray) : base(vertexArray) { }

        public Point(Vector2 position, BoundingCircle? boundingCircle, BoundingRectangle? boundingRectangle) : base(
            VertexArray.Create(position), boundingCircle, boundingRectangle) { }

        public static Point Create(float x, float y) => new(new Vector2(x, y));

        #endregion

        #region Construction

        public static readonly Point Zero = new(Vector2.Zero);
        public static readonly Point Unit = new(Vector2.One);

        #endregion
    }
}