﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class MessageResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public MessageResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public MessageResponse(bool success)
        {
            Success = success;
        }
    }
}
