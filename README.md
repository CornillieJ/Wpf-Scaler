# WPF Scaler

## Explanation
The WPF Scaler is a utility class designed to ensure that WPF (Windows Presentation Foundation) windows appear consistent across different DPI (dots per inch) settings. It provides a solution to the problem of windows appearing differently on various monitors due to differences in DPI settings.

The class file calculates the necessary scaling factor based on the DPI of the programmer's screen and adjusts the window's content accordingly. By integrating this class into your WPF projects, you can ensure that your application's UI looks the same regardless of the user's monitor setup.

## Installation Instructions
1. **Choose Your Scaler File:** Select the appropriate scaler file based on your monitor's DPI setting and screen resolution.
2. **Add to Your WPF Project:** Incorporate the chosen scaler file into your WPF project. Note that it cannot be added to a class library.
3. **Integrate with Your Window:** In the `Window_Loaded` event handler of your WPF window, add the following line of code: 
    - `Dpi.ScaleWpf(this);`
## Options
- **Scaler File Dpi72:** Suitable for programmers using older monitors with a lower resolution, such as 1280x1024, and a 19-inch screen.
- **Scaler File Dpi96:** Designed for programmers working on a standard 1920x1080 resolution monitor with a 24-inch screen and 100% scaling.
- **Scaler File Dpi120:** Intended for programmers working on a 1920x1080 resolution monitor with a 24-inch screen and 125% scaling.
- **Scaler File Dpi144:** Ideal for programmers using high-DPI monitors with a 2560x1440 resolution, typically 27 inches in size.
- **Scaler File Dpi192:** Tailored for programmers working on ultra-high-DPI monitors with a 3840x2160 resolution, commonly 27 inches or larger.

Choose the scaler file that matches your monitor setup to achieve optimal results.

