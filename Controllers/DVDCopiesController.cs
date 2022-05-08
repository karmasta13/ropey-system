using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RopeyDVDSystem.Data;

namespace RopeyDVDSystem.Controllers
{
    public class DVDCopiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DVDCopiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allDVDCopies = await _context.DVDCopies.ToListAsync();
            return View();
        }

        public async Task<IActionResult> OlderThan365Days()
        {

            //Get Distinct Copy Numbers of Loaned DVDs
            var loanedCopy = (from loan in _context.Loans
                                 where loan.DateReturn == null
                                 select loan.CopyNumber).Distinct();

            //Get Data of Copies that have not been loaned
            var notLoanedCopy = (from copy in _context.DVDCopies
                                    join dvdtitle in _context.DVDTitles on copy.DVDNumber equals dvdtitle.DVDNumber
                                    where !(loanedCopy).Contains(copy.CopyNumber)
                                    select new
                                    {
                                        CopyNumber = copy.CopyNumber,
                                        DVDTitle = dvdtitle.DVDTitleName,
                                        DatePurchased = copy.DatePurchased
                                    });

            return View(await notLoanedCopy.ToListAsync());
        }

        // GET: DVDCopy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCopyModel = await _context.DVDCopies
                .Include(d => d.DVDTitle)
                .FirstOrDefaultAsync(m => m.CopyNumber == id);
            if (dVDCopyModel == null)
            {
                return NotFound();
            }

            return View(dVDCopyModel);
        }

        // POST: DVDCopy/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dVDCopyModel = await _context.DVDCopies.FindAsync(id);
            _context.DVDCopies.Remove(dVDCopyModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(OlderThan365Days));
        }

        public async Task<IActionResult> RemoveAll()
        {
            //Get Distinct Copy Numbers of Loaned DVDs
            var loanedCopy = (from loan in _context.Loans
                                 where loan.DateReturn == null
                                 select loan.CopyNumber).Distinct();

            //Get Data of Copies that have not been loaned
            var notLoanedCopy = (from copy in _context.DVDCopies
                                    join dvdtitle in _context.DVDTitles on copy.DVDNumber equals dvdtitle.DVDNumber
                                    where !(loanedCopy).Contains(copy.CopyNumber)
                                    select new
                                    {
                                        CopyNumber = copy.CopyNumber,
                                        DVDTitle = dvdtitle.DVDTitleName,
                                        DatePurchased = copy.DatePurchased
                                    });

            foreach (var copy in notLoanedCopy.ToList())
            {
                //Check if the Copy is older than one year.
                if (DateTime.Now.Subtract(copy.DatePurchased).Days > 365)
                {
                    //Remove the Copy from the Database if the Copy is older than one year.
                    var remove = (from removeCopy in _context.DVDCopies
                                  where removeCopy.CopyNumber == copy.CopyNumber
                                  select removeCopy).FirstOrDefault();
                    _context.DVDCopies.Remove(remove);

                }
            }
            //Save the changes made to the database.
            _context.SaveChanges();
            return RedirectToAction("OlderThan365Days");
        }
    }
}
