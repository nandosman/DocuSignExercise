using System;

namespace DocuSign.BusinessObject.Errors
{
    /**
    * This is an Error Exception used for all Pajamas related errors.
    * It's usually thrown when a Person tries to equip a clothing item, without first removing his/her pajamas.
    */
    public class PajamasException : ClothingException
    {
        public PajamasException() : this(null) { }
        public PajamasException(string msg) : base(msg) { }
    }
}
