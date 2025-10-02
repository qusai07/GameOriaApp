﻿namespace GameOria.Api.Response
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } = null;
        public List<string> Errors { get; set; } = new();
    }

}
