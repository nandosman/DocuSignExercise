using System;

namespace DocuSign.BusinessObject.Errors
{
    /**
    * This is an Error Exception used for all clothing related events.
    * It's usually thrown when a Person tries to equip a clothing slot that already had an item.
    */
    public class ClothingException : Exception
    {
        public ClothingException() : this(null) { }
        public ClothingException(string msg) : base(msg) { }
    }
}
