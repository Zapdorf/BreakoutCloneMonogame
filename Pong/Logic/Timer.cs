using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Logic
{
    public class Timer
    {
        /*
         * Basically a coroutine
         * Create these and destroy them as needed
         */

        public bool done;
        
        private double _duration; // in seconds
        private Action _action;
        private double _timeElapsed;
        

        public Timer(double dur, Action act) 
        {
            done = false;
            _duration = dur;
            _action = act;
            _timeElapsed = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (done) return;
            _timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if(_timeElapsed > _duration) Finish();
        }

        public void TerminateEarly()
        {
            done = true;
        }

        private void Finish()
        {
            done = true;
            _action.Invoke();
        }
    }
}
