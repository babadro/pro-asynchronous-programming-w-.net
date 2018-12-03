﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _05_AsynchronousAPIs
{
    //public class DataImport : IImport
    //{
    //   // public Task ImportXmlFileAsync(string dataDirectory)
    //   // {
    //   //     return ImportXmlFilesAsync()
    //   // }
    //
    //    //public Task ImportXmlFilesAsync(string dataDirectory, CancellationToken ct)
    //    //{
    //    //    return ImportXmlFilesAsync(dataDirectory, ct, new Progress<ImportProgress>());
    //    //}
    //
    //    public Task ImportXmlFilesAsync(string dataDirectory, CancellationToken ct, IProgress<ImportProgress> progressObserver)
    //    {
    //        return Task.Run(() =>
    //        {
    //            FileInfo[] files = new DirectoryInfo(dataDirectory).GetFiles("*.xml");
    //            int nFileProcessed = 0;
    //            foreach (FileInfo file in files)
    //            {
    //                XElement doc = null;
    //
    //                Mutex fileMutex = new Mutex(false, file.Name);
    //                bool cancelled = WaitHandle.WaitAny(new[] {fileMutex, ct.WaitHandle}) == 1;
    //
    //                try
    //                {
    //                    ct.ThrowIfCancellationRequested();
    //                    doc = XElement.Load(file.FullName);
    //                }
    //                finally
    //                {
    //                    fileMutex.ReleaseMutex();
    //                }
    //
    //                var progress = nFileProcessed / (double) files.Length * 100.0;
    //
    //                progressObserver.Report(new ImportProgress((int)progress, file.Name));
    //                InternalProcessXml(doc);
    //                nFileProcessed++;
    //            }
    //        }, ct);
    //    }
    //
    //    private void InternalProcessXml(XElement doc)
    //    {
    //        Thread.Sleep(1000);
    //    }
    //}

    public interface IImport
    {
        Task ImportXmlFilesAsync(string dataDirectory);
        Task ImportXmlFilesAsync(string dataDirectory, CancellationToken ct);
        Task ImportXmlFilesAsync(string dataDirectory, CancellationToken ct, IProgress<ImportProgress> progress);
    }



    public class ImportProgress
    {
        public int OverallProgress { get; private set; }
        public string CurrentFile { get; private set; }

        public ImportProgress(int overallProgress, string currentFile)
        {
            OverallProgress = overallProgress;
            CurrentFile = currentFile;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}