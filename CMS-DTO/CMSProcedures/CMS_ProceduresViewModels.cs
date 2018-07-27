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

        public CMS_ProceduresViewModels()
        {
            ListProcedures = new List<CMS_ProceduresModels>() {
                new CMS_ProceduresModels()
                {
                    ShortDescription = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias provident destiny is about voles.",
                    ImageUrl =CMS_Common.Commons._PublicImages+ "Procedures/popular-procedures1.jpg",
                    ProceduresName = "Aromatheraphy",
                    Preparation = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain.",
                    Process = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    SpaTreatment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    Duration = "1-2hr",
                    Price = 18000,
                },
                new CMS_ProceduresModels()
                {
                    ShortDescription = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias provident destiny is about voles.",
                    ImageUrl =CMS_Common.Commons._PublicImages+ "Procedures/popular-procedures2.jpg",
                    ProceduresName = "Skin Care",
                    Preparation = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain.",
                    Process = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    SpaTreatment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    Duration = "1-2hr",
                    Price = 20000,
                },
                new CMS_ProceduresModels()
                {
                    ShortDescription = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias provident destiny is about voles.",
                    ImageUrl = CMS_Common.Commons._PublicImages + "Procedures/popular-procedures3.jpg",
                    ProceduresName = "Herbal Spa",
                    Preparation = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain.",
                    Process = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    SpaTreatment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    Duration = "1-2hr",
                    Price = 30000,
                },
                new CMS_ProceduresModels()
                {
                    ShortDescription = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias provident destiny is about voles.",
                    ImageUrl = CMS_Common.Commons._PublicImages + "Procedures/popular-procedures1.jpg",
                    ProceduresName = "Drinking cures",
                    Preparation = "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain.",
                    Process = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    SpaTreatment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi hendrerit elit turpis, a porttitor tellus sollicitudin at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
                    Duration = "1-2hr",
                    Price = 18000,
                }

            };
        }
    }
}
