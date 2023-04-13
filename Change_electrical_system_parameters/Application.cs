#region Namespaces
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
#endregion

namespace Change_electrical_system_parameters
{
    class Application : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel ribbon_panel = Ribbon_panel(application);
            PushButton button = ribbon_panel.AddItem(new PushButtonData("Button", "Change electrical system", Assembly.GetExecutingAssembly().Location, "Change_electrical_system_parameters.Command")) as PushButton; // Button name
            button.ToolTip = "Change electrical system parameters"; // Description
            button.LargeImage = new BitmapImage(new Uri("pack://application:,,,/Change_electrical_system_parameters;component/Resources/button_image_large.png")); // Button image from resource

            application.ApplicationClosing += Application_closing;
            application.Idling += Application_idling;

            return Result.Succeeded;
        }

        void Application_idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs event_args)
        {

        }

        void Application_closing(object sender, Autodesk.Revit.UI.Events.ApplicationClosingEventArgs event_args)
        {
            throw new NotImplementedException();
        }

        public RibbonPanel Ribbon_panel(UIControlledApplication application)
        {
            string tab_name = "NurJ";
            RibbonPanel ribbonpanel_result = null;

            try
            {
                application.CreateRibbonTab(tab_name);
            }
            catch { }

            try
            {
                application.CreateRibbonPanel(tab_name, "Change electrical system parameters"); // Ribbon panel name
            }
            catch { }

            List<RibbonPanel> ribbon_panels = application.GetRibbonPanels(tab_name);
            foreach (RibbonPanel ribbon_panel in ribbon_panels)
            {
                if (ribbon_panel.Name == "Change electrical system parameters") // Ribbon panel name
                {
                    ribbonpanel_result = ribbon_panel;
                }
            }

            return ribbonpanel_result;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}