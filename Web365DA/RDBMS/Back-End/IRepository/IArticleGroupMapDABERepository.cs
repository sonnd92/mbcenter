﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365DA.RDBMS.Back_End.IRepository
{
    public interface IArticleGroupMapDABERepository
    {
        void ResetGroupOfNews(int newsId, int[] groupId);
        void ResetNewsOfGroup(int groupId, int[] newsId);
    }
}
