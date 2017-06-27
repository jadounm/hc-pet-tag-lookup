using System;
using System.Collections.Generic;
// using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Cors;
using WebTagLookup.Models;

namespace WebTagLookup.Controllers.Api
{
	[Route("api/[controller]")]
	// [EnableCors("AllowSpecificOrigins")]
	public class ApiAnimalsController : Controller
	{

		private readonly int _maxResults;
		
		public PostsController(PostContext context)
		{
			_maxResults = 10;
		}
		
		// Get /api/search/:term?page=1
		[HttpGet]
		public IEnumerable<TagData> Search(string term=null, int page=1)
		{
			if (!term) {
				return NotFound();
			}
			// var offset = (page - 1) * _maxResults;
			// HttpContext.Response.Headers.Add("totalCount", _context.Posts.Count().ToString());
			// return _context.Posts.Skip(offset).Take(_maxResults).ToList();
			// return _context.Posts.ToList();
		}
		
	}
}
