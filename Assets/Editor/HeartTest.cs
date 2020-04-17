using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HeartTest 
{
    private Heart _heart;
    private Image _image;
    [SetUp]
    public void SetMethod()
    {
        _image = new GameObject().AddComponent<Image>();
        _heart = new Heart(_image);
    }

    public class TheEmptyHeartPiecesProperty : HeartTest
    {
        [Test]
        public void _100_PERCENT_IMAGE_FILL_IS_0_EMPTY_HEART_PIECES()
        {
            _image.fillAmount = 1;
            Assert.AreEqual(0, _heart.EmptyHeartPieces);
        }

        [Test]
        public void _75_PERCENT_IMAGE_FILL_IS_1_EMPTY_HEART_PIECE()
        {
            _image.fillAmount = 0.75f;
            Assert.AreEqual(1, _heart.EmptyHeartPieces);
        }
    }

    public class TheFilledHeartPiecesProperty : HeartTest
    {
        [Test]
        public void _0_IMAGE_FILL_IS_0_HEART_PIECES()
        {
            _image.fillAmount = 0;

            Assert.AreEqual(0, _heart.FilledHeartPieces);
        }

        [Test]
        public void _25_IMAGE_FILL_IS_1_HEART_PIECE()
        {
            _image.fillAmount = 0.25f;

            Assert.AreEqual(1, _heart.FilledHeartPieces);
        }

        [Test]
        public void _50_IMAGE_FILL_IS_2_HEART_PIECE()
        {
            _image.fillAmount = 0.5f;

            Assert.AreEqual(2, _heart.FilledHeartPieces);
        }

        [Test]
        public void _75_IMAGE_FILL_IS_3_HEART_PIECE()
        {
            _image.fillAmount = 0.75f;

            Assert.AreEqual(3, _heart.FilledHeartPieces);
        }

        [Test]
        public void _100_IMAGE_FILL_IS_4_HEART_PIECE()
        {
            _image.fillAmount = 1f;

            Assert.AreEqual(4, _heart.FilledHeartPieces);
        }
    }
    public class TheReplenishMethod : HeartTest
    {
        [Test]
        public void _0_SETS_IMAGE_WITH_0_FILL_TO_0_FILL()
        {
            
            _image.fillAmount = 0;


            _heart.Replenish(0);

            Assert.AreEqual(0, _image.fillAmount);
        }

        [Test]
        public void _1_SETS_IMAGE_WITH_0_FILL_TO_25_PERCENT_FILL()
        {
            _image.fillAmount = 0;
            _heart.Replenish(1);

            Assert.AreEqual(0.25f, _image.fillAmount);
        }

        [Test]
        public void _2_SETS_IMAGE_WITH_25_FILL_TO_50_PERCENT_FILL()
        {
            _image.fillAmount = 0.25f;
            _heart.Replenish(1);

            Assert.AreEqual(0.5f, _image.fillAmount);
        }

        [Test]
        public void _3_SETS_IMAGE_WITH_50_FILL_TO_75_PERCENT_FILL()
        {
            _image.fillAmount = 0.5f;
            _heart.Replenish(1);

            Assert.AreEqual(0.75f, _image.fillAmount);
        }

        [Test]
        public void _4_SETS_IMAGE_WITH_75_FILL_TO_100_PERCENT_FILL()
        {
            _image.fillAmount = 0.75f;
            _heart.Replenish(1);

            Assert.AreEqual(1f, _image.fillAmount);
        }

        [Test]
        public void _5_THROWS_EXCEPTION_FOR_NEGATIVE_NUMBER_OF_HEART_PIECES()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _heart.Deplate(-1));
        }
    }
    public class TheDeplateMethod : HeartTest
    {
        [Test]
        public void _0_SETS_IMAGE_WITH_100_FILL_TO_100_PERCENT_FILL()
        {
            _image.fillAmount = 1;
            _heart.Deplate(0);

            Assert.AreEqual(1, _image.fillAmount);
        }

        [Test]
        public void _1_SETS_IMAGE_WITH_100_FILL_TO_75_PERCENT_FILL()
        {
            _image.fillAmount = 1;
            _heart.Deplate(1);

            Assert.AreEqual(0.75f, _image.fillAmount);
        }

        [Test]
        public void _2_SETS_IMAGE_WITH_75_FILL_TO_50_PERCENT_FILL()
        {
            _image.fillAmount = 0.75f;
            _heart.Deplate(1);

            Assert.AreEqual(0.5f, _image.fillAmount);
        }

        [Test]
        public void _3_SETS_IMAGE_WITH_50_FILL_TO_25_PERCENT_FILL()
        {
            _image.fillAmount = 0.5f;
            _heart.Deplate(1);

            Assert.AreEqual(0.25f, _image.fillAmount);
        }

        [Test]
        public void _4_SETS_IMAGE_WITH_25_FILL_TO_0_PERCENT_FILL()
        {
            _image.fillAmount = 0.25f;
            _heart.Deplate(1);

            Assert.AreEqual(0, _image.fillAmount);
        }

        [Test]
        public void _5_THROWS_EXCEPTION_FOR_NEGATIVE_NUMBER_OF_HEART_PIECES()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _heart.Deplate(-1));
        }
    }
}
