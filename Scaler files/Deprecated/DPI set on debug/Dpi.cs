using System;
using System.Windows;
using System.Windows.Media;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Application = System.Windows.Application;

namespace ModernWindow;

public static class Dpi
{
    public static double OriginDpi
    {
        get
        {
            string json = File.ReadAllText("appSettings.json");
            dynamic settings = JsonConvert.DeserializeObject(json);
            return settings["DpiSettings"]["OriginDpi"];
        }
        set
        {
            string json = File.ReadAllText("appSettings.json");
            dynamic settings = JsonConvert.DeserializeObject(json);
            settings["DpiSettings"]["OriginDpi"] = value;
            File.WriteAllText("appSettings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
    }
    public static void ScaleWpf(Window thisWindow)
    {
        (thisWindow.Width, thisWindow.Height) = Dpi.GetUpdatedWidthAndLength(thisWindow.Width,thisWindow.Height);
        ScaleWindow(thisWindow);
    }
    public static void ScaleWindow(Window currentWindow)
    {
        double scale = CalculateScale();
        ScaleTransform scaleTransform = new(scale, scale);
        UIElement windowContent = ((UIElement)currentWindow.Content);
        windowContent.RenderTransform = scaleTransform;
    }
    public static (double,double) GetUpdatedWidthAndLength(double width, double height)
    {
        double scale = CalculateScale();
        return (width*scale, height*scale);
    }
    private static double CalculateScale()
    {
#if DEBUG
        double OriginDpi = GetOriginDpi();
#endif       
        double monitorDpi = GetMonitorDpi();
        return OriginDpi/monitorDpi;
    }
    private static double GetOriginDpi()
    {
        PresentationSource source = PresentationSource.FromVisual(Application.Current.MainWindow);
        return 96.0 * source.CompositionTarget.TransformToDevice.M11;
    }

    // private static double GetMonitorDpi()
    // {
    //     using Graphics g = Graphics.FromHwnd(IntPtr.Zero);
    //     return g.DpiX;
    // }
    // private static double GetMonitorDpi()
    // {
    //     using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop"))
    //     {
    //         string dpiString = key.GetValue("LogPixels")?.ToString();
    //         if (int.TryParse(dpiString, out int dpi))
    //         {
    //             return dpi;
    //         }
    //     }
    //     return 96; // Default DPI if unable to retrieve from registry
    // }
    [DllImport("user32.dll")]
    private static extern uint GetDpiForSystem();

    public static double GetMonitorDpi()
    {
        uint dpi = GetDpiForSystem();
        return dpi;
    }
}