﻿using Microsoft.AspNetCore.Mvc;

using Route = Domic.Common.ClassConsts.Route;

namespace Domic.WebAPI.EntryPoints.HTTPs.BackOffice;

[Route(Route.BaseBackOfficeUrl + Route.BaseTicketUrl)]
public class BaseTicketController : ControllerBase;