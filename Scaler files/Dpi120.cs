using System;
using System.Windows;
using System.Windows.Media;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Application = System.Windows.Application;

public static class Dpi
{
    [DllImport("user32.dll")]
    private static extern uint GetDpiForSystem();
    public static void ScaleWpf(Window thisWindow)
    {
        double scale = CalculateScale();
        ScaleWindow(thisWindow, scale);
        (thisWindow.Width, thisWindow.Height) = GetUpdatedWidthAndLength(thisWindow.Width,thisWindow.Height, scale);
    }

    private static void ScaleWindow(Window currentWindow, double scale)
    {
        //ScaleTransform scaleTransform = new(scale, scale);
        //UIElement windowContent = (UIElement)currentWindow.Content;
        //windowContent.RenderTransform = scaleTransform;
        ScaleTransform scaleTransform = new(scale*.98, scale*.98);
        ((FrameworkElement)currentWindow.Content).LayoutTransform = scaleTransform;
    }

    private static (double,double) GetUpdatedWidthAndLength(double width, double height, double scale)
    {
        return (width*scale, height*scale);
    }
    private static double CalculateScale()
    {
        double originDpi = 120;
        double monitorDpi = GetDpiForSystem();
        return originDpi/monitorDpi;
    }
    private static double GetOriginDpi()
    {
        PresentationSource source = PresentationSource.FromVisual(Application.Current.MainWindow);
        return 96.0 * source.CompositionTarget.TransformToDevice.M11;
    }
}