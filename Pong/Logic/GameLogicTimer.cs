using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Pong.Logic
{
    public class GameLogicTimer
    {
        /*
         * Basically a coroutine
         * Create these and destroy them as needed
         */

        public bool done;
        public bool running;
        
        private double _duration; // in seconds
        private Action _action;
        private double _timeElapsed;
        

        public GameLogicTimer(double dur, bool startNow, Action act) 
        {
            done = false;
            _duration = dur;
            _action = act;
            _timeElapsed = 0;
            running = startNow;
        }

        public void Update(GameTime gameTime)
        {
            if (done || !running) return;
            _timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if(_timeElapsed > _duration) Finish();
        }

        public void TerminateEarly()
        {
            // cancels the timer
            done = true;
        }

        public void Reset()
        {
            _timeElapsed = 0;
            done = false;
        }

        public void ActivateNow()
        {
            // for use with delayed start
            running = true;
        }

        public void PauseTimer()
        {
            running = false;
        }

        private void Finish()
        {
            done = true;
            _action.Invoke();
        }
    }
}
