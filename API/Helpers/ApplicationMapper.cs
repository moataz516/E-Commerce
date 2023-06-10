using AutoMapper;
using E_Website.Models;
using E_Website.Models.ViewModel;

namespace E_Website.Helpers
{
    public class ApplicationMapper: Profile
    {
        public ApplicationMapper()
        {
            CreateMap<product, ProductVM>().ForMember(des => des.image , opt=>opt.MapFrom(
                s=> Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(@"C:\Users\RTX\Desktop\Angular and .Net\E-Website\Resourses\Images\Products", s.image))))).ReverseMap();
        }
    }
}
