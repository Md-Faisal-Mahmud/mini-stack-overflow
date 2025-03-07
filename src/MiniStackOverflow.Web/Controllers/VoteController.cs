﻿using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MiniStackOverflow.Web.Models;
using MiniStackOverflow.Infrastructure.Membership;


namespace StackOverflow.Web.Controllers
{
    public class VoteController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<VoteController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public VoteController(ILifetimeScope scope,
            ILogger<VoteController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> Create(VoteModel model, Guid questionId, int voteType)
        {
            model.Resolve(_scope);
            var user = await _userManager.GetUserAsync(User);
            model.UserId = user.Id;
            model.VoteType = voteType;
            if (ModelState.IsValid)
            {
                await model.CreateVoteAsync();
            }
            return Json(new { success = true, message = "Thanks for the feedback!" });
        }
    }
}