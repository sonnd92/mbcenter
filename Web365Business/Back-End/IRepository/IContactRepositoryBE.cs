﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365Business.Back_End.IRepository
{
    public interface IContactRepositoryBE : IBaseRepositoryBE
    {
        List<ContactItem> GetList(out int total, string name, string title, DateTime? fromDate, DateTime? toDate, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false);  
    }
}
