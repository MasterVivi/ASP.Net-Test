using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPIASP.Models;

namespace TestAPIASP.Controllers
{
    [Route("v1/trackeditems")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly TrackingContext _context;

        public TrackingController(TrackingContext context)
        {
            _context = context;

            if (_context.TrackedItems.Count() == 0)
            {
                // Create a new TrackingItem if collection is empty,
                // which means you can't delete all TrackedItems.
                _context.TrackedItems.Add(new TrackingItem { Title = "Phone" });
                _context.SaveChanges();
            }
        }

        // GET: v1/trackeditems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackingItem>>> GetTrackedItems()
        {
            return await _context.TrackedItems.ToListAsync();
        }

        // GET: v1/trackeditems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackingItem>> GetTrackedItem(long id)
        {
            var trackedItem = await _context.TrackedItems.FindAsync(id);
            if (trackedItem == null)
            {
                return NotFound();
            }

            return trackedItem;
        }

        // POST: v1/trackeditems
        [HttpPost]
        public async Task<ActionResult<TrackingItem>> PostTrackingItem(TrackingItem trackingItem)
        {
            _context.TrackedItems.Add(trackingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrackedItem", new { id = trackingItem.Id }, trackingItem);
        }

        // PUT: v1/trackeditems/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTrackingItem(long id, TrackingItem trackingItem)
        {
            if (id != trackingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(trackingItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: v1/trackeditems/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<TrackingItem>> DeleteTrackingItem(long id)
        {
            var trackingItem = await _context.TrackedItems.FindAsync(id);
            if (trackingItem == null)
            {
                return NotFound();
            }

            _context.TrackedItems.Remove(trackingItem);
            await _context.SaveChangesAsync();

            return trackingItem;
        }
    }
}