using App.Xuatnhapcanh.Dal.DatabaseSpecific;
using App.Xuatnhapcanh.Dal.EntityClasses;
using App.Xuatnhapcanh.Dal.HelperClasses;
using App.Xuatnhapcanh.Services;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace HieuTest.Manager
{
    public class CustomsAnswerOptionManager
    {
        public CustomsAnswerOptionEntity SelectOne(string _id)
        {
            var toReturn = new EntityCollection<CustomsAnswerOptionEntity>();
            using (DataAccessAdapterBase dbAdapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = CustomsAnswerOptionFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)
                };
                dbAdapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return new CustomsAnswerOptionEntity();
        }

        public bool Insert(CustomsAnswerOptionEntity entity)
        {
            using (DataAccessAdapterBase dbAdapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                dbAdapter.SaveEntity(entity, true);
                return true;
            }
        }

        public bool Update(CustomsAnswerOptionEntity entity)
        {
            using (DataAccessAdapterBase dbAdapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                dbAdapter.SaveEntity(entity, false);
                return true;
            }
        }

        public bool Delete(string id)
        {
            using (DataAccessAdapterBase dbAdapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var entity = SelectOne(id);
                if (entity == null || string.IsNullOrEmpty(entity.Id))
                    return false;
                dbAdapter.DeleteEntity(entity);
                return true;
            }
        }
    }

}
