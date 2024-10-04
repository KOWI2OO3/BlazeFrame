using BlazeFrame.Maths;

namespace BlazeFrame.Core.Test.Maths;

public class Matrix4Test
{
    [Test]
    public void TestEquality()
    {
        var identity = Matrix4.Identity;
        Assert.That(identity == Matrix4.Identity, Is.True);
        Assert.That(identity != Matrix4.Identity, Is.False);
        Assert.That(identity, Is.EqualTo(Matrix4.Identity));
    }

    [Test]
    public void TestDeterminant() 
    {
        var matrix = new Matrix4(1, 0, 0, 0,
                                 0, 5, 0, 0,
                                 0, 0, 1, 0,
                                 0, 0, 0, 1);
        Assert.That(matrix.Determinant, Is.EqualTo(5));
    }

    [Test]
    public void TestIndexing()
    {
        var matrix = Matrix4.Identity;
        Assert.That(matrix[0, 0], Is.EqualTo(1));
        Assert.That(matrix[1, 0], Is.EqualTo(0));

        matrix[0, 0] = 2;
        matrix[1, 0] = 6;
        Assert.That(matrix[0, 0], Is.EqualTo(2));
        Assert.That(matrix[1, 0], Is.EqualTo(6));
    }

    [Test]
    public void TestOutOfRange()
    {
        var matrix = Matrix4.Identity;
        Assert.Throws<IndexOutOfRangeException>(() => _ = matrix[4, 0]);
        Assert.Throws<IndexOutOfRangeException>(() => _ = matrix[0, 4]);
    }
    
    [Test]
    public void TestComplexDeterminant() 
    {
        var matrix = new Matrix4(1, 7, 0, 1,
                                 5, 8, 5, 0,
                                 9, 10, 1, 0,
                                 0, 5, 4, 1);
        Assert.That(matrix.Determinant, Is.EqualTo(126));
    }

    [Test]
    public void TestNullableEquality()
    {
        Matrix4? a = null;
        Assert.That(a == null, Is.True);
        Assert.That(a != null, Is.False);

        a = Matrix4.Identity;

        Assert.That(a == null, Is.False);
        Assert.That(a != null, Is.True);
    }

    public void TestInvertability()
    {
        var matrix = new Matrix4(1, 2, 3, 4,
                                 5, 6, 7, 8,
                                 9, 10, 11, 12,
                                 13, 14, 15, 16);

        var inverted = matrix.Inverted;

        Assert.That(matrix * inverted, Is.EqualTo(Matrix4.Identity));
    }

    [Test]
    public void TestIdentityDeterminant()
    {
        var identity = Matrix4.Identity;
        Assert.That(identity.Determinant, Is.EqualTo(1));
    }

    [Test]
    public void TestInvertedIdentity()
    {
        var identity = Matrix4.Identity;
        Assert.That(identity.Inverted, Is.EqualTo(identity));
    }

    [Test]
    public void TestInverted()
    {
        var matrix = new Matrix4(1, 0, 0, 1,
                                 0, 1, 0, 1,
                                 0, 0, 1, 1,
                                 0, 0, 0, 1);
        Assert.That(matrix.Inverted, Is.EqualTo(new Matrix4(
            1, 0, 0, -1,
            0, 1, 0, -1,
            0, 0, 1, -1,
            0, 0, 0, 1
        )));
    }
    
    [Test]
    public void TestComplexerInverted()
    {
        var matrix = new Matrix4(1, 5, 4, 1,
                                 0, 1, 0, 1,
                                 0, 0, 1, 1,
                                 0, 2, 0, 1);
        Assert.That(matrix.Inverted, Is.EqualTo(new Matrix4(
            1, 11, -4, -8,
            0, -1, 0, 1,
            0, -2, 1, 1,
            0, 2, 0, -1
        )));
    }

    
    [Test]
    public void TestComplexInverted()
    {
        var matrix = new Matrix4(1, 5, 4, 1,
                                 7, 1, 0, 1,
                                 0, 8, 1, 1,
                                 3, 2, 4, 1);
        Assert.That(matrix * matrix.Inverted, Is.EqualTo(Matrix4.Identity));
    }
}
