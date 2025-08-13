using App.Xuatnhapcanh.Dal.DatabaseSpecific;
using App.Xuatnhapcanh.Dal.EntityClasses;
using App.Xuatnhapcanh.Dal.HelperClasses;
using App.Xuatnhapcanh.Services;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System.Linq;

namespace HieuTest.Manager
{
    public class CustomsAnswerOptionManager
    {
        public IEnumerable<CustomsAnswerOptionEntity> SelectAll()
        {
            var toReturn = new EntityCollection<CustomsAnswerOptionEntity>();
            using (DataAccessAdapterBase dbAdapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    // Không filter => lấy tất cả
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)
                };
                dbAdapter.FetchEntityCollection(parameters);
            }
            return toReturn; // IEnumerable<CustomsAnswerOptionEntity>
        }

        public CustomsAnswerOptionEntity? SelectOne(string id)
        {
            var toReturn = new EntityCollection<CustomsAnswerOptionEntity>();
            using var dbAdapter = (new DataAccessAdapterFactory()).CreateAdapter();
            var parameters = new QueryParameters
            {
                CollectionToFetch = toReturn,
                FilterToUse = CustomsAnswerOptionFields.Id == id,
                CacheResultset = true,
                CacheDuration = TimeSpan.FromSeconds(10)
            };
            dbAdapter.FetchEntityCollection(parameters);
            return (toReturn != null && toReturn.Items.Count > 0) ? toReturn.Items[0] : null;
        }

        public bool Insert(CustomsAnswerOptionEntity entity)
        {
            using var dbAdapter = (new DataAccessAdapterFactory()).CreateAdapter();

            // Nếu bạn muốn bắt buộc phải có QuestionId:
            if (string.IsNullOrWhiteSpace(entity.QuestionId))
                throw new ArgumentException("QuestionId is required.");

            // Nếu QuestionId != null => xác thực tồn tại và gắn navigation
            if (!string.IsNullOrWhiteSpace(entity.QuestionId))
            {
                var parent = new CustomsQuestionEntity(entity.QuestionId);
                if (!dbAdapter.FetchEntity(parent))
                    throw new InvalidOperationException($"CustomsQuestion '{entity.QuestionId}' not found.");

                entity.CustomsQuestion = parent; // đảm bảo FK hợp lệ
            }

            dbAdapter.SaveEntity(entity, true);
            return true;
        }

        public bool Update(CustomsAnswerOptionEntity entity)
        {
            using var dbAdapter = (new DataAccessAdapterFactory()).CreateAdapter();

            // Nếu người dùng đổi QuestionId
            if (!string.IsNullOrWhiteSpace(entity.QuestionId))
            {
                var parent = new CustomsQuestionEntity(entity.QuestionId);
                if (!dbAdapter.FetchEntity(parent))
                    throw new InvalidOperationException($"CustomsQuestion '{entity.QuestionId}' not found.");
                entity.CustomsQuestion = parent;
            }
            else
            {
                // Cho phép clear FK (vì cột nullable). Nếu không muốn, ném lỗi như Insert.
                entity.CustomsQuestion = null;
            }

            dbAdapter.SaveEntity(entity, false);
            return true;
        }

        public bool Delete(string id)
        {
            using var dbAdapter = (new DataAccessAdapterFactory()).CreateAdapter();
            var entity = SelectOne(id);
            if (entity is null) return false;
            dbAdapter.DeleteEntity(entity);
            return true;
        }
    }

}
