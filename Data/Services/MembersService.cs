﻿using Microsoft.EntityFrameworkCore;

using RopeyDVDSystem.Data;
using RopeyDVDSystem.Models.ViewModels;

namespace RopeyDVDSystem.Data.Services
{
    public class MembersService : IMembersService
    {
        private ApplicationDbContext _context;
        public MembersService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<MemberDetailViewModel>> GetAllDetailsAsync()
        {
            var result = await (from m in _context.Members
                                join mc in _context.MembershipCategories on m.MemberCategoryNumber equals mc.MembershipCategoryNumber
                                orderby m.MemberFirstName, m.MemberLastName
                                select new MemberDetailViewModel
                                {
                                    MemberNumber = m.MemberNumber,
                                    MembershipCategory = mc.MembershipCategoryName,
                                    MemberFirstName = m.MemberFirstName,
                                    MemberLastName = m.MemberLastName,
                                    LoanCount = (from l in _context.Loans
                                                 where l.MemberNumber == m.MemberNumber && l.DateReturn == DateTime.MinValue
                                                 select l.LoanNumber).Count(),
                                    Remarks = (from l in _context.Loans
                                               where l.MemberNumber == m.MemberNumber && l.DateReturn == DateTime.MinValue
                                               select l.LoanNumber).Count() > (int)mc.MembershipCategoryTotalLoans ? "Too many DVDs" : null
                                }).ToListAsync();

            return result;
        }


        public async Task<IEnumerable<MemberDetailViewModel>> GetAllDetailsAsync(int id, string lastName)
        {
            var result = await (from m in _context.Members
                                join mc in _context.MembershipCategories on m.MemberCategoryNumber equals mc.MembershipCategoryNumber
                                where m.MemberNumber == id || m.MemberLastName.Contains(lastName)
                                orderby m.MemberFirstName, m.MemberLastName
                                select new MemberDetailViewModel
                                {
                                    MemberNumber = m.MemberNumber,
                                    MembershipCategory = mc.MembershipCategoryName,
                                    MemberFirstName = m.MemberFirstName,
                                    MemberLastName = m.MemberLastName,
                                    LoanCount = (from l in _context.Loans
                                                 where l.MemberNumber == m.MemberNumber && l.DateReturn == DateTime.MinValue
                                                 select l.LoanNumber).Count(),
                                    Remarks = (from l in _context.Loans
                                               where l.MemberNumber == m.MemberNumber && l.DateReturn == DateTime.MinValue
                                               select l.LoanNumber).Count() > (int)mc.MembershipCategoryTotalLoans ? "Too many DVDs" : null
                                }).ToListAsync();

            return result;
        }


    }
}