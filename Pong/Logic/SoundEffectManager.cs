using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Logic
{
    public static class SoundEffectManager
    {
        public static SoundEffect BlockBreakSound = Globals.Content.Load<SoundEffect>("Sound/explosion2");
        public static SoundEffect VictorySound = Globals.Content.Load<SoundEffect>("Sound/powerUp");
    }
}
