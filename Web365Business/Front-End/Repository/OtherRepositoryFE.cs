using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Domain.Service;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class OtherRepositoryFE : BaseFE, IOtherRepositoryFE
    {
        private readonly IOtherDAFERepository otherDAFERepository;

        public OtherRepositoryFE(IOtherDAFERepository _otherDAFERepository)
        {
            otherDAFERepository = _otherDAFERepository;
        }

        public bool AddContact(tblContact contact)
        {
            return otherDAFERepository.AddContact(contact);   
        }

        public void AddEmailPromotion(tblReceiveInfo info)
        {
            otherDAFERepository.AddEmailPromotion(info);   
        }

        public List<ServiceItem> GetServiceItems()
        {
            var key = string.Format("OtherRepositoryFE{0}", "GetOtherArticle");

            var item = new List<ServiceItem>();

            var isDataCache = TryGetCache<List<ServiceItem>>(out item, key);

            if (!isDataCache)
            {
                item = otherDAFERepository.GetServiceItems();
            }

            SetCache(key, item, isDataCache, ConfigCache.Cache10Minute);

            return item;  
        }
        public ServiceItem GetServiceItemByNameascii(string nameAscii)
        {
            var key = string.Format("OtherRepositoryFE{0}{1}", "GetServiceItemByNameascii", nameAscii);

            var item = new ServiceItem();

            var isDataCache = TryGetCache<ServiceItem>(out item, key);

            if (!isDataCache)
            {
                item = otherDAFERepository.GetServiceItemByNameascii(nameAscii);
            }

            SetCache(key, item, isDataCache, ConfigCache.Cache10Minute);

            return item;  
        }

        public List<StepOfServiceItem> GetStepOfServiceItems(int id)
        {
            var key = string.Format("OtherRepositoryFE{0}{1}", "GetStepOfServiceItems", id);

            var item = new List<StepOfServiceItem>();

            var isDataCache = TryGetCache < List<StepOfServiceItem>>(out item, key);

            if (!isDataCache)
            {
                item = otherDAFERepository.GetStepOfServiceItems(id);
            }

            SetCache(key, item, isDataCache, ConfigCache.Cache10Minute);

            return item;  
        }

        public List<GroupOfServiceItem> GetDetailServiceItems(int id)
        {
            var key = string.Format("OtherRepositoryFE{0}{1}", "GetDetailServiceItems", id);

            var item = new List<GroupOfServiceItem>();

            var isDataCache = TryGetCache<List<GroupOfServiceItem>>(out item, key);

            if (!isDataCache)
            {
                item = otherDAFERepository.GetDetailServiceItems(id);
            }

            SetCache(key, item, isDataCache, ConfigCache.Cache10Minute);

            return item;  
        }        
    }
}
