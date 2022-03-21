// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CommandLine;
using Microsoft.Azure.Devices;

namespace ModifyDevice
{
    /// <summary>
    /// Parameters for the application.
    /// </summary>
    internal class Parameters
    {
        [Option(
            'h',
            "hue",
            Required = true,
            HelpText = "Sets the color hue"
        )]
        public int Hue { get; set; }

        [Option(
            's',
            "Saturation",
            Required = true,
            HelpText = "Sets the color saturation"
        )]
        public int Saturation { get; set; }

                [Option(
            'b',
            "Brightness",
            Required = true,
            HelpText = "Sets the colors brightness"
        )]
        public int Brightness { get; set; }

        [Option(
            'r',
            "reset",
            Required = false,
            HelpText = "Resets to standard Breathmode"
        )]
        public bool Reset { get; set; }
        
    }
}
