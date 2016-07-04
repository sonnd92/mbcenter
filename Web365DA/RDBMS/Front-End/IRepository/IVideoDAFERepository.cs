﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;
using Web365Models;

namespace Web365DA.RDBMS.Front_End.IRepository
{
    public interface IVideoDAFERepository
    {
        VideoModel GetListByType(int id, string ascii, int skip, int top);
        VideoItem GetHomePageVideo();
    }
}
