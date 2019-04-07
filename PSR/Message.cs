using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSR
{
    public enum MESSAGE_TYPE
    {
        OK,
        FAIL,
        JOIN,
        DATA,
        START,
        STATUS,
        RESULT,
        CLOSE
    }

    [Serializable]
    public class Message : ICloneable
    {
        public MESSAGE_TYPE type;
        public string msg;
        public int value;

        public Message()
        {

        }

        public Message(MESSAGE_TYPE type,string msg)
        {
            this.type = type;
            this.msg = msg;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        /*
       public MESSAGE_TYPE Type { get => type; set => type = value; }
       public string Msg { get => msg; set => msg = value; }
       */
    }
}
