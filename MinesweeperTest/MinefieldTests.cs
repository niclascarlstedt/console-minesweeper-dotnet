using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTest;

[TestClass]
public class MinefieldTests
{
    [TestMethod]
    public void TestTaskExampleVisibleLayer()
    {
        // Arrange
        int size = 5;
        var field = new Minefield(size);

        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        string[] expected = {
            "?????",
            "?????",
            "?????",
            "??111",
            "??1  "
        };

        // Act
        field.AttemptClearBomb(4,0);

        var rows = field.GetDisplayRows(false);

        // Assert
        for (int i = 0; i < rows.Length; i++) {
            Assert.AreEqual(expected[i], rows[i], $"Expected: {expected[i]}, was {rows[i]}");
        }
    }

    [TestMethod]
    public void TestTaskExampleAdjescentBombsLayer()
    {
        // Arrange
        int size = 5;
        var field = new Minefield(size);

        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        string[] expected = {
            "1X1  ",
            "11111",
            "2211X",
            "XX111",
            "X31  "
        };

        // Act
        var rows = field.GetDisplayRows(true);

        // Assert
        for (int i = 0; i < rows.Length; i++) {
            Assert.AreEqual(expected[i], rows[i], $"Expected: {expected[i]}, was {rows[i]}");
        }
    }

    [TestMethod]
    public void TestTaskExampleFullPlay()
    {
        // Arrange
        int size = 5;
        var field = new Minefield(size);

        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        string[] expected = {
            "1?1  ",
            "11111",
            "2211?",
            "??111",
            "?31  "
        };

        // Act
        field.AttemptClearBomb(4,0);
        field.AttemptClearBomb(1,0);
        field.AttemptClearBomb(0,4);
        field.AttemptClearBomb(3,4);
        field.AttemptClearBomb(0,3);
        field.AttemptClearBomb(1,3);
        field.AttemptClearBomb(0,2);
        field.AttemptClearBomb(1,2);
        field.AttemptClearBomb(2,2);
        field.AttemptClearBomb(3,2);

        // Assert
        var rows = field.GetDisplayRows(false);
        for (int i = 0; i < rows.Length; i++) {
            Assert.AreEqual(expected[i], rows[i], $"Expected: {expected[i]}, was {rows[i]}");
        }
    }

    [TestMethod]
    public void TestExplosion()
    {
        // Arrange
        int size = 1;
        var field = new Minefield(size);
        field.SetBomb(0, 0);

        // Act
        bool attemptSuccess = field.AttemptClearBomb(0,0);

        // Assert
        Assert.AreEqual(false, attemptSuccess, "Expected AttemptClearBomb to return false");
    }
}
