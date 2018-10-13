using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace StockManagement
{
    static class Program
    {
        /// <summary>
        /// Main entrypoint of this application
        /// </summary>
        [STAThread]
        static void Main()
       {
            /*Original code
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainMenu());
            */

            // New code to be able to close every form without getting a failure - program won't terminate anymore if the entry-form is closed
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Main main = new Main();

            main.FormClosed += new FormClosedEventHandler(FormClosed);
            main.Show();
            
            Application.Run();
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string thisExe = Assembly.GetExecutingAssembly().GetName().Name;
            AssemblyName embeddedAssembly = new AssemblyName(args.Name);
            string resourceName = thisExe + "." + embeddedAssembly.Name + ".dll";
            
            switch (embeddedAssembly.Name)
            {
                case "DocumentFormat.OpenXml":
                {
                    return Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bin\\DocumentFormat.OpenXml.dll");
                }
                break;
                case "System.Data.SQLite":
                {
                    return Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bin\\System.Data.SQLite.dll");
                }
                break;
                case "StockManagementCore":
                {
                    //Debug.Print(resourceName + "<>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<");
                    return Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bin\\StockManagementCore.dll");
                }
                break;

                case "Localisation":
                {
                    return Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bin\\Localisation_en-US.dll");
                }
                break;
                case "Localisation.resources":
                case "StockManagement.resources":
                {
                    return Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\bin\\Localisation_de-DE.dll");
                }
                break;
                default:
                {
                    MessageBox.Show(embeddedAssembly.Name + " - " + resourceName);
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                    {
                        byte[] assemblyData = new byte[stream.Length];
                        stream.Read(assemblyData, 0, assemblyData.Length);

                        return Assembly.Load(assemblyData);
                    }
                }
                break;
            }
        }

        static void FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= FormClosed;

            if (Application.OpenForms.Count == 0)
            {
                Application.ExitThread();
            }
            else
            {
                Application.OpenForms[0].FormClosed += FormClosed;
            }
        }
    }
}
