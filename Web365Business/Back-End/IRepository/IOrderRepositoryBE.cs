﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365Business.Back_End.IRepository
{
    public interface IOrderRepositoryBE : IBaseRepositoryBE
    {
        List<OrderItem> GetList(out int total, int? id, string customerName, string email, string phone, int? statusId, DateTime? fromDate, DateTime? toDate, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool? isViewed, bool isDelete = false);
    }
}
