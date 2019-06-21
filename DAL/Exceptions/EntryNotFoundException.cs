﻿using System;

namespace DAL.Exceptions
{
    public class EntryNotFoundException : Exception
    {
        public EntryNotFoundException() { }

        public EntryNotFoundException(string message) : base(message) { }

        public EntryNotFoundException(string message, Exception inner) : base(message, inner) { }

    }
}
