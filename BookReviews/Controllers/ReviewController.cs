﻿using BookReviews.Data;
using BookReviews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookReviews.Controllers
{
    public class ReviewController : Controller
    {
        IReviewRepository repo;
        public ReviewController(IReviewRepository r)
        {
            repo = r;
        }

        public IActionResult Index(String reviewerName, String bookTitle, String reviewDate)
        {
            List<Review> reviews;

            // reviewerName is not null
            if (reviewerName != null)
            {
                reviews = (
                    from r in repo.Reviews
                    where r.Reviewer.UserName == reviewerName
                    select r
                    ).ToList<Review>();
            }
            // bookTitle is not null
            else if (bookTitle != null)
            {
                reviews = (
                    from r in repo.Reviews
                    where r.Book.BookTitle == bookTitle
                    select r
                    ).ToList<Review>();
            }
            // reviewDate is not null
            else if (reviewDate != null)
            {
                var date = DateOnly.Parse(reviewDate);
                reviews = (
                    from r in repo.Reviews
                    where DateOnly.FromDateTime(r.ReviewDate) == date
                    select r
                    ).ToList<Review>();
            }
            // Both query parameters are null
            else
            {
                reviews = repo.Reviews.ToList<Review>();
            }

            return View(reviews);
        }



        public IActionResult Review()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Review(Review model)
        {
            if (repo.StoreReview(model) > 0)
            {
                return RedirectToAction("Index", new { reviewId = model.ReviewId });
            }
            else
            {
                return View();  // TODO: Send an error message to the view
            }

        }
    }
}
