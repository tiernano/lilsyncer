using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace lilsyncer
{
    class Program
    {
        static string dropboxFolder = @"C:\Users\<yourname>\Dropbox\Camera Uploads";
        static string photoStreamFolder = @"C:\Users\<yourname>\Pictures\Photo Stream\My Photo Stream";
        static string photoStreamUploadFolder = @"C:\Users\<yourname>\Pictures\Photo Stream\Uploads";
        static string skyDriveFolder = @"C:\Users\<yourname>\SkyDrive\SkyDrive camera roll";
        static string gDriveFolder = @"C:\Users\<yourname>\Google Drive\Photos";
        

        static void Main(string[] args)
        {
            FileSystemWatcher dropboxWatcher = new FileSystemWatcher(dropboxFolder);
            FileSystemWatcher photoStreamWatcher = new FileSystemWatcher(photoStreamFolder);
            FileSystemWatcher skydriveWatcher = new FileSystemWatcher(skyDriveFolder);
            FileSystemWatcher googleDriveWatcher = new FileSystemWatcher(gDriveFolder);


            
            dropboxWatcher.Created += new FileSystemEventHandler(dropboxWatcher_Created);
            dropboxWatcher.EnableRaisingEvents = true;
            photoStreamWatcher.Created += new FileSystemEventHandler(photoStreamWatcher_Created);
            photoStreamWatcher.EnableRaisingEvents = true;
            skydriveWatcher.Created += new FileSystemEventHandler(skydriveWatcher_Created);
            skydriveWatcher.EnableRaisingEvents = true;
            googleDriveWatcher.Created += new FileSystemEventHander(googleDriveWatcher_Created);
            googleDriveeWatcher.EnableRaisingEvents = true;
            
            Console.WriteLine("Watching folders... ");
            Console.WriteLine("Hit enter to kill");
            Console.ReadLine();
        }

        
        static void skydriveWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Found {0} on Skydrive", e.Name);
            CopyToPhotoStream(e);
            CopyToDropbox(e);
            CopyToGDrive(e);
        }

        static void photoStreamWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Found {0} on PhotoStream", e.Name);
            CopyToDropbox(e);
            CopyToSkyDrive(e);
            CopyToGDrive(e);
        }

        static void dropboxWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Found {0} on Dropbox", e.Name);
            CopyToPhotoStream(e);
            CopyToSkyDrive(e);
            CopyToGDrive(e);
        }
        
        static void googleDriveWatcher_Created(object sender, FileSystemEvenArgs e){
            Console.WriteLine("Found {0} on Google Drive", e.Name);
            CopyToPhotoStream(e);
            CopyToSkyDrive(e);
            CopyToDropbox(e);
        }

        private static void CopyToDropbox(FileSystemEventArgs e)
        {
            if (!File.Exists(Path.Combine(dropboxFolder, e.Name)))
            {
                Console.WriteLine("Copying {0} to Dropbox", e.Name);
                File.Copy(e.FullPath, Path.Combine(dropboxFolder, e.Name));
            }
            else
            {
                Console.WriteLine("Not copying {0}. Already exists on Dropbox", e.Name);
            }
        }

        private static void CopyToPhotoStream(FileSystemEventArgs e)
        {
            if (!File.Exists(Path.Combine(photoStreamUploadFolder, e.Name)))
            {
                Console.WriteLine("Copying {0} to PhotoStream", e.Name);
                File.Copy(e.FullPath, Path.Combine(photoStreamUploadFolder, e.Name));
            }
            else
            {
                Console.WriteLine("Not copying {0}. Already exists on PhotoStream", e.Name);
            }
        }

        private static void CopyToSkyDrive(FileSystemEventArgs e)
        {
            if (!File.Exists(Path.Combine(skyDriveFolder, e.Name)))
            {
                Console.WriteLine("Copying {0} to SkyDrive", e.Name);
                File.Copy(e.FullPath, Path.Combine(skyDriveFolder, e.Name));
            }
            else
            {
                Console.WriteLine("Not copying {0}. Already exists on Skydrive", e.Name);
            }
        }
        
        private static void CopyToGDrive(FileSystemEventArgs e){
            if(!File.Exists(Path.Combine(gDriveFolder, e.Name))){
                Console.WriteLine("Copying {0} to GDrive", e.Name);
                File.Copy(e.FullPath, Path.Combine(gDriveFolder, e.Name));
            }
            else{
                Console.Writeline("Not copying {0}. Already exists on GDrive", e.Name);
            }
        }

       
    }
}
