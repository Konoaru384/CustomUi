﻿using Exiled.API.Interfaces;

namespace Player_Ui
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }
    }
}