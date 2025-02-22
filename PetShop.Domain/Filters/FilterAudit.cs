//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace PetShop.Domain.Filters
//{
//    public class FilterAudit : IQueryObject<AuditoriaModel>
//    {
//        public DateTime? DataOcorrencia { get; set; }
//        public Dictionary<string, Expression<Func<AuditoriaModel, object>>> Map()
//        {
//            var columnsMap = new Dictionary<string, Expression<Func<AuditoriaModel, object>>>()
//            {
//                ["id"] = c => c.Id,
//            };

//            return columnsMap;
//        }
//    }
