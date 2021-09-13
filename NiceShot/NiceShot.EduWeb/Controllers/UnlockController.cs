using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using NiceShot.EduWeb.Models;

namespace NiceShot.EduWeb.Controllers
{
    public class UnlockController : Controller
    {
        public ActionResult Basic()
        {
            return View();
        }

        public ActionResult L1()
        {
            return View();
        }

        public ActionResult L2()
        {
            return View();
        }

        public ActionResult L3()
        {
            return View();
        }

        public ActionResult L4()
        {
            return View();
        }

        public ActionResult L5()
        {
            return View();
        }

        public ActionResult GetLevel5FileList(string book_type, string media_type, string unit)
        {
            var webRootDirPath = AppDomain.CurrentDomain.BaseDirectory;
            var level = 5;
            var rootDir = Path.Combine(webRootDirPath, @"unlock2019\Media\L{2}\Unlock 2e {0} {2}\Class {1}");
            rootDir = string.Format(rootDir, book_type, media_type, level);

            var rootDirInfo = new DirectoryInfo(rootDir);

            FileInfo[] files;
            if (media_type == "Audio")
                files = rootDirInfo.GetFiles(string.Format("*Unlock2e_C1_{0}.*", unit));
            else
            {
                if (int.Parse(unit) <= 9)
                    files = rootDirInfo.GetFiles(string.Format("*Unit 0{0}.*", unit));
                else
                    files = rootDirInfo.GetFiles(string.Format("*Unit {0}.*", unit));
            }

            var musicList = new List<MusicInfo>();
            foreach (var file in files)
            {
                var url = file.FullName.Replace(webRootDirPath, "").Replace("\\", "/");

                var sort = 0;
                if (media_type == "Audio")
                    sort = int.Parse(file.Name.Replace("Unlock2e_C1_" + unit + ".", "").Replace(".mp3", ""));
                musicList.Add(new MusicInfo { Name = file.Name, Url = url, Sort = sort });
            }

            if (media_type == "Audio")
                musicList = musicList.OrderBy(m => m.Sort).ToList();

            return Json(musicList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLevel4FileList(string book_type, string media_type, string unit)
        {
            var webRootDirPath = AppDomain.CurrentDomain.BaseDirectory;
            var level = 4;
            var rootDir = Path.Combine(webRootDirPath, @"unlock2019\Media\L{2}\Unlock 2e {0} {2}\Class {1}");
            rootDir = string.Format(rootDir, book_type, media_type, level);

            var rootDirInfo = new DirectoryInfo(rootDir);

            FileInfo[] files;
            if (media_type == "Audio")
                files = rootDirInfo.GetFiles(string.Format("*Unlock2e_B2_{0}.*", unit));
            else
                files = rootDirInfo.GetFiles(string.Format("*Unit {0}_*", unit));

            var musicList = new List<MusicInfo>();
            foreach (var file in files)
            {
                var url = file.FullName.Replace(webRootDirPath, "").Replace("\\", "/");

                var sort = 0;
                if (media_type == "Audio")
                    sort = int.Parse(file.Name.Replace("Unlock2e_B2_" + unit + ".", "").Replace(".mp3", ""));
                musicList.Add(new MusicInfo { Name = file.Name, Url = url, Sort = sort });
            }

            if (media_type == "Audio")
                musicList = musicList.OrderBy(m => m.Sort).ToList();

            return Json(musicList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLevel3FileList(string book_type, string media_type, string unit)
        {
            var webRootDirPath = AppDomain.CurrentDomain.BaseDirectory;
            var level = 3;
            var rootDir = Path.Combine(webRootDirPath, @"unlock2019\Media\L{2}\Unlock 2e {0} {2}\Class {1}");
            rootDir = string.Format(rootDir, book_type, media_type, level);

            var rootDirInfo = new DirectoryInfo(rootDir);

            FileInfo[] files;
            if (media_type == "Audio")
                files = rootDirInfo.GetFiles(string.Format("*Unlock2e_B1_{0}.*", unit));
            else
                files = rootDirInfo.GetFiles(string.Format("*Unit {0}_*", unit));

            var musicList = new List<MusicInfo>();
            foreach (var file in files)
            {
                var url = file.FullName.Replace(webRootDirPath, "").Replace("\\", "/");

                var sort = 0;
                if (media_type == "Audio")
                    sort = int.Parse(file.Name.Replace("Unlock2e_B1_" + unit + ".", "").Replace(".mp3", ""));
                musicList.Add(new MusicInfo { Name = file.Name, Url = url, Sort = sort });
            }

            if (media_type == "Audio")
                musicList = musicList.OrderBy(m => m.Sort).ToList();

            return Json(musicList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLevel2FileList(string book_type, string media_type, string unit)
        {
            var webRootDirPath = AppDomain.CurrentDomain.BaseDirectory;
            var level = 2;
            var rootDir = Path.Combine(webRootDirPath, @"unlock2019\Media\L{2}\Unlock 2e {0} {2}\Class {1}");
            rootDir = string.Format(rootDir, book_type, media_type, level);

            var rootDirInfo = new DirectoryInfo(rootDir);

            FileInfo[] files;
            if (media_type == "Audio")
                files = rootDirInfo.GetFiles(string.Format("*Unlock2e_L2_{0}.*", unit));
            else
                files = rootDirInfo.GetFiles(string.Format("*Unit {0}_*", unit));

            var musicList = new List<MusicInfo>();
            foreach (var file in files)
            {
                var url = file.FullName.Replace(webRootDirPath, "").Replace("\\", "/");

                var sort = 0;
                if (media_type == "Audio")
                    sort = int.Parse(file.Name.Replace("Unlock2e_L2_" + unit + ".", "").Replace(".mp3", ""));
                musicList.Add(new MusicInfo { Name = file.Name, Url = url, Sort = sort });
            }

            if (media_type == "Audio")
                musicList = musicList.OrderBy(m => m.Sort).ToList();

            return Json(musicList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLevel1FileList(string book_type, string media_type, string unit)
        {
            var webRootDirPath = AppDomain.CurrentDomain.BaseDirectory;
            var level = 1;
            var rootDir = Path.Combine(webRootDirPath, @"unlock2019\Media\L{2}\Unlock 2e {0} {2}\Class {1}");
            rootDir = string.Format(rootDir, book_type, media_type,level);

            var rootDirInfo = new DirectoryInfo(rootDir);

            FileInfo[] files;
            if (media_type == "Audio")
                files = rootDirInfo.GetFiles(string.Format("*Unlock2e_A1_{0}.*", unit));
            else
                files = rootDirInfo.GetFiles(string.Format("*Unit {0}_*", unit));

            var musicList = new List<MusicInfo>();
            foreach (var file in files)
            {
                var url = file.FullName.Replace(webRootDirPath, "").Replace("\\", "/");

                var sort = 0;
                if (media_type == "Audio")
                    sort = int.Parse(file.Name.Replace("Unlock2e_A1_" + unit + ".", "").Replace(".mp3", ""));
                musicList.Add(new MusicInfo { Name = file.Name, Url = url, Sort = sort });
            }

            if (media_type == "Audio")
                musicList = musicList.OrderBy(m => m.Sort).ToList();

            return Json(musicList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBasicLevelFileList(string book_type, string media_type, string unit)
        {
            var webRootDirPath = AppDomain.CurrentDomain.BaseDirectory;
            var rootDir = Path.Combine(webRootDirPath, @"unlock2019\Basic\Unlock 2e Basic {0} {1}");
            rootDir = string.Format(rootDir, book_type, media_type);

            var rootDirInfo = new DirectoryInfo(rootDir);

            FileInfo[] files= { };
            if (media_type == "Audio")
                files = rootDirInfo.GetFiles(string.Format("*Track {0}.*", unit));
            else if(media_type == "Videos")
                files = rootDirInfo.GetFiles(string.Format("*Basic Skills Video Unit {0}.mp4", int.Parse(unit) + 1));
            else if(media_type == "Videos with subtitles")
                files = rootDirInfo.GetFiles(string.Format("*Basic Skills Video Unit {0} Part*", int.Parse(unit) + 1));

            var musicList = new List<MusicInfo>();
            foreach (var file in files)
            {
                var url = file.FullName.Replace(webRootDirPath, "").Replace("\\", "/");

                var sort = 0;
                var abbr = string.Empty;
                if (media_type == "Audio")
                {
                    sort = int.Parse(file.Name.Replace("Basic " + book_type + " Track " + unit + ".", "").Replace(".mp3", ""));
                    abbr = string.Format("{0}.{1}", unit, sort);
                }
                else if (media_type == "Videos")
                {
                    //sort = int.Parse(file.Name.Replace("Basic Skills Video Unit " + (int.Parse(unit) +1), "").Replace(".mp4", ""));
                    abbr = string.Format("Unit{0}", (int.Parse(unit) + 1));
                }
                else if (media_type == "Videos with subtitles")
                {
                    sort = int.Parse(file.Name.Replace("Basic Skills Video Unit "+ (int.Parse(unit) + 1) + " Part ", "").Replace(" with subtitles.mp4", ""));
                    abbr = string.Format("Unit{0} Part{1}", (int.Parse(unit) + 1),sort);
                }
                musicList.Add(new MusicInfo { Name = file.Name, Url = url, Sort = sort, Abbr = abbr });
            }

            if (media_type == "Audio")
                musicList = musicList.OrderBy(m => m.Sort).ToList();

            return Json(musicList, JsonRequestBehavior.AllowGet);
        }

    }
}