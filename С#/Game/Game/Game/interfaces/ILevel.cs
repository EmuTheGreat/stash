﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.interfaces
{
    public interface ILevel
    {
        int[,] map { get; }
        int mapWidth { get; }
        int mapHeight { get; }
        List<IEntity> entities { get; }
    }
}
