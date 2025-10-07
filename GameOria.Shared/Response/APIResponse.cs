﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Shared.Response
{
    public class APIResponse
    {
            public bool Success { get; set; }
            public string Message { get; set; }
            public object Data { get; set; } = null;
            public List<string> Errors { get; set; } = new();
        }
    }

