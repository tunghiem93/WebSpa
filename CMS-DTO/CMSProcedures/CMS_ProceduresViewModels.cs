using CMS_DTO.CMSDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSProcedures
{
    public class CMS_ProceduresViewModels
    {
        public List<CMS_ProceduresModels> ListProcedures { get; set; }
        public List<CMS_DiscountModels> LstDiscount { get; set; }

        public CMS_ProceduresViewModels()
        {
            LstDiscount = new List<CMS_DiscountModels>();
            ListProcedures = new List<CMS_ProceduresModels>() {
                new CMS_ProceduresModels()
                {
                    ShortDescription = "Trong cuộc sống nhộn nhịp với bao lo toan, các chị em phụ nữ nên dành cho mình ít thời gian massage thư giãn làm đẹp, đồng thời massage cũng  kích thích các nguyệt đạo cơ thể giúp giảm stress cũng như làm lưu thông mạch máu cơ thể.",
                    ImageUrl =CMS_Common.Commons._PublicImages+ "Procedures/popular-procedures1.jpg",
                    ProceduresName = "Massage",
                    Preparation = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain.",
                    Process = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    SpaTreatment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    Duration = "1-2 giờ",
                    Price = 180000,
                },
                new CMS_ProceduresModels()
                {
                    ShortDescription = "Thanh lọc da tiêu chuẩn, Cung cấp sức sống cho làn da, Liệu pháp giảm căng thẳng bằng hương thơm, Chăm sóc da cơ bản bằng vitamin C, Chăm sóc da khô và lão hóa bằng vitamin C, Chăm sóc da bằng thuốc bắc, Chăm sóc da bằng mật ong sữa chua.",
                    ImageUrl =CMS_Common.Commons._PublicImages+ "Procedures/popular-procedures2.jpg",
                    ProceduresName = "Chăm sóc da",
                    Preparation = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain.",
                    Process = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    SpaTreatment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    Duration = "1-2 giờ",
                    Price = 200000,
                },
                new CMS_ProceduresModels()
                {
                    ShortDescription = "Thảo dược là một trong những bí quyết làm đẹp an toàn, hiệu quả đến từ tự nhiên. Thành phần khoáng chất, dinh dưỡng chứa trong thảo dược giúp duy trì độ săn chắc của làn da, giúp tái tạo các tế bào mới, cho làn da tươi sáng và khoẻ mạnh hơn.",
                    ImageUrl = CMS_Common.Commons._PublicImages + "Procedures/popular-procedures3.jpg",
                    ProceduresName = "Liệu trình thảo dược",
                    Preparation = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain.",
                    Process = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    SpaTreatment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    Duration = "1-2 giờ",
                    Price = 300000,
                },
                new CMS_ProceduresModels()
                {
                    ShortDescription = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias provident destiny is about voles.",
                    ImageUrl = CMS_Common.Commons._PublicImages + "Procedures/popular-procedures1.jpg",
                    ProceduresName = "Thực dưỡng",
                    Preparation = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain.",
                    Process = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    SpaTreatment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    Duration = "1-2 giờ",
                    Price = 180000,
                }

            };
        }
    }
}
