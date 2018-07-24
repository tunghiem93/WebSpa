using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSCategories
{
    public class CMS_CategoryViewModels
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int TotalProduct { get; set; }
        public List<CMS_CategoryViewModels> Children { get; set; }
        public CMS_CategoryViewModels()
        {
            Children = new List<CMS_CategoryViewModels>();
        }
    }
}
