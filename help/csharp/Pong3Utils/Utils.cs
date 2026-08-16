// For examples, see:
// https://thegraybook.vvvv.org/reference/extending/writing-nodes.html#examples

namespace Main;

public static class Utils
{
    public static int LineIntersectsCircle(Vector2 center, float radius, Vector2 L1, Vector2 L2, out Vector2 p1, out Vector2 p2)
    {
        p1 = new Vector2();
        p2 = new Vector2();

        var dx = L2.X - L1.X;
        var dy = L2.Y - L1.Y;
        var a = dx * dx + dy * dy;
        var b = 2 * (dx * (L1.X - center.X) + dy * (L1.Y - center.Y));
        var c = L1.X * L1.X + L1.Y * L1.Y + center.X * center.X + center.Y * center.Y - 2 * (center.X * L1.X + center.Y * L1.Y) - (radius * radius);
        var bb4ac = b * b - 4 * a * c;

        var mu1 = (float)(-b + Math.Sqrt(bb4ac)) / (2 * a);
        var mu2 = (float)(-b - Math.Sqrt(bb4ac)) / (2 * a);

        var i1x = L1.X + mu1 * (dx);
        var i1y = L1.Y + mu1 * (dy);

        var i2x = L1.X + mu2 * (dx);
        var i2y = L1.Y + mu2 * (dy);

        var dist1 = Math.Sqrt((center.X - L1.X) * (center.X - L1.X) + (center.Y - L1.Y) * (center.Y - L1.Y));
        var dist2 = Math.Sqrt((center.X - L2.X) * (center.X - L2.X) + (center.Y - L2.Y) * (center.Y - L2.Y));

        if ((bb4ac < 0))   // Not intersecting  (bb4ac < 0)
        {
            return 0;
        }

        if (i1x < L1.X & i2x < L1.X & i1x < L2.X & i2x < L2.X |
            i1y < L1.Y & i2y < L1.Y & i1y < L2.Y & i2y < L2.Y |
            i1x > L1.X & i2x > L1.X & i1x > L2.X & i2x > L2.X |
            i1y > L1.Y & i2y > L1.Y & i1y > L2.Y & i2y > L2.Y)  // No intersecting, line outside the circle
        {
            return 0;
        }

        if ((bb4ac == 0) & dist1 < radius & dist2 < radius) //1 solution tangent  (bb4ac == 0)
        {
            p1.X = i1x;
            p1.Y = i1y;
            return 1;
        }

        if (bb4ac > 0 & (dist1 >= radius & dist2 >= radius))    //2 solutions  Line radiusossing the circle
        {
            p1.X = i1x;
            p1.Y = i1y;
            p2.X = i2x;
            p2.Y = i2y;
            return 2;
        }

        if ((dist1 >= radius & dist2 <= radius))    //1 solution  One Point in the circle
        {
            p1.X = i2x;
            p1.Y = i2y;
            return 1;
        }

        if ((dist1 <= radius & dist2 >= radius))    //1 solution   One Point in the circle
        {
            p1.X = i1x;
            p1.Y = i1y;
            return 1;
        }

        {
            return 0;
        }
    }
}