using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public int pageSize = 3;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        public HomeController(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        // localhost:5000/?page=2
        public IActionResult Index(string category, int page = 1)
        {
            return View(new ProductListViewModel
            {
                Products = _storeRepository.GetProductsByCategory(category, page, pageSize)
                    .Select(product => _mapper.Map<ProductViewModel>(product)),
                //Select(p => _mapper.Map<ProductViewModel>(p)), olabilir çünü product sýnýfýnýn bir nesnesi p
                PageInfo = new PageInfo
                {
                    ItemsPerPage = pageSize,
                    CurrentPage = page,
                    TotalItems = _storeRepository.GetProductCount(category)
                }
            });

        }


        /*    localhost:5000/?page=2
        public IActionResult Index(string category, int page = 1)
        {
            return View(new ProductListViewModel
            {
                Products = _storeRepository.GetProductsByCategory(category, page, pageSize).Select(p =>
                            new ProductViewModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Description = p.Description,
                                Price = p.Price
                            }),
                PageInfo = new PageInfo
                {
                    ItemsPerPage = pageSize,
                    CurrentPage = page,
                    TotalItems = _storeRepository.GetProductCount(category)
                }
            });

        }*/
    }
}
