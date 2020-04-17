
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;

public partial class HeartContainerTests
{
    public partial class TheReplenishMethod
    {
        private Image Target;
        private Heart _heart;

        [SetUp]
        public void BeforeEveryTest()
        {
            Target = An.Image();
        }

        [Test]
        public void _0_SETS_IMAGE_WITH_0_FILL_TO_0_PERCENT_()
        {
            ((HeartContainer)A.HeartContainer().With(A.Heart().With(Target))).Replenish(0);

            Assert.AreEqual(0, Target.fillAmount);
        }

        [Test]
        public void _1_SETS_IMAGE_WITH_0_FILL_TO_25_PERCENT_()
        {
            ((HeartContainer)A.HeartContainer().With(A.Heart().With(Target))).Replenish(1);

            Assert.AreEqual(0.25f, Target.fillAmount);
        }

        [Test]
        public void _2_SETS_IMAGE_WITH_25_FILL_TO_50_PERCENT_()
        {
            Target.fillAmount = 0.25f;
            ((HeartContainer)A.HeartContainer().With(A.Heart().With(Target))).Replenish(1);

            Assert.AreEqual(0.5f, Target.fillAmount);
        }

        [Test]
        public void _HEARTS_ARE_REPLENISHED_IN_ORDER()
        {
            ((HeartContainer)A.HeartContainer().With(
                A.Heart(),
                A.Heart().With(Target)))
                .Replenish(1);

            Assert.AreEqual(0f, Target.fillAmount);
        }

        [Test]
        public void _DISTRIBUTES_HEART_PIECES_ACROSS_MULTIPLE_UNFILLED_HEARTS()
        {
            ((HeartContainer)A.HeartContainer().With(
                A.Heart().With(An.Image().WithFillAmount(1f)),
                A.Heart().With(Target)))
                .Replenish(2);

            Assert.AreEqual(0.5f, Target.fillAmount);
        }
    }

    public class TheDepleteMethod
    {
        protected Image Target;

        [SetUp]
        public void BeforeEveryTest()
        {
            Target = An.Image().WithFillAmount(1);
        }

        [Test]
        public void _0_SETS_FULL_IMAGE_TO_100_PERCENT_FILL()
        {
            ((HeartContainer)A.HeartContainer()
                .With(A.Heart().With(Target))).Deplate(0);
            Assert.AreEqual(1, Target.fillAmount);
        }

        [Test]
        public void _1_SETS_FULL_IMAGE_TO_75_PERCENT_FILL()
        {
            ((HeartContainer)A.HeartContainer()
                .With(A.Heart().With(Target))).Deplate(1);
            Assert.AreEqual(0.75f, Target.fillAmount);
        }

        [Test]
        public void _2_SETS_FULL_IMAGE_TO_75_PERCENT_FILL_AFTER_DISTRIBUATION()
        {
            ((HeartContainer)A.HeartContainer()
                .With(
                A.Heart().With(An.Image().WithFillAmount(1)),
                A.Heart().With(Target)
                )).Deplate(1);
            Assert.AreEqual(0.75f, Target.fillAmount);
        }
    }
}
