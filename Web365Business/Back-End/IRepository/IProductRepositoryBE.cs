﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365Business.Back_End.IRepository
{
    public interface IProductRepositoryBE : IBaseRepositoryBE
    {
        List<ProductItem> GetList(out int total,
            string name,
            int currentRecord,
            int numberRecord,
            string propertyNameSort,
            bool descending,
            bool isDelete = false);
        ProductItem GetEditFormFilter(int productId);
        void LabelForProduct(int productId, int[] labelId);
        void FilterForProduct(int productId, string filterId);
        void ResetListPicture(int id, string listPictureId);
        void ResetListFile(int id, string listFileId);
    }
}
