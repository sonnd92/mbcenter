using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;
using Web365Models;

namespace Web365DA.RDBMS.Front_End.IRepository
{
    public interface IPictureTypeDAFERepository
    {
        List<PictureTypeItem> GetAllChildOfType(int[] parent, bool isShow = true, bool isDeleted = false);
    }
}
