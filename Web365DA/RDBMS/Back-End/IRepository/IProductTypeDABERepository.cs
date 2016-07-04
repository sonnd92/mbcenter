﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365DA.RDBMS.Back_End.IRepository
{
    public interface IProductTypeDABERepository : IBaseDABERepository
    {
        List<ProductTypeItem> GetList(out int total, string name, int? parentId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false);
        List<ProductTypeItem> GetListOfGroup(int groupId, bool isShow = true, bool isDelete = false);
        void ResetListPicture(int id, string listPictureId);
    }
}
