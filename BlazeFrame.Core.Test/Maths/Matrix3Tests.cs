using BlazeFrame.Maths;

namespace BlazeFrame.Core.Test.Maths;

public class Matrix3Tests
{
    [Test]
    public void TestIdentityDeterminant()
    {
        var identity = Matrix3.Identity;
        Assert.That(identity.Determinant, Is.EqualTo(1));
    }

    [Test]
    public void TestInvertedIdentity()
    {
        var identity = Matrix3.Identity;
        Assert.That(identity.Inverted, Is.EqualTo(identity));
    }

    [Test]
    public void TestInverted()
    {
        var matrix = new Matrix3(
            1, 0, 0,
            3, 3, 0,
            5, 2, -1
        );
        var inverted = matrix.Inverted;
        Assert.That(matrix * inverted, Is.EqualTo(Matrix3.Identity));
    }

    [Test]
    public void TestTranspose()
    {
        var matrix = new Matrix3(
            1, 2, 3,
            4, 5, 6,
            7, 8, 9
        );
        var transposed = new Matrix3(
            1, 4, 7,
            2, 5, 8,
            3, 6, 9
        );
        Assert.That(matrix.Transposed, Is.EqualTo(transposed));
    }

    [Test]
    public void TestMultiplyIdentity()
    {
        var matrix = new Matrix3(
            1, 2, 3,
            4, 5, 6,
            7, 8, 9
        );
        Assert.That(matrix.Multiply(Matrix3.Identity), Is.EqualTo(matrix));
    }

    [Test]
    public void TestMultiply()
    {
        var matrix1 = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        var matrix2 = new Matrix3(
            1, 7, 1,
            3, 4, 1,
            8, 5, 6
        );
        Assert.That(matrix1 * matrix2, Is.EqualTo(new Matrix3(
            20, 21, 14,
            23, 42, 17,
            30, 74, 18
        )));
    }
    
    [Test]
    public void TestMultiplyReversed()
    {
        var matrix1 = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        var matrix2 = new Matrix3(
            1, 7, 1,
            3, 4, 1,
            8, 5, 6
        );
        Assert.That(matrix2 * matrix1, Is.EqualTo(new Matrix3(
            36, 13, 17,
            26, 12, 15,
            70, 43, 32
        )));
    }
    
    [Test]
    public void TestAddition()
    {
        var matrix1 = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        var matrix2 = new Matrix3(
            1, 7, 1,
            3, 4, 1,
            8, 5, 6
        );
        Assert.That(matrix1 + matrix2, Is.EqualTo(new Matrix3(
            2, 8, 3,
            7, 5, 3,
            15, 10, 7
        )));
    }

    [Test]
    public void TestAdditionIdentity()
    {
        var matrix1 = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        var matrix2 = Matrix3.Identity;
        Assert.That(matrix1 + matrix2, Is.EqualTo(new Matrix3(
            2, 1, 2,
            4, 2, 2,
            7, 5, 2
        )));
    }

    [Test]
    public void TestSubtraction()
    {
        var matrix1 = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        var matrix2 = new Matrix3(
            1, 7, 1,
            3, 4, 1,
            8, 5, 6
        );
        Assert.That(matrix1 - matrix2, Is.EqualTo(new Matrix3(
            0, -6, 1,
            1, -3, 1,
            -1, 0, -5
        )));
    }

    [Test]
    public void TestSubtractionIdentity()
    {
        var matrix1 = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        var matrix2 = Matrix3.Identity;
        Assert.That(matrix1 - matrix2, Is.EqualTo(new Matrix3(
            0, 1, 2,
            4, 0, 2,
            7, 5, 0
        )));
    }

    [Test]
    public void TestNegation()
    {
        var matrix = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        Assert.That(-matrix, Is.EqualTo(new Matrix3(
            -1, -1, -2,
            -4, -1, -2,
            -7, -5, -1
        )));
    }

    [Test]
    public void TestScalarMultiplicationIdentity()
    {
        Assert.That(Matrix3.Identity * 2, Is.EqualTo(new Matrix3(
            2, 0, 0,
            0, 2, 0,
            0, 0, 2
        )));
    }

    [Test]
    public void TestScalarMultiplication()
    {
        var matrix = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        Assert.That(matrix * 2, Is.EqualTo(new Matrix3(
            2, 2, 4,
            8, 2, 4,
            14, 10, 2
        )));
    }

    [Test]
    public void TestScalarMultiplierInversion() 
    {
        Assert.That(Matrix3.Identity * 2, Is.EqualTo(2 * Matrix3.Identity));
    }

    [Test]
    public void TestScalarDivisionIdentity()
    {
        Assert.That(Matrix3.Identity / 2, Is.EqualTo(new Matrix3(
            .5f, 0, 0,
            0, .5f, 0,
            0, 0, .5f
        )));
    }

    [Test]
    public void TestScalarDivision()
    {
        var matrix = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        Assert.That(matrix / 2, Is.EqualTo(new Matrix3(
            .5f, .5f, 1,
            2, .5f, 1,
            3.5f, 2.5f, .5f
        )));
    }

    [Test]
    public void TestTransformIdentity()
    {
        var matrix = Matrix3.Identity;
        var vector = new Vector3(1, 2, 3);
        Assert.That(matrix.Transform(vector), Is.EqualTo(vector));
    }

    [Test]
    public void TestTransform()
    {
        var matrix = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        var vector = new Vector3(1, 2, 3);
        Assert.That(matrix.Transform(vector), Is.EqualTo(new Vector3(9, 12, 20)));
    }

    
    [Test]
    public void TestTransformMethodAndOperator()
    {
        var matrix = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        var vector = new Vector3(1, 2, 3);
        Assert.That(matrix.Transform(vector), Is.EqualTo(matrix * vector));
    }

    [Test]
    public void TestMatrixColumnVariableToVariableLink()
    {
        var matrix = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        var column = matrix.Column1;
        column.X = 10;
        Assert.That(matrix.Column1.X, Is.EqualTo(10));
        Assert.That(matrix.M11, Is.EqualTo(10));
    }

    [Test]
    public void TestMatrixColumnToVariableLink()
    {
        var matrix = new Matrix3(
            1, 1, 2,
            4, 1, 2,
            7, 5, 1
        );
        matrix.Column1 = new(10, 1, 2);
        Assert.That(matrix.Column1.X, Is.EqualTo(10));
        Assert.That(matrix.M11, Is.EqualTo(10));
    }
}
