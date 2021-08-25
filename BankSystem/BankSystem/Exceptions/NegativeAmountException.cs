﻿using System;

namespace BankSystem.Exceptions
{
    public class NegativeAmountException : Exception
    {
        public NegativeAmountException(string message) : base(message)
        {

        }
    }
}