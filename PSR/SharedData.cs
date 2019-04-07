using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSR
{
    class SharedData
    {
        private readonly object block = new object();
        private Message msg;
        private bool isReaded = true;

        public Message Message
        {
            set
            {
                lock (block)
                {
                    isReaded = false;
                    msg = value;
                }
            }

            get
            {
                lock (block)
                {
                    isReaded = true;
                    return msg;
                }
            }
        }
        public bool IsReaded
        {
            get
            {
                return isReaded;
            }
        }


    }
}
