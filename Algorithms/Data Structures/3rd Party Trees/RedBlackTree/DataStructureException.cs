using System;

namespace DataStructureLibrary
{
    /// <summary>
    /// A data structure exception to distinguish from a .Net exception.
    /// </summary>
    public class DataStructureException : Exception
    {
        private string _message;


        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="message"></param>
        public DataStructureException(string message)
        {
            _message = message;
        }


        public override String Message
        {
            get
            {
                if (InnerException == null)
                    return _message;
                else
                    return _message + " Reason: " + InnerException.Message;
            }
        } 
    }
}
