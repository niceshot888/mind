using NiceShot.SpeechQuartz.Core;
using NiceShot.SpeechQuartz.Jobs;
using NiceShot.SpeechQuartz.Models;
using Quartz;
using Quartz.Impl;
using SpeechLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiceShot.SpeechQuartz
{
    public partial class MainForm : Form
    {
        private SpeechVoiceSpeakFlags _spFlags;
        private SpVoice _voice;

        public MainForm()
        {
            InitializeComponent();

            _voice = new SpVoice();
            _spFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;


            cbxSexy.Items.Clear();
            var deslist = GetVoiceDescriptions();
            foreach (var des in deslist)
            {
                cbxSexy.Items.Add(des);
            }
            cbxSexy.SelectedIndex = 0;

            //_voice.Voice = _voice.GetVoices("", "").Item(cbxSexy.SelectedIndex);
            //_voice.Voice.GetDescription(cbxlang.SelectedIndex);
        }

        private async void btnSpeech_Click(object sender, EventArgs e)
        {
            //SpeechHelper.TheadStart(tbxContent.Text);
            //await SpeechToDo();
        }

        public async Task SpeechToDo()
        {

            var todoList = new List<StudyPlanModel>() {
                new StudyPlanModel{ Date="8:30", ToDo="起床"},
                new StudyPlanModel{ Date="9:00–9:30", ToDo="读书（日有所诵）"},
                new StudyPlanModel{ Date="9:40–10:40", ToDo="自学语文（预习）"},
                new StudyPlanModel{ Date="11:00–11:30", ToDo="写作业/阅读/英语"},
                new StudyPlanModel{ Date="12:40–13:10", ToDo="写字"},
                new StudyPlanModel{ Date="13:00–14:00", ToDo="休息"},
                new StudyPlanModel{ Date="14:10–15:10", ToDo="自学数学（预习）"},
                new StudyPlanModel{ Date="15:20–15:50", ToDo="运动/休闲时间"},
                new StudyPlanModel{ Date="16:00–17:00", ToDo="阅读/画画/手工"},
                new StudyPlanModel{ Date="19:20–20:20", ToDo="读书（史记）"},
                new StudyPlanModel{ Date="20:40–21:00", ToDo="洗澡"},
                new StudyPlanModel{ Date="21:30", ToDo="睡觉"},
            };

            todoList = new List<StudyPlanModel>() {
                new StudyPlanModel{ Date="23:13", ToDo="起床"},
                new StudyPlanModel{ Date="23:14–23:15", ToDo="读书（日有所诵）"},
                new StudyPlanModel{ Date="20:42–20:43", ToDo="自学语文（预习）"},
                new StudyPlanModel{ Date="20:43–20:44", ToDo="写作业/阅读/英语"},
                new StudyPlanModel{ Date="20:44–20:45", ToDo="写字"},
                new StudyPlanModel{ Date="20:45–20:46", ToDo="休息"},
                new StudyPlanModel{ Date="20:46–20:47", ToDo="自学数学（预习）"},
                new StudyPlanModel{ Date="20:47–20:48", ToDo="运动/休闲时间"},
                new StudyPlanModel{ Date="20:48–20:49", ToDo="阅读/画画/手工"},
                new StudyPlanModel{ Date="20:49–20:50", ToDo="读书（史记）"},
                new StudyPlanModel{ Date="20:50–20:51", ToDo="洗澡"},
                new StudyPlanModel{ Date="20:51", ToDo="睡觉"},
            };

            var factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();

            await scheduler.Start();

            var index = 0;
            foreach (var model in todoList)
            {
                index++;
                var date = model.Date.Split('–')[0].Split(':');
                var hour = int.Parse(date[0]);
                var minute = int.Parse(date[1]);

                var job = JobBuilder.Create<StudyPlanJob>()
                .WithIdentity("job" + index, "group1")
                .Build();
                job.JobDataMap.Put("todo", model.ToDo);

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger" + index, "group1")
                    .WithCronSchedule(string.Format("0 {1} {0} * * ? *", hour, minute))
                    //.WithCronSchedule("0 18 16 ? * SAT-SUN")
                    //.StartNow()
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            //_voice.Voice = _voice.GetVoices("", "").Item(cbxSexy.SelectedIndex);
            var des = cbxSexy.SelectedItem.ToString();
            SetVoiceDescription(des);
            _voice.Voice.GetDescription(cbxlang.SelectedIndex);

            _voice.Speak(tbxContent.Text, _spFlags);
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            _voice.Pause();
        }

        private void Resume_Click(object sender, EventArgs e)
        {
            _voice.Resume();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            _voice.Speak(string.Empty, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
        }


        /// <summary>
        /// 获取语音库
        /// </summary>
        /// <returns>List<string></returns>
        public List<string> GetVoiceDescriptions()
        {
            List<string> list = new List<string>();
            /*var voices = _voice.GetVoices();
            int count = voices.Count;
            for (int i = 0; i < count; i++)
            {
                string desc = voices.Item(i).GetDescription(); 
                list.Add(desc);
                Console.WriteLine(desc);
            }*/
            list.Add("Microsoft Huihui Desktop - Chinese (Simplified)");
            list.Add("Microsoft Simplified Chinese");
            list.Add("Microsoft Zira Desktop - English (United States)");
            return list;
        }

        public bool SetVoiceDescription(string name)
        {
            //List<string> list = new List<string>();
            var voices = _voice.GetVoices();
            int count = voices.Count;//获取语音库总数
            bool result = false;
            for (int i = 0; i < count; i++)
            {
                string desc = voices.Item(i).GetDescription(); //遍历语音库
                if (desc.Equals(name))
                {
                    _voice.Voice = voices.Item(i);
                    result = true;
                }
            }
            return result;
        }

        private void cbxlang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lang = cbxlang.SelectedItem.ToString();
            if (lang == "中文")
                cbxSexy.SelectedIndex = 0;
            else if (lang == "english")
                cbxSexy.SelectedIndex = 2;
        }
    }
}
