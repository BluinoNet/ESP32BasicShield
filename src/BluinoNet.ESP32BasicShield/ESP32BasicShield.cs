using BluinoNet.Modules;
using nanoFramework.Hardware.Esp32;
using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Device.Spi;

namespace BluinoNet
{
    public class ESP32BasicShield
    {
        GpioController controller;
        // Browse our samples repository: https://github.com/nanoframework/samples
        // Check our documentation online: https://docs.nanoframework.net/
        // Join our lively Discord community: https://discord.gg/gCyBu8T
        public Relay BoardRelay { get; set; }
        public Button BoardButton { get; set; }
        public Tunes BoardBuzzer { get; set; }
        public Led BoardLed { get; set; }

        /// <summary>
        /// Get SPI Interface for module communication
        /// </summary>
        /// <param name="ChipSelectPin">Chip Select pin</param>
        /// <param name="ClockFrequency">Frekunsi SPI</param>
        /// <param name="DataBitLength">Databit length</param>
        /// <param name="Mode">SPI Mode</param>
        /// <param name="SpiBus">Default = 1</param>
        /// <returns></returns>
        public SpiDevice GetSpi(int ChipSelectPin, int ClockFrequency, int DataBitLength, SpiMode Mode, int SpiBus=1)
        {
            Configuration.SetPinFunction(ESP32Pins.IO23, DeviceFunction.SPI1_MOSI);
            Configuration.SetPinFunction(ESP32Pins.IO19, DeviceFunction.SPI1_CLOCK);
            Configuration.SetPinFunction(ESP32Pins.IO25, DeviceFunction.SPI1_MISO);

            var spiSettings = new SpiConnectionSettings(SpiBus, ChipSelectPin);
            spiSettings.ClockFrequency = ClockFrequency;
            spiSettings.DataBitLength = DataBitLength;
            spiSettings.Mode = Mode;
          
            var spi = SpiDevice.Create(spiSettings);
            return spi;
        }

        /// <summary>
        /// Get i2c device for communication
        /// </summary>
        /// <param name="DeviceAddress">address of device</param>
        /// <param name="BusSpeed">standard or fast</param>
        /// <param name="i2cBus">default = 1</param>
        /// <returns></returns>
        public I2cDevice GetI2C(int DeviceAddress, I2cBusSpeed BusSpeed, int i2cBus=1)
        {
            Configuration.SetPinFunction(ESP32Pins.IO22, DeviceFunction.I2C1_CLOCK);
            Configuration.SetPinFunction(ESP32Pins.IO21, DeviceFunction.I2C1_DATA);
            var i2c = new I2cConnectionSettings(i2cBus, DeviceAddress, BusSpeed);

            var device = new I2cDevice(i2c);
            return device;
        }

        /// <summary>
        /// Get GPIO for Specific Pin 
        /// </summary>
        /// <param name="Pin">pin number</param>
        /// <param name="Mode">input, output or interupt</param>
        /// <returns></returns>
        public GpioPin GetGpio(int Pin, PinMode Mode)
        {
            var device = controller.OpenPin(Pin, Mode);            
            return device;
        }
        public ESP32BasicShield(int PinButton, int PinBuzzer, int PinLed, int PinRelay): this()
        {
            SetupRelay(PinRelay);
          
            SetupButton(PinButton);

            SetupBuzzer(PinBuzzer);

            SetupLed(PinLed);
            
        }

        public ESP32BasicShield()
        {
            controller = new GpioController();
        }
        public void SetupLed(int PinLed)
        {
            this.BoardLed = new Led(PinLed);
        }

        public void SetupBuzzer(int PinBuzzer)
        {
            this.BoardBuzzer = new Tunes(PinBuzzer);
        }
        public void SetupButton(int PinButton)
        {
            this.BoardButton = new Button(PinButton);
        }
        public void SetupRelay(int PinRelay)
        {
            this.BoardRelay = new Relay(PinRelay);
        }
    }

    public class ESP32Pins
    {
        public const int IO00 = 0;

        //
        // Summary:
        //     Gpio IO01 (UART0 TXD)
        public const int IO01 = 1;

        //
        // Summary:
        //     Gpio IO02
        public const int IO02 = 2;

        //
        // Summary:
        //     Gpio IO03 (UART0 RXD)
        public const int IO03 = 3;

        //
        // Summary:
        //     Gpio IO04
        public const int IO04 = 4;

        //
        // Summary:
        //     Gpio IO05
        public const int IO05 = 5;

        //
        // Summary:
        //     Gpio IO06 (Reserved for SPI flash)
        public const int IO06 = 6;

        //
        // Summary:
        //     Gpio IO07 (Reserved for SPI flash)
        public const int IO07 = 7;

        //
        // Summary:
        //     Gpio IO08 (Reserved for SPI flash)
        public const int IO08 = 8;

        //
        // Summary:
        //     Gpio IO09 (Reserved for SPI flash)
        public const int IO09 = 9;

        //
        // Summary:
        //     Gpio IO10 (Reserved for SPI flash)
        public const int IO10 = 10;

        //
        // Summary:
        //     Gpio IO11 (Reserved for SPI flash)
        public const int IO11 = 11;

        //
        // Summary:
        //     Gpio IO12 (also used for JTAG TDI)
        public const int IO12 = 12;

        //
        // Summary:
        //     Gpio IO13 (also used for JTAG TCK)
        public const int IO13 = 13;

        //
        // Summary:
        //     Gpio IO14 (also used for JTAG TMS)
        public const int IO14 = 14;

        //
        // Summary:
        //     Gpio IO15 (also used for JTAG TDO)
        public const int IO15 = 15;

        //
        // Summary:
        //     Gpio IO16
        public const int IO16 = 16;

        //
        // Summary:
        //     Gpio IO17
        public const int IO17 = 17;

        //
        // Summary:
        //     Gpio IO18
        public const int IO18 = 18;

        //
        // Summary:
        //     Gpio IO19
        public const int IO19 = 19;

        //
        // Summary:
        //     Gpio IO20, No Physical pin for IO20
        public const int IO20 = 20;

        //
        // Summary:
        //     Gpio IO21
        public const int IO21 = 21;

        //
        // Summary:
        //     Gpio IO22
        public const int IO22 = 22;

        //
        // Summary:
        //     Gpio IO23
        public const int IO23 = 23;

        //
        // Summary:
        //     Gpio IO24, No Physical pin for IO24
        public const int IO24 = 24;

        //
        // Summary:
        //     Gpio IO25
        public const int IO25 = 25;

        //
        // Summary:
        //     Gpio IO26
        public const int IO26 = 26;

        //
        // Summary:
        //     Gpio IO27
        public const int IO27 = 27;

        //
        // Summary:
        //     Gpio IO28, No Physical pin for IO28
        public const int IO28 = 28;

        //
        // Summary:
        //     Gpio IO29, No Physical pin for IO29
        public const int IO29 = 29;

        //
        // Summary:
        //     Gpio IO30, No Physical pin for IO30
        public const int IO30 = 30;

        //
        // Summary:
        //     Gpio IO31, No Physical pin for IO31
        public const int IO31 = 31;

        //
        // Summary:
        //     Gpio IO32
        public const int IO32 = 32;

        //
        // Summary:
        //     Gpio IO33
        public const int IO33 = 33;

        //
        // Summary:
        //     Gpio IO34 (Input Only, no software pullup/pulldown functions)
        public const int IO34 = 34;

        //
        // Summary:
        //     Gpio IO35 (Input Only, no software pullup/pulldown functions)
        public const int IO35 = 35;

        //
        // Summary:
        //     Gpio IO36 SENSOR_VP (Input Only, no software pullup/pulldown functions)
        public const int IO36 = 36;

        //
        // Summary:
        //     Gpio IO37 (Input Only, no software pullup/pulldown functions)
        public const int IO37 = 37;

        //
        // Summary:
        //     Gpio IO38 (Input Only, no software pullup/pulldown functions)
        public const int IO38 = 38;

        //
        // Summary:
        //     Gpio IO39 SENSOR_VN (Input Only, no software pullup/pulldown functions)
        public const int IO39 = 39;

    }
}
