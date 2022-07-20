﻿using Bebrand.Application.ViewModels.VacanciesMail;
using Bebrand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels.JobsView
{
    public class JobsViewModel
    {
        public Guid Id { get; set; }
        public string JobsTitle { get; set; }
        public List<VacanciesMailViewModel> vacanciesMails { get; set; }

    }
}
