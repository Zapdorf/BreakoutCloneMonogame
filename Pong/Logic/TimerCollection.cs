using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Logic
{
    public class TimerCollection
    {
        /*
         * Holds a collection of timers and manages them
         * Lots of stuff I don't want to implement repeatedly
         */

        private List<Timer> timers;

        public TimerCollection() 
        { 
            timers = new List<Timer>();
        }

        public void Update(GameTime gameTime)
        {
            var doneTimers = new List<Timer>();
            foreach (Timer timer in timers)
            {
                timer.Update(gameTime);
                if (timer.done) doneTimers.Add(timer);
            }

            CullTimers(doneTimers);
        }

        public void AddTimer(Timer newTimer)
        {
            timers.Add(newTimer);
        }

        public void KillTimer()
        {
            // assign timers names so that they can be found
        }

        private void CullTimers(List<Timer> timersToRemove)
        {
            foreach (Timer timer in timersToRemove)
            {
                timers.Remove(timer); // make sure this removes the right timers
            }
        }
    }
}
