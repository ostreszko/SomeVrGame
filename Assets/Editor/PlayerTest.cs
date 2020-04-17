using NUnit.Framework;
using System;

public class PlayerTest 
{
    [Test]
    public void HEALTH_DEFAULTS_TO_0()
    {
        var player = new PlayerController(0);
        Assert.AreEqual(0, player.CurrentHealth);
    }

    [Test]
    public void THROWS_EXCEPTION_WHEN_CURRENT_HEALTH_IS_LESS_THAN_0()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PlayerController(-1));
    }

    [Test]
    public void THROWS_EXCEPTION_WHEN_CURRENT_HEALTH_IS_GREATER_THAN_MAX_HEALTH()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PlayerController(2,1));
    }

    public class TheHealMethod
    {
        [Test]
        public void _0_DOES_NOTHING()
        {
            var player = new PlayerController(0);
            player.Heal(0);

            Assert.AreEqual(0, player.CurrentHealth);
        }

        [Test]
        public void _1_INCREMENTS_CURRENT_HEALTH()
        {
            var player = new PlayerController(0);
            player.Heal(1);

            Assert.AreEqual(1, player.CurrentHealth);
        }

        [Test]
        public void _2_OVERHEAL_IS_IGNORED()
        {
            var player = new PlayerController(0,1);
            player.Heal(2);

            Assert.AreEqual(1, player.CurrentHealth);
        }
    }

    public class TheDamageMethod
    {
        [Test]
        public void _0_DOES_NOTHING()
        {
            var player = new PlayerController(1);
            player.Damage(0);

            Assert.AreEqual(1, player.CurrentHealth);
        }

        [Test]
        public void _1_DECREMENT_CURRENT_HEALTH()
        {
            var player = new PlayerController(1);
            player.Damage(1);

            Assert.AreEqual(0, player.CurrentHealth);
        }

        [Test]
        public void _2_OVERKILL_IS_IGNORED()
        {
            var player = new PlayerController(1);
            player.Damage(2);

            Assert.AreEqual(0, player.CurrentHealth);
        }
    }

    public class TheHealedEvent
    {
        [Test]
        public void RAISES_EVENT_ON_HEAL()
        {
            var amount = -1;
            var player = new PlayerController(1);
            player.Healed += (sender, args) =>
            {
                amount = args.Amount;
            };
            player.Heal(0);
            Assert.AreEqual(0, amount);
        }

        [Test]
        public void OVERHEALING_IS_IGNORED()
        {
            var amount = -1;
            var player = new PlayerController(1,1);
            player.Healed += (sender, args) =>
            {
                amount = args.Amount;
            };
            player.Heal(1);
            Assert.AreEqual(0, amount);
        }
    }

    public class TheDamagedEvent
    {
        [Test]
        public void RAISES_EVENT_ON_DAMAGE()
        {
            var amount = -1;
            var player = new PlayerController(1);
            player.Damaged += (sender, args) =>
            {
                amount = args.Amount;
            };
            player.Damage(0);
            Assert.AreEqual(0, amount);
        }

        [Test]
        public void OVERKILL_IS_IGNORED()
        {
            var amount = -1;
            var player = new PlayerController(0);
            player.Damaged += (sender, args) =>
            {
                amount = args.Amount;
            };
            player.Damage(1);
            Assert.AreEqual(0, amount);
        }
    }
}

