using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.Persistence;
using Excellency.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Excellency
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<ICompany, CompanyService>();
            services.AddScoped<IBranch, BranchService>();
            services.AddScoped<IDepartment, DepartmentService>();
            services.AddScoped<IPosition, PositionService>();
            services.AddScoped<IModule, ModuleService>();
            services.AddScoped<IUserAccount, AccountService>();
            services.AddScoped<IEmployee, EmployeeService>();
            services.AddScoped<IRaterAssignment, RaterAssignmentService> ();
            services.AddScoped<IRating, RatingService> ();
            services.AddScoped<IKeyResultArea, KeyResultAreaService> ();
            services.AddScoped<IRatingTable, RatingTableService> ();
            services.AddScoped<IEvaluation, EvaluationService> ();
            services.AddScoped<IBehavioralFactor, BehavioralFactorService>();
            services.AddScoped<IEmployeeCategory, EmployeeCategoryService>();
            services.AddScoped<IEmployeeAssignment, EmployeeAssignmentService>();
            services.AddScoped<IApproverAssignment, ApproverAssignmentService>();
            services.AddScoped<IEvaluationApproval, EvaluationApprovalService>();
            services.AddScoped<IAccountRole, AccountRoleService>();
            services.AddScoped<IHome, HomeService>();
            services.AddScoped<IPeerCriteria, PeerCriteriaService>();
            services.AddScoped<IInterpretation, InterpretationService>();
            services.AddScoped<IEvaluationReport,EvaluationReportService>();
            services.AddScoped<IPeerEvaluation, PeerEvaluationService>();
            services.AddScoped<IApprovalLevel, ApprovalLevelService>();
            services.AddScoped<IApproval, ApprovalService>();
            services.AddScoped<IPeerAssignment, PeerAssignmentService>();
            services.AddScoped<IRecommendation, RecommendationService>();
            services.AddScoped<IEvaluationSeason, EvaluationSeasonService>();
            services.AddScoped<IEvaluationInformation, EvaluationInformationService>();
            services.AddScoped<IUserAccountNew, UserAccountService>();
            services.AddScoped<IRecommendationAssignment, RecommendationAssignmentService>();
            services.AddScoped<IReport, ReportService>();
            services.AddDbContext<EASDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ExcellencyConnection")));


            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Login}/{id?}");
            });
        }
        
    }
}
