using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Editor.Infrastructure
{
    public class HeartContainerBuilder : TestDataBuilder<HeartContainer>
    {
        private List<Heart> m_hearts;

        public HeartContainerBuilder(List<Heart> hearts)
        {
            m_hearts = hearts;
        }

        public HeartContainerBuilder() : this(new List<Heart>())
        {
        }

        public HeartContainerBuilder With(Heart heart, Heart target = null)
        {
            m_hearts.Add(heart);
            if (target != null) m_hearts.Add(target);
            return this;
        }

        public override HeartContainer Build() {
           return new HeartContainer(m_hearts);
        }
            
    }
}
