﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RewatchIt.Pages
{
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  [IgnoreAntiforgeryToken]
  public class ErrorModel : PageModel
  {
    #region Fields

    private readonly ILogger<ErrorModel> _logger;

    #endregion

    #region Constructors

    public ErrorModel(ILogger<ErrorModel> logger)
    {
      _logger = logger;
    }

    #endregion

    #region Properties

    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    #endregion

    #region Event Handlers

    public void OnGet()
    {
      RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }

    #endregion
  }
}