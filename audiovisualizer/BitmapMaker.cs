using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

namespace LEDVisualizer
{
    public class BitmapMaker
    {

        const int adapterNum = 0;
        const int outputNum = 0;
        static Factory1 factory = new Factory1();
        static SharpDX.DXGI.Adapter1 adapter = factory.GetAdapter1(adapterNum);
        static Device device = new Device(adapter);
        static Output output = adapter.GetOutput(outputNum);
        static Output1 output1 = output.QueryInterface<Output1>();
        static int width = 3840;
        static int height = 2160;
        static int verticalOffset = 0;//280
        static Texture2DDescription textureDesc = new Texture2DDescription
        {
            CpuAccessFlags = CpuAccessFlags.Read,
            BindFlags = BindFlags.None,
            Format = Format.B8G8R8A8_UNorm,
            Width = width,
            Height = height,
            OptionFlags = ResourceOptionFlags.None,
            MipLevels = 1,
            ArraySize = 1,
            SampleDescription = { Count = 1, Quality = 0 },
            Usage = ResourceUsage.Staging
        };
        static Texture2D screenTexture = new Texture2D(device, textureDesc);
        static OutputDuplication duplicatedOutput = output1.DuplicateOutput(device);

        public Bitmap Make()
        {
            var bitmap = new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);
            try
            {
                SharpDX.DXGI.Resource screenResource;
                OutputDuplicateFrameInformation duplicateFrameInformation;

                // Try to get duplicated frame within given time
                duplicatedOutput.TryAcquireNextFrame(100000, out duplicateFrameInformation, out screenResource);

                // copy resource into memory that can be accessed by the CPU
                using (var screenTexture2D = screenResource.QueryInterface<Texture2D>())
                    device.ImmediateContext.CopyResource(screenTexture2D, screenTexture);

                // Get the desktop capture texture
                var mapSource = device.ImmediateContext.MapSubresource(screenTexture, 0, MapMode.Read, MapFlags.None);

                // Create Drawing.Bitmap
                var boundsRect = new System.Drawing.Rectangle(0, 0, width, height);

                // Copy pixels from screen capture Texture to GDI bitmap
                var mapDest = bitmap.LockBits(boundsRect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
                var sourcePtr = mapSource.DataPointer;
                var destPtr = mapDest.Scan0;
                for (int y = 0; y < height; y++)
                {
                    // Copy a single line 
                    Utilities.CopyMemory(destPtr, sourcePtr, width * 4);

                    // Advance pointers
                    sourcePtr = IntPtr.Add(sourcePtr, mapSource.RowPitch);
                    destPtr = IntPtr.Add(destPtr, mapDest.Stride);
                }

                // Release source and dest locks
                bitmap.UnlockBits(mapDest);
                device.ImmediateContext.UnmapSubresource(screenTexture, 0);

                // Save the output
                //bitmap.Save("testscreenshoot.bmp");

                screenResource.Dispose();
                duplicatedOutput.ReleaseFrame();

            }
            catch (SharpDXException e)
            {
                if (e.ResultCode.Code != SharpDX.DXGI.ResultCode.WaitTimeout.Result.Code)
                {
                    throw e;
                }
            }
            catch (NullReferenceException n)
            {
                return bitmap;
            }
            return bitmap;
        }
    }
}
