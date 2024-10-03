using BlazeFrame.Maths;

namespace BlazeFrame.Core.Test.Maths;

public class Matrix4Test
{
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

}
