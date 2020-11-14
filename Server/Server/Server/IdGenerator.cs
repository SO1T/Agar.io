using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public interface IIdGenerator
    {
        public int GetId();
    }
    public class IdGenerator : IIdGenerator
    {
        private int lastId = 0;
        public int GetId()
        {
            lastId++;
            return lastId;
        }
    }
}
