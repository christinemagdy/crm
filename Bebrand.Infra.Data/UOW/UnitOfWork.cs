//using Bebrand.Domain;
//using Bebrand.Domain.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bebrand.Infra.Data.UOW
//{
//    public class UnitOfWork : IUnitOfWork
//    {
//        public IServiceRepository _serviceRepository { get; }
//        private readonly BebrandContext _context;

//        public UnitOfWork(IServiceRepository serviceRepository, BebrandContext context)
//        {
//            _serviceRepository = serviceRepository;
//            _context = context;
//        }

//        public bool Commit()
//        {
//            return _context.SaveChanges() > 0;
//        }

//        public void Dispose()
//        {
//            _context.Dispose();
//        }
//    }
//}
