using System;
using System.Collections.Generic;
using System.Text;

namespace CardsApp.Classes
{
    class ReturnObject<ReturnedType>
    {
        public DateTime ReturnTime;
        public bool Success;
        public string ExceptionMessage;
        public ReturnedType ReturnData;
    }
}
