﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Domain.Service;
using Web365Utility;

namespace Web365Business.Back_End.Repository
{
    public class ServiceRepositoryBE : BaseBE<tblService>, IServiceRepositoryBE
    {
        private readonly IServiceDABERepository serviceDABERepository;

        public ServiceRepositoryBE(IServiceDABERepository _serviceDABERepository)
            : base(_serviceDABERepository)
        {
            serviceDABERepository = _serviceDABERepository;
        }

        /// <summary>
        /// function get all data tblArticle
        /// </summary>
        /// <returns></returns>
        public List<ServiceItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return serviceDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }
    }
}
