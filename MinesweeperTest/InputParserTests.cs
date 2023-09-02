using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTest;

[TestClass]
public class InputParserTests
{
    [TestMethod]
    public void VerifyNotAcceptOneCharacter()
    {
        int[] result = InputParser.Parse("a");

        Assert.IsTrue(result.Length == 0, "InputParser should not parse 'a'.");
    }

    [TestMethod]
    public void VerifyNotAcceptFourCharacters()
    {
        int[] result = InputParser.Parse("1234");

        Assert.IsTrue(result.Length == 0, "InputParser should not parse '1234'.");
    }

    [TestMethod]
    public void VerifyCanParseTwoInts()
    {
        int[] result = InputParser.Parse("1 2");

        Assert.IsTrue(result.Length == 2, "InputParser should handle two integers being parsed.");
    }

    [TestMethod]
    public void VerifyCanParseTwoSpecificInts()
    {
        int[] result = InputParser.Parse("1 2");

        Assert.IsTrue(result[0] == 1, "InputParser should parse exactly int 1.");
        Assert.IsTrue(result[1] == 2, "InputParser should parse exactly int 2.");
    }
}
