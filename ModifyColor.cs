using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModifyDevice
{
    class ModifyColor
    {
        class ColorCSHV
        {
            public int Hue { get; set; }
            public int Saturation { get; set; }
            public int Brightness { get; set; }
        }

        RegistryManager registryManager;
        ColorCSHV color = new ColorCSHV();
        bool isReset = false;

        public ModifyColor(RegistryManager registryManager, Parameters args)
        {
            this.registryManager = registryManager;

            if (!args.Reset)
            {
                this.color.Brightness = args.Brightness;
                this.color.Hue = args.Hue;
                this.color.Saturation = args.Saturation;
            }else
                isReset = true;
        }

        public async Task ChangeColorProperties(ServiceClient client, string deviceId)
        {

            var twin = await registryManager.GetTwinAsync(deviceId);
            var updatePatch =
                @"{
                        properties: {
                            desired: {
                                hue : " + color.Hue.ToString() + @",
                                saturation: " + color.Saturation.ToString() + @",
                                brightness: " + color.Brightness.ToString() + @",
                                mode: 1
                            }
                        }
                    }";

            var resetPatch =
                @"{
                        properties: {
                            desired: {
                                hue : 14,
                                saturation: 255,
                                brightness: 255,
                                mode: 2
                            }
                        }
                    }";

            var patch = this.isReset? resetPatch : updatePatch;
            await registryManager.UpdateTwinAsync(twin.DeviceId, patch, twin.ETag);

            var query = registryManager.CreateQuery(
              "SELECT * FROM devices WHERE connectionState='connected' AND deviceId='"+twin.DeviceId+"'", 100);
            var twins = await query.GetNextAsTwinAsync();
            Console.WriteLine("Device : \n{0}", string.Join("\n", twins.Select(t => $"{t.DeviceId}, mode: {t.Properties.Desired["mode"]}")));
        }
    }
}