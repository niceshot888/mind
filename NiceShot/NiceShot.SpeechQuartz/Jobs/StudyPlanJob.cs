using NiceShot.SpeechQuartz.Core;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceShot.SpeechQuartz.Jobs
{
    public class StudyPlanJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var data = context.JobDetail.JobDataMap;

            string todo = data.GetString("todo");

            return Task.Factory.StartNew(()=> {
                SpeechHelper.TheadStart(todo);
            });
        }
    }
}
