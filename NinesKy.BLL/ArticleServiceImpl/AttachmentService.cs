using NinesKy.DAL.Repository;
using NinesKy.IBLL.ArticleService;
using NinesKy.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.BLL.ArticleServiceImpl
{
    public class AttachmentService : BaseService<Attachment>,InterfaceAttachmentService
    {
        public AttachmentService() : base(RepositoryFactory.AttachmentRepository) { }

        public IQueryable<Attachment> FindList(int? modelID, string owner, string type)
        {
            var _attachment = CurrentRepository.Entities.Where(a => a.ModelID == modelID);
            if (!string.IsNullOrEmpty(owner)) _attachment = _attachment.Where(a => a.Owner == owner);
            if (!string.IsNullOrEmpty(type)) _attachment = _attachment.Where(a => a.Type == type);
            return _attachment;
        }

        public IQueryable<Attachment> FindList(int modelID, string owner, string type, bool withModelIDNull)
        {
            var _attachment = CurrentRepository.Entities;
            if (withModelIDNull) _attachment = _attachment.Where(a => a.ModelID == modelID || a.ModelID == null);
            else _attachment = _attachment.Where(a => a.ModelID == modelID);
            if (!string.IsNullOrEmpty(owner)) _attachment = _attachment.Where(a => a.Owner == owner);
            if (!string.IsNullOrEmpty(type)) _attachment = _attachment.Where(a => a.Type == type);
            return _attachment;
        }
    }
}
