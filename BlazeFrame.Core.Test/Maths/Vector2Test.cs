using BlazeFrame.Maths;

namespace BlazeFrame.Core.Test.Maths;

public class Vector2Test
{
    [Test]
    public void TestIndexing()
    {
        var vector = new Vector2(10, 50);
        Assert.That(vector[0], Is.EqualTo(10));
        Assert.That(vector[1], Is.EqualTo(50));

        vector[0] = 20;
        vector[1] = 100;
        Assert.That(vector[0], Is.EqualTo(20));
        Assert.That(vector[1], Is.EqualTo(100));

    }

}
