using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {

        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            // Executa a consulta sem GroupBy
            var result = await _context.SalesRecord
                .Include(x => x.Seller)
                .ThenInclude(s => s.Department)
                .Where(x => !minDate.HasValue || x.Date >= minDate.Value)
                .Where(x => !maxDate.HasValue || x.Date <= maxDate.Value)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            // Realiza o GroupBy em memória
            return result
                .GroupBy(x => x.Seller.Department)
                .ToList();
        }

    }
}
