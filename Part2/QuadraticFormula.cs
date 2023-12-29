using MathNet.Numerics.LinearAlgebra;
using Part2;
using System.Text;

class QuadraticFormula
{
    public long A { get; }
    public long B { get; }
    public long C { get; }

    public static QuadraticFormula GetQuadraticFormula(Coord p1, Coord p2, Coord p3)
    {
        Matrix<double> matrix = Matrix<double>.Build.DenseOfArray(new double[,] {
            { p1.X * p1.X, p1.X, 1 },
            { p2.X * p2.X, p2.X, 1 },
            { p3.X * p3.X, p3.X, 1 }
        });

        Vector<double> vector = Vector<double>.Build.Dense(new double[] { p1.Y, p2.Y, p3.Y });

        Vector<double> result = matrix.Solve(vector);

        double a = result[0];
        double b = result[1];
        double c = result[2];

        return new QuadraticFormula((long)Math.Round(a), (long)Math.Round(b), (long)Math.Round(c));
    }

    public QuadraticFormula(long a, long b, long c)
    {
        A = a;
        B = b;
        C = c;
    }

    public long GetY(long x)
    {
        return A * x * x + B * x + C;
    }

    public override string ToString()
    {
        var parts = new List<string>();
        if (A != 0)
            parts.Add($"{A}x²");

        if (B != 0)
        {
            if(parts.Count > 0)
            {
                parts.Add($"{Sign(B)} {Math.Abs(B)}x");
            } else
            {
                parts.Add($"{B}x");
            }

        }

        if (C != 0)
        {
            if(parts.Count > 0)
            {
                parts.Add($"{Sign(C)} {Math.Abs(C)}");
            } else
            {
                parts.Add($"{C}");
            }
        }

        return string.Join(' ', parts);
    }

    private string Sign(long n)
    {
        return n < 0 ? "-" : "+";
    }
}
