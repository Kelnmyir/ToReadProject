using System;
using System.Collections.Generic;
using System.Linq;
using ToRead.MVC.Models;
using ToRead.Data;
using ToRead.Data.Models;
using AutoMapper;

namespace ToRead.Services
{
    public class LocationService : ILocationService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(
            ILocationRepository locationRepository,
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public void Create(LocationModel model)
        {
            var location = _mapper.Map<LocationEntity>(model);
            _locationRepository.Create(location);
            foreach (BookModel bookModel in model.BookModels)
            {
                var book = _bookRepository.GetBookDetailed(bookModel.Id);
                book.Location = location;
                _bookRepository.Update(book);
            }
        }

        public void Delete(LocationModel model)
        {
            var location = _locationRepository.GetLocationDetailed(model.Id);
            foreach(var book in location.Books.ToList())
            {
                Console.WriteLine(book.Name);
                book.Location = null;
                _bookRepository.Update(book);
            }
            _locationRepository.Delete(location);
        }

        public ICollection<LocationModel> GetAllLocations()
        {
            var locations = _locationRepository.Get().ToList();
            return _mapper.Map<List<LocationEntity>, ICollection<LocationModel>>(locations);
        }

        public LocationModel GetLocation(int id)
        {
            var location = _locationRepository.GetLocationDetailed(id);
            var model = _mapper.Map<LocationModel>(location);
            model.BookModels = _mapper.Map<ICollection<BookEntity>, ICollection<BookModel>>(location.Books);
            return model;
        }

        public void Update(LocationModel model)
        {
            var location = _mapper.Map<LocationEntity>(model);
            _locationRepository.Update(location);
        }
    }
}
