using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData JobData;

        static JobController()
        {
            JobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            Job job = JobData.Find(id);
            return View(job);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
           if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = JobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = JobData.Locations.Find(newJobViewModel.LocationID),
                    CoreCompetency = JobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
                    PositionType = JobData.PositionTypes.Find(newJobViewModel.PositionID)
                };

                JobData.Jobs.Add(newJob);
                return Redirect("/Job?id="+newJob.ID);    
            }          
                    
                    
            
            return View(newJobViewModel);
        }
    }
}
