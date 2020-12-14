﻿using Aptacode.Geometry.Primitives;
using System.Numerics;

namespace Aptacode.Geometry.Collision
{
    public class FineCollisionDetector : CollisionDetector
    {
        #region Point

        public override bool CollidesWith(Point p1, Point p2) => p1 == p2;

        public override bool CollidesWith(Point p1, PolyLine p2)
        {
            foreach (var (v1, v2) in p2.LineSegments())
            {
                if(Helpers.OnLineSegment((v1, v2), p1.Position))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool CollidesWith(Point p1, Polygon p2)
        {
            var collision = false;
            var edges = p2.Edges();
            var point = p1.Position;
            foreach (var (a, b) in edges)
            {
                if ((a.Y >= point.Y && b.Y < point.Y || a.Y < point.Y && b.Y >= point.Y) &&
                    point.X < (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X)
                {
                    collision = !collision;
                }
            }


            return collision;
        }

        public override bool CollidesWith(Point p1, Circle p2)
        {
            return BoundingCircleAlgorithm.IsInside(p2, p1.Position);
        }

        #endregion

        #region PolyLine

        public override bool CollidesWith(PolyLine p1, Point p2) => false;

        public override bool CollidesWith(PolyLine p1, PolyLine p2) => false;

        public override bool CollidesWith(PolyLine p1, Polygon p2) => false;

        public override bool CollidesWith(PolyLine p1, Circle p2) => false;

        #endregion

        #region Polygon

        public override bool CollidesWith(Polygon p1, Point p2) => false;

        public override bool CollidesWith(Polygon p1, PolyLine p2) => false;

        public override bool CollidesWith(Polygon p1, Polygon p2) => false;

        public override bool CollidesWith(Polygon p1, Circle p2) => false;

        #endregion

        #region Circle

        public override bool CollidesWith(Circle p1, Point p2) => BoundingCircleAlgorithm.IsInside(p1, p2.Position);

        public override bool CollidesWith(Circle p1, PolyLine p2)
        {
            foreach (var (v1, v2) in p2.LineSegments())
            {
                if(BoundingCircleAlgorithm.IsInside(p1, v1) || BoundingCircleAlgorithm.IsInside(p1, v2))
                {
                    return true;
                }

                var dot = ((p1.Position.X - v1.X) * (v2.X - v1.X) + (p1.Position.Y - v1.Y) * (v2.Y - v1.Y)) / (v2 - v1).LengthSquared();
                var closestX = v1.X + dot * (v2.X - v1.X);
                var closestY = v1.Y + dot * (v2.Y - v1.Y);
                var closestPoint = new Vector2(closestX, closestY); //The point of intersection of a line from the center of the circle perpendicular to the line segment (possibly the ray) with the line segment (or ray).
                if (!Helpers.OnLineSegment((v1, v2), closestPoint)) //Closest intersection point may be beyond the ends of the line segment.
                {
                    return false;
                }
                if(BoundingCircleAlgorithm.IsInside(p1, closestPoint)) //Closest intersection point is inside the circle means circle intersects the line.
                {
                    return true;
                }
            }
            return false;
        }

        public override bool CollidesWith(Circle p1, Polygon p2)
        {
            foreach (var (v1, v2) in p2.Edges())
            {
                if (BoundingCircleAlgorithm.IsInside(p1, v1) || BoundingCircleAlgorithm.IsInside(p1, v2))
                {
                    return true;
                }

                var dot = ((p1.Position.X - v1.X) * (v2.X - v1.X) + (p1.Position.Y - v1.Y) * (v2.Y - v1.Y)) / (v2 - v1).LengthSquared();
                var closestX = v1.X + dot * (v2.X - v1.X);
                var closestY = v1.Y + dot * (v2.Y - v1.Y);
                var closestPoint = new Vector2(closestX, closestY); //The point of intersection of a line from the center of the circle perpendicular to the edge (possibly the ray) with the line segment (or ray).
                if (!Helpers.OnLineSegment((v1, v2), closestPoint)) //Closest intersection point may be beyond the ends of the edge.
                {
                    return false;
                }
                if (BoundingCircleAlgorithm.IsInside(p1, closestPoint)) //Closest intersection point is inside the circle means circle intersects the edge.
                {
                    return true;
                }
            }
            return false;
        }

        public override bool CollidesWith(Circle p1, Circle p2)
        {
            var d = (p2.Center - p1.Center).Length();
            if (d < p1.Radius + p2.Radius)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}