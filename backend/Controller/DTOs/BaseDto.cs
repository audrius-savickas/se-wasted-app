﻿using System;
namespace backend.Controller.DTOs
{
    public abstract class BaseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public BaseDto(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
