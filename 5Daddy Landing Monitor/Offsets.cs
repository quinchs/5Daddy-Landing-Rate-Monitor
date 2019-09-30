using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FSUIPC;

namespace _5Daddy_Landing_Monitor
{
    class Offsets
    {
        private List<Offset> OffsetsList = new List<Offset>();
        private Offset<object> offset1 = new Offset<object>(0x0020); // Size: 4. Description: Ground altitude in Metres x 256. (see also offset 0B4C)
        private Offset<object> offset2 = new Offset<object>(0x0024); // Size: Var. Description: Zero terminated string giving the Start-Up situation or flight name, including the path from the FS folder (usually PILOTS\ …)
        private Offset<object> offset3 = new Offset<object>(0x012C); // Size: Varies. Description: Zero terminated string giving the name of the current Log book, with the default being called just ‘logbook’ instead of the true filename. <em>(This applies to FS2002, but hasn’t been verified on the others)</em>
        private Offset<object> offset4 = new Offset<object>(0x0238); // Size: 1. Description: Hour of local time in FS (0–23)
        private Offset<object> offset5 = new Offset<object>(0x0239); // Size: 1. Description: Minute of local time in FS (0–59)
        private Offset<object> offset6 = new Offset<object>(0x023A); // Size: 1. Description: Second of time in FS (0–59)
        private Offset<object> offset7 = new Offset<object>(0x023B); // Size: 1. Description: Hour of Zulu time in FS (also known at UTC or GMT)
        private Offset<object> offset8 = new Offset<object>(0x023C); // Size: 1. Description: Minute of Zulu time in FS2
        private Offset<object> offset9 = new Offset<object>(0x023E); // Size: 2. Description: Day number in Year in FS (counting from 1)
        private Offset<object> offset10 = new Offset<object>(0x0240); // Size: 2. Description: Year in FS
        private Offset<object> offset11 = new Offset<object>(0x0246); // Size: 2. Description: Local time offset from Zulu (minutes). +ve = behind Zulu, –ve = ahead
        private Offset<object> offset12 = new Offset<object>(0x0248); // Size: 2. Description: Season: 0=Winter, 1=Spring, 2=Summer, 3=Fall
        private Offset<object> offset13 = new Offset<object>(0x0262); // Size: 2. Description: Pause control (write 1 to pause, 0 to un-pause).
        private Offset<object> offset14 = new Offset<object>(0x0264); // Size: 2. Description: Pause indicator (0=Not paused, 1=Paused)
        private Offset<object> offset15 = new Offset<object>(0x0274); // Size: 2. Description: Frame rate is given by 32768/this value
        private Offset<object> offset16 = new Offset<object>(0x0278); // Size: 2. Description: Auto-co-ordination (“auto-rudder”), 1=on, 0=off
        private Offset<object> offset17 = new Offset<object>(0x0280); // Size: 1. Description: Lights: this operates the NAV lights, plus, on FS2000, the TAXI, PANEL and WING lights. For separate switches on FS2000 (and CFS2?) see offset 0D0C
        private Offset<object> offset18 = new Offset<object>(0x0281); // Size: 1. Description: Beacon and Strobe lights. For separate switches on FS2000 (and CFS2?( see offset 0D0C
        private Offset<object> offset19 = new Offset<object>(0x028C); // Size: 1. Description: Landing lights. (See also offset 0D0C on FS2000, and maybe CFS2).
        private Offset<object> offset20 = new Offset<object>(0x029C); // Size: 1. Description: Pitot Heat switch (0=off, 1=on)
        private Offset<object> offset21 = new Offset<object>(0x02A0); // Size: 2. Description: Magnetic variation (signed, –ve = West). For degrees *360/65536. Convert True headings to Magnetic by <em>subtracting </em>this value, Magnetic headings to True by <em>adding </em>this value.
        private Offset<object> offset22 = new Offset<object>(0x02B2); // Size: 2. Description: Zoom factor: FS2002 only, and read-only. 64=x1, 128=x2 et cetera
        private Offset<object> offset23 = new Offset<object>(0x02B4); // Size: 4. Description: GS: Ground Speed, as 65536*metres/sec. Not updated in Slew mode!
        private Offset<object> offset24 = new Offset<object>(0x02B8); // Size: 4. Description: TAS: True Air Speed, as knots * 128
        private Offset<object> offset25 = new Offset<object>(0x02BC); // Size: 4. Description: IAS: Indicated Air Speed, as knots * 128
        private Offset<object> offset26 = new Offset<object>(0x02C4); // Size: 4. Description: Barber pole airspeed, as knots * 128
        private Offset<object> offset27 = new Offset<object>(0x02C8); // Size: 4. Description: Vertical speed, signed, as 256 * metres/sec. For the more usual ft/min you need to apply the conversion *60*3.28084/256
        private Offset<object> offset28 = new Offset<object>(0x02CC); // Size: 8. Description: Whiskey Compass, degrees in ‘double’ floating point format (FLOAT64)
        private Offset<object> offset29 = new Offset<object>(0x02D4); // Size: 2. Description: (FS2004 only) ADF2 Frequency: main 3 digits, in Binary Coded Decimal. See also offset 02D6. A frequency of 1234.5 will have 0x0234 here and 0x0105 in offset 02D6.
        private Offset<object> offset30 = new Offset<object>(0x02D6); // Size: 2. Description: (FS2004 only) Extended ADF2 frequency. The high byte contains the 1000’s digit and the low byte the fraction, so, for a frequency of 1234.5 this offset will contain 0x0105.
        private Offset<object> offset31 = new Offset<object>(0x02D8); // Size: 2. Description: (FS2004 only) ADF2: relative bearing to NDB ( *360/65536 for degrees, –ve left, +ve right)
        private Offset<object> offset32 = new Offset<object>(0x02DC); // Size: 6. Description: (FS2004 only) ADF2 IDENTITY (string supplied: 6 bytes including zero terminator)
        private Offset<object> offset33 = new Offset<object>(0x02E2); // Size: 25. Description: (FS2004 only) ADF2 name (string supplied: 25 bytes including zero terminator)
        private Offset<object> offset34 = new Offset<object>(0x02FB); // Size: 1. Description: (FS2004 only) ADF1 morse ID sound (1 = on, 0 = off), read for state, write to control
        private Offset<object> offset35 = new Offset<object>(0x0310); // Size: 8. Description: FS2002 timer (double float, elapsed seconds including fractions, incremented each ‘tick’ – i.e. 1/18<sup>th</sup> sec). This runs all the time. It is used for all sorts of things, including the elapsed time between key/mouse-originated controls, to determine whether to accelerate inc/dec types. See also 0368,
        private Offset<object> offset36 = new Offset<object>(0x032C); // Size: 2. Description: “Plane is in fuel box” flag (same as Scenery BGL variable 0288)
        private Offset<object> offset37 = new Offset<object>(0x0330); // Size: 2. Description: Altimeter pressure setting (“Kollsman” window). As millibars (hectoPascals) * 16
        private Offset<object> offset38 = new Offset<object>(0x0338); // Size: 2. Description: Airframe can suffer damage if stressed (0=no, 1=yes)
        private Offset<object> offset39 = new Offset<object>(0x033A); // Size: 2. Description: Manual fuel tank selection if set (appears to be standard anyway in FS2000)
        private Offset<object> offset40 = new Offset<object>(0x033C); // Size: 2. Description: Engine stops when out of fuel if set
        private Offset<object> offset41 = new Offset<object>(0x033E); // Size: 2. Description: Jet engine can flameout if set (appears not an option in FS2000?)
        private Offset<object> offset42 = new Offset<object>(0x0340); // Size: 2. Description: Manual magneto controls if set (appears to be standard anyway in FS2000)
        private Offset<object> offset43 = new Offset<object>(0x0342); // Size: 2. Description: Manual mixture control if set
        private Offset<object> offset44 = new Offset<object>(0x034C); // Size: 2. Description: ADF1 Frequency: main 3 digits, in Binary Coded Decimal. See also offset 0356. A frequency of 1234.5 will have 0x0234 here and 0x0105 in offset 0356.</span><span style="font-family: Tahoma; font-size: x-small;">(See also offset 0389)
        private Offset<object> offset45 = new Offset<object>(0x034E); // Size: 2. Description: COM1 frequency, 4 digits in BCD format. A frequency of 123.45 is represented by 0x2345. The leading 1 is assumed.
        private Offset<object> offset46 = new Offset<object>(0x0350); // Size: 2. Description: NAV1 frequency, 4 digits in BCD format. A frequency of 113.45 is represented by 0x1345. The leading 1 is assumed. (See also offset 0388)
        private Offset<object> offset47 = new Offset<object>(0x0352); // Size: 2. Description: NAV2 frequency, 4 digits in BCD format. A frequency of 113.45 is represented by 0x1345. The leading 1 is assumed. (See also offset 0388)
        private Offset<object> offset48 = new Offset<object>(0x0354); // Size: 2. Description: Transponder setting, 4 digits in BCD format: 0x1200 means 1200 on the dials.
        private Offset<object> offset49 = new Offset<object>(0x0356); // Size: 2. Description: Extended ADF1 frequency. The high byte contains the 1000’s digit and the low byte the fraction, so, for a frequency of 1234.5 this offset will contain 0x0105.
        private Offset<object> offset50 = new Offset<object>(0x0358); // Size: 2. Description: COM frequency settable in 25KHz increments if true (else 50KHz)
        private Offset<object> offset51 = new Offset<object>(0x035C); // Size: 2. Description: ADF frequency settable in 100Hz increments if true (else 1KHz)
        private Offset<object> offset52 = new Offset<object>(0x0366); // Size: 2. Description: Aircraft on ground flag (0=airborne, 1=on ground). Not updated in Slew mode.
        private Offset<object> offset53 = new Offset<object>(0x0368); // Size: 4. Description: Control timer 2 (see also 0310), a 32-bit ‘float’.
        private Offset<object> offset54 = new Offset<object>(0x036C); // Size: 1. Description: Stall warning (0=no, 1=stall)
        private Offset<object> offset55 = new Offset<object>(0x036D); // Size: 1. Description: Overspeed warning (0=no, 1=overspeed)
        private Offset<object> offset56 = new Offset<object>(0x036E); // Size: 1. Description: Turn co-ordinator ball position (slip and skid). –128 is extreme left, +127 is extreme right, 0 is balanced.
        private Offset<object> offset57 = new Offset<object>(0x0372); // Size: 2. Description: Reliability % (0–100). (Not sure if this is effective in FS2000)
        private Offset<object> offset58 = new Offset<object>(0x0374); // Size: 2. Description: NAV1 or NAV2 select (256=NAV1, 512=NAV2)
        private Offset<object> offset59 = new Offset<object>(0x0378); // Size: 2. Description: DME1 or DME2 select (1=DME1, 2=DME2)
        private Offset<object> offset60 = new Offset<object>(0x037C); // Size: 2. Description: Turn Rate (for turn coordinator). 0=level, –512=2min Left, +512=2min Right
        private Offset<object> offset61 = new Offset<object>(0x0388); // Size: 1. Description: NAV radio activation. If you change the NAV1 or NAV2 frequencies, writing 2 here makes FS re-scan for VORs to receive on those frequencies.
        private Offset<object> offset62 = new Offset<object>(0x0389); // Size: 1. Description: ADF radio activation. If you change the ADF frequency, writing 2 here makes FS re-scan for an NDB to receive on that frequency. (Although FS2000 seems to do this quite soon in any case)
        private Offset<object> offset63 = new Offset<object>(0x038A); // Size: 1. Description: COM radio activation. If you change the COM radio, writing a 1 here makes FS scan for ATIS broadcasts to receive on that frequency.
        private Offset<object> offset64 = new Offset<object>(0x04B0); // Size: 48. Description: Area reserved by FSUIPC. (See details for user accessible parts earlier in this document). [FS2000 &amp; CFS2 only]. The more useful ones follow:
        private Offset<object> offset65 = new Offset<object>(0x04B4); // Size: 2. Description: &gt;fs2k adventure weather: This provides the temperature_surface_alt in metres. This is used to provide the METAR reporting station altitude so that the cloud bases can be converted to AGL.
        private Offset<object> offset66 = new Offset<object>(0x04BA); // Size: 2. Description: &gt;fs2k adventure weather: This provides the wind_surf_turb which is used to provide the surface wind’s upper gust speed in knots, with zero indicating no gusts.
        private Offset<object> offset67 = new Offset<object>(0x04BC); // Size: 2. Description: &gt;fs2k adventure weather: This provides the barometric_drift variable, which is used to provide the <em>difference </em>between the current aircraft position QNH (which may be in transition), and the METAR reported QNH as set by the weather control program. Adding this ‘drift’ value to the pressure will give the correct value for ATIS reports
        private Offset<object> offset68 = new Offset<object>(0x04C0); // Size: 2. Description: &gt;fs2k adventure weather: This provides the fsuipc_visibility in statute miles * 100
        private Offset<object> offset69 = new Offset<object>(0x04C2); // Size: 2. Description: &gt;fs2k adventure weather: This provides the cloud_thunder_base in metres AMSL
        private Offset<object> offset70 = new Offset<object>(0x04C4); // Size: 2. Description: &gt;fs2k adventure weather: This provides the cloud_low_base in metres AMSL
        private Offset<object> offset71 = new Offset<object>(0x04C6); // Size: 2. Description: &gt;fs2k adventure weather: This provides the cloud_high_base in metres AMSL
        private Offset<object> offset72 = new Offset<object>(0x04C8); // Size: 2. Description: Dew point as degrees C *256, for the surface temperature layer, FS2k/CFS2 read only
        private Offset<object> offset73 = new Offset<object>(0x04CB); // Size: 1. Description: Precipitation rate, 0–5, FS2k/CFS2 read only. <em>Note that in FS2004, rate 0 = light drizzle. Type=0 is no rain/snow</em>
        private Offset<object> offset74 = new Offset<object>(0x04CC); // Size: 1. Description: Precipitation type, 0=none, 1=rain, 2=snow, FS2k/CFS2 read only.
        private Offset<object> offset75 = new Offset<object>(0x04CD); // Size: 1. Description: &gt;fs2k adventure weather: This provides the cloud_thunder_cover 0–8
        private Offset<object> offset76 = new Offset<object>(0x04CE); // Size: 1. Description: &gt;fs2k adventure weather: This provides the cloud_low_cover 0–8
        private Offset<object> offset77 = new Offset<object>(0x04CF); // Size: 1. Description: &gt;fs2k adventure weather: This provides the cloud_high_cover 0–8
        private Offset<object> offset78 = new Offset<object>(0x04D2); // Size: 2. Description: Precipitation control: write hi-byte=type 0–2 (see above), low byte=rate 0–5. Write 0xFFFF to release control back to FS2k/CFS2.
        private Offset<object> offset79 = new Offset<object>(0x04D4); // Size: 2. Description: Dew point control: degrees C * 256. Sets surface layer dewpoint only, FSUIPC does rest. Write 0x8000 to release control back to FS2k/CFS2.
        private Offset<object> offset80 = new Offset<object>(0x04D8); // Size: 2. Description: Surface layer wind speed, in knots (FS2k/CFS2). This may be different to the current wind speed at the aircraft—see offset 0E90. This also provides wind_surf_vel for FS2k Adventures.
        private Offset<object> offset81 = new Offset<object>(0x04DA); // Size: 2. Description: Surface layer wind direction, *360/65536 to get degrees MAGNETIC (FS2k/CFS2). This may be different to the current wind direction at the aircraft—see offset 0E92. This also provides wind_surf_dir for FS2k Adventures.
        private Offset<object> offset82 = new Offset<object>(0x04E0); // Size: 88. Description: Area reserved for Project Magenta
        private Offset<object> offset83 = new Offset<object>(0x0570); // Size: 8. Description: Altitude, in metres and fractional metres. The units are in the high 32-bit integer (at 0574) and the fractional part is in the low 32-bit integer (at 0570). [Can be written to move aircraft: in FS2002 only in slew or pause states]
        private Offset<object> offset84 = new Offset<object>(0x0578); // Size: 4. Description: Pitch, *360/(65536*65536) for degrees. 0=level, –ve=pitch up, +ve=pitch down</span><span style="font-family: Tahoma; font-size: x-small;">[Can be set in slew or pause states]
        private Offset<object> offset85 = new Offset<object>(0x057C); // Size: 4. Description: Bank, *360/(65536*65536) for degrees. 0=level, –ve=bank right, +ve=bank left</span><span style="font-family: Tahoma; font-size: x-small;">[Can be set in slew or pause states]
        private Offset<object> offset86 = new Offset<object>(0x0580); // Size: 4. Description: Heading, *360/(65536*65536) for degrees TRUE.</span><span style="font-family: Tahoma; font-size: x-small;">[Can be set in slew or pause states]
        private Offset<object> offset87 = new Offset<object>(0x05B0); // Size: 24. Description: The viewpoint Latitude (8 bytes), Longitude (8 bytes) and Altitude (8 bytes) in the same format as 0560–0577 above. This is read only and seems to relate to the position of the viewer whether in cockpit, tower or spot views. 
        private Offset<object> offset88 = new Offset<object>(0x05D2); // Size: 2. Description: Current view direction, *360/65536 for degrees TRUE. Read only.
        private Offset<object> offset89 = new Offset<object>(0x05D4); // Size: 2. Description: Smoke system available if True
        private Offset<object> offset90 = new Offset<object>(0x05D8); // Size: 2. Description: Smoke system enable: write 1 to switch on, 0 to switch off (see also 05D4)
        private Offset<object> offset91 = new Offset<object>(0x05DC); // Size: 2. Description: Slew mode (indicator and control), 0=off, 1=on. (See 05DE also).
        private Offset<object> offset92 = new Offset<object>(0x05DE); // Size: 2. Description: Slew control: write non-zero value here <em>at same time </em>as changing 05DC above, and the Slew mode change includes the swapping of the assigned joystick axes. [ignored in<em> </em>FS2004 – the axes are swapped in any case. See offset 310B for control of axis connection in slew mode]
        private Offset<object> offset93 = new Offset<object>(0x05E4); // Size: 2. Description: Slew roll rate: 0=static, –ve = right roll, +ve=left roll, rate is such that 192 gives a complete 360 roll in about one minute.
        private Offset<object> offset94 = new Offset<object>(0x05E6); // Size: 2. Description: Slew yaw rate: 0=heading constant, –ve = right, +ve=left, rate is such that 24 gives a complete 360 turn in about one minute.
        private Offset<object> offset95 = new Offset<object>(0x05E8); // Size: 2. Description: Slew vertical rate: 16384=no change, 16385–32767 increasing rate down, 16383–0 increasing rate up. One keypress on Q (up) or A (down) makes a change of 512 units.
        private Offset<object> offset96 = new Offset<object>(0x05EB); // Size: 1. Description: Slew forward/backward movement: +ve=backward, –ve=forward. Values 1–127 give slow to fast slewing (–128 is the fastest forward slew).
        private Offset<object> offset97 = new Offset<object>(0x05ED); // Size: 1. Description: Slew left/right movement: +ve=right, –ve=left. Values 1–127 give slow to fast sideways slewing (–128 is the fastest leftward slew).
        private Offset<object> offset98 = new Offset<object>(0x05EE); // Size: 2. Description: Slew pitch rate: 16384=no change, &lt;16384=pitch up, &gt;16384 pitch down, range 0–32767.
        private Offset<object> offset99 = new Offset<object>(0x05F4); // Size: 2. Description: Slew mode display: 0=off, 1=coords/hdg/spd, 2=fps, 3=all
        private Offset<object> offset100 = new Offset<object>(0x05FC); // Size: 2. Description: Flight mode display: 0=off, 1=coords/hdg/spd, 2=fps, 3=all
        private Offset<object> offset101 = new Offset<object>(0x0609); // Size: 1. Description: Engine type: 0=Piston (and some FS2004 Helos), 1=Jet, 2=Sailplane, 3=Helo, 4=Rocket, 5=Turboprop
        private Offset<object> offset102 = new Offset<object>(0x060C); // Size: 2. Description: Gear type. 0=non-retractable standard, 1=retractable, 2=slides
        private Offset<object> offset103 = new Offset<object>(0x060E); // Size: 2. Description: Retractable gear flag (0 if not, 1 if retractable)
        private Offset<object> offset104 = new Offset<object>(0x0612); // Size: 2. Description: Display IAS if TRUE, TAS otherwise
        private Offset<object> offset105 = new Offset<object>(0x0628); // Size: 4. Description: Instant replay flag &amp; control, 1=on, 0=off. Can write to turn on and off whilst there is still time to play (see offset 062C)
        private Offset<object> offset106 = new Offset<object>(0x062C); // Size: 4. Description: Instant replay: time left to run, in seconds. Whilst this is non-zero, the flag in offset 0628 controls the playback.
        private Offset<object> offset107 = new Offset<object>(0x0700); // Size: 96. Description: Area used for operating, controlling and configuring the facilities in FSUIPC for feedback flight control (bank, pitch, speed). For full details of this please see the separate TXT documentation in the SDK.
        private Offset<object> offset108 = new Offset<object>(0x0760); // Size: 4?. Description: Video recording flag, 1=on, 0=off. [<em>Not verified, maybe FS2002 only</em>]
        private Offset<object> offset109 = new Offset<object>(0x0764); // Size: 4. Description: Autopilot available
        private Offset<object> offset110 = new Offset<object>(0x0768); // Size: 4. Description: Autopilot V/S hold available
        private Offset<object> offset111 = new Offset<object>(0x076C); // Size: 4. Description: Autothrottle airspeed hold available
        private Offset<object> offset112 = new Offset<object>(0x0770); // Size: 4. Description: Autothrottle mach hold available
        private Offset<object> offset113 = new Offset<object>(0x0774); // Size: 4. Description: Autothrottle RPM hold available
        private Offset<object> offset114 = new Offset<object>(0x0778); // Size: 4. Description: Flaps available
        private Offset<object> offset115 = new Offset<object>(0x077C); // Size: 4. Description: Stall horn available
        private Offset<object> offset116 = new Offset<object>(0x0780); // Size: 4. Description: Engine mixture available
        private Offset<object> offset117 = new Offset<object>(0x0784); // Size: 4. Description: Carb heat available
        private Offset<object> offset118 = new Offset<object>(0x0788); // Size: 4. Description: Pitot heat available
        private Offset<object> offset119 = new Offset<object>(0x078C); // Size: 4. Description: Spoiler available
        private Offset<object> offset120 = new Offset<object>(0x0790); // Size: 4. Description: Aircraft is tail dragger
        private Offset<object> offset121 = new Offset<object>(0x0794); // Size: 4. Description: Strobes available
        private Offset<object> offset122 = new Offset<object>(0x0798); // Size: 4. Description: Prop type available
        private Offset<object> offset123 = new Offset<object>(0x079C); // Size: 4. Description: Toe brakes available
        private Offset<object> offset124 = new Offset<object>(0x07A0); // Size: 4. Description: NAV1 available
        private Offset<object> offset125 = new Offset<object>(0x07A4); // Size: 4. Description: NAV2 available
        private Offset<object> offset126 = new Offset<object>(0x07A8); // Size: 4. Description: Marker indicators available
        private Offset<object> offset127 = new Offset<object>(0x07AC); // Size: 4. Description: NAV1 OBS available
        private Offset<object> offset128 = new Offset<object>(0x07B0); // Size: 4. Description: NAV2 OBS available
        private Offset<object> offset129 = new Offset<object>(0x07B4); // Size: 4. Description: VOR2 gauge available
        private Offset<object> offset130 = new Offset<object>(0x07B8); // Size: 4. Description: Gyro drift available
        private Offset<object> offset131 = new Offset<object>(0x07BC); // Size: 4. Description: Autopilot Master switch
        private Offset<object> offset132 = new Offset<object>(0x07C0); // Size: 4. Description: Autopilot wing leveller
        private Offset<object> offset133 = new Offset<object>(0x07C4); // Size: 4. Description: Autopilot NAV1 lock
        private Offset<object> offset134 = new Offset<object>(0x07C8); // Size: 4. Description: Autopilot heading lock
        private Offset<object> offset135 = new Offset<object>(0x07CC); // Size: 2. Description: Autopilot heading value, as degrees*65536/360
        private Offset<object> offset136 = new Offset<object>(0x07D0); // Size: 4. Description: Autopilot altitude lock
        private Offset<object> offset137 = new Offset<object>(0x07D4); // Size: 4. Description: Autopilot altitude value, as metres*65536
        private Offset<object> offset138 = new Offset<object>(0x07D8); // Size: 4. Description: Autopilot attitude hold
        private Offset<object> offset139 = new Offset<object>(0x07DC); // Size: 4. Description: Autopilot airspeed hold
        private Offset<object> offset140 = new Offset<object>(0x07E2); // Size: 2. Description: Autopilot airspeed value, in knots
        private Offset<object> offset141 = new Offset<object>(0x07E4); // Size: 4. Description: Autopilot mach hold
        private Offset<object> offset142 = new Offset<object>(0x07E8); // Size: 4. Description: Autopilot mach value, as Mach*65536
        private Offset<object> offset143 = new Offset<object>(0x07EC); // Size: 4. Description: Autopilot vertical speed hold [<em>Not connected in FS2002/4</em>]
        private Offset<object> offset144 = new Offset<object>(0x07F2); // Size: 2. Description: Autopilot vertical speed value, as ft/min
        private Offset<object> offset145 = new Offset<object>(0x07F4); // Size: 4. Description: Autopilot RPM hold
        private Offset<object> offset146 = new Offset<object>(0x07FA); // Size: 2. Description: Autopilot RPM value ??
        private Offset<object> offset147 = new Offset<object>(0x07FC); // Size: 4. Description: Autopilot GlideSlope hold</span><span style="font-family: Tahoma; font-size: x-small;">N.B. In at least FS2002 and FS2004 (and maybe FS2000 as well) setting this also sets 0800, approach hold. To clear both you need to write 0 to them in the same FSUIPC process call, as if they are separated by an FS frame, an interlock stops them clearing.
        private Offset<object> offset148 = new Offset<object>(0x0800); // Size: 4. Description: Autopilot Approach hold.</span><span style="font-family: Tahoma; font-size: x-small;">See the note above, for offset 07FC.
        private Offset<object> offset149 = new Offset<object>(0x0804); // Size: 4. Description: Autopilot Back course hold. </span><span style="font-family: Tahoma; font-size: x-small;">The note for offset 07FC may also apply here.
        private Offset<object> offset150 = new Offset<object>(0x0808); // Size: 4. Description: Yaw damper
        private Offset<object> offset151 = new Offset<object>(0x080C); // Size: 4. Description: Autothrottle TOGA (take off power)
        private Offset<object> offset152 = new Offset<object>(0x0810); // Size: 4. Description: Autothrottle Arm
        private Offset<object> offset153 = new Offset<object>(0x0814); // Size: 4. Description: Flight analysis mode (0=0ff, 1=Landing, 2=Course tracking, 3=Manoevres)
        private Offset<object> offset154 = new Offset<object>(0x0830); // Size: 4. Description: Action on crash (0=ignore, 1=reset, 2=graph). [<em>Graph mode not applicable to FS2002</em>]
        private Offset<object> offset155 = new Offset<object>(0x0840); // Size: 2. Description: Crashed flag
        private Offset<object> offset156 = new Offset<object>(0x0842); // Size: 2. Description: Vertical speed in metres per minute, but with –ve for UP, +ve for DOWN. Multiply by 3.28084 and reverse the sign for the normal fpm measure. This works even in slew mode (except in FS2002).
        private Offset<object> offset157 = new Offset<object>(0x0848); // Size: 2. Description: Off-runway crash detection
        private Offset<object> offset158 = new Offset<object>(0x084A); // Size: 2. Description: Can collide with dynamic scenery
        private Offset<object> offset159 = new Offset<object>(0x085C); // Size: 4. Description: VOR1 Latitude in FS form. Convert to degrees by *90/10001750.</span><span style="font-family: Tahoma; font-size: x-small;">If NAV1 is tuned to an ILS this gives the glideslope transmitter Latitude.
        private Offset<object> offset160 = new Offset<object>(0x0864); // Size: 4. Description: VOR1 Longitude in FS form. Convert to degrees by *360/(65536*65536).</span><span style="font-family: Tahoma; font-size: x-small;">If NAV1 is tuned to an ILS this gives the glideslope transmitter Longitude.
        private Offset<object> offset161 = new Offset<object>(0x086C); // Size: 4. Description: VOR1 Elevation in metres. </span><span style="font-family: Tahoma; font-size: x-small;">If NAV1 is tuned to an ILS this gives the glideslope transmitter Elevation.
        private Offset<object> offset162 = new Offset<object>(0x0870); // Size: 2. Description: ILS localiser inverse runway heading if VOR1 is ILS. Convert to degrees by *360/65536. This is 180 degrees different to the direction of flight to follow the localiser.
        private Offset<object> offset163 = new Offset<object>(0x0872); // Size: 2. Description: ILS glideslope inclination if VOR1 is ILS. Convert to degrees by *360/65536
        private Offset<object> offset164 = new Offset<object>(0x0874); // Size: 4. Description: VOR1 Latitude, as in 085C above, except when NAV1 is tuned to an ILS, in which case this gives the localiser Latitude. [FS2002 and later]
        private Offset<object> offset165 = new Offset<object>(0x0878); // Size: 4. Description: [FS2002/4 only]: VOR1 Longitude, as in 0864 above, except when NAV1 is tuned to an ILS, in which case this gives the localiser Longitude.
        private Offset<object> offset166 = new Offset<object>(0x087C); // Size: 4. Description: [FS2002/4 only]: VOR1 Elevation, as in 086C above, except when NAV1 is tuned to an ILS, in which case this gives the localiser Elevation.
        private Offset<object> offset167 = new Offset<object>(0x0880); // Size: 4. Description: [FS2002/4 only]: DME Latitude when available separately. Same units as in 085C above.
        private Offset<object> offset168 = new Offset<object>(0x0884); // Size: 4. Description: [FS2002/4 only]: DME Longitude when available separately. Same units as in 0864 above.
        private Offset<object> offset169 = new Offset<object>(0x0888); // Size: 1. Description: Active engine (select) flags. Bit 0 = Engine 1 selected … Bit 3 = Engine 4 selected. See notes against offset 0892.
        private Offset<object> offset170 = new Offset<object>(0x088C); // Size: 152. Description: ENGINE 1 values, as detailed below
        private Offset<object> offset171 = new Offset<object>(0x088C); // Size: 2. Description: Engine 1 Throttle lever, –4096 to +16384
        private Offset<object> offset172 = new Offset<object>(0x088E); // Size: 2. Description: Engine 1 Prop lever, –4096 to +16384
        private Offset<object> offset173 = new Offset<object>(0x0890); // Size: 2. Description: Engine 1 Mixture lever, 0 – 16384
        private Offset<object> offset174 = new Offset<object>(0x0894); // Size: 2. Description: Engine 1 combustion flag (TRUE if engine firing)
        private Offset<object> offset175 = new Offset<object>(0x0896); // Size: 2. Description: Engine 1 Jet N2 as 0 – 16384 (100%). This also appears to be the Turbine RPM % for proper helo models.
        private Offset<object> offset176 = new Offset<object>(0x0898); // Size: 2. Description: Engine 1 Jet N1 as 0 – 16384 (100%), or Prop RPM (derive RPM by multiplying this value by the RPM Scaler (see 08C8) and dividing by 65536). Note that Prop RPM is signed and negative for counter-rotating propellers.
        private Offset<object> offset177 = new Offset<object>(0x08A0); // Size: 2. Description: Engine 1 Fuel Flow PPH SSL (pounds per hour, standardised to sea level). Don’t know units, but it seems to match some gauges if divided by 128. Not maintained in all cases.
        private Offset<object> offset178 = new Offset<object>(0x08B2); // Size: 2. Description: Engine 1 Anti-Ice or Carb Heat switch (1=On)
        private Offset<object> offset179 = new Offset<object>(0x08B8); // Size: 2. Description: Engine 1 Oil temperature, 16384 = 140 C.
        private Offset<object> offset180 = new Offset<object>(0x08BA); // Size: 2. Description: Engine 1 Oil pressure, 16384 = 55 psi. Not that in some FS2000 aircraft (the B777) this can exceed the 16-bit capacity of this location. FSUIPC limits it to fit, i.e.65535 = 220 psi
        private Offset<object> offset181 = new Offset<object>(0x08BC); // Size: 2. Description: Engine 1 Pressure Ratio (where calculated): 16384 = 1.60
        private Offset<object> offset182 = new Offset<object>(0x08BE); // Size: 2. Description: Engine 1 EGT, 16384 = 860 C. [<em>Note that for Props this value is not actually correct. For FS2004 at least you will get the correct value from 3B70. In FS2004 the value here has been derived by FSUIPC to be compatible with FS2002 et cetera</em>]
        private Offset<object> offset183 = new Offset<object>(0x08C0); // Size: 2. Description: Engine 1 Manifold Pressure: Inches Hg * 1024
        private Offset<object> offset184 = new Offset<object>(0x08C8); // Size: 2. Description: Engine 1 RPM Scaler: For Props, use this to calculate RPM – see offset 0898
        private Offset<object> offset185 = new Offset<object>(0x08D0); // Size: 4. Description: Engine 1 Oil Quantity: 16384 = 100% On FS2000 FSUIPC usually has to derive this from a leakage value as it isn’t provided directly.
        private Offset<object> offset186 = new Offset<object>(0x08D4); // Size: 4. Description: Engine 1 Vibration: 16384 = 5.0. This is a relative measure of amplitude from the sensors on the engine which when too high is an indication of a problem. The value at which you should be concerned varies according to aircraft and engine.
        private Offset<object> offset187 = new Offset<object>(0x08D8); // Size: 4. Description: Engine 1 Hydraulic pressure: appears to be 4*psi
        private Offset<object> offset188 = new Offset<object>(0x08DC); // Size: 4. Description: Engine 1 Hydraulic quantity: 16384 = 100%
        private Offset<object> offset189 = new Offset<object>(0x08E8); // Size: 8. Description: Engine 1 CHT, degrees F in double floating point (FLOAT64)
        private Offset<object> offset190 = new Offset<object>(0x08F0); // Size: 4. Description: Engine 1 Turbine temperature: degree C *16384, valid for FS2004 helo models
        private Offset<object> offset191 = new Offset<object>(0x08F4); // Size: 4. Description: Engine 1 Torque % (16384 = 100%), valid for FS2004 helo models
        private Offset<object> offset192 = new Offset<object>(0x08F8); // Size: 4. Description: Engine 1 Fuel pressure, psf (i.e. psi*144): not all aircraft files provide this, valid for FS2004 helo models.
        private Offset<object> offset193 = new Offset<object>(0x08FC); // Size: 2?. Description: Engine 1 electrical load, possibly valid for FS2004 helo models.
        private Offset<object> offset194 = new Offset<object>(0x0900); // Size: 4. Description: Engine 1 Transmission oil pressure (psi * 16384): for helos
        private Offset<object> offset195 = new Offset<object>(0x0904); // Size: 4. Description: Engine 1 Transmission oil temperature (degrees C * 16384): for helos
        private Offset<object> offset196 = new Offset<object>(0x0908); // Size: 4. Description: Engine 1 Rotor RPM % (16384=100%): for helos
        private Offset<object> offset197 = new Offset<object>(0x0918); // Size: 8. Description: Engine 1 Fuel Flow Pounds per Hour, as floating point double (FLOAT64)
        private Offset<object> offset198 = new Offset<object>(0x0924); // Size: 152. Description: ENGINE 2 values, as detailed below
        private Offset<object> offset199 = new Offset<object>(0x0924); // Size: 2. Description: Engine 2 Throttle lever, –4096 to +16384
        private Offset<object> offset200 = new Offset<object>(0x0926); // Size: 2. Description: Engine 2 Prop lever, –4096 to +16384
        private Offset<object> offset201 = new Offset<object>(0x0928); // Size: 2. Description: Engine 2 Mixture lever, 0 – 16384
        private Offset<object> offset202 = new Offset<object>(0x092A); // Size: 2. Description: Engine 2 Starter switch position (Magnetos),</span><span style="font-family: Tahoma; font-size: x-small;">Jet/turbo: 0=Off, 1=Start, 2=Gen; Prop: 0=Off, 1=right, 2=Left, 3=Both, 4=Start</span><span style="font-family: Tahoma; font-size: x-small;">(See Notes in Engine 1 entry)
        private Offset<object> offset203 = new Offset<object>(0x092C); // Size: 2. Description: Engine 2 combustion flag (TRUE if engine firing)
        private Offset<object> offset204 = new Offset<object>(0x092E); // Size: 2. Description: Engine 2 Jet N2 as 0 – 16384 (100%)
        private Offset<object> offset205 = new Offset<object>(0x0930); // Size: 2. Description: Engine 2 Jet N1 as 0 – 16384 (100%), or Prop RPM (derive RPM by multiplying this value by the RPM Scaler (see 08C8) and dividing by 65536). Note that Prop RPM is signed and negative for counter-rotating propellers.
        private Offset<object> offset206 = new Offset<object>(0x0938); // Size: 2. Description: Engine 2 Fuel Flow PPH SSL (pounds per hour, standardised to sea level). Don’t know units, but it seems to match some gauges if divided by 128. Not maintained in all cases.
        private Offset<object> offset207 = new Offset<object>(0x094A); // Size: 2. Description: Engine 2 Anti-Ice or Carb Heat switch (1=On)
        private Offset<object> offset208 = new Offset<object>(0x0950); // Size: 2. Description: Engine 2 Oil temperature, 16384 = 140 C.
        private Offset<object> offset209 = new Offset<object>(0x0952); // Size: 2. Description: Engine 2 Oil pressure, 16384 = 55 psi. Not that in some FS2000 aircraft (the B777) this can exceed the 16-bit capacity of this location. FSUIPC limits it to fit, i.e.65535 = 220 psi
        private Offset<object> offset210 = new Offset<object>(0x0954); // Size: 2. Description: Engine 2 Pressure Ratio (where calculated): 16384 = 1.60
        private Offset<object> offset211 = new Offset<object>(0x0956); // Size: 2. Description: Engine 2 EGT, 16384 = 860 C. [<em>Note that for Props this value is not actually correct. For FS2004 at least you will get the correct value from 3AB0. In FS2004 the value here has been derived by FSUIPC to be compatible with FS2002 et cetera</em>]
        private Offset<object> offset212 = new Offset<object>(0x0958); // Size: 2. Description: Engine 2 Manifold Pressure: Inches Hg * 1024
        private Offset<object> offset213 = new Offset<object>(0x0960); // Size: 2. Description: Engine 2 RPM Scaler: For Props, use this to calculate RPM – see offset 0898
        private Offset<object> offset214 = new Offset<object>(0x0968); // Size: 4. Description: Engine 2 Oil Quantity: 16384 = 100% On FS2000 FSUIPC usually has to derive this from a leakage value as it isn’t provided directly.
        private Offset<object> offset215 = new Offset<object>(0x096C); // Size: 4. Description: Engine 2 Vibration: 16384 = 5.0. This is a relative measure of amplitude from the sensors on the engine which when too high is an indication of a problem. The value at which you should be concerned varies according to aircraft and engine.
        private Offset<object> offset216 = new Offset<object>(0x0970); // Size: 4. Description: Engine 2 Hydraulic pressure: appears to be 4*psi
        private Offset<object> offset217 = new Offset<object>(0x0974); // Size: 4. Description: Engine 2 Hydraulic quantity: 16384 = 100%
        private Offset<object> offset218 = new Offset<object>(0x0980); // Size: 8. Description: Engine 2 CHT, degrees F in double floating point (FLOAT64)
        private Offset<object> offset219 = new Offset<object>(0x0988); // Size: 4. Description: Engine 2 Turbine temperature: degree C *16384
        private Offset<object> offset220 = new Offset<object>(0x098C); // Size: 4. Description: Engine 2 Torque % (16384 = 100%)
        private Offset<object> offset221 = new Offset<object>(0x0990); // Size: 4. Description: Engine 2 Fuel pressure, psf (i.e. psi*144): not all aircraft files provide this.
        private Offset<object> offset222 = new Offset<object>(0x0998); // Size: 4. Description: Engine 2 Transmission pressure (psi * 16384): for helos
        private Offset<object> offset223 = new Offset<object>(0x099C); // Size: 4. Description: Engine 2 Transmission temperature (degrees C * 16384): for helos
        private Offset<object> offset224 = new Offset<object>(0x09A0); // Size: 4. Description: Engine 2 Rotor RPM % (16384=100%): for helos
        private Offset<object> offset225 = new Offset<object>(0x09B0); // Size: 8. Description: Engine 2 Fuel Flow Pounds per Hour, as floating point double (FLOAT64)
        private Offset<object> offset226 = new Offset<object>(0x09BC); // Size: 152. Description: ENGINE 3 values, as detailed below
        private Offset<object> offset227 = new Offset<object>(0x09BC); // Size: 2. Description: Engine 3 Throttle lever, –4096 to +16384
        private Offset<object> offset228 = new Offset<object>(0x09BE); // Size: 2. Description: Engine 3 Prop lever, –4096 to +16384
        private Offset<object> offset229 = new Offset<object>(0x09C0); // Size: 2. Description: Engine 3 Mixture lever, 0 – 16384
        private Offset<object> offset230 = new Offset<object>(0x09C2); // Size: 2. Description: Engine 3 Starter switch position (Magnetos),</span><span style="font-family: Tahoma; font-size: x-small;">Jet/turbo: 0=Off, 1=Start, 2=Gen; Prop: 0=Off, 1=right, 2=Left, 3=Both, 4=Start</span><span style="font-family: Tahoma; font-size: x-small;">(see Notes in Engine 1 entry)
        private Offset<object> offset231 = new Offset<object>(0x09C4); // Size: 2. Description: Engine 3 combustion flag (TRUE if engine firing)
        private Offset<object> offset232 = new Offset<object>(0x09C6); // Size: 2. Description: Engine 3 Jet N2 as 0 – 16384 (100%)
        private Offset<object> offset233 = new Offset<object>(0x09C8); // Size: 2. Description: Engine 3 Jet N1 as 0 – 16384 (100%), or Prop RPM (derive RPM by multiplying this value by the RPM Scaler (see 08C8) and dividing by 65536). Note that Prop RPM is signed and negative for counter-rotating propellers.
        private Offset<object> offset234 = new Offset<object>(0x09D0); // Size: 2. Description: Engine 3 Fuel Flow PPH SSL (pounds per hour, standardised to sea level). Don’t know units, but it seems to match some gauges if divided by 128. Not maintained in all cases.
        private Offset<object> offset235 = new Offset<object>(0x09E2); // Size: 2. Description: Engine 3 Anti-Ice or Carb Heat switch (1=On)
        private Offset<object> offset236 = new Offset<object>(0x09E8); // Size: 2. Description: Engine 3 Oil temperature, 16384 = 140 C.
        private Offset<object> offset237 = new Offset<object>(0x09EA); // Size: 2. Description: Engine 3 Oil pressure, 16384 = 55 psi. Not that in some FS2000 aircraft (the B777) this can exceed the 16-bit capacity of this location. FSUIPC limits it to fit, i.e.65535 = 220 psi
        private Offset<object> offset238 = new Offset<object>(0x09EC); // Size: 2. Description: Engine 3 Pressure Ratio (where calculated): 16384 = 1.60
        private Offset<object> offset239 = new Offset<object>(0x09EE); // Size: 2. Description: Engine 3 EGT, 16384 = 860 C. [<em>Note that for Props this value is not actually correct. For FS2004 at least you will get the correct value from 39F0. In FS2004 the value here has been derived by FSUIPC to be compatible with FS2002 et cetera</em>]
        private Offset<object> offset240 = new Offset<object>(0x09F0); // Size: 2. Description: Engine 3 Manifold Pressure: Inches Hg * 1024
        private Offset<object> offset241 = new Offset<object>(0x09F8); // Size: 2. Description: Engine 3 RPM Scaler: For Props, use this to calculate RPM – see offset 0898
        private Offset<object> offset242 = new Offset<object>(0x0A00); // Size: 4. Description: Engine 3 Oil Quantity: 16384 = 100% On FS2000 FSUIPC usually has to derive this from a leakage value as it isn’t provided directly.
        private Offset<object> offset243 = new Offset<object>(0x0A04); // Size: 4. Description: Engine 3 Vibration: 16384 = 5.0. This is a relative measure of amplitude from the sensors on the engine which when too high is an indication of a problem. The value at which you should be concerned varies according to aircraft and engine.
        private Offset<object> offset244 = new Offset<object>(0x0A08); // Size: 4. Description: Engine 3 Hydraulic pressure: appears to be 4*psi
        private Offset<object> offset245 = new Offset<object>(0x0A0C); // Size: 4. Description: Engine 3 Hydraulic quantity: 16384 = 100%
        private Offset<object> offset246 = new Offset<object>(0x0A18); // Size: 8. Description: Engine 3 CHT, degrees F in double floating point (FLOAT64)
        private Offset<object> offset247 = new Offset<object>(0x0A20); // Size: 4. Description: Engine 3 Turbine temperature: degree C *16384
        private Offset<object> offset248 = new Offset<object>(0x0A24); // Size: 4. Description: Engine 3 Torque % (16384 = 100%)
        private Offset<object> offset249 = new Offset<object>(0x0A28); // Size: 4. Description: Engine 3 Fuel pressure, psf (i.e. psi*144): not all aircraft files provide this.
        private Offset<object> offset250 = new Offset<object>(0x0A30); // Size: 4. Description: Engine 3 Transmission pressure (psi * 16384): for helos
        private Offset<object> offset251 = new Offset<object>(0x0A34); // Size: 4. Description: Engine 3 Transmission temperature (degrees C * 16384): for helos
        private Offset<object> offset252 = new Offset<object>(0x0A38); // Size: 4. Description: Engine 3 Rotor RPM % (16384=100%): for helos
        private Offset<object> offset253 = new Offset<object>(0x0A48); // Size: 8. Description: Engine 3 Fuel Flow Pounds per Hour, as floating point double (FLOAT64)
        private Offset<object> offset254 = new Offset<object>(0x0A54); // Size: 152. Description: ENGINE 4 values, as detailed below
        private Offset<object> offset255 = new Offset<object>(0x0A54); // Size: 2. Description: Engine 4 Throttle lever, –4096 to +16384
        private Offset<object> offset256 = new Offset<object>(0x0A56); // Size: 2. Description: Engine 4 Prop lever, –4096 to +16384
        private Offset<object> offset257 = new Offset<object>(0x0A58); // Size: 2. Description: Engine 4 Mixture lever, 0 – 16384
        private Offset<object> offset258 = new Offset<object>(0x0A5A); // Size: 2. Description: Engine 4 Starter switch position (Magnetos),</span><span style="font-family: Tahoma; font-size: x-small;">Jet/turbo: 0=Off, 1=Start, 2=Gen; Prop: 0=Off, 1=right, 2=Left, 3=Both, 4=Start</span><span style="font-family: Tahoma; font-size: x-small;">(see Notes in Engine 1 entry)
        private Offset<object> offset259 = new Offset<object>(0x0A5C); // Size: 2. Description: Engine 4 combustion flag (TRUE if engine firing)
        private Offset<object> offset260 = new Offset<object>(0x0A5E); // Size: 2. Description: Engine 4 Jet N2 as 0 – 16384 (100%)
        private Offset<object> offset261 = new Offset<object>(0x0A60); // Size: 2. Description: Engine 4 Jet N1 as 0 – 16384 (100%), or Prop RPM (derive RPM by multiplying this value by the RPM Scaler (see 08C8) and dividing by 65536). Note that Prop RPM is signed and negative for counter-rotating propellers.
        private Offset<object> offset262 = new Offset<object>(0x0A68); // Size: 2. Description: Engine 4 Fuel Flow PPH SSL (pounds per hour, standardised to sea level). Don’t know units, but it seems to match some gauges if divided by 128. Not maintained in all cases.
        private Offset<object> offset263 = new Offset<object>(0x0A7A); // Size: 2. Description: Engine 4 Anti-Ice or Carb Heat switch (1=On)
        private Offset<object> offset264 = new Offset<object>(0x0A80); // Size: 2. Description: Engine 4 Oil temperature, 16384 = 140 C.
        private Offset<object> offset265 = new Offset<object>(0x0A82); // Size: 2. Description: Engine 4 Oil pressure, 16384 = 55 psi. Not that in some FS2000 aircraft (the B777) this can exceed the 16-bit capacity of this location. FSUIPC limits it to fit, i.e.65535 = 220 psi
        private Offset<object> offset266 = new Offset<object>(0x0A84); // Size: 2. Description: Engine 4 Pressure Ratio (where calculated): 16384 = 1.60
        private Offset<object> offset267 = new Offset<object>(0x0A86); // Size: 2. Description: Engine 4 EGT, 16384 = 860 C. [<em>Note that for Props this value is not actually correct. For FS2004 at least you will get the correct value from 3930. In FS2004 the value here has been derived by FSUIPC to be compatible with FS2002 et cetera</em>]
        private Offset<object> offset268 = new Offset<object>(0x0A88); // Size: 2. Description: Engine 4 Manifold Pressure: Inches Hg * 1024
        private Offset<object> offset269 = new Offset<object>(0x0A90); // Size: 2. Description: Engine 4 RPM Scaler: For Props, use this to calculate RPM – see offset 0898
        private Offset<object> offset270 = new Offset<object>(0x0A98); // Size: 4. Description: Engine 4 Oil Quantity: 16384 = 100% On FS2000 FSUIPC usually has to derive this from a leakage value as it isn’t provided directly.
        private Offset<object> offset271 = new Offset<object>(0x0A9C); // Size: 4. Description: Engine 4 Vibration: 16384 = 5.0. This is a relative measure of amplitude from the sensors on the engine which when too high is an indication of a problem. The value at which you should be concerned varies according to aircraft and engine.
        private Offset<object> offset272 = new Offset<object>(0x0AA0); // Size: 4. Description: Engine 4 Hydraulic pressure: appears to be 4*psi
        private Offset<object> offset273 = new Offset<object>(0x0AA4); // Size: 4. Description: Engine 4 Hydraulic quantity: 16384 = 100%
        private Offset<object> offset274 = new Offset<object>(0x0AB0); // Size: 8. Description: Engine 4 CHT, degrees F in double floating point (FLOAT64)
        private Offset<object> offset275 = new Offset<object>(0x0AB8); // Size: 4. Description: Engine 4 Turbine temperature: degree C *16384
        private Offset<object> offset276 = new Offset<object>(0x0ABC); // Size: 4. Description: Engine 4 Torque % (16384 = 100%)
        private Offset<object> offset277 = new Offset<object>(0x0AC0); // Size: 4. Description: Engine 4 Fuel pressure, psf (i.e. psi*144): not all aircraft files provide this.
        private Offset<object> offset278 = new Offset<object>(0x0AC8); // Size: 4. Description: Engine 4 Transmission pressure (psi * 16384): for helos
        private Offset<object> offset279 = new Offset<object>(0x0ACC); // Size: 4. Description: Engine 4 Transmission temperature (degrees C * 16384): for helos
        private Offset<object> offset280 = new Offset<object>(0x0AD0); // Size: 4. Description: Engine 4 Rotor RPM % (16384=100%): for helos
        private Offset<object> offset281 = new Offset<object>(0x0AE0); // Size: 8. Description: Engine 4 Fuel Flow Pounds per Hour, as floating point double (FLOAT64)
        private Offset<object> offset282 = new Offset<object>(0x0AEC); // Size: 2. Description: Number of Engines
        private Offset<object> offset283 = new Offset<object>(0x0AF0); // Size: 2. Description: Propeller pitch control: 0=Fixed, 1=Auto, 2=Manual. On FS2004 this is 0=fixed pitch, 1=constant speed, no differentiation between auto and manual.
        private Offset<object> offset284 = new Offset<object>(0x0AF4); // Size: 2. Description: Fuel weight as pounds per gallon * 256
        private Offset<object> offset285 = new Offset<object>(0x0B00); // Size: 2. Description: Throttle lower limit, 16384=100%. (e.g. for aircraft with reverse thrust this is normally:</span><span style="font-family: Tahoma; font-size: x-small;"> –4096 indicating 25% in reverse)
        private Offset<object> offset286 = new Offset<object>(0x0B0C); // Size: 4. Description: Mach Max Operating speed *20480
        private Offset<object> offset287 = new Offset<object>(0x0B18); // Size: 8. Description: Gyro suction in inches of mercury (Hg), floating point double (FLOAT64)
        private Offset<object> offset288 = new Offset<object>(0x0B20); // Size: 2. Description: Sound control: 0 to switch off, 1 to switch on
        private Offset<object> offset289 = new Offset<object>(0x0B24); // Size: 2. Description: Sound flag: reads 0 is off, 1 if on
        private Offset<object> offset290 = new Offset<object>(0x0B4C); // Size: 2. Description: Ground altitude (metres). See 0020 for more accuracy.
        private Offset<object> offset291 = new Offset<object>(0x0B60); // Size: 2. Description: Scenery complexity level, 0 – 4 in FS98, 0 – 5 in FS2000 on
        private Offset<object> offset292 = new Offset<object>(0x0B64); // Size: 1. Description: Fail mode: 0 ok, ADF inoperable = 1 (both ADFs on FS2004)
        private Offset<object> offset293 = new Offset<object>(0x0B65); // Size: 1. Description: Fail mode: 0 ok, ASI inoperable = 1
        private Offset<object> offset294 = new Offset<object>(0x0B66); // Size: 1. Description: Fail mode: 0 ok, Altimeter inoperable = 1
        private Offset<object> offset295 = new Offset<object>(0x0B67); // Size: 1. Description: Fail mode: 0 ok, Attitude Indicator inoperable = 1
        private Offset<object> offset296 = new Offset<object>(0x0B68); // Size: 1. Description: Fail mode: 0 ok, COM1 radio inoperable = 1</span><span style="font-family: Tahoma; font-size: x-small;">See also 3BD6 (FS2002/FS2004)
        private Offset<object> offset297 = new Offset<object>(0x0B69); // Size: 1. Description: Fail mode: 0 ok, Mag Compass inoperable = 1
        private Offset<object> offset298 = new Offset<object>(0x0B6A); // Size: 1. Description: Fail mode: 0 ok, Electrics inoperable = 1
        private Offset<object> offset299 = new Offset<object>(0x0B6B); // Size: 1. Description: Fail mode: 0 ok, Engine inoperable = 1, extended for FS2000/CFS2 for up to 4 individual engines: bit 0 =Engine 1 … bit 3= Engine 4. (<em>but note that this may not work for FS98 aircraft transposed into FS2k/CFS2</em>).
        private Offset<object> offset300 = new Offset<object>(0x0B6C); // Size: 1. Description: Fail mode: 0 ok, Fuel indicators inoperable = 1
        private Offset<object> offset301 = new Offset<object>(0x0B6D); // Size: 1. Description: Fail mode: 0 ok, Direction Indicator inoperable = 1
        private Offset<object> offset302 = new Offset<object>(0x0B6E); // Size: 1. Description: Fail mode: 0 ok, VSI inoperable = 1
        private Offset<object> offset303 = new Offset<object>(0x0B6F); // Size: 1. Description: Fail mode: 0 ok, Transponder inoperable = 1
        private Offset<object> offset304 = new Offset<object>(0x0B70); // Size: 1. Description: Fail mode: 0 ok, NAV radios inoperable = 1 (NAV1 only in FS2002 and FS2004: see also 3BD6)
        private Offset<object> offset305 = new Offset<object>(0x0B71); // Size: 1. Description: Fail mode: 0 ok, Pitot inoperable = 1
        private Offset<object> offset306 = new Offset<object>(0x0B72); // Size: 1. Description: Fail mode: 0 ok, Turn coordinator inoperable = 1
        private Offset<object> offset307 = new Offset<object>(0x0B73); // Size: 1. Description: Fail mode: 0 ok, Vacuum inoperable = 1
        private Offset<object> offset308 = new Offset<object>(0x0B74); // Size: 4. Description: Fuel: centre tank level, % * 128 * 65536
        private Offset<object> offset309 = new Offset<object>(0x0B78); // Size: 4. Description: Fuel: centre tank capacity: US Gallons (see also offsets 1244– for extra FS2k/CFS2 fuel tanks)
        private Offset<object> offset310 = new Offset<object>(0x0B7C); // Size: 4. Description: Fuel: left main tank level, % * 128 * 65536
        private Offset<object> offset311 = new Offset<object>(0x0B80); // Size: 4. Description: Fuel: left main tank capacity: US Gallons
        private Offset<object> offset312 = new Offset<object>(0x0B84); // Size: 4. Description: Fuel: left aux tank level, % * 128 * 65536
        private Offset<object> offset313 = new Offset<object>(0x0B88); // Size: 4. Description: Fuel: left aux tank capacity: US Gallons
        private Offset<object> offset314 = new Offset<object>(0x0B8C); // Size: 4. Description: Fuel: left tip tank level, % * 128 * 65536
        private Offset<object> offset315 = new Offset<object>(0x0B90); // Size: 4. Description: Fuel: left tip tank capacity: US Gallons
        private Offset<object> offset316 = new Offset<object>(0x0B94); // Size: 4. Description: Fuel: right main tank level, % * 128 * 65536
        private Offset<object> offset317 = new Offset<object>(0x0B98); // Size: 4. Description: Fuel: right main tank capacity: US Gallons
        private Offset<object> offset318 = new Offset<object>(0x0B9C); // Size: 4. Description: Fuel: right aux tank level, % * 128 * 65536
        private Offset<object> offset319 = new Offset<object>(0x0BA0); // Size: 4. Description: Fuel: right aux tank capacity: US Gallons
        private Offset<object> offset320 = new Offset<object>(0x0BA4); // Size: 4. Description: Fuel: right tip tank level, % * 128 * 65536
        private Offset<object> offset321 = new Offset<object>(0x0BA8); // Size: 4. Description: Fuel: right tip tank capacity: US Gallons
        private Offset<object> offset322 = new Offset<object>(0x0BAC); // Size: 2. Description: Inner Marker: activated when TRUE
        private Offset<object> offset323 = new Offset<object>(0x0BAE); // Size: 2. Description: Middle Marker: activated when TRUE
        private Offset<object> offset324 = new Offset<object>(0x0BB0); // Size: 2. Description: Outer Marker: activated when TRUE
        private Offset<object> offset325 = new Offset<object>(0x0BB2); // Size: 2. Description: Elevator control input: –16383 to +16383
        private Offset<object> offset326 = new Offset<object>(0x0BB4); // Size: 2. Description: Elevator position indicator (maybe adjusted from input!)
        private Offset<object> offset327 = new Offset<object>(0x0BB6); // Size: 2. Description: Aileron control input: –16383 to +16383
        private Offset<object> offset328 = new Offset<object>(0x0BB8); // Size: 2. Description: Aileron position indicator (maybe adjusted from input!)
        private Offset<object> offset329 = new Offset<object>(0x0BBA); // Size: 2. Description: Rudder control input: –16383 to +16383
        private Offset<object> offset330 = new Offset<object>(0x0BBC); // Size: 2. Description: Rudder position indicator (maybe adjusted from input!)
        private Offset<object> offset331 = new Offset<object>(0x0BC0); // Size: 2. Description: Elevator trim control input: –16383 to +16383
        private Offset<object> offset332 = new Offset<object>(0x0BC2); // Size: 2. Description: Elevator trim indicator (follows input)
        private Offset<object> offset333 = new Offset<object>(0x0BC4); // Size: 2. Description: Left brake application read-out (0 off, 16383 full: parking brake=16383). You can apply a fixed brake pressure here, or else use the byte at 0C01 to apply brakes emulating the keypress.</span><span style="font-family: Tahoma; font-size: x-small;">[<em>Note: In FS2002 reading this ranges up to 32767, i.e. twice the written value.</em>]
        private Offset<object> offset334 = new Offset<object>(0x0BC6); // Size: 2. Description: Right brake application read-out (0 off, 16383 full: parking brake=16383). You can apply a fixed brake pressure here, or else use the byte at 0C00 to apply brakes emulating the keypress.</span><span style="font-family: Tahoma; font-size: x-small;">[<em>Note: In FS2002 reading this ranges up to 32767, i.e. twice the written value.</em>]
        private Offset<object> offset335 = new Offset<object>(0x0BC8); // Size: 2. Description: Parking brake: 0=off, 32767=on
        private Offset<object> offset336 = new Offset<object>(0x0BCA); // Size: 2. Description: Braking indicator: brake applied if non-zero (16383=on, 0=off). <em>Note that in FS2002 this is artificially created by FSUIPC from the previous three settings.</em>
        private Offset<object> offset337 = new Offset<object>(0x0BCC); // Size: 4. Description: Spoilers arm (0=off, 1=arm for auto deployment)
        private Offset<object> offset338 = new Offset<object>(0x0BD0); // Size: 4. Description: Spoilers control, 0 off to 16383 fully deployed (4800 is set by arming)
        private Offset<object> offset339 = new Offset<object>(0x0BD4); // Size: 4. Description: Spoiler Left position indicator (0-16383)
        private Offset<object> offset340 = new Offset<object>(0x0BD8); // Size: 4. Description: Spoiler Right position indicator (0-16383)
        private Offset<object> offset341 = new Offset<object>(0x0BDC); // Size: 4. Description: Flaps control, 0=up, 16383=full. The “notches” for different aircraft are spaced equally across this range: calculate the increment by 16383/(number of positions-1), ignoring fractions. See also offset 3BFA below.</span><span style="font-family: Tahoma; font-size: x-small;">N.B. Do not expect to read this and see 100% accurate values. For example, 3&#215;2047=6141 for the 3<sup>rd</sup> dTtente up. But FS2000, at least, stores the flaps lever position in the FLT file as a % of 16384, and the percentage is stored to two decimal places. 6141 gets saved as 37.48% which converts back to 6140.7232 and this gets truncated here as 6140. However, 6140/2047 = 2.9995 which is as close as you need. Just round if you are using integers.
        private Offset<object> offset342 = new Offset<object>(0x0BE0); // Size: 4. Description: Flaps position indicator (left). Note that in FS2002 and FS2004 this gives the correct proportional amount, with 16383=full deflection. It doesn’t correspond to the equally spaced notches used for the control lever. If you know the maximum deflection angle you can derive the current angle by ((max * position indicator) / 16383).</span><span style="font-family: Tahoma; font-size: x-small;">Also, in FS2002 and FS2004 this only gives the inboard trailing edge flaps. Please see offsets 30E0–30FF for greater details where needed.
        private Offset<object> offset343 = new Offset<object>(0x0BE4); // Size: 4. Description: Flaps position indicator (right). Note that in FS2002 and FS2004 this gives the correct proportional amount, with 16384=full deflection. It doesn’t correspond to the equally spaced notches used for the control lever.</span><span style="font-family: Tahoma; font-size: x-small;">Also, in FS2002 and FS2004 this only gives the inboard trailing edge flaps. Please see offsets 30E0–30FF for greater details where needed.
        private Offset<object> offset344 = new Offset<object>(0x0BE8); // Size: 4. Description: Gear control: 0=Up, 16383=Down
        private Offset<object> offset345 = new Offset<object>(0x0BEC); // Size: 4. Description: Gear position (nose): 0=full up, 16383=full down
        private Offset<object> offset346 = new Offset<object>(0x0BF0); // Size: 4. Description: Gear position (right): 0=full up, 16383=full down
        private Offset<object> offset347 = new Offset<object>(0x0BF4); // Size: 4. Description: Gear position (left): 0=full up, 16383=full down
        private Offset<object> offset348 = new Offset<object>(0x0BF8); // Size: 4. Description: Unlimited visibility value, as 1600* statute miles. This is the value set in the Display Quality Settings.
        private Offset<object> offset349 = new Offset<object>(0x0C00); // Size: 1. Description: Right toe brake control: 0 – 200, proportional braking with timed decay
        private Offset<object> offset350 = new Offset<object>(0x0C01); // Size: 1. Description: Left toe brake control: 0 –200, proportional braking with timed decay
        private Offset<object> offset351 = new Offset<object>(0x0C18); // Size: 2. Description: International units: 0=US, 1=Metric+feet, 2=Metric+metres
        private Offset<object> offset352 = new Offset<object>(0x0C1A); // Size: 2. Description: Simulation rate *256 (i.e. 256=1x)
        private Offset<object> offset353 = new Offset<object>(0x0C20); // Size: 9. Description: Local time in character format: “hh:mm:ss” (with zero terminator)
        private Offset<object> offset354 = new Offset<object>(0x0C29); // Size: 5. Description: DME1 distance as character string, either “nn.n” or “nnn.” (when &gt; 99.9 nm). The 5<sup>th</sup> character may be a zero or a space. Don’t rely on it.
        private Offset<object> offset355 = new Offset<object>(0x0C2E); // Size: 5. Description: DME1 speed as character string, “nnn” followed by either space then zero or just zero.
        private Offset<object> offset356 = new Offset<object>(0x0C33); // Size: 5. Description: DME2 distance as character string, either “nn.n” or “nnn.” (when &gt; 99.9 nm). The 5<sup>th</sup> character may be a zero or a space. Don’t rely on it.
        private Offset<object> offset357 = new Offset<object>(0x0C38); // Size: 5. Description: DME2 speed as character string, “nnn” followed by either space then zero or just zero.
        private Offset<object> offset358 = new Offset<object>(0x0C3E); // Size: 2. Description: Gyro drift amount ( *360/65536 for degrees).
        private Offset<object> offset359 = new Offset<object>(0x0C44); // Size: 2. Description: Realism setting, 0 – 100
        private Offset<object> offset360 = new Offset<object>(0x0C48); // Size: 1. Description: NAV1 Localiser Needle: –127 left to +127 right
        private Offset<object> offset361 = new Offset<object>(0x0C49); // Size: 1. Description: NAV1 Glideslope Needle: –127 up to +127 down
        private Offset<object> offset362 = new Offset<object>(0x0C4B); // Size: 1. Description: NAV1 To/From flag: 0=not active, 1=To, 2=From
        private Offset<object> offset363 = new Offset<object>(0x0C4C); // Size: 1. Description: NAV1 GS flag: TRUE if GS alive
        private Offset<object> offset364 = new Offset<object>(0x0C4E); // Size: 2. Description: NAV1 OBS setting (degrees, 0–359)
        private Offset<object> offset365 = new Offset<object>(0x0C50); // Size: 2. Description: NAV1 radial ( *360/65536 for degrees)
        private Offset<object> offset366 = new Offset<object>(0x0C59); // Size: 1. Description: NAV2 Localiser Needle: –127 left to +127 right
        private Offset<object> offset367 = new Offset<object>(0x0C5B); // Size: 1. Description: NAV2 To/From flag: 0=not active, 1=To, 2=From
        private Offset<object> offset368 = new Offset<object>(0x0C5E); // Size: 2. Description: NAV2 OBS setting (degrees, 0–359)
        private Offset<object> offset369 = new Offset<object>(0x0C60); // Size: 2. Description: NAV2 radial ( *360/65536 for degrees)
        private Offset<object> offset370 = new Offset<object>(0x0C6A); // Size: 2. Description: ADF1: relative bearing to NDB ( *360/65536 for degrees, –ve left, +ve right)
        private Offset<object> offset371 = new Offset<object>(0x0C6C); // Size: 2. Description: ADF1: dial bearing, where adjustable (in degrees, 1–360)
        private Offset<object> offset372 = new Offset<object>(0x0C92); // Size: 2. Description: Texture quality, 0–3, as on FS2K’s slider in Display Quality
        private Offset<object> offset373 = new Offset<object>(0x0D50); // Size: 24. Description: The Tower Latitude (8 bytes), Longitude (8 bytes) and Altitude (8 bytes) in the same format as 0560–0577 above.
        private Offset<object> offset374 = new Offset<object>(0x0D98); // Size: 2. Description: International N/S setting: 2=North, 3=South
        private Offset<object> offset375 = new Offset<object>(0x0D9C); // Size: 2. Description: International E/W setting: 0=East, 1=West
        private Offset<object> offset376 = new Offset<object>(0x0DD6); // Size: 2. Description: Scenery BGL variable “usrvar” (originally 0312h in BGL)</span><span style="font-family: Tahoma; font-size: x-small;"><em>(*In FS2004 this is moved to globals by FSUIPC unless ‘MoveBGLvariables=No’)</em>
        private Offset<object> offset377 = new Offset<object>(0x0DD8); // Size: 2. Description: Scenery BGL variable “usrvr2” (originally 0314h in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(*In FS2004 this is moved to globals by FSUIPC unless ‘MoveBGLvariables=No’)</em>
        private Offset<object> offset378 = new Offset<object>(0x0DDA); // Size: 2. Description: Scenery BGL variable “usrvr3” (originally 0316h in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(*In FS2004 this is moved to globals by FSUIPC unless ‘MoveBGLvariables=No’)</em>
        private Offset<object> offset379 = new Offset<object>(0x0DDC); // Size: 2. Description: Scenery BGL variable “usrvr4” (originally 0318h in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(*In FS2004 this is moved to globals by FSUIPC unless ‘MoveBGLvariables=No’)</em>
        private Offset<object> offset380 = new Offset<object>(0x0DDE); // Size: 2. Description: Scenery BGL variable “usrvr5” (originally 031Ah in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(*In FS2004 this is moved to globals by FSUIPC unless ‘MoveBGLvariables=No’)</em>
        private Offset<object> offset381 = new Offset<object>(0x0DE2); // Size: 2. Description: Scenery BGL variable “spar10” (originally 031Eh in BGL)</span><span style="font-family: Tahoma; font-size: x-small;"><em>(doesn’t appear to be supported at all in FS2004)</em>
        private Offset<object> offset382 = new Offset<object>(0x0DE4); // Size: 2. Description: Scenery BGL variable “spar11” (originally 0320h in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(doesn’t appear to be supported at all in FS2004)</em>
        private Offset<object> offset383 = new Offset<object>(0x0DE6); // Size: 2. Description: Scenery BGL variable “spar12” (originally 0322h in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(doesn’t appear to be supported at all in FS2004)</em>
        private Offset<object> offset384 = new Offset<object>(0x0DE8); // Size: 2. Description: Scenery BGL variable “spar13” (originally 0324h in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(doesn’t appear to be supported at all in FS2004)</em>
        private Offset<object> offset385 = new Offset<object>(0x0DEA); // Size: 2. Description: Scenery BGL variable “spar14” (originally 0326h in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(doesn’t appear to be supported at all in FS2004)</em>
        private Offset<object> offset386 = new Offset<object>(0x0DEC); // Size: 2. Description: Scenery BGL variable “spar15” (originally 0328h in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(doesn’t appear to be supported at all in FS2004)</em>
        private Offset<object> offset387 = new Offset<object>(0x0DEE); // Size: 2. Description: Scenery BGL variable “spar16” (originally 032Ah in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(doesn’t appear to be supported at all in FS2004)</em>
        private Offset<object> offset388 = new Offset<object>(0x0DF0); // Size: 2. Description: Scenery BGL variable “spar17” (originally 032Ch in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(doesn’t appear to be supported at all in FS2004)</em>
        private Offset<object> offset389 = new Offset<object>(0x0DF2); // Size: 2. Description: Scenery BGL variable “spar18” (originally 032Eh in BGL) </span><span style="font-family: Tahoma; font-size: x-small;"><em>(doesn’t appear to be supported at all in FS2004)</em>
        private Offset<object> offset390 = new Offset<object>(0x0E5A); // Size: 2. Description: EFIS active (1=enabled)
        private Offset<object> offset391 = new Offset<object>(0x0E5C); // Size: 2. Description: EFIS VOR/ILS elevation in metres
        private Offset<object> offset392 = new Offset<object>(0x0E5E); // Size: 2. Description: EFIS density: 0=thin, 1=medium, 2=thick
        private Offset<object> offset393 = new Offset<object>(0x0E60); // Size: 2. Description: EFIS range: 0=short, 1=medium, 2=long
        private Offset<object> offset394 = new Offset<object>(0x0E62); // Size: 2. Description: EFIS mode: 0=normal, 1=reset, 2=plot intercept
        private Offset<object> offset395 = new Offset<object>(0x0E64); // Size: 2. Description: EFIS via VOR (2) or ILS (4)
        private Offset<object> offset396 = new Offset<object>(0x0E66); // Size: 2. Description: EFIS NAV select (1 or 2)
        private Offset<object> offset397 = new Offset<object>(0x0E68); // Size: 2. Description: EFIS display type: 0=rectangles, 1=telegraph poles, 2=yellow brick road
        private Offset<object> offset398 = new Offset<object>(0x0E8A); // Size: 2. Description: Current visibility (Statue miles * 100)
        private Offset<object> offset399 = new Offset<object>(0x0E8C); // Size: 2. Description: Outside Air Temperature (OAT), degrees C * 256
        private Offset<object> offset400 = new Offset<object>(0x0E90); // Size: 2. Description: Ambient wind speed (at aircraft) in knots
        private Offset<object> offset401 = new Offset<object>(0x0E92); // Size: 2. Description: Ambient wind direction (at aircraft), *360/65536 to get degrees Magnetic <em>or</em> True.</span><span style="font-family: Tahoma; font-size: x-small;">For compatibility with FS98, the direction is Magnetic for surface winds (aircraft below the altitude set into offset 0EEE), but True for all upper winds. See offset 02A0 for magnetic variation and how to convert.
        private Offset<object> offset402 = new Offset<object>(0x0E9A); // Size: 112. Description: Current Weather as Set: details follow. [See 0F1C for Global weather setting area]</span><span style="font-family: Tahoma; font-size: x-small;"><em>On FS2000/CFS2 FSUIPC maps writes to this area to the Global weather area starting at 0F1C, and reads from the Global weather area to this Current weather area. Therefore you may not always read back what you last wrote. The main differences occur when FS local weather is in operation.</em></span><span style="font-family: Tahoma; font-size: x-small;">N.B. See also 0E8A above, which is the “current” visibility equivalent of the global setting at 0F8C.
        private Offset<object> offset403 = new Offset<object>(0x0E9A); // Size: 2. Description: Upper cloud layer ceiling in metres AMSL
        private Offset<object> offset404 = new Offset<object>(0x0E9C); // Size: 2. Description: Upper cloud layer base in metres AMSL
        private Offset<object> offset405 = new Offset<object>(0x0E9E); // Size: 2. Description: Upper cloud layer coverage, 65535 = 8 oktas, … 32768= 4 oktas … 0 = clear
        private Offset<object> offset406 = new Offset<object>(0x0EA0); // Size: 2. Description: Upper cloud layer, cloud altitude variation (metres)
        private Offset<object> offset407 = new Offset<object>(0x0EA2); // Size: 2. Description: Lower cloud layer ceiling in metres AMSL
        private Offset<object> offset408 = new Offset<object>(0x0EA4); // Size: 2. Description: Lower cloud layer base in metres AMSL
        private Offset<object> offset409 = new Offset<object>(0x0EA6); // Size: 2. Description: Lower cloud layer coverage, 65535 = 8 oktas, … 32768= 4 oktas … 0 = clear
        private Offset<object> offset410 = new Offset<object>(0x0EA8); // Size: 2. Description: Lower cloud layer, cloud altitude variation (metres)
        private Offset<object> offset411 = new Offset<object>(0x0EAA); // Size: 2. Description: Storm layer ceiling in metres AMSL
        private Offset<object> offset412 = new Offset<object>(0x0EAC); // Size: 2. Description: Storm layer base in metres AMSL (if a Storm layer is present, it must be the lowest, below “Lower Cloud”).
        private Offset<object> offset413 = new Offset<object>(0x0EAE); // Size: 2. Description: Storm cloud layer coverage, 65535 = 8 oktas, … 32768= 4 oktas … 0 = clear
        private Offset<object> offset414 = new Offset<object>(0x0EB0); // Size: 2. Description: Storm cloud layer, cloud altitude variation (metres)
        private Offset<object> offset415 = new Offset<object>(0x0EB2); // Size: 2. Description: Upper Temperature level, metres AMSL
        private Offset<object> offset416 = new Offset<object>(0x0EB4); // Size: 2. Description: Upper Temperature in degrees C * 256
        private Offset<object> offset417 = new Offset<object>(0x0EB6); // Size: 2. Description: Middle Temperature level, metres AMSL
        private Offset<object> offset418 = new Offset<object>(0x0EB8); // Size: 2. Description: Middle Temperature in degrees C * 256
        private Offset<object> offset419 = new Offset<object>(0x0EBA); // Size: 2. Description: Lower Temperature level, metres AMSL
        private Offset<object> offset420 = new Offset<object>(0x0EBC); // Size: 2. Description: Lower Temperature in degrees C * 256
        private Offset<object> offset421 = new Offset<object>(0x0EBE); // Size: 2. Description: Surface Temperature level, metres AMSL (best to be the ground elevation)
        private Offset<object> offset422 = new Offset<object>(0x0EC0); // Size: 2. Description: Surface Temperature in degrees C * 256
        private Offset<object> offset423 = new Offset<object>(0x0EC2); // Size: 2. Description: Temperature drift, degrees C *256 (not used in FS2k/CFS2?)
        private Offset<object> offset424 = new Offset<object>(0x0EC4); // Size: 2. Description: Temperature day/night variation, degrees C *256
        private Offset<object> offset425 = new Offset<object>(0x0EC6); // Size: 2. Description: Pressure (QNH) as millibars (hectoPascals) *16.
        private Offset<object> offset426 = new Offset<object>(0x0EC8); // Size: 2. Description: Pressure drift as millibars *16 (not used on FS2k/CFS2?)
        private Offset<object> offset427 = new Offset<object>(0x0ECA); // Size: 2. Description: Upper wind ceiling, metres AMSL
        private Offset<object> offset428 = new Offset<object>(0x0ECC); // Size: 2. Description: Upper wind base, metres AMSL
        private Offset<object> offset429 = new Offset<object>(0x0ECE); // Size: 2. Description: Upper wind speed, knots
        private Offset<object> offset430 = new Offset<object>(0x0ED0); // Size: 2. Description: Upper wind direction, *360/65536 gives degrees True
        private Offset<object> offset431 = new Offset<object>(0x0ED2); // Size: 2. Description: Upper wind turbulence setting, 0 none, 64, 128, 192, 224, 255 worst
        private Offset<object> offset432 = new Offset<object>(0x0ED4); // Size: 2. Description: Upper wind gusts, enabled if True.
        private Offset<object> offset433 = new Offset<object>(0x0ED6); // Size: 2. Description: Middle wind ceiling, metres AMSL
        private Offset<object> offset434 = new Offset<object>(0x0ED8); // Size: 2. Description: Middle wind base, metres AMSL
        private Offset<object> offset435 = new Offset<object>(0x0EDA); // Size: 2. Description: Middle wind speed, knots
        private Offset<object> offset436 = new Offset<object>(0x0EDC); // Size: 2. Description: Middle wind direction, *360/65536 gives degrees True
        private Offset<object> offset437 = new Offset<object>(0x0EDE); // Size: 2. Description: Middle wind turbulence setting, 0 none, 64, 128, 192, 224, 255 worst
        private Offset<object> offset438 = new Offset<object>(0x0EE0); // Size: 2. Description: Middle wind gusts, enabled if True.
        private Offset<object> offset439 = new Offset<object>(0x0EE2); // Size: 2. Description: Lower wind ceiling, metres AMSL
        private Offset<object> offset440 = new Offset<object>(0x0EE4); // Size: 2. Description: Lower wind base, metres AMSL
        private Offset<object> offset441 = new Offset<object>(0x0EE6); // Size: 2. Description: Lower wind speed, knots
        private Offset<object> offset442 = new Offset<object>(0x0EE8); // Size: 2. Description: Lower wind direction, *360/65536 gives degrees True
        private Offset<object> offset443 = new Offset<object>(0x0EEA); // Size: 2. Description: Lower wind turbulence setting, 0 none, 64, 128, 192, 224, 255 worst
        private Offset<object> offset444 = new Offset<object>(0x0EEC); // Size: 2. Description: Lower wind gusts, enabled if True.
        private Offset<object> offset445 = new Offset<object>(0x0EEE); // Size: 2. Description: Surface wind ceiling, metres AMSL
        private Offset<object> offset446 = new Offset<object>(0x0EF0); // Size: 2. Description: Surface wind speed, knots. [See also 04D8]
        private Offset<object> offset447 = new Offset<object>(0x0EF2); // Size: 2. Description: Surface wind direction, *360/65536 gives degrees Magnetic (!). [See also 04DA]
        private Offset<object> offset448 = new Offset<object>(0x0EF4); // Size: 2. Description: Surface wind turbulence setting, 0 none, 64, 128, 192, 224, 255 worst
        private Offset<object> offset449 = new Offset<object>(0x0EF6); // Size: 2. Description: Surface wind gusts, enabled if True.
        private Offset<object> offset450 = new Offset<object>(0x0EF8); // Size: 2. Description: Upper cloud layer type: 0=user-defined, 1=cirrus, 8=stratus, 9=cumulus
        private Offset<object> offset451 = new Offset<object>(0x0EFA); // Size: 2. Description: Upper cloud layer icing: enabled if True
        private Offset<object> offset452 = new Offset<object>(0x0EFC); // Size: 2. Description: Upper cloud layer turbulence (0 to 255 I think). Divided into steps by FSUIPC for FS2k/CFS2.
        private Offset<object> offset453 = new Offset<object>(0x0EFE); // Size: 2. Description: Lower cloud layer type: 0=user-defined, 1=cirrus, 8=stratus, 9=cumulus
        private Offset<object> offset454 = new Offset<object>(0x0F00); // Size: 2. Description: Lower cloud layer icing: enabled if True
        private Offset<object> offset455 = new Offset<object>(0x0F02); // Size: 2. Description: Lower cloud layer turbulence (0 to 255 I think). Divided into steps by FSUIPC for FS2k/CFS2.
        private Offset<object> offset456 = new Offset<object>(0x0F04); // Size: 2. Description: Storm layer type: 10=storm. [FSUIPC allows this to be a third and lowest layer of any type, for FS2k/CFS2, so then: 0=user-defined, 1=cirrus, 8=stratus, 9=cumulus]
        private Offset<object> offset457 = new Offset<object>(0x0F06); // Size: 2. Description: Storm layer icing: enabled if True
        private Offset<object> offset458 = new Offset<object>(0x0F08); // Size: 2. Description: Storm layer turbulence (0 to 255 I think). Divided into steps by FSUIPC for FS2k/CFS2.
        private Offset<object> offset459 = new Offset<object>(0x0F1C); // Size: 114. Description: Global Weather setting area: details follow. [See 0E9A for Current weather setting area]</span><span style="font-family: Tahoma; font-size: x-small;"><em>On FS2000/CFS2 FSUIPC maps reads from this area to the Current weather area starting at 0E9A, and writes to the Current weather area to this Global weather area. Therefore you may not always read back what you last wrote. The main differences occur when FS local weather is in operation.</em>
        private Offset<object> offset460 = new Offset<object>(0x0F1C); // Size: 2. Description: Upper cloud layer ceiling in metres AMSL
        private Offset<object> offset461 = new Offset<object>(0x0F1E); // Size: 2. Description: Upper cloud layer base in metres AMSL
        private Offset<object> offset462 = new Offset<object>(0x0F20); // Size: 2. Description: Upper cloud layer coverage, 65535 = 8 oktas, … 32768= 4 oktas … 0 = clear
        private Offset<object> offset463 = new Offset<object>(0x0F22); // Size: 2. Description: Upper cloud layer, cloud altitude variation (metres)
        private Offset<object> offset464 = new Offset<object>(0x0F24); // Size: 2. Description: Lower cloud layer ceiling in metres AMSL
        private Offset<object> offset465 = new Offset<object>(0x0F26); // Size: 2. Description: Lower cloud layer base in metres AMSL
        private Offset<object> offset466 = new Offset<object>(0x0F28); // Size: 2. Description: Lower cloud layer coverage, 65535 = 8 oktas, … 32768= 4 oktas … 0 = clear
        private Offset<object> offset467 = new Offset<object>(0x0F2A); // Size: 2. Description: Lower cloud layer, cloud altitude variation (metres)
        private Offset<object> offset468 = new Offset<object>(0x0F2C); // Size: 2. Description: Storm layer ceiling in metres AMSL
        private Offset<object> offset469 = new Offset<object>(0x0F2E); // Size: 2. Description: Storm layer base in metres AMSL (if a Storm layer is present, it must be the lowest, below “Lower Cloud”).
        private Offset<object> offset470 = new Offset<object>(0x0F30); // Size: 2. Description: Storm cloud layer coverage, 65535 = 8 oktas, … 32768= 4 oktas … 0 = clear
        private Offset<object> offset471 = new Offset<object>(0x0F32); // Size: 2. Description: Storm cloud layer, cloud altitude variation (metres)
        private Offset<object> offset472 = new Offset<object>(0x0F34); // Size: 2. Description: Upper Temperature level, metres AMSL
        private Offset<object> offset473 = new Offset<object>(0x0F36); // Size: 2. Description: Upper Temperature in degrees C * 256
        private Offset<object> offset474 = new Offset<object>(0x0F38); // Size: 2. Description: Middle Temperature level, metres AMSL
        private Offset<object> offset475 = new Offset<object>(0x0F3A); // Size: 2. Description: Middle Temperature in degrees C * 256
        private Offset<object> offset476 = new Offset<object>(0x0F3C); // Size: 2. Description: Lower Temperature level, metres AMSL
        private Offset<object> offset477 = new Offset<object>(0x0F3E); // Size: 2. Description: Lower Temperature in degrees C * 256
        private Offset<object> offset478 = new Offset<object>(0x0F40); // Size: 2. Description: Surface Temperature level, metres AMSL (set this to the ground elevation of the weather reporting station)
        private Offset<object> offset479 = new Offset<object>(0x0F42); // Size: 2. Description: Surface Temperature in degrees C * 256
        private Offset<object> offset480 = new Offset<object>(0x0F44); // Size: 2. Description: Temperature drift, degrees C *256 (not used in FS2k/CFS2?)
        private Offset<object> offset481 = new Offset<object>(0x0F46); // Size: 2. Description: Temperature day/night variation, degrees C *256
        private Offset<object> offset482 = new Offset<object>(0x0F48); // Size: 2. Description: Pressure (QNH) as millibars (hectoPascals) *16.
        private Offset<object> offset483 = new Offset<object>(0x0F4A); // Size: 2. Description: Pressure drift as millibars *16 (not used on FS2k/CFS2?)
        private Offset<object> offset484 = new Offset<object>(0x0F4C); // Size: 2. Description: Upper wind ceiling, metres AMSL
        private Offset<object> offset485 = new Offset<object>(0x0F4E); // Size: 2. Description: Upper wind base, metres AMSL
        private Offset<object> offset486 = new Offset<object>(0x0F50); // Size: 2. Description: Upper wind speed, knots
        private Offset<object> offset487 = new Offset<object>(0x0F52); // Size: 2. Description: Upper wind direction, *360/65536 gives degrees True
        private Offset<object> offset488 = new Offset<object>(0x0F54); // Size: 2. Description: Upper wind turbulence setting, 0 none, 64, 128, 192, 224, 255 worst
        private Offset<object> offset489 = new Offset<object>(0x0F56); // Size: 2. Description: Upper wind gusts, enabled if True.
        private Offset<object> offset490 = new Offset<object>(0x0F58); // Size: 2. Description: Middle wind ceiling, metres AMSL
        private Offset<object> offset491 = new Offset<object>(0x0F5A); // Size: 2. Description: Middle wind base, metres AMSL
        private Offset<object> offset492 = new Offset<object>(0x0F5C); // Size: 2. Description: Middle wind speed, knots
        private Offset<object> offset493 = new Offset<object>(0x0F5E); // Size: 2. Description: Middle wind direction, *360/65536 gives degrees True
        private Offset<object> offset494 = new Offset<object>(0x0F60); // Size: 2. Description: Middle wind turbulence setting, 0 none, 64, 128, 192, 224, 255 worst
        private Offset<object> offset495 = new Offset<object>(0x0F62); // Size: 2. Description: Middle wind gusts, enabled if True.
        private Offset<object> offset496 = new Offset<object>(0x0F64); // Size: 2. Description: Lower wind ceiling, metres AMSL
        private Offset<object> offset497 = new Offset<object>(0x0F66); // Size: 2. Description: Lower wind base, metres AMSL
        private Offset<object> offset498 = new Offset<object>(0x0F68); // Size: 2. Description: Lower wind speed, knots
        private Offset<object> offset499 = new Offset<object>(0x0F6A); // Size: 2. Description: Lower wind direction, *360/65536 gives degrees True
        private Offset<object> offset500 = new Offset<object>(0x0F6C); // Size: 2. Description: Lower wind turbulence setting, 0 none, 64, 128, 192, 224, 255 worst
        private Offset<object> offset501 = new Offset<object>(0x0F6E); // Size: 2. Description: Lower wind gusts, enabled if True.
        private Offset<object> offset502 = new Offset<object>(0x0F70); // Size: 2. Description: Surface wind ceiling, metres AMSL
        private Offset<object> offset503 = new Offset<object>(0x0F72); // Size: 2. Description: Surface wind speed, knots. [See also 04D8]
        private Offset<object> offset504 = new Offset<object>(0x0F74); // Size: 2. Description: Surface wind direction, *360/65536 gives degrees Magnetic (!). [See also 04DA]
        private Offset<object> offset505 = new Offset<object>(0x0F76); // Size: 2. Description: Surface wind turbulence setting, 0 none, 64, 128, 192, 224, 255 worst
        private Offset<object> offset506 = new Offset<object>(0x0F78); // Size: 2. Description: Surface wind gusts, enabled if True.
        private Offset<object> offset507 = new Offset<object>(0x0F7A); // Size: 2. Description: Upper cloud layer type: 0=user-defined, 1=cirrus, 8=stratus, 9=cumulus
        private Offset<object> offset508 = new Offset<object>(0x0F7C); // Size: 2. Description: Upper cloud layer icing: enabled if True
        private Offset<object> offset509 = new Offset<object>(0x0F7E); // Size: 2. Description: Upper cloud layer turbulence (0 to 255 I think). Divided into steps by FSUIPC for FS2k/CFS2.
        private Offset<object> offset510 = new Offset<object>(0x0F80); // Size: 2. Description: Lower cloud layer type: 0=user-defined, 1=cirrus, 8=stratus, 9=cumulus
        private Offset<object> offset511 = new Offset<object>(0x0F82); // Size: 2. Description: Lower cloud layer icing: enabled if True
        private Offset<object> offset512 = new Offset<object>(0x0F84); // Size: 2. Description: Lower cloud layer turbulence (0 to 255 I think). Divided into steps by FSUIPC for FS2k/CFS2.
        private Offset<object> offset513 = new Offset<object>(0x0F86); // Size: 2. Description: Storm layer type: 10=storm. [FSUIPC allows this to be a third and lowest layer of any type, for FS2k/CFS2, so then: 0=user-defined, 1=cirrus, 8=stratus, 9=cumulus]
        private Offset<object> offset514 = new Offset<object>(0x0F88); // Size: 2. Description: Storm layer icing: enabled if True
        private Offset<object> offset515 = new Offset<object>(0x0F8A); // Size: 2. Description: Storm layer turbulence (0 to 255 I think). Divided into steps by FSUIPC for FS2k/CFS2.
        private Offset<object> offset516 = new Offset<object>(0x0F8C); // Size: 2. Description: Visibility setting as 100 * statute miles
        private Offset<object> offset517 = new Offset<object>(0x0FF0); // Size: 272. Description: Path and filename reading facility: see section in text preceding this table
        private Offset<object> offset518 = new Offset<object>(0x115E); // Size: 1. Description: Time of day indicator, 1=Day, 2=Dusk or Dawn, 4=Night. Set according to the local time, read for lighting effects and so on in BGLs.
        private Offset<object> offset519 = new Offset<object>(0x11BA); // Size: 2. Description: G Force: units unknown, but /625 seems to give quite sensible values.
        private Offset<object> offset520 = new Offset<object>(0x11BE); // Size: 2. Description: Angle of Attack. This is actually a relative value, giving in %*32767 the difference between the current AofA and the maximum angle of attack for the current aircraft. For a relative measure of AofA calculate 100-(100*#/32767), where # is this number. (<em>Thanks to Sergey Khantsis for this clarification</em>).
        private Offset<object> offset521 = new Offset<object>(0x11C6); // Size: 2. Description: Mach speed *20480.
        private Offset<object> offset522 = new Offset<object>(0x11D0); // Size: 2. Description: Total Air Temperature (TAT), degrees Celsius * 256
        private Offset<object> offset523 = new Offset<object>(0x11D4); // Size: 4. Description: This is an internal pointer, not for specific use by applications, <em>except</em> that it can be used as a flag to indicate when it is possible to read or write most of the simulation variables. When this DWORD is zero FSUIPC cannot obtain correct values from SIM1.SIM (SIM1.DLL in FS2002) because either it isn’t loaded or because it is busy re-calculating values by reading and processing Flight or aircraft files.
        private Offset<object> offset524 = new Offset<object>(0x1244); // Size: 4. Description: Fuel: centre 2 tank level, % * 128 * 65536 [FS2k/CFS2 only]
        private Offset<object> offset525 = new Offset<object>(0x1248); // Size: 4. Description: Fuel: centre 2 tank capacity: US Gallons [FS2k/CFS2 only]
        private Offset<object> offset526 = new Offset<object>(0x124C); // Size: 4. Description: Fuel: centre 3 tank level, % * 128 * 65536 [FS2k/CFS2 only]
        private Offset<object> offset527 = new Offset<object>(0x1250); // Size: 4. Description: Fuel: centre 3 tank capacity: US Gallons [FS2k/CFS2 only]
        private Offset<object> offset528 = new Offset<object>(0x1254); // Size: 4. Description: Fuel: external 1 tank level, % * 128 * 65536 [FS2k/CFS2 only]
        private Offset<object> offset529 = new Offset<object>(0x1258); // Size: 4. Description: Fuel: external 1 tank capacity: US Gallons [FS2k/CFS2 only]
        private Offset<object> offset530 = new Offset<object>(0x125C); // Size: 4. Description: Fuel: external 2 tank level, % * 128 * 65536 [FS2k/CFS2 only]
        private Offset<object> offset531 = new Offset<object>(0x1260); // Size: 4. Description: Fuel: external 2 tank capacity: US Gallons [FS2k/CFS2 only]
        private Offset<object> offset532 = new Offset<object>(0x1274); // Size: 2. Description: Text display mode (eg for ATIS): =0 static, =1 scrolling [FS2k/CFS2 only]. (<em>Note that this is accessible in FS98 at 1254, but this was discovered after the FS2k extra fuel information was mapped.</em>)
        private Offset<object> offset533 = new Offset<object>(0x132C); // Size: 4. Description: NAV/GPS switch, in FS2000 &amp; FS2002. 0=NAV, 1=GPS
        private Offset<object> offset534 = new Offset<object>(0x13FC); // Size: 4. Description: Count of Payload Stations (FS2004 only)
        private Offset<object> offset535 = new Offset<object>(0x1F80); // Size: *. Description: Write-only area for a TCAS_DATA structure, used to add entries to the TCAS data tables—see offset, below, and the section on TCAS earlier in this document.</span><span style="font-family: Tahoma; font-size: x-small;">* The length of data written here is determined by the size of the TCAS_DATA structure, currently 40 bytes (but read this from offset F000).
        private Offset<object> offset536 = new Offset<object>(0x2000); // Size: 8. Description: Turbine Engine 1 N1 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset537 = new Offset<object>(0x2008); // Size: 8. Description: Turbine Engine 1 N2 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset538 = new Offset<object>(0x2010); // Size: 8. Description: Turbine Engine 1 corrected N1 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset539 = new Offset<object>(0x2018); // Size: 8. Description: Turbine Engine 1 corrected N2 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset540 = new Offset<object>(0x2020); // Size: 8. Description: Turbine Engine 1 corrected fuel flow (pounds per hour) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset541 = new Offset<object>(0x2028); // Size: 8. Description: Turbine Engine 1 max torque fraction (range 0.0–1.0) as a double (FLOAT64). (<em>Only tested on turboprops</em>).
        private Offset<object> offset542 = new Offset<object>(0x2030); // Size: 8. Description: Turbine Engine 1 EPR as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset543 = new Offset<object>(0x2038); // Size: 8. Description: Turbine Engine 1 ITT (interstage turbine temperature) in degrees Rankine, as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset544 = new Offset<object>(0x204C); // Size: 8. Description: Turbine Engine 1 jet thrust, in pounds, as a double (FLOAT64). This is the jet thrust. See 2410 for propeller thrust (turboprops have both).
        private Offset<object> offset545 = new Offset<object>(0x205C); // Size: 4. Description: Turbine Engine 1, number of fuel tanks available
        private Offset<object> offset546 = new Offset<object>(0x2060); // Size: 8. Description: Turbine Engine 1 fuel flow (pounds per hour) as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset547 = new Offset<object>(0x206C); // Size: 8. Description: Turbine Engine 1 bleed air pressure (pounds per square inch) as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset548 = new Offset<object>(0x207C); // Size: 8. Description: Turbine Engine 1 reverser fraction, a double (FLOAT64), in the range 0.0–1.0, providing the reverse as a proportion of the maximum reverse throttle position.
        private Offset<object> offset549 = new Offset<object>(0x2100); // Size: 8. Description: Turbine Engine 2 N1 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset550 = new Offset<object>(0x2108); // Size: 8. Description: Turbine Engine 2 N2 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset551 = new Offset<object>(0x2110); // Size: 8. Description: Turbine Engine 2 corrected N1 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset552 = new Offset<object>(0x2118); // Size: 8. Description: Turbine Engine 2 corrected N2 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset553 = new Offset<object>(0x2120); // Size: 8. Description: Turbine Engine 2 corrected fuel flow (pounds per hour) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset554 = new Offset<object>(0x2128); // Size: 8. Description: Turbine Engine 2 max torque fraction (range 0.0–1.0) as a double (FLOAT64). (<em>Only tested on turboprops</em>).
        private Offset<object> offset555 = new Offset<object>(0x2130); // Size: 8. Description: Turbine Engine 2 EPR as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset556 = new Offset<object>(0x2138); // Size: 8. Description: Turbine Engine 2 ITT (interstage turbine temperature) in degrees Rankine, as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset557 = new Offset<object>(0x214C); // Size: 8. Description: Turbine Engine 2 jet thrust, in pounds, as a double (FLOAT64). This is the jet thrust. See 2510 for propeller thrust (turboprops have both).
        private Offset<object> offset558 = new Offset<object>(0x215C); // Size: 4. Description: Turbine Engine 2, number of fuel tanks available
        private Offset<object> offset559 = new Offset<object>(0x2160); // Size: 8. Description: Turbine Engine 2 fuel flow (pounds per hour) as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset560 = new Offset<object>(0x216C); // Size: 8. Description: Turbine Engine 2 bleed air pressure (pounds per square inch) as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset561 = new Offset<object>(0x217C); // Size: 8. Description: Turbine Engine 2 reverser fraction, a double (FLOAT64), in the range 0.0–1.0, providing the reverse as a proportion of the maximum reverse throttle position.
        private Offset<object> offset562 = new Offset<object>(0x2200); // Size: 8. Description: Turbine Engine 3 N1 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset563 = new Offset<object>(0x2208); // Size: 8. Description: Turbine Engine 3 N2 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset564 = new Offset<object>(0x2210); // Size: 8. Description: Turbine Engine 3 corrected N1 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset565 = new Offset<object>(0x2218); // Size: 8. Description: Turbine Engine 3 corrected N2 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset566 = new Offset<object>(0x2220); // Size: 8. Description: Turbine Engine 3 corrected fuel flow (pounds per hour) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset567 = new Offset<object>(0x2228); // Size: 8. Description: Turbine Engine 3 max torque fraction (range 0.0–1.0) as a double (FLOAT64). (<em>Only tested on turboprops</em>).
        private Offset<object> offset568 = new Offset<object>(0x2230); // Size: 8. Description: Turbine Engine 3 EPR as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset569 = new Offset<object>(0x2238); // Size: 8. Description: Turbine Engine 3 ITT (interstage turbine temperature) in degrees Rankine, as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset570 = new Offset<object>(0x224C); // Size: 8. Description: Turbine Engine 3 jet thrust, in pounds, as a double (FLOAT64). This is the jet thrust. See 2610 for propeller thrust (turboprops have both).
        private Offset<object> offset571 = new Offset<object>(0x225C); // Size: 4. Description: Turbine Engine 3, number of fuel tanks available
        private Offset<object> offset572 = new Offset<object>(0x2260); // Size: 8. Description: Turbine Engine 3 fuel flow (pounds per hour) as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset573 = new Offset<object>(0x226C); // Size: 8. Description: Turbine Engine 3 bleed air pressure (pounds per square inch) as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset574 = new Offset<object>(0x227C); // Size: 8. Description: Turbine Engine 3 reverser fraction, a double (FLOAT64), in the range 0.0–1.0, providing the reverse as a proportion of the maximum reverse throttle position.
        private Offset<object> offset575 = new Offset<object>(0x2300); // Size: 8. Description: Turbine Engine 4 N1 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset576 = new Offset<object>(0x2308); // Size: 8. Description: Turbine Engine 4 N2 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset577 = new Offset<object>(0x2310); // Size: 8. Description: Turbine Engine 4 corrected N1 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset578 = new Offset<object>(0x2318); // Size: 8. Description: Turbine Engine 4 corrected N2 value (%) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset579 = new Offset<object>(0x2320); // Size: 8. Description: Turbine Engine 4 corrected fuel flow (pounds per hour) as a double (FLOAT64). This is for jets and turboprops—it has no meaning on reciprocating prop aircraft.
        private Offset<object> offset580 = new Offset<object>(0x2328); // Size: 8. Description: Turbine Engine 4 max torque fraction (range 0.0–1.0) as a double (FLOAT64). (<em>Only tested on turboprops</em>).
        private Offset<object> offset581 = new Offset<object>(0x2330); // Size: 8. Description: Turbine Engine 4 EPR as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset582 = new Offset<object>(0x2338); // Size: 8. Description: Turbine Engine 4 ITT (interstage turbine temperature) in degrees Rankine, as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset583 = new Offset<object>(0x234C); // Size: 8. Description: Turbine Engine 4 jet thrust, in pounds, as a double (FLOAT64). This is the jet thrust. See 2710 for propeller thrust (turboprops have both).
        private Offset<object> offset584 = new Offset<object>(0x235C); // Size: 4. Description: Turbine Engine 4, number of fuel tanks available
        private Offset<object> offset585 = new Offset<object>(0x2360); // Size: 8. Description: Turbine Engine 4 fuel flow (pounds per hour) as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset586 = new Offset<object>(0x236C); // Size: 8. Description: Turbine Engine 4 bleed air pressure (pounds per square inch) as a double (FLOAT64). This is for jets and turboprops.
        private Offset<object> offset587 = new Offset<object>(0x237C); // Size: 8. Description: Turbine Engine 4 reverser fraction, a double (FLOAT64), in the range 0.0–1.0, providing the reverse as a proportion of the maximum reverse throttle position.
        private Offset<object> offset588 = new Offset<object>(0x2400); // Size: 8. Description: Propeller 1 RPM as a double (FLOAT64). This value is for props and turboprops and is negative for counter-rotating propellers.
        private Offset<object> offset589 = new Offset<object>(0x2408); // Size: 8. Description: Propeller 1 RPM as a fraction of the maximum RPM. (double)
        private Offset<object> offset590 = new Offset<object>(0x2410); // Size: 8. Description: Propeller 1 thrust in pounds, as a double (FLOAT64). This is for props and turboprops.
        private Offset<object> offset591 = new Offset<object>(0x2418); // Size: 8. Description: Propeller 1 Beta blade angle in radians, as a double (FLOAT64). This is for props and turboprops.
        private Offset<object> offset592 = new Offset<object>(0x2500); // Size: 8. Description: Propeller 2 RPM as a double (FLOAT64). This value is for props and turboprops and is negative for counter-rotating propellers.
        private Offset<object> offset593 = new Offset<object>(0x2508); // Size: 8. Description: Propeller 2 RPM as a fraction of the maximum RPM. (double)
        private Offset<object> offset594 = new Offset<object>(0x2510); // Size: 8. Description: Propeller 2 thrust in pounds, as a double (FLOAT64). This is for props and turboprops.
        private Offset<object> offset595 = new Offset<object>(0x2518); // Size: 8. Description: Propeller 2 Beta blade angle in radians, as a double (FLOAT64). This is for props and turboprops.
        private Offset<object> offset596 = new Offset<object>(0x2600); // Size: 8. Description: Propeller 3 RPM as a double (FLOAT64). This value is for props and turboprops and is negative for counter-rotating propellers.
        private Offset<object> offset597 = new Offset<object>(0x2608); // Size: 8. Description: Propeller 3 RPM as a fraction of the maximum RPM. (double)
        private Offset<object> offset598 = new Offset<object>(0x2610); // Size: 8. Description: Propeller 3 thrust in pounds, as a double (FLOAT64). This is for props and turboprops.
        private Offset<object> offset599 = new Offset<object>(0x2618); // Size: 8. Description: Propeller 3 Beta blade angle in radians, as a double (FLOAT64). This is for props and turboprops.
        private Offset<object> offset600 = new Offset<object>(0x2700); // Size: 8. Description: Propeller 4 RPM as a double (FLOAT64). This value is for props and turboprops and is negative for counter-rotating propellers.
        private Offset<object> offset601 = new Offset<object>(0x2708); // Size: 8. Description: Propeller 4 RPM as a fraction of the maximum RPM. (double)
        private Offset<object> offset602 = new Offset<object>(0x2710); // Size: 8. Description: Propeller 4 thrust in pounds, as a double (FLOAT64). This is for props and turboprops.
        private Offset<object> offset603 = new Offset<object>(0x2718); // Size: 8. Description: Propeller 4 Beta blade angle in radians, as a double (FLOAT64). This is for props and turboprops.
        private Offset<object> offset604 = new Offset<object>(0x281C); // Size: 4. Description: Master battery switch (1=On, 0=Off)
        private Offset<object> offset605 = new Offset<object>(0x28C0); // Size: 8. Description: Ambient air density, in slugs per cubic foot, double floating point. (FS2002+)
        private Offset<object> offset606 = new Offset<object>(0x28C8); // Size: 8. Description: Ambient air pressure, in lbs per square foot, double floating point. (FS2002+)
        private Offset<object> offset607 = new Offset<object>(0x28D0); // Size: 8. Description: Static air temperature, in degrees Fahrenheit, double floating point. (FS2002+)
        private Offset<object> offset608 = new Offset<object>(0x28D8); // Size: 8. Description: Static air temperature, in degrees Rankine, double floating point. (FS2002+)
        private Offset<object> offset609 = new Offset<object>(0x28E0); // Size: 8. Description: “Theta”, or standard temperature ratio (i.e ambient air temperature divided by standard ISO air temperature), double floating point. (FS2002+)
        private Offset<object> offset610 = new Offset<object>(0x28E8); // Size: 8. Description: “Delta”, or standard pressure ratio (ambient pressure divided by the ISO standard pressure, double floating point. (FS2002+)
        private Offset<object> offset611 = new Offset<object>(0x28F0); // Size: 8. Description: “Sigma”, or standard density ratio (ambient density divided by the ISO standard density, double floating point. (FS2002+)
        private Offset<object> offset612 = new Offset<object>(0x290C); // Size: 4. Description: Number of Hot Joystick Button slots available for Application Programs to use. Currently this is fixed at 56, representing the 56 DWORDs available in the following offsets:
        private Offset<object> offset613 = new Offset<object>(0x2910); // Size: 224. Description: 56 DWORDs containing zero (when free for use), or a Hot Joystick Button specification as detailed earlier in this document. See also 32FF below.
        private Offset<object> offset614 = new Offset<object>(0x2DC6); // Size: 2 . Description: Helicopter “beep” (whatever that is—something to do with the governor). This value is also controlled by the <em>Increase Heli Beep</em> and <em>Decrease Heli Beep</em> FS controls. It appears to change from 0 to 16313 then more slowly to 16368.
        private Offset<object> offset615 = new Offset<object>(0x2DC8); // Size: 8. Description: For FS2004 only, this is the wind at the aircraft in the lateral (X) axis—relative to the aircraft orientation, in feet per second, as a 64-bit double.</span><span style="font-family: Tahoma; font-size: x-small;">[Note that this will not necessarily be correct if the facilities in offsets 2DE0/2DE8 below have been used to change the wind speed or direction.]
        private Offset<object> offset616 = new Offset<object>(0x2DD0); // Size: 8. Description: For FS2004 only, this is the wind at the aircraft in the vertical (Y) axis—relative to the aircraft orientation, in feet per second, as a 64-bit double. </span><span style="font-family: Tahoma; font-size: x-small;">[Note that this will not necessarily be correct if the facilities in offsets 2DE0/2DE8 below have been used to change the wind speed or direction.]
        private Offset<object> offset617 = new Offset<object>(0x2DD8); // Size: 8. Description: For FS2004 only, this is the wind at the aircraft in the longitudinal (Z) axis—relative to the aircraft orientation, in feet per second, as a 64-bit double.</span><span style="font-family: Tahoma; font-size: x-small;">[Note that this will not necessarily be correct if the facilities in offsets 2DE0/2DE8 below have been used to change the wind speed or direction.]
        private Offset<object> offset618 = new Offset<object>(0x2DE0); // Size: 8. Description: For FS2004 only, Wind direction at the aircraft, in degrees True, as a 64-bit double floating point.</span><span style="font-family: Tahoma; font-size: x-small;">This can be written to directly affect the wind direction at the aircraft. This value is set <em>before </em>FSUIPC performs any smoothing or limiting actions, and effectively become the new target value. FSUIPC sustains this as a target for a maximum of 14 seconds, with the next write to the same location restarting this timeout. After the timeout has been allowed to expire the intended FS value will take over, with smoothing and so on if enabled.</span><span style="font-family: Tahoma; font-size: x-small;">Note that wind direction set in this fashion is <em>not </em>reflected in any weather data supplied by the weather system in FS nor FSUIPC. It is acting locally to the aircraft and can be monitored by Shift+Z or the ambient weather read-outs in FSUIPC.
        private Offset<object> offset619 = new Offset<object>(0x2DE8); // Size: 8. Description: For FS2004 only, Wind speed at the aircraft, in knots, as a 64-bit double floating point.</span><span style="font-family: Tahoma; font-size: x-small;">This can be written to directly affect the wind speed at the aircraft. This value is set <em>before </em>FSUIPC performs any smoothing or limiting actions, and effectively become the new target value. FSUIPC sustains this as a target for a maximum of 14 seconds, with the next write to the same location restarting this timeout. After the timeout has been allowed to expire the intended FS value will take over, with smoothing and so on if enabled.</span><span style="font-family: Tahoma; font-size: x-small;">Note that wind speed set in this fashion is <em>not </em>reflected in any weather data supplied by the weather system in FS nor FSUIPC. It is acting locally to the aircraft and can be monitored by Shift+Z or the ambient weather read-outs in FSUIPC.
        private Offset<object> offset620 = new Offset<object>(0x2DF0); // Size: 8. Description: For FS2004 only, Visibility at the aircraft, in metres, as a 64-bit double floating point.</span><span style="font-family: Tahoma; font-size: x-small;">This can be written to directly affect the visibility at the aircraft. This value is set <em>before </em>FSUIPC performs any smoothing or limiting actions, and effectively become the new target value. FSUIPC sustains this as a target for a maximum of 14 seconds, with the next write to the same location restarting this timeout. After the timeout has been allowed to expire the intended FS value will take over, with smoothing and so on if enabled.</span><span style="font-family: Tahoma; font-size: x-small;">Note that visibility set in this fashion is <em>not </em>reflected in any weather data supplied by the weather system in FS nor FSUIPC. It is acting locally to the aircraft and can be monitored by Shift+Z or the ambient weather read-outs in FSUIPC.
        private Offset<object> offset621 = new Offset<object>(0x2E80); // Size: 4. Description: Master avionics switch (0=Off, 1=On)
        private Offset<object> offset622 = new Offset<object>(0x2E88); // Size: 4. Description: Panel auto-feather arm switch (0=Off, 1=On)
        private Offset<object> offset623 = new Offset<object>(0x2E98); // Size: 8. Description: Elevator deflection, in radians, as a double (FLOAT64). Up positive, down negative.
        private Offset<object> offset624 = new Offset<object>(0x2EA0); // Size: 8. Description: Elevator trim deflection, in radians, as a double (FLOAT64). Up positive, down negative.
        private Offset<object> offset625 = new Offset<object>(0x2EA8); // Size: 8. Description: Aileron deflection, in radians, as a double (FLOAT64). Right turn positive, left turn negative.
        private Offset<object> offset626 = new Offset<object>(0x2EB0); // Size: 8. Description: Aileron trim deflection, in radians, as a double (FLOAT64). Right turn positive, left turn negative.
        private Offset<object> offset627 = new Offset<object>(0x2EB8); // Size: 8. Description: Rudder deflection, in radians, as a double (FLOAT64).
        private Offset<object> offset628 = new Offset<object>(0x2EC0); // Size: 8. Description: Rudder trim deflection, in radians, as a double (FLOAT64).
        private Offset<object> offset629 = new Offset<object>(0x2EC8); // Size: 4. Description: Prop sync active (1=Active, 0=Inactive)
        private Offset<object> offset630 = new Offset<object>(0x2ED0); // Size: 8. Description: Incidence “alpha”, in radians, as a double (FLOAT64). This is the aircraft <em>body</em> angle of attack (AoA) not the <em>wing</em> AoA.
        private Offset<object> offset631 = new Offset<object>(0x2ED8); // Size: 8. Description: Incidence “beta”, in radians, as a double (FLOAT64). This is the side slip angle.
        private Offset<object> offset632 = new Offset<object>(0x2EE0); // Size: 4. Description: Flight Director Active, control and indicator. 1=active, 0=inactive. [FS2000–FS2004 only]
        private Offset<object> offset633 = new Offset<object>(0x2EE8); // Size: 8. Description: Flight director pitch value, in degrees. Double floating point format, only when FD is active. [FS2000–FS2004 only]
        private Offset<object> offset634 = new Offset<object>(0x2EF0); // Size: 8. Description: Flight director bank value, in degrees. Double floating point format, right is negative, left positive. [FS2000–FS2004 only]
        private Offset<object> offset635 = new Offset<object>(0x2EF8); // Size: 8. Description: CG percent, as a double (FLOAT64). This is probably the position of the actual CoG as a % of MAC (?).
        private Offset<object> offset636 = new Offset<object>(0x2F70); // Size: 8. Description: Attitude indicator pitch value, in degrees. Double floating point format. This is the ATTITUDE_INDICATOR _PITCH_DEGREES variable previously listed as specific to FS2000. [FS2000/FS2002 only]
        private Offset<object> offset637 = new Offset<object>(0x2F78); // Size: 8. Description: Attitude indicator bank value, in degrees. Double floating point format. This is the ATTITUDE_INDICATOR_BANK_DEGREES variable previously listed as specific to FS2000. [FS2000/FS2002 only]
        private Offset<object> offset638 = new Offset<object>(0x2F80); // Size: 1. Description: PANEL AUTOBRAKE SWITCH</span><span style="font-family: Tahoma; font-size: x-small;">Read to check setting, write to change it.</span><span style="font-family: Tahoma; font-size: x-small;">0=RTO, 1=Off, 2=brake1, 3=brake2, 4=brake3, 5=max
        private Offset<object> offset639 = new Offset<object>(0x2FE0); // Size: 32. Description: Modules Menu, application item write area (see earlier in this document)
        private Offset<object> offset640 = new Offset<object>(0x3000); // Size: 6. Description: VOR1 IDENTITY (string supplied: 6 bytes including zero terminator)
        private Offset<object> offset641 = new Offset<object>(0x3006); // Size: 25. Description: VOR1 name (string supplied: 25 bytes including zero terminator)
        private Offset<object> offset642 = new Offset<object>(0x301F); // Size: 6. Description: VOR2 IDENTITY (string supplied: 6 bytes including zero terminator)
        private Offset<object> offset643 = new Offset<object>(0x3025); // Size: 25. Description: VOR2 name (string supplied: 25 bytes needed including zero terminator)
        private Offset<object> offset644 = new Offset<object>(0x303E); // Size: 6. Description: ADF1 IDENTITY (string supplied: 6 bytes including zero terminator)
        private Offset<object> offset645 = new Offset<object>(0x3044); // Size: 25. Description: ADF1 name (string supplied: 25 bytes including zero terminator)
        private Offset<object> offset646 = new Offset<object>(0x3060); // Size: 8. Description: X (lateral, or left/right) acceleration in ft/sec/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset647 = new Offset<object>(0x3068); // Size: 8. Description: Y (vertical, or up/down) acceleration in ft/sec/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset648 = new Offset<object>(0x3070); // Size: 8. Description: Z (longitudinal, or forward/backward) acceleration in ft/sec/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset649 = new Offset<object>(0x3078); // Size: 8. Description: Pitch acceleration in radians/sec/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset650 = new Offset<object>(0x3080); // Size: 8. Description: Roll acceleration in radians/sec/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset651 = new Offset<object>(0x3088); // Size: 8. Description: Yaw acceleration in radians/sec/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset652 = new Offset<object>(0x3090); // Size: 8. Description: Z (longitudinal, or forward/backward) GS-velocity in ft/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset653 = new Offset<object>(0x3098); // Size: 8. Description: X (lateral, or left/right) GS-velocity in ft/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset654 = new Offset<object>(0x30A0); // Size: 8. Description: Y (vertical, or up/down) GS-velocity in ft/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset655 = new Offset<object>(0x30A8); // Size: 8. Description: Pitch velocity in rads/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset656 = new Offset<object>(0x30B0); // Size: 8. Description: Roll velocity in rads/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset657 = new Offset<object>(0x30B8); // Size: 8. Description: Yaw velocity in rads/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset658 = new Offset<object>(0x30C0); // Size: 8. Description: Current loaded weight in lbs. This is in double floating point format (FLOAT64). [FS2000 and later]
        private Offset<object> offset659 = new Offset<object>(0x30C8); // Size: 8. Description: Plane’s current mass, in slugs (1 slug = 1lb*G = 32.174049 lbs) mass. This is in double floating point format (FLOAT64).</span><span style="font-family: Tahoma; font-size: x-small;">The current mass = current loaded weight (as in 30C0) * G, where G is 32.174049.
        private Offset<object> offset660 = new Offset<object>(0x30D0); // Size: 8. Description: Vertical acceleration in G’s. This is in double floating point format (FLOAT64). [FS2k only]
        private Offset<object> offset661 = new Offset<object>(0x30D8); // Size: 8. Description: Dynamic pressure (lbs/sqft). [FS2k/CFS2/FS2002 only]
        private Offset<object> offset662 = new Offset<object>(0x30E0); // Size: 2. Description: [FS2002/4 only]: Trailing edge left inboard flap extension as a percentage of its maximum, with 16383 = 100%
        private Offset<object> offset663 = new Offset<object>(0x30E2); // Size: 2. Description: [FS2002/4 only]: Trailing edge left outboard flap extension as a percentage of its maximum, with 16383 = 100%
        private Offset<object> offset664 = new Offset<object>(0x30E4); // Size: 2. Description: [FS2002/4 only]: Trailing edge right inboard flap extension as a percentage of its maximum, with 16383 = 100%
        private Offset<object> offset665 = new Offset<object>(0x30E6); // Size: 2. Description: [FS2002/4 only]: Trailing edge right outboard flap extension as a percentage of its maximum, with 16383 = 100%
        private Offset<object> offset666 = new Offset<object>(0x30E8); // Size: 2. Description: [FS2002/4 only]: Leading edge left inboard flap extension as a percentage of its maximum, with 16383 = 100%
        private Offset<object> offset667 = new Offset<object>(0x30EA); // Size: 2. Description: [FS2002/4 only]: Leading edge left outboard flap extension as a percentage of its maximum, with 16383 = 100%
        private Offset<object> offset668 = new Offset<object>(0x30EC); // Size: 2. Description: [FS2002/4 only]: Leading edge right inboard flap extension as a percentage of its maximum, with 16383 = 100%
        private Offset<object> offset669 = new Offset<object>(0x30EE); // Size: 2. Description: [FS2002/4 only]: Leading edge right outboard flap extension as a percentage of its maximum, with 16383 = 100%
        private Offset<object> offset670 = new Offset<object>(0x30F0); // Size: 2. Description: [FS2002/4 only]: Trailing edge left inboard flap extension in degrees * 256.
        private Offset<object> offset671 = new Offset<object>(0x30F2); // Size: 2. Description: [FS2002/4 only]: Trailing edge left outboard flap extension in degrees * 256.
        private Offset<object> offset672 = new Offset<object>(0x30F4); // Size: 2. Description: [FS2002/4 only]: Trailing edge right inboard flap extension in degrees * 256.
        private Offset<object> offset673 = new Offset<object>(0x30F6); // Size: 2. Description: [FS2002/4 only]: Trailing edge right outboard flap extension in degrees * 256.
        private Offset<object> offset674 = new Offset<object>(0x30F8); // Size: 2. Description: [FS2002/4 only]: Leading edge left inboard flap extension in degrees * 256.
        private Offset<object> offset675 = new Offset<object>(0x30FA); // Size: 2. Description: [FS2002/4 only]: Leading edge left outboard flap extension in degrees * 256.
        private Offset<object> offset676 = new Offset<object>(0x30FC); // Size: 2. Description: [FS2002/4 only]: Leading edge right inboard flap extension in degrees * 256.
        private Offset<object> offset677 = new Offset<object>(0x30FE); // Size: 2. Description: [FS2002/4 only]: Leading edge right outboard flap extension in degrees * 256.
        private Offset<object> offset678 = new Offset<object>(0x3100); // Size: 1. Description: Engine primer (just write a non-zero byte to operate the primer. This is a one-shot and reading it is meaningless) [FS2000+]
        private Offset<object> offset679 = new Offset<object>(0x3101); // Size: 1. Description: Alternator (1 = on, 0 = off), read for state, write to control [FS2000+]
        private Offset<object> offset680 = new Offset<object>(0x3102); // Size: 1. Description: Battery (1 = on, 0 = off), read for state, write to control [FS2000+]
        private Offset<object> offset681 = new Offset<object>(0x3103); // Size: 1. Description: Avionics (1 = on, 0 = off), read for state, write to control [FS2000+]
        private Offset<object> offset682 = new Offset<object>(0x3104); // Size: 1. Description: Fuel pump (1 = on, 0 = off), read for state, write to control [FS2000+]. For separate switches for separate fuel pumps see offset 3125.
        private Offset<object> offset683 = new Offset<object>(0x3105); // Size: 1. Description: VOR1 morse ID sound (1 = on, 0 = off), read for state, write to control [FS2000+]
        private Offset<object> offset684 = new Offset<object>(0x3106); // Size: 1. Description: VOR2 morse ID sound (1 = on, 0 = off), read for state, write to control [FS2000+]
        private Offset<object> offset685 = new Offset<object>(0x3107); // Size: 1. Description: ADF1 morse ID sound (1 = on, 0 = off), read for state, write to control [FS2000+]
        private Offset<object> offset686 = new Offset<object>(0x3108); // Size: 1. Description: Write 1 here to disable FSUIPC’s “AutoTune ADF1” facility, if this has been enabled by the user in FSUIPC.INI.
        private Offset<object> offset687 = new Offset<object>(0x3109); // Size: 1. Description: Write 1 here to disable AxisCalibration even if enabled in FSUIPC.INI.
        private Offset<object> offset688 = new Offset<object>(0x310C); // Size: 4. Description: <em>Reserved</em>
        private Offset<object> offset689 = new Offset<object>(0x3110); // Size: 8. Description: Operates a facility to send any ‘controls’ to Flight simulator. This works with <em>all </em>versions of FS &amp; CFS. Write all 8 bytes for controls which use a value (axes and all _SET controls), but just 4 will do for ‘button’ types.</span><span style="font-family: Tahoma; font-size: x-small;">This is really two 32-bit integers. The first contains the Control number (normally 65536 upwards), as seen in my FS Controls lists. The second integer is used for the parameter, such as the scaled axis value, where this is appropriate. Always write all 8 bytes in one IPC block if a parameter is used, as FSUIPC will fire the control when you write to 3110.</span><span style="font-family: Tahoma; font-size: x-small;">Since version 3.40, FSUIPC-added controls (other than the offset ones) can be used via these offsets too. See the Advanced User’s Guide for a current list.
        private Offset<object> offset690 = new Offset<object>(0x3118); // Size: 2. Description: COM2 frequency (FS2002+ only), 4 digits in BCD format. A frequency of 123.45 is represented by 0x2345. The leading 1 is assumed.
        private Offset<object> offset691 = new Offset<object>(0x311A); // Size: 2. Description: COM1 standby frequency (FS2002+ only), 4 digits in BCD format. A frequency of 123.45 is represented by 0x2345. The leading 1 is assumed.
        private Offset<object> offset692 = new Offset<object>(0x311C); // Size: 2. Description: COM2 standby frequency (FS2002+ only), 4 digits in BCD format. A frequency of 123.45 is represented by 0x2345. The leading 1 is assumed.
        private Offset<object> offset693 = new Offset<object>(0x311E); // Size: 2. Description: NAV1 standby frequency (FS2002+ only), 4 digits in BCD format. A frequency of 113.45 is represented by 0x1345. The leading 1 is assumed.
        private Offset<object> offset694 = new Offset<object>(0x3120); // Size: 2. Description: NAV2 standby frequency (FS2002+ only), 4 digits in BCD format. A frequency of 113.45 is represented by 0x1345. The leading 1 is assumed.
        private Offset<object> offset695 = new Offset<object>(0x3124); // Size: 1. Description: FS2002 only: “electric always available” flag: set if 1, clear if 0. Can be controlled by writing also.
        private Offset<object> offset696 = new Offset<object>(0x3125); // Size: 1. Description: FS2000/FS2002 only: separate switches for up to 4 Fuel Pumps (one for each engine). Bit 2^0=Pump1, 2^1=Pump2, 2^2=Pump3, 2^4=Pump4. (<em>see also offset 3104</em>)
        private Offset<object> offset697 = new Offset<object>(0x3127); // Size: 9. Description: FSUIPC weather option control area: see text section earlier in this document.
        private Offset<object> offset698 = new Offset<object>(0x3130); // Size: 12. Description: ATC flight number string for currently loaded user aircraft, as declared in the AIRCRAFT.CFG file. This is limited to a maximum of 12 characters, including a zero terminator. [FS2002+ only]
        private Offset<object> offset699 = new Offset<object>(0x313C); // Size: 12. Description: ATC identifier (tail number) string for currently loaded user aircraft, as declared in the AIRCRAFT.CFG file. This is limited to a maximum of 12 characters, including a zero terminator. [FS2002+ only]
        private Offset<object> offset700 = new Offset<object>(0x3148); // Size: 24. Description: ATC airline name string for currently loaded user aircraft, as declared in the AIRCRAFT.CFG file. This is limited to a maximum of 24 characters, including a zero terminator. [FS2002+ only]
        private Offset<object> offset701 = new Offset<object>(0x3160); // Size: 24. Description: ATC aircraft type string for currently loaded user aircraft, as declared in the AIRCRAFT.CFG file. This is limited to a maximum of 24 characters, including a zero terminator. [FS2002+ only]
        private Offset<object> offset702 = new Offset<object>(0x3178); // Size: 8. Description: Z (longitudinal, or forward/backward) TAS-velocity in ft/sec relative to the body axes. This is in double floating point format (FLOAT64). [[FS2002+ only]]
        private Offset<object> offset703 = new Offset<object>(0x3180); // Size: 8. Description: X (lateral, or left/right) TAS-velocity in ft/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [[FS2002+ only]]
        private Offset<object> offset704 = new Offset<object>(0x3188); // Size: 8. Description: Y (vertical, or up/down) TAS-velocity in ft/sec relative to the body axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [[FS2002+ only]]
        private Offset<object> offset705 = new Offset<object>(0x3190); // Size: 8. Description: Z (longitudinal, or forward/backward) GS-velocity in ft/sec relative to world axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000+]
        private Offset<object> offset706 = new Offset<object>(0x3198); // Size: 8. Description: X (lateral, or left/right) GS-velocity in ft/sec relative to world axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000+]</span><span style="font-family: Tahoma; font-size: x-small;">N.B. The sign may be reversed in FS2002?
        private Offset<object> offset707 = new Offset<object>(0x31A0); // Size: 8. Description: Y (vertical, or up/down) GS-velocity in ft/sec relative to world axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000+]
        private Offset<object> offset708 = new Offset<object>(0x31A8); // Size: 8. Description: Pitch velocity in rads/sec relative to world axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000+]
        private Offset<object> offset709 = new Offset<object>(0x31B0); // Size: 8. Description: Roll velocity in rads/sec relative to world axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000+] N.B. In FS2002 the sign may be reversed, and the units may be 16x
        private Offset<object> offset710 = new Offset<object>(0x31B8); // Size: 8. Description: Yaw velocity in rads/sec relative to world axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2000+] N.B. In FS2002 the sign may be reversed, and the units may be 16x
        private Offset<object> offset711 = new Offset<object>(0x31C0); // Size: 8. Description: X (lateral, or left/right) acceleration in ft/sec/sec relative to the world axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2002+]
        private Offset<object> offset712 = new Offset<object>(0x31C8); // Size: 8. Description: Y (vertical, or up/down) acceleration in ft/sec/sec relative to the world axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2002+]
        private Offset<object> offset713 = new Offset<object>(0x31D0); // Size: 8. Description: Z (longitudinal, or forward/backward) acceleration in ft/sec/sec relative to the world axes (<em>see Note at end of table</em>). This is in double floating point format (FLOAT64). [FS2002+]
        private Offset<object> offset714 = new Offset<object>(0x31D8); // Size: 2. Description: Slew mode longitudinal axis (i.e. forward/backward) input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310B)
        private Offset<object> offset715 = new Offset<object>(0x31DA); // Size: 2. Description: Slew mode lateral axis (i.e. left/right) input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310B)
        private Offset<object> offset716 = new Offset<object>(0x31DC); // Size: 2. Description: Slew mode yaw axis (i.e. heading) input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310B)
        private Offset<object> offset717 = new Offset<object>(0x31DE); // Size: 2. Description: Slew mode vertical axis (i.e. altitude) input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310B)
        private Offset<object> offset718 = new Offset<object>(0x31E0); // Size: 2. Description: Slew mode roll axis (i.e. bank) input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310B)
        private Offset<object> offset719 = new Offset<object>(0x31E2); // Size: 2. Description: Slew mode pitch axis input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310B)
        private Offset<object> offset720 = new Offset<object>(0x31E4); // Size: 4. Description: Radio altitude in metres * 65536</span><span style="font-family: Tahoma; font-size: x-small;">(Calculated by FSUIPC from ground altitude and aircraft altitude)
        private Offset<object> offset721 = new Offset<object>(0x31F0); // Size: 4. Description: Pushback status (FS2002 only).</span><span style="font-family: Tahoma; font-size: x-small;">3=off, 0=pushing back, 1=pushing back, tail to swing to left (port), 2=pushing back, tail to swing to right (starboard)
        private Offset<object> offset722 = new Offset<object>(0x31F4); // Size: 4. Description: Pushback control (FS2002 only). Write 0–3 here to set pushback operation, as described for the status, above.
        private Offset<object> offset723 = new Offset<object>(0x31F8); // Size: 4. Description: Tug Heading (FS2002 only). [<em>not investigated</em>]
        private Offset<object> offset724 = new Offset<object>(0x31FC); // Size: 4. Description: Tug Speed (FS2002 only). [<em>not investigate</em>]
        private Offset<object> offset725 = new Offset<object>(0x320C); // Size: 4. Description: Number of Hot Key slots available for Application Programs to use. Currently this is fixed at 56, representing the 56 DWORDs available in the following offsets:
        private Offset<object> offset726 = new Offset<object>(0x3210); // Size: 224. Description: 56 DWORDs containing zero (when free for use), or a Hot Key specification as detailed earlier in this document. See also 32FE below.
        private Offset<object> offset727 = new Offset<object>(0x32F4); // Size: 2. Description: The 16-bit ID of the last menu command item accessed in FS can be read here. By “access” is not meant “used”–that cannot be determined easily. Just having a menu command highlit will denote an access.</span><span style="font-family: Tahoma; font-size: x-small;">To decode command Ids, use FSUIPC logging. First, before running FSUIPC set “Debug=Please” and “LogExtras=64” into the FSUIPC.INI file. Then run FS and select the menu items in which you are interested. Examine the FSUIPC Log afterwards to determine the ID.
        private Offset<object> offset728 = new Offset<object>(0x32F9); // Size: 1. Description: <em>Reserved</em>
        private Offset<object> offset729 = new Offset<object>(0x32FC); // Size: 2. Description: AIR file change counter (incremented by FSUIPC whenever the AIR file as defined at offset 3C00 changes).</span><span style="font-family: Tahoma; font-size: x-small;">This is also incremented when the FS2004 control to “reload user aircraft” is detected—assign it to a joystick button or to a Key in FSUIPC for this. FSUIPC cannot detect controls arising from key presses assigned in FS dialogues.
        private Offset<object> offset730 = new Offset<object>(0x32FE); // Size: 1. Description: Hot Key change counter, incremented by FSUIPC whenever any of the Hot Keys defined in the table at offset 3210 occurs and therefore has its flag set by FSUIPC.
        private Offset<object> offset731 = new Offset<object>(0x32FF); // Size: 1. Description: Hot Button change counter, incremented by FSUIPC whenever any of the Hot Buttons defined in the table at offset 2910 changes state in the right way, and therefore has its flag set by FSUIPC.
        private Offset<object> offset732 = new Offset<object>(0x3302); // Size: 2. Description: Assorted FSUIPC options, set by user parameters: read-only via the IPC. Those allocated so far (bits from least significant):</span><span style="font-family: Tahoma; font-size: x-small;"> 0 = Static (i.e. non-scrolling) messages sent to FS are to be displayed in white rather than the default red. (If AdvDisplay is installed it must be version 2.11 or later for this option).</span><span style="font-family: Tahoma; font-size: x-small;">1 = This is FS2004 (or later) but MakeItVersionFS2002 has been used in the INI to “fiddle” the reported value in 3308 to show FS2002.
        private Offset<object> offset733 = new Offset<object>(0x3304); // Size: 4. Description: FSUIPC version number:</span><span style="font-family: Tahoma; font-size: x-small;">The HIWORD (i.e. bytes 3306-7) gives the main version as BCD x 1000: e.g. 0x1998 for 1.998</span><span style="font-family: Tahoma; font-size: x-small;">The LOWORD (bytes 3304-5) gives the Interim build letter: 0=none, 1-26=a-z: e.g. 0x0005 = &#8216;e&#8217;
        private Offset<object> offset734 = new Offset<object>(0x330A); // Size: 2. Description: Fixed <em>read-only</em> pattern, set to 0xFADE. Use this to check that the values in 3304-3308 are valid (Note: the supplied LIB writes its version number here, but this has no effect and is only for assistance when viewing LOG files).
        private Offset<object> offset735 = new Offset<object>(0x330E); // Size: 1. Description: Count of external IPC applications seen connecting since the session began. Keeps increasing till it gets to 255 then stays at that value.
        private Offset<object> offset736 = new Offset<object>(0x330F); // Size: 17. Description: Reserved area for WideFS KeySend facility (version 4.23 and later)
        private Offset<object> offset737 = new Offset<object>(0x3322); // Size: 2. Description: WideServer version number, if running <em>and</em> if version 5.00 or later. Otherwise this is zero.</span><span style="font-family: Tahoma; font-size: x-small;">This is a BCD value giving the version number x 1000, for example 0x5110 means version 5.110.</span><span style="font-family: Tahoma; font-size: x-small;">See also offset 333C.
        private Offset<object> offset738 = new Offset<object>(0x3324); // Size: 4. Description: This is the altimeter reading in feet (or metres, if the user is running with the preference for altitudes in metres), as a 32-bit signed integer. Please check offset 0C18 to determine when metres are used (0C18 contains ‘2’).</span><span style="font-family: Tahoma; font-size: x-small;">The same value can be calculated from the actual altitude and the difference between the QNH and the altimeter “Kollsman” pressure setting, but this value ensures agreement.
        private Offset<object> offset739 = new Offset<object>(0x3328); // Size: 2. Description: Elevator Axis input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310A).
        private Offset<object> offset740 = new Offset<object>(0x332A); // Size: 2. Description: Aileron Axis input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310A).
        private Offset<object> offset741 = new Offset<object>(0x332C); // Size: 2. Description: Rudder Axis input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310A).
        private Offset<object> offset742 = new Offset<object>(0x332E); // Size: 2. Description: Throttle Axis input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310A). This is the single throttle, applied to whichever engines are denoted by the bits in offset 0888.
        private Offset<object> offset743 = new Offset<object>(0x3330); // Size: 2. Description: Throttle 1 Axis input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310A).
        private Offset<object> offset744 = new Offset<object>(0x3332); // Size: 2. Description: Throttle 2 Axis input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310A).
        private Offset<object> offset745 = new Offset<object>(0x3334); // Size: 2. Description: Throttle 3 Axis input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310A).
        private Offset<object> offset746 = new Offset<object>(0x3336); // Size: 2. Description: Throttle 4 Axis input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310A).
        private Offset<object> offset747 = new Offset<object>(0x3338); // Size: 2. Description: Elevator Trim Axis input value, post calibration, just before being applied to the simulation (if allowed to by the byte at offset 310A).
        private Offset<object> offset748 = new Offset<object>(0x333A); // Size: 2. Description: Throttle lower limit (FS2002 and later only). This is normally 0 if no reverse is available, otherwise gives the reverse limit such as –4096 (for 25%). For earlier versions than FS2002 this location will be zero.
        private Offset<object> offset749 = new Offset<object>(0x333E); // Size: 2. Description: Weather clear count: This is incremented every time FS’s “clear weather” routine is called, for whatever reason.
        private Offset<object> offset750 = new Offset<object>(0x3340); // Size: 36. Description: This area is used for externally signalled “joystick button” control. Each DWORD or 32 bits represents one “joystick” with 32 buttons. If an external program sets or clears a bit in any of these 9 DWORDS the “Buttons” page in FSUIPC will register the change as a button operation on one of Joystick numbers 64 to 73 (corresponding to the 9 DWORDs). So, FSUIPC can be used to program whatever actions the user wants.
        private Offset<object> offset751 = new Offset<object>(0x3364); // Size: 1. Description: FS2004 “Ready to Fly” indicator. This is non-zero when FS is loading, or reloading a flight or aircraft or scenery, and becomes zero when flight mode is enabled (even if the simulator is paused or in Slew mode).
        private Offset<object> offset752 = new Offset<object>(0x3366); // Size: 1. Description: This byte reflects the FS2004 “Engine on Fire” flags. I’m not sure if FS actually simulates such events, but it appears to have allocated Gauge-accessible variables to indicate them. This byte uses bits 2^0–2^3 as flags for fires in Engines 1 to 4, respectively.
        private Offset<object> offset753 = new Offset<object>(0x3367); // Size: 1. Description: This byte shows doors that are open (FS2004 only). At present this only provides bit 2^0 for the main doors.
        private Offset<object> offset754 = new Offset<object>(0x336C); // Size: 2. Description: Frame rate calling counter. This is simply a number that is incremented each time FSUIPC is entered from FS using the entry related to frame rates. 
        private Offset<object> offset755 = new Offset<object>(0x336E); // Size: 2. Description: Toe brake axes have been selected as “Set” in FSUIPC’s joystick pages if this is non-zero. Byte 336E is non-zero for Left Brake, byte 336F for Right Brake.</span><span style="font-family: Tahoma; font-size: x-small;">Note that this only means that the user has told FSUIPC to handle the toe braking, by pressing “Set”. It will only actually do so if it sees brake messages.
        private Offset<object> offset756 = new Offset<object>(0x3374); // Size: 4. Description: This is the “live” millisecond count as used in the FSUIPC Log. It is updated on each FS chained call to FSUIPC.
        private Offset<object> offset757 = new Offset<object>(0x3378); // Size: 4. Description: This is the millisecond timestamp value of the most recent line in the current FSUIPC Log. It is updated when each line is logged.
        private Offset<object> offset758 = new Offset<object>(0x337C); // Size: 1. Description: Propeller de-ice switch, (1 = on, 0 = off), read for state, write to control [FS2002+]. <em>This should operate with aircraft defined to have the facility, but in fact it merely reflects the older Anti-Ice switch. The </em>TOGGLE_PROP_DEICE <em>control does nothing.</em>
        private Offset<object> offset759 = new Offset<object>(0x337D); // Size: 1. Description: Structural de-ice switch, (1 = on, 0 = off), read for state, write to control [FS2002+]. <em>Although this is documented in both FS2002 and FS2004 panel SDKs, with a token value and an FS control, it appears to do nothing. Possibly it needs some action in the AIR file or Aircraft.CFG, but there’s nothing in the official documentation.</em>
        private Offset<object> offset760 = new Offset<object>(0x337E); // Size: 2. Description: FSUIPC activity count. Simply a number that is incremented every time FSUIPC receives a call or message from Flight Simulator. This can be used through WideFS to check if FS is still active, for example. Note that when FS is loading aircraft or scenery/textures, this value may not change for many seconds as FSUIPC is then not getting any processor time at all.
        private Offset<object> offset761 = new Offset<object>(0x3380); // Size: 128. Description: Message text area, used by AdvDisplay.dll for a copy of the ADVenture text display: useful for programs wishing to display the adventure texts on a separate PC, via WideFS. The text is truncated if longer than 127 characters, there always being a zero terminator provided.</span><span style="font-family: Tahoma; font-size: x-small;">You can also <em>write </em>messages to this area, always zero terminated, for display on the FS windshield or via AdvDisplay if it is running. After placing the message text, you must write the 16-bit timer value to offset 32FA to make FSUIPC send the message through to FS (see 32FA above).
        private Offset<object> offset762 = new Offset<object>(0x3470); // Size: 8. Description: Ambient wind X component, double float (FS2002+)
        private Offset<object> offset763 = new Offset<object>(0x3478); // Size: 8. Description: Ambient wind Y component, double float (FS2002+).</span><span style="font-family: Tahoma; font-size: x-small;">In FS2004 (only), values written here are sustained by FSUIPC for up to 14 seconds, or until another value is written. This can be used by applications to provide things like lift for glider soaring, or fast varitions to emulate vertical turbulence. The values written here are <em>not </em>subject to FSUIPC’s smoothing, and won’t be reflected in any normal weather read-outs like ATIS.
        private Offset<object> offset764 = new Offset<object>(0x3480); // Size: 8. Description: Ambient wind Z component, double float (FS2002+)
        private Offset<object> offset765 = new Offset<object>(0x3488); // Size: 8. Description: Ambient wind velocity, double float (FS2002+)
        private Offset<object> offset766 = new Offset<object>(0x3490); // Size: 8. Description: Ambient wind direction, double float (FS2002+)
        private Offset<object> offset767 = new Offset<object>(0x3498); // Size: 8. Description: Ambient pressure, double float (FS2002+). This is accurate in FS2004, but suspicious in FS2002.
        private Offset<object> offset768 = new Offset<object>(0x34A0); // Size: 8. Description: Sea level pressure (QNH), double float (FS2002+)
        private Offset<object> offset769 = new Offset<object>(0x34A8); // Size: 8. Description: Ambient temperature, double float (FS2002+)
        private Offset<object> offset770 = new Offset<object>(0x3542); // Size: 2. Description: Standby altimeter pressure setting (“Kollsman” window). As millibars (hectoPascals) * 16. [<em>This is used by FSUIPC to maintain offset 3544. It is not used by FS at all</em>]
        private Offset<object> offset771 = new Offset<object>(0x3544); // Size: 4. Description: This is the standby altimeter reading in feet (or metres, if the user is running with the preference for altitudes in metres), as a 32-bit signed integer. Please check offset 0C18 to determine when metres are used (0C18 contains ‘2’).</span><span style="font-family: Tahoma; font-size: x-small;">This value is maintained by FSUIPC using the pressure setting supplied in offset 3542. It isn’t used in FS itself, but is supplied for additional gauges and external altimeters so that the standby can be kept at the correct (or last notified) QNH whilst the main altimeter is used for Standard settings (for airliners flying Flight Levels).
        private Offset<object> offset772 = new Offset<object>(0x3548); // Size: 8. Description: Horizon bars offset, as a percentage of maximum, in floating point double format. (–100.0 down to +100.0 up). On the default Cessnas the maximum offset is 10 degrees. [Read only on FS2004]
        private Offset<object> offset773 = new Offset<object>(0x3590); // Size: 4. Description: Engine 1 Fuel Valve, 1 = open, 2 = closed. Can write to operate. [FS2002+]
        private Offset<object> offset774 = new Offset<object>(0x3594); // Size: 4. Description: Engine 2 Fuel Valve, 1 = open, 2 = closed. Can write to operate. [FS2002+]
        private Offset<object> offset775 = new Offset<object>(0x3598); // Size: 4. Description: Engine 3 Fuel Valve, 1 = open, 2 = closed. Can write to operate. [FS2002+]
        private Offset<object> offset776 = new Offset<object>(0x359C); // Size: 4. Description: Engine 4 Fuel Valve, 1 = open, 2 = closed. Can write to operate. [FS2002+]
        private Offset<object> offset777 = new Offset<object>(0x35A0); // Size: 8. Description: Airspeed Mach value, double float (FS2002+)
        private Offset<object> offset778 = new Offset<object>(0x35A8); // Size: 8. Description: Reciprocating engine 4 manifold pressure, in lbs/sqft, as a double (FLOAT64). Divide by 70.7262 for inches Hg.
        private Offset<object> offset779 = new Offset<object>(0x35B0); // Size: 8. Description: Engine 4 cowl flap position, as a double float: 0.0=fully closed, 1.0=fully open. Can be used to handle position and set it. . [FS2000–FS2004 only]
        private Offset<object> offset780 = new Offset<object>(0x35D0); // Size: 4. Description: Reciprocating engine 4, left magneto select (1 = on, 0 = off)
        private Offset<object> offset781 = new Offset<object>(0x35D4); // Size: 4. Description: Reciprocating engine 4, right magneto select (1 = on, 0 = off)
        private Offset<object> offset782 = new Offset<object>(0x35D8); // Size: 8. Description: Reciprocating engine 4 fuel/air mass ratio, as a double (FLOAT64).
        private Offset<object> offset783 = new Offset<object>(0x35E0); // Size: 8. Description: Reciprocating engine 4 brake power in ft-lbs, as a double (FLOAT64). Divide by 550 for HP.
        private Offset<object> offset784 = new Offset<object>(0x35E8); // Size: 8. Description: Reciprocating engine 4 carburettor temperature, in degrees Rankine, as a double (FLOAT64). [<em>FSUIPC version 3.401 or later</em>]
        private Offset<object> offset785 = new Offset<object>(0x3628); // Size: 8. Description: Reciprocating engine 4 fuel pressure (double or FLOAT64)
        private Offset<object> offset786 = new Offset<object>(0x3640); // Size: 4. Description: Reciprocating engine 4 tank selector, using the same numbers as 0AF8.
        private Offset<object> offset787 = new Offset<object>(0x3648); // Size: 4. Description: Reciprocating engine 4, number of fuel tanks supplying fuel.
        private Offset<object> offset788 = new Offset<object>(0x3654); // Size: 4. Description: Reciprocating engine 4 fuel available flag (0 or 1).
        private Offset<object> offset789 = new Offset<object>(0x3668); // Size: 8. Description: Reciprocating engine 3 manifold pressure, in lbs/sqft, as a double (FLOAT64). Divide by 70.7262 for inches Hg.
        private Offset<object> offset790 = new Offset<object>(0x3670); // Size: 8. Description: Engine 3 cowl flap position, as a double float: 0.0=fully closed, 1.0=fully open. Can be used to handle position and set it. [FS2000–FS2004 only]
        private Offset<object> offset791 = new Offset<object>(0x3690); // Size: 4. Description: Reciprocating engine 3, left magneto select (1 = on, 0 = off)
        private Offset<object> offset792 = new Offset<object>(0x3694); // Size: 4. Description: Reciprocating engine 3, right magneto select (1 = on, 0 = off)
        private Offset<object> offset793 = new Offset<object>(0x3698); // Size: 8. Description: Reciprocating engine 3 fuel/air mass ratio, as a double (FLOAT64).
        private Offset<object> offset794 = new Offset<object>(0x36A0); // Size: 8. Description: Reciprocating engine 3 brake power in ft-lbs, as a double (FLOAT64). Divide by 550 for HP.
        private Offset<object> offset795 = new Offset<object>(0x36A8); // Size: 8. Description: Reciprocating engine 3 carburettor temperature, in degrees Rankine, as a double (FLOAT64). [<em>FSUIPC version 3.401 or later</em>]
        private Offset<object> offset796 = new Offset<object>(0x36E8); // Size: 8. Description: Reciprocating engine 3 fuel pressure (double or FLOAT64)
        private Offset<object> offset797 = new Offset<object>(0x3700); // Size: 4. Description: Reciprocating engine 3 tank selector, using the same numbers as 0AF8.
        private Offset<object> offset798 = new Offset<object>(0x3708); // Size: 4. Description: Reciprocating engine 3, number of fuel tanks supplying fuel.
        private Offset<object> offset799 = new Offset<object>(0x3714); // Size: 4. Description: Reciprocating engine 3, fuel available flag (0 or 1).
        private Offset<object> offset800 = new Offset<object>(0x3728); // Size: 8. Description: Reciprocating engine 2 manifold pressure, in lbs/sqft, as a double (FLOAT64). Divide by 70.7262 for inches Hg.
        private Offset<object> offset801 = new Offset<object>(0x3730); // Size: 8. Description: Engine 2 cowl flap position, as a double float: 0.0=fully closed, 1.0=fully open. Can be used to handle position and set it. . [FS2000–FS2004 only]
        private Offset<object> offset802 = new Offset<object>(0x3750); // Size: 4. Description: Reciprocating engine 2, left magneto select (1 = on, 0 = off)
        private Offset<object> offset803 = new Offset<object>(0x3754); // Size: 4. Description: Reciprocating engine 2, right magneto select (1 = on, 0 = off)
        private Offset<object> offset804 = new Offset<object>(0x3758); // Size: 8. Description: Reciprocating engine 2 fuel/air mass ratio, as a double (FLOAT64).
        private Offset<object> offset805 = new Offset<object>(0x3760); // Size: 8. Description: Reciprocating engine 2 brake power in ft-lbs, as a double (FLOAT64). Divide by 550 for HP.
        private Offset<object> offset806 = new Offset<object>(0x3768); // Size: 8. Description: Reciprocating engine 2 carburettor temperature, in degrees Rankine, as a double (FLOAT64). [<em>FSUIPC version 3.401 or later</em>]
        private Offset<object> offset807 = new Offset<object>(0x37A8); // Size: 8. Description: Reciprocating engine 2 fuel pressure (double or FLOAT64)
        private Offset<object> offset808 = new Offset<object>(0x37C0); // Size: 4. Description: Reciprocating engine 2 tank selector, using the same numbers as 0AF8.
        private Offset<object> offset809 = new Offset<object>(0x37C8); // Size: 4. Description: Reciprocating engine 2, number of fuel tanks supplying fuel.
        private Offset<object> offset810 = new Offset<object>(0x37D4); // Size: 4. Description: Reciprocating engine 2, fuel available flag (0 or 1).
        private Offset<object> offset811 = new Offset<object>(0x37E8); // Size: 8. Description: Reciprocating engine 1 manifold pressure, in lbs/sqft, as a double (FLOAT64). Divide by 70.7262 for inches Hg.
        private Offset<object> offset812 = new Offset<object>(0x37F0); // Size: 8. Description: Engine 1 cowl flap position, as a double float: 0.0=fully closed, 1.0=fully open. Can be used to handle position and set it. [FS2000–FS2004 only]
        private Offset<object> offset813 = new Offset<object>(0x3810); // Size: 4. Description: Reciprocating engine 1, left magneto select (1 = on, 0 = off)
        private Offset<object> offset814 = new Offset<object>(0x3814); // Size: 4. Description: Reciprocating engine 1, right magneto select (1 = on, 0 = off)
        private Offset<object> offset815 = new Offset<object>(0x3818); // Size: 8. Description: Reciprocating engine 1 fuel/air mass ratio, as a double (FLOAT64).
        private Offset<object> offset816 = new Offset<object>(0x3820); // Size: 8. Description: Reciprocating engine 1 brake power in ft-lbs, as a double (FLOAT64). Divide by 550 for HP.
        private Offset<object> offset817 = new Offset<object>(0x3828); // Size: 8. Description: Reciprocating engine 1 carburettor temperature, in degrees Rankine, as a double (FLOAT64). [<em>FSUIPC version 3.401 or later</em>]
        private Offset<object> offset818 = new Offset<object>(0x3868); // Size: 8. Description: Reciprocating engine 1 fuel pressure (double or FLOAT64)
        private Offset<object> offset819 = new Offset<object>(0x3880); // Size: 4. Description: Reciprocating engine 1 tank selector, using the same numbers as 0AF8.
        private Offset<object> offset820 = new Offset<object>(0x3888); // Size: 4. Description: Reciprocating engine 1, number of fuel tanks supplying fuel.
        private Offset<object> offset821 = new Offset<object>(0x3894); // Size: 4. Description: Reciprocating engine 1, fuel available flag (0 or 1).
        private Offset<object> offset822 = new Offset<object>(0x38A8); // Size: 8. Description: General engine 4 throttle lever position, as a double (FLOAT64). 0.0=idle, 1.0=max
        private Offset<object> offset823 = new Offset<object>(0x38B0); // Size: 8. Description: General engine 4 mixture lever position, as a double (FLOAT64). 0.0=cutoff, 1.0=full rich
        private Offset<object> offset824 = new Offset<object>(0x38B8); // Size: 8. Description: General engine 4 propeller lever position, as a double (FLOAT64). 0–1
        private Offset<object> offset825 = new Offset<object>(0x3918); // Size: 8. Description: General engine 4 oil temperature in degrees Rankine, as a double (FLOAT64).
        private Offset<object> offset826 = new Offset<object>(0x3920); // Size: 8. Description: General engine 4 oil pressure in lbs/sqft, as a double (FLOAT64). Divide by 144 for PSI.
        private Offset<object> offset827 = new Offset<object>(0x3930); // Size: 8. Description: General engine 4 EGT in degrees Rankine, as a double (FLOAT64). Convert to Fahrenheit by Rankine – 459.67. FS default gauges show Centigrade.
        private Offset<object> offset828 = new Offset<object>(0x3938); // Size: 4. Description: Engine 4 generator switch, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset829 = new Offset<object>(0x393C); // Size: 4. Description: Engine 4 generator active, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset830 = new Offset<object>(0x3958); // Size: 4. Description: Engine 4 fuel pump switch, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset831 = new Offset<object>(0x3968); // Size: 8. Description: General engine 3 throttle lever position, as a double (FLOAT64). 0.0=idle, 1.0=max
        private Offset<object> offset832 = new Offset<object>(0x3970); // Size: 8. Description: General engine 3 mixture lever position, as a double (FLOAT64). 0.0=cutoff, 1.0=full rich
        private Offset<object> offset833 = new Offset<object>(0x3978); // Size: 8. Description: General engine 3 propeller lever position, as a double (FLOAT64). 0–1
        private Offset<object> offset834 = new Offset<object>(0x39D8); // Size: 8. Description: General engine 3 oil temperature in degrees Rankine, as a double (FLOAT64).
        private Offset<object> offset835 = new Offset<object>(0x39E0); // Size: 8. Description: General engine 3 oil pressure in lbs/sqft, as a double (FLOAT64). Divide by 144 for PSI.
        private Offset<object> offset836 = new Offset<object>(0x39F0); // Size: 8. Description: General engine 3 EGT in degrees Rankine, as a double (FLOAT64). Convert to Fahrenheit by Rankine – 459.67. FS default gauges show Centigrade.
        private Offset<object> offset837 = new Offset<object>(0x39F8); // Size: 4. Description: Engine 3 generator switch, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset838 = new Offset<object>(0x39FC); // Size: 4. Description: Engine 3 generator active, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset839 = new Offset<object>(0x3A18); // Size: 4. Description: Engine 3 fuel pump switch, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset840 = new Offset<object>(0x3A28); // Size: 8. Description: General engine 2 throttle lever position, as a double (FLOAT64). 0.0=idle, 1.0=max
        private Offset<object> offset841 = new Offset<object>(0x3A30); // Size: 8. Description: General engine 2 mixture lever position, as a double (FLOAT64). 0.0=cutoff, 1.0=full rich
        private Offset<object> offset842 = new Offset<object>(0x3A38); // Size: 8. Description: General engine 2 propeller lever position, as a double (FLOAT64). 0–1
        private Offset<object> offset843 = new Offset<object>(0x3A98); // Size: 8. Description: General engine 2 oil temperature in degrees Rankine, as a double (FLOAT64).
        private Offset<object> offset844 = new Offset<object>(0x3AA0); // Size: 8. Description: General engine 2 oil pressure in lbs/sqft, as a double (FLOAT64). Divide by 144 for PSI.
        private Offset<object> offset845 = new Offset<object>(0x3AB0); // Size: 8. Description: General engine 2 EGT in degrees Rankine, as a double (FLOAT64). Convert to Fahrenheit by Rankine – 459.67. FS default gauges show Centigrade.
        private Offset<object> offset846 = new Offset<object>(0x3AB8); // Size: 4. Description: Engine 2 generator switch, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset847 = new Offset<object>(0x3ABC); // Size: 4. Description: Engine 2 generator active, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset848 = new Offset<object>(0x3AD8); // Size: 4. Description: Engine 2 fuel pump switch, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset849 = new Offset<object>(0x3AE8); // Size: 8. Description: General engine 1 throttle lever position, as a double (FLOAT64). 0.0=idle, 1.0=max
        private Offset<object> offset850 = new Offset<object>(0x3AF0); // Size: 8. Description: General engine 1 mixture lever position, as a double (FLOAT64). 0.0=cutoff, 1.0=full rich
        private Offset<object> offset851 = new Offset<object>(0x3AF8); // Size: 8. Description: General engine 1 propeller lever position, as a double (FLOAT64). 0–1
        private Offset<object> offset852 = new Offset<object>(0x3B58); // Size: 8. Description: General engine 1 oil temperature in degrees Rankine, as a double (FLOAT64).
        private Offset<object> offset853 = new Offset<object>(0x3B60); // Size: 8. Description: General engine 1 oil pressure in lbs/sqft, as a double (FLOAT64). Divide by 144 for PSI.
        private Offset<object> offset854 = new Offset<object>(0x3B70); // Size: 8. Description: General engine 1 EGT in degrees Rankine, as a double (FLOAT64). Convert to Fahrenheit by Rankine – 459.67. FS default gauges show Centigrade.
        private Offset<object> offset855 = new Offset<object>(0x3B78); // Size: 4. Description: Engine 1 generator switch, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset856 = new Offset<object>(0x3B7C); // Size: 4. Description: Engine 1 generator active, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset857 = new Offset<object>(0x3B98); // Size: 4. Description: Engine 1 fuel pump switch, a 32-bit BOOL (0 = off, 1= on) [FS2000–FS2004 only]
        private Offset<object> offset858 = new Offset<object>(0x3BA0); // Size: 8. Description: The tailhook position, as a double floating point value (0.0=fully retracted, 1.0=fully lowered). [FS2002 and FS2004 only]
        private Offset<object> offset859 = new Offset<object>(0x3BA8); // Size: 44. Description: Area used only in PFC.DLL. Please see its documentation for details.
        private Offset<object> offset860 = new Offset<object>(0x3BD2); // Size: 2. Description: This is a 16-bit counter that is incremented each time a FLT file is saved in FS. This applies to flights saved through FS Flights menu, the shortcut key (;), AutoSave, and via the FSUIPC flight saving facilities. </span><span style="font-family: Tahoma; font-size: x-small;">Flight filenames can be read using the path reading facility at offsets 0FF0 ff.
        private Offset<object> offset861 = new Offset<object>(0x3BE8); // Size: 8. Description: Attitude Indicator failure timer, as a double floating point value.
        private Offset<object> offset862 = new Offset<object>(0x3BF0); // Size: 4. Description: Attitude indicator lock indicator, 32-bit integer but probable only Boolean (0 or 1) [FS2k/CFS2 only]
        private Offset<object> offset863 = new Offset<object>(0x3BF4); // Size: 4. Description: Low vacuum indicator, 32-bit integer but probable only Boolean (0 or 1) [FS2k/CFS2 only]
        private Offset<object> offset864 = new Offset<object>(0x3BFA); // Size: 2. Description: Flaps dTtente increment. The full range of flap movement is 0–0x3FFF (16383). Each dTtente position or “notch” is spaced equally over this range, no matter what flap angle is represented—a table in the AIR file gives those. To obtain the number of dTtentes, divide this increment value into 16383 and add 1. For example 2047 (0x7FF) would be the increment for 9 positions as on the default FS2K 737.
        private Offset<object> offset865 = new Offset<object>(0x3BFC); // Size: 4. Description: Zero Fuel Weight, lbs * 256. This is the aircraft weight plus the payload weight, minus fuel. In FS2004 this changes as the payload is adjusted.
        private Offset<object> offset866 = new Offset<object>(0x3C00); // Size: 256. Description: Pathname of the current AIR file, excluding the FS main path (see 3E00), but including everything from “Aircraft\…” to the final “…air”. This is zero padded to fill the 256 bytes available.</span><span style="font-family: Tahoma; font-size: x-small;">When this changes the 16-bit counter at 32FC is incremented, so interested programs don’t have to keep on reading the whole 256 bytes to check.
        private Offset<object> offset867 = new Offset<object>(0x3D00); // Size: 256. Description: Name of the current aircraft (from the “title” parameter in the AIRCRAFT.CFG file). Valid for FS2K only.
        private Offset<object> offset868 = new Offset<object>(0x3E00); // Size: 256. Description: Path of the Flight Simulator installation, down to and including the FS main folder and a following \ character. If the PC is on a Network and the drive or path is shared, then the full UNC (universal naming convention) path is given. Examples are:</span><span style="font-family: Tahoma; font-size: x-small;"> D:\FS2000\ (non-Network)</span><span style="font-family: Tahoma; font-size: x-small;">\\MyMainPC\drived\FS2000\ (Network, named PC and named shared drive))
        private Offset<object> offset869 = new Offset<object>(0x3F02); // Size: 2. Description: FLT/STN file loading counter (incremented by FSUIPC whenever the FLT or STN file, as defined at offset 3F04 changes or is reloaded). This word is read only—attempting to write here will do no harm.
        private Offset<object> offset870 = new Offset<object>(0x4000); // Size: 5632. Description: <em>Reserved</em>
        private Offset<object> offset871 = new Offset<object>(0x5600); // Size: 2560. Description: <em>Available for applications: apply for allocations to Pete Dowson</em>
        private Offset<object> offset872 = new Offset<object>(0x6000); // Size: 512. Description: FS2004 GPS data area—only known offsets listed below:
        private Offset<object> offset873 = new Offset<object>(0x6010); // Size: 8. Description: FS2004 GPS: aircraft latitude, floating point double, in degrees (+ve = N, –ve = S).
        private Offset<object> offset874 = new Offset<object>(0x6018); // Size: 8. Description: FS2004 GPS: aircraft longitude, floating point double, in degrees (+ve = E, –ve = W).
        private Offset<object> offset875 = new Offset<object>(0x6020); // Size: 8. Description: FS2004 GPS: aircraft altitude, floating point double, in metres.
        private Offset<object> offset876 = new Offset<object>(0x6028); // Size: 8. Description: FS2004 GPS: magnetic variation at aircraft, , floating point double, in radians (add to magnetic for true, subtract from true for magnetic).
        private Offset<object> offset877 = new Offset<object>(0x6030); // Size: 8. Description: FS2004 GPS: aircraft ground speed, floating point double, metres per second.
        private Offset<object> offset878 = new Offset<object>(0x6038); // Size: 8. Description: FS2004 GPS: aircraft true heading, floating point double, in radians.
        private Offset<object> offset879 = new Offset<object>(0x6040); // Size: 8. Description: FS2004 GPS: aircraft magnetic track, floating point double, in radians.
        private Offset<object> offset880 = new Offset<object>(0x6048); // Size: 8. Description: FS2004 GPS: distance to next way point, floating point double, in metres.
        private Offset<object> offset881 = new Offset<object>(0x6050); // Size: 8. Description: FS2004 GPS: magnetic bearing to next way point, floating point double, in radians.
        private Offset<object> offset882 = new Offset<object>(0x6058); // Size: 8. Description: FS2004 GPS: cross track error, floating point double, in metres.
        private Offset<object> offset883 = new Offset<object>(0x6060); // Size: 8. Description: FS2004 GPS: required true heading, floating point double, in radians.
        private Offset<object> offset884 = new Offset<object>(0x6078); // Size: 8. Description: FS2004 GPS: aircraft vertical speed (<em>Needs checking</em>)
        private Offset<object> offset885 = new Offset<object>(0x6080); // Size: 1. Description: FS2004 GPS: previous way point valid flag (=0 if not valid)
        private Offset<object> offset886 = new Offset<object>(0x6081); // Size: 6?. Description: FS2004 GPS: string ID of previous way point, zero terminated
        private Offset<object> offset887 = new Offset<object>(0x608C); // Size: 8. Description: FS2004 GPS: previous way point latitude, floating point double, in degrees (+ve = N, –ve = S).
        private Offset<object> offset888 = new Offset<object>(0x6094); // Size: 8. Description: FS2004 GPS: previous way point longitude, floating point double, in degrees (+ve = E, –ve = W).
        private Offset<object> offset889 = new Offset<object>(0x609C); // Size: 8. Description: FS2004 GPS: previous way point aircraft altitude, floating point double, in metres.
        private Offset<object> offset890 = new Offset<object>(0x60A4); // Size: 6?. Description: FS2004 GPS: string ID of next way point, zero terminated
        private Offset<object> offset891 = new Offset<object>(0x60AC); // Size: 8. Description: FS2004 GPS: next way point latitude, floating point double, in degrees (+ve = N, –ve = S).
        private Offset<object> offset892 = new Offset<object>(0x60B4); // Size: 8. Description: FS2004 GPS: next way point longitude, floating point double, in degrees (+ve = E, –ve = W).
        private Offset<object> offset893 = new Offset<object>(0x60BC); // Size: 8. Description: FS2004 GPS: next way point aircraft altitude, floating point double, in metres.
        private Offset<object> offset894 = new Offset<object>(0x60E4); // Size: 4. Description: FS2004 GPS: Next way point ETE as 32-bit integer, in seconds
        private Offset<object> offset895 = new Offset<object>(0x60E8); // Size: 4. Description: FS2004 GPS: Next way point ETA as 32-bit integer in seconds, local time
        private Offset<object> offset896 = new Offset<object>(0x60FC); // Size: 4. Description: FS2004 GPS: Approach mode, as 32-bit integer (<em>needs checking</em>)
        private Offset<object> offset897 = new Offset<object>(0x6100); // Size: 4. Description: FS2004 GPS: Approach way point type, as 32-bit integer (<em>needs checking</em>)
        private Offset<object> offset898 = new Offset<object>(0x6104); // Size: 4. Description: FS2004 GPS: Approach segment type, as 32-bit integer (<em>needs checking</em>)
        private Offset<object> offset899 = new Offset<object>(0x6108); // Size: 1. Description: FS2004 GPS: Approach mode, flag indicating approach waypoint is the runway (<em>needs checking</em>)
        private Offset<object> offset900 = new Offset<object>(0x6120); // Size: 4. Description: FS2004 GPS: Flight Plan, total number of waypoints, as 32-bit integer
        private Offset<object> offset901 = new Offset<object>(0x6128); // Size: 4. Description: FS2004 GPS: Approach way point count, as 32-bit integer (<em>needs checking</em>)
        private Offset<object> offset902 = new Offset<object>(0x613C); // Size: 4. Description: FS2004 GPS: Approach way point index, as 32-bit integer (<em>needs checking</em>)
        private Offset<object> offset903 = new Offset<object>(0x6150); // Size: 4. Description: FS2004 GPS: Approach transition index, as 32-bit integer (<em>needs checking</em>). –1 means not valid.
        private Offset<object> offset904 = new Offset<object>(0x615C); // Size: 1. Description: FS2004 GPS: Approach is missed flag (<em>needs checking</em>)
        private Offset<object> offset905 = new Offset<object>(0x6160); // Size: 4. Description: FS2004 GPS: Approach type (<em>needs checking</em>)
        private Offset<object> offset906 = new Offset<object>(0x6168); // Size: 4. Description: FS2004 GPS: Approach time zone deviation, as 32-bit integer (<em>needs checking</em>)
        private Offset<object> offset907 = new Offset<object>(0x616C); // Size: 4. Description: FS2004 GPS: Current way point index, starting at 1, as 32-bit integer
        private Offset<object> offset908 = new Offset<object>(0x6170); // Size: 4. Description: FS2004 GPS: Approach current way point index, as 32-bit integer (<em>needs checking</em>)
        private Offset<object> offset909 = new Offset<object>(0x6198); // Size: 4. Description: FS2004 GPS: Destination ETE as 32-bit integer, in seconds
        private Offset<object> offset910 = new Offset<object>(0x619C); // Size: 4. Description: FS2004 GPS: Destination ETA as 32-bit integer, in seconds, local time
        private Offset<object> offset911 = new Offset<object>(0x6200); // Size: 1088. Description: <em>Reserved</em>
        private Offset<object> offset912 = new Offset<object>(0x6640); // Size: 192. Description: <em>Available for applications: apply for allocations to Pete Dowson</em>
        private Offset<object> offset913 = new Offset<object>(0x6700); // Size: 1536. Description: <em>Reserved</em>
        private Offset<object> offset914 = new Offset<object>(0x6D00); // Size: 3712. Description: <em>Available for applications: apply for allocations to Pete Dowson</em>
        private Offset<object> offset915 = new Offset<object>(0x7B80); // Size: 1024. Description: <em>Reserved</em>
        private Offset<object> offset916 = new Offset<object>(0x8000); // Size: 768. Description: <em>Reserved for FSUIPC and WideFS internals</em>
        private Offset<object> offset917 = new Offset<object>(0x8300); // Size: 11520. Description: <em>Available for applications: apply for allocations to Pete Dowson</em>
        private Offset<object> offset918 = new Offset<object>(0xB000); // Size: 4096. Description: <em>Reserved for future improvements</em>
        private Offset<object> offset919 = new Offset<object>(0xC000); // Size: 4096. Description: FS2004 New Weather Interface areas, allowing both local and global weather data to be read and written.</span><span style="font-family: Tahoma; font-size: x-small;">(details of the NWI are provided separately in the SDK)
        private Offset<object> offset920 = new Offset<object>(0xD000); // Size: 2048. Description: FS2004 A.I. ground aircraft additional traffic data (see section on AI Traffic earlier)
        private Offset<object> offset921 = new Offset<object>(0xD800); // Size: 2048. Description: FS2004 A.I. airborne aircraft additional traffic data (see section on AI Traffic earlier)
        private Offset<object> offset922 = new Offset<object>(0xE000); // Size: 4096. Description: FS2002/4 A.I. ground aircraft traffic data (see section on AI Traffic earlier)
        private Offset<object> offset923 = new Offset<object>(0xF000); // Size: 4096. Description: FS2002/4 A.I. airborne aircraft traffic data (see section on AI Traffic earlier)
        private Offset<object> offset925 = new Offset<object>(0x2048); // Size: TURB_ENGINE_1_AFTERBURNER. Description: 632
        private Offset<object> offset926 = new Offset<object>(0x2054); // Size: TURB_ENGINE_1_TANK_SELECTOR. Description: 635
        private Offset<object> offset927 = new Offset<object>(0x2058); // Size: TURB_ENGINE_1_TANKS_USED. Description: 636
        private Offset<object> offset928 = new Offset<object>(0x2068); // Size: TURB_ENGINE_1_FUEL_AVAILABLE. Description: 639
        private Offset<object> offset929 = new Offset<object>(0x2074); // Size: TURB_ENGINE_1_PCT_AREA. Description: 640
        private Offset<object> offset930 = new Offset<object>(0x2084); // Size: TURB_ENGINE_1_VIBRATION. Description: 642
        private Offset<object> offset931 = new Offset<object>(0x2148); // Size: TURB_ENGINE_2_AFTERBURNER. Description: 651
        private Offset<object> offset932 = new Offset<object>(0x2154); // Size: TURB_ENGINE_2_TANK_SELECTOR. Description: 654
        private Offset<object> offset933 = new Offset<object>(0x2158); // Size: TURB_ENGINE_2_TANKS_USED. Description: 655
        private Offset<object> offset934 = new Offset<object>(0x2168); // Size: TURB_ENGINE_2_FUEL_AVAILABLE. Description: 658
        private Offset<object> offset935 = new Offset<object>(0x2174); // Size: TURB_ENGINE_2_PCT_AREA. Description: 659
        private Offset<object> offset936 = new Offset<object>(0x2184); // Size: TURB_ENGINE_2_VIBRATION. Description: 661
        private Offset<object> offset937 = new Offset<object>(0x2248); // Size: TURB_ENGINE_3_AFTERBURNER. Description: 670
        private Offset<object> offset938 = new Offset<object>(0x2254); // Size: TURB_ENGINE_3_TANK_SELECTOR. Description: 673
        private Offset<object> offset939 = new Offset<object>(0x2258); // Size: TURB_ENGINE_3_TANKS_USED. Description: 674
        private Offset<object> offset940 = new Offset<object>(0x2268); // Size: TURB_ENGINE_3_FUEL_AVAILABLE. Description: 677
        private Offset<object> offset941 = new Offset<object>(0x2274); // Size: TURB_ENGINE_3_PCT_AREA. Description: 678
        private Offset<object> offset942 = new Offset<object>(0x2284); // Size: TURB_ENGINE_3_VIBRATION. Description: 680
        private Offset<object> offset943 = new Offset<object>(0x2348); // Size: TURB_ENGINE_4_AFTERBURNER. Description: 689
        private Offset<object> offset944 = new Offset<object>(0x2354); // Size: TURB_ENGINE_4_TANK_SELECTOR. Description: 692
        private Offset<object> offset945 = new Offset<object>(0x2358); // Size: TURB_ENGINE_4_TANKS_USED. Description: 693
        private Offset<object> offset946 = new Offset<object>(0x2368); // Size: TURB_ENGINE_4_FUEL_AVAILABLE. Description: 696
        private Offset<object> offset947 = new Offset<object>(0x2374); // Size: TURB_ENGINE_4_PCT_AREA. Description: 697
        private Offset<object> offset948 = new Offset<object>(0x2384); // Size: TURB_ENGINE_4_VIBRATION. Description: 699
        private Offset<object> offset949 = new Offset<object>(0x2420); // Size: PROPELLER_1_FEATHERING_INHIBIT. Description: 704
        private Offset<object> offset950 = new Offset<object>(0x2424); // Size: PROPELLER_1_FEATHERED. Description: 705
        private Offset<object> offset951 = new Offset<object>(0x2428); // Size: PROPELLER_1_SYNC_DELTA_LEVER. Description: 706
        private Offset<object> offset952 = new Offset<object>(0x2430); // Size: PROPELLER_1_AUTOFEATHER_ARMED. Description: 707
        private Offset<object> offset953 = new Offset<object>(0x2520); // Size: PROPELLER_2_FEATHERING_INHIBIT. Description: 712
        private Offset<object> offset954 = new Offset<object>(0x2524); // Size: PROPELLER_2_FEATHERED. Description: 713
        private Offset<object> offset955 = new Offset<object>(0x2528); // Size: PROPELLER_2_SYNC_DELTA_LEVER. Description: 714
        private Offset<object> offset956 = new Offset<object>(0x2530); // Size: PROPELLER_2_AUTOFEATHER_ARMED. Description: 715
        private Offset<object> offset957 = new Offset<object>(0x2620); // Size: PROPELLER_3_FEATHERING_INHIBIT. Description: 720
        private Offset<object> offset958 = new Offset<object>(0x2624); // Size: PROPELLER_3_FEATHERED. Description: 721
        private Offset<object> offset959 = new Offset<object>(0x2628); // Size: PROPELLER_3_SYNC_DELTA_LEVER. Description: 722
        private Offset<object> offset960 = new Offset<object>(0x2630); // Size: PROPELLER_3_AUTOFEATHER_ARMED. Description: 723
        private Offset<object> offset961 = new Offset<object>(0x2720); // Size: PROPELLER_4_FEATHERING_INHIBIT. Description: 728
        private Offset<object> offset962 = new Offset<object>(0x2724); // Size: PROPELLER_4_FEATHERED. Description: 729
        private Offset<object> offset963 = new Offset<object>(0x2728); // Size: PROPELLER_4_SYNC_DELTA_LEVER. Description: 730
        private Offset<object> offset964 = new Offset<object>(0x2730); // Size: PROPELLER_4_AUTOFEATHER_ARMED. Description: 731
        private Offset<object> offset965 = new Offset<object>(0x2824); // Size: TOTAL_LOAD_AMPS. Description: 750
        private Offset<object> offset966 = new Offset<object>(0x282C); // Size: BATTERY_LOAD. Description: 751
        private Offset<object> offset967 = new Offset<object>(0x2834); // Size: BATTERY_VOLTAGE. Description: 752
        private Offset<object> offset968 = new Offset<object>(0x2840); // Size: MAIN_BUS_VOLTAGE. Description: 753
        private Offset<object> offset969 = new Offset<object>(0x2848); // Size: MAIN_BUS_AMPS. Description: 754
        private Offset<object> offset970 = new Offset<object>(0x2850); // Size: AVIONICS_BUS_VOLTAGE. Description: 755
        private Offset<object> offset971 = new Offset<object>(0x2858); // Size: AVIONICS_BUS_AMPS. Description: 756
        private Offset<object> offset972 = new Offset<object>(0x2860); // Size: HOT_BATTERY_BUS_VOLTAGE. Description: 757
        private Offset<object> offset973 = new Offset<object>(0x2868); // Size: HOT_BATTERY_BUS_AMPS. Description: 758
        private Offset<object> offset974 = new Offset<object>(0x2870); // Size: BATTERY_BUS_VOLTAGE. Description: 759
        private Offset<object> offset975 = new Offset<object>(0x2878); // Size: BATTERY_BUS_AMPS. Description: 760
        private Offset<object> offset976 = new Offset<object>(0x2880); // Size: GENERATOR_ALTERNATOR_1_BUS_VOLTAGE. Description: 761
        private Offset<object> offset977 = new Offset<object>(0x2888); // Size: GENERATOR_ALTERNATOR_1_BUS_AMPS. Description: 762
        private Offset<object> offset978 = new Offset<object>(0x2890); // Size: GENERATOR_ALTERNATOR_2_BUS_VOLTAGE. Description: 763
        private Offset<object> offset979 = new Offset<object>(0x2898); // Size: GENERATOR_ALTERNATOR_2_BUS_AMPS. Description: 764
        private Offset<object> offset980 = new Offset<object>(0x28A0); // Size: GENERATOR_ALTERNATOR_3_BUS_VOLTAGE. Description: 765
        private Offset<object> offset981 = new Offset<object>(0x28A8); // Size: GENERATOR_ALTERNATOR_3_BUS_AMPS. Description: 766
        private Offset<object> offset982 = new Offset<object>(0x28B0); // Size: GENERATOR_ALTERNATOR_4_BUS_VOLTAGE. Description: 767
        private Offset<object> offset983 = new Offset<object>(0x28B8); // Size: GENERATOR_ALTERNATOR_4_BUS_AMPS. Description: 768
        private Offset<object> offset984 = new Offset<object>(0x2A00); // Size: ELEVON_1_DEFLECTION. Description: 809
        private Offset<object> offset985 = new Offset<object>(0x2A08); // Size: ELEVON_2_DEFLECTION. Description: 810
        private Offset<object> offset986 = new Offset<object>(0x2A10); // Size: ELEVON_3_DEFLECTION. Description: 811
        private Offset<object> offset987 = new Offset<object>(0x2A18); // Size: ELEVON_4_DEFLECTION. Description: 812
        private Offset<object> offset988 = new Offset<object>(0x2A20); // Size: ELEVON_5_DEFLECTION. Description: 813
        private Offset<object> offset989 = new Offset<object>(0x2A28); // Size: ELEVON_6_DEFLECTION. Description: 814
        private Offset<object> offset990 = new Offset<object>(0x2A30); // Size: ELEVON_7_DEFLECTION. Description: 815
        private Offset<object> offset991 = new Offset<object>(0x2A38); // Size: ELEVON_8_DEFLECTION. Description: 816
        private Offset<object> offset992 = new Offset<object>(0x2B08); // Size: HYDRAULICS1_PRESSURE_PSF. Description: 732
        private Offset<object> offset993 = new Offset<object>(0x2B1C); // Size: HYDRAULICS1_RESERVOIR_PCT. Description: 733
        private Offset<object> offset994 = new Offset<object>(0x2C08); // Size: HYDRAULICS2_PRESSURE_PSF. Description: 734
        private Offset<object> offset995 = new Offset<object>(0x2C1C); // Size: HYDRAULICS2_RESERVOIR_PCT. Description: 735
        private Offset<object> offset996 = new Offset<object>(0x2D08); // Size: HYDRAULICS3_PRESSURE_PSF. Description: 736
        private Offset<object> offset997 = new Offset<object>(0x2D1C); // Size: HYDRAULICS3_RESERVOIR_PCT. Description: 737
        private Offset<object> offset998 = new Offset<object>(0x2E08); // Size: HYDRAULICS4_PRESSURE_PSF. Description: 738
        private Offset<object> offset999 = new Offset<object>(0x2E1C); // Size: HYDRAULICS4_RESERVOIR_PCT. Description: 739
        private Offset<object> offset1000 = new Offset<object>(0x2E90); // Size: STANDBY_VACUUM_CIRCUIT_ON. Description: 778
        private Offset<object> offset1001 = new Offset<object>(0x2F00); // Size: CG_AFT_LIMIT. Description: 796
        private Offset<object> offset1002 = new Offset<object>(0x2F08); // Size: CG_FWD_LIMIT. Description: 797
        private Offset<object> offset1003 = new Offset<object>(0x2F10); // Size: CG_MAX_MACH. Description: 798
        private Offset<object> offset1004 = new Offset<object>(0x2F18); // Size: CG_MIN_MACH. Description: 799
        private Offset<object> offset1005 = new Offset<object>(0x2F20); // Size: CONCORDE_VISOR_NOSE_HANDLE. Description: 805
        private Offset<object> offset1006 = new Offset<object>(0x2F28); // Size: CONCORDE_VISOR_POS_PCT. Description: 806
        private Offset<object> offset1007 = new Offset<object>(0x2F30); // Size: CONCORDE_NOSE_ANGLE. Description: 807
        private Offset<object> offset1008 = new Offset<object>(0x2F38); // Size: GEAR_POS_TAIL. Description: 808
        private Offset<object> offset1009 = new Offset<object>(0x2F40); // Size: AUTOPILOT_MAX_SPEED. Description: 820
        private Offset<object> offset1010 = new Offset<object>(0x2F48); // Size: AUTOPILOT_CRUISE_SPEED. Description: 821
        private Offset<object> offset1011 = new Offset<object>(0x2F50); // Size: BARBER_POLE_MACH. Description: 822
        private Offset<object> offset1012 = new Offset<object>(0x2F58); // Size: SELECTED_FUEL_TRANSFER_MODE. Description: 823
        private Offset<object> offset1013 = new Offset<object>(0x2F60); // Size: HYDRAULIC_SYSTEM_INTEGRITY. Description: 824
        private Offset<object> offset1014 = new Offset<object>(0x2F68); // Size: ATTITUDE_CAGE_BUTTON. Description: 825
        private Offset<object> offset1015 = new Offset<object>(0x3420); // Size: RAD_INS_SWITCH. Description: 613
        private Offset<object> offset1016 = new Offset<object>(0x3424); // Size: LOW_HEIGHT_WARNING. Description: 616
        private Offset<object> offset1017 = new Offset<object>(0x3428); // Size: DECISION_HEIGHT. Description: 615
        private Offset<object> offset1018 = new Offset<object>(0x3438); // Size: ENGINE_1_FUELFLOW_BUG_POSITION. Description: 801
        private Offset<object> offset1019 = new Offset<object>(0x3440); // Size: ENGINE_2_FUELFLOW_BUG_POSITION. Description: 802
        private Offset<object> offset1020 = new Offset<object>(0x3448); // Size: ENGINE_3_FUELFLOW_BUG_POSITION. Description: 803
        private Offset<object> offset1021 = new Offset<object>(0x3450); // Size: ENGINE_4_FUELFLOW_BUG_POSITION. Description: 804
        private Offset<object> offset1022 = new Offset<object>(0x3458); // Size: PANEL_AUTOPILOT_SPEED_SETTING. Description: 817
        private Offset<object> offset1023 = new Offset<object>(0x3460); // Size: AUTOPILOT_AIRSPEED_HOLD_CURRENT. Description: 819
        private Offset<object> offset1024 = new Offset<object>(0x34D0); // Size: G_FORCE_MAXIMUM. Description: 605
        private Offset<object> offset1025 = new Offset<object>(0x34D8); // Size: G_FORCE_MINIMUM. Description: 606
        private Offset<object> offset1026 = new Offset<object>(0x34E8); // Size: ENGINE1_MAX_RPM. Description: 608
        private Offset<object> offset1027 = new Offset<object>(0x34EC); // Size: ENGINE2_MAX_RPM. Description: 609
        private Offset<object> offset1028 = new Offset<object>(0x34F0); // Size: ENGINE3_MAX_RPM. Description: 610
        private Offset<object> offset1029 = new Offset<object>(0x34F4); // Size: ENGINE4_MAX_RPM. Description: 611
        private Offset<object> offset1030 = new Offset<object>(0x3550); // Size: ENGINE4_THROTTLE_LEVER_POS. Description: 233
        private Offset<object> offset1031 = new Offset<object>(0x3552); // Size: ENGINE4_PROPELLER_LEVER_POS. Description: 234
        private Offset<object> offset1032 = new Offset<object>(0x3554); // Size: ENGINE4_MIXTURE_LEVER_POS. Description: 235
        private Offset<object> offset1033 = new Offset<object>(0x3556); // Size: ENGINE4_STARTER_SWITCH_POS. Description: 237
        private Offset<object> offset1034 = new Offset<object>(0x3558); // Size: ENGINE4_MAGNETO_LEFT. Description: 238
        private Offset<object> offset1035 = new Offset<object>(0x355A); // Size: ENGINE4_MAGNETO_RIGHT. Description: 239
        private Offset<object> offset1036 = new Offset<object>(0x3560); // Size: ENGINE3_THROTTLE_LEVER_POS. Description: 198
        private Offset<object> offset1037 = new Offset<object>(0x3562); // Size: ENGINE3_PROPELLER_LEVER_POS. Description: 199
        private Offset<object> offset1038 = new Offset<object>(0x3564); // Size: ENGINE3_MIXTURE_LEVER_POS. Description: 200
        private Offset<object> offset1039 = new Offset<object>(0x3566); // Size: ENGINE3_STARTER_SWITCH_POS. Description: 202
        private Offset<object> offset1040 = new Offset<object>(0x3568); // Size: ENGINE3_MAGNETO_LEFT. Description: 203
        private Offset<object> offset1041 = new Offset<object>(0x356A); // Size: ENGINE3_MAGNETO_RIGHT. Description: 204
        private Offset<object> offset1042 = new Offset<object>(0x3570); // Size: ENGINE2_THROTTLE_LEVER_POS. Description: 163
        private Offset<object> offset1043 = new Offset<object>(0x3572); // Size: ENGINE2_PROPELLER_LEVER_POS. Description: 164
        private Offset<object> offset1044 = new Offset<object>(0x3574); // Size: ENGINE2_MIXTURE_LEVER_POS. Description: 165
        private Offset<object> offset1045 = new Offset<object>(0x3576); // Size: ENGINE2_STARTER_SWITCH_POS. Description: 167
        private Offset<object> offset1046 = new Offset<object>(0x3578); // Size: ENGINE2_MAGNETO_LEFT. Description: 168
        private Offset<object> offset1047 = new Offset<object>(0x357A); // Size: ENGINE2_MAGNETO_RIGHT. Description: 169
        private Offset<object> offset1048 = new Offset<object>(0x3580); // Size: ENGINE1_THROTTLE_LEVER_POS. Description: 128
        private Offset<object> offset1049 = new Offset<object>(0x3582); // Size: ENGINE1_PROPELLER_LEVER_POS. Description: 129
        private Offset<object> offset1050 = new Offset<object>(0x3584); // Size: ENGINE1_MIXTURE_LEVER_POS. Description: 130
        private Offset<object> offset1051 = new Offset<object>(0x3586); // Size: ENGINE1_STARTER_SWITCH_POS. Description: 132
        private Offset<object> offset1052 = new Offset<object>(0x3588); // Size: ENGINE1_MAGNETO_LEFT. Description: 133
        private Offset<object> offset1053 = new Offset<object>(0x358A); // Size: ENGINE1_MAGNETO_RIGHT. Description: 134
        private Offset<object> offset1054 = new Offset<object>(0x35B8); // Size: RECIP_ENGINE4_CARB_HEAT_POS. Description: 513
        private Offset<object> offset1055 = new Offset<object>(0x35C0); // Size: RECIP_ENGINE4_ALTERNATE_AIR_POS. Description: 514
        private Offset<object> offset1056 = new Offset<object>(0x35C8); // Size: RECIP_ENGINE4_COOLANT_RESERVOIR_PCT. Description: 515
        private Offset<object> offset1057 = new Offset<object>(0x35F0); // Size: RECIP_ENGINE4_STARTER_TORQUE. Description: 522
        private Offset<object> offset1058 = new Offset<object>(0x35F8); // Size: RECIP_ENGINE4_TURBOCHARGER_FAILED. Description: 524
        private Offset<object> offset1059 = new Offset<object>(0x35FC); // Size: RECIP_ENGINE4_EMERGENCY_BOOST_ACTIVE. Description: 525
        private Offset<object> offset1060 = new Offset<object>(0x3600); // Size: RECIP_ENGINE4_EMERGENCY_BOOST_ELAPSED_TIME. Description: 526
        private Offset<object> offset1061 = new Offset<object>(0x3608); // Size: RECIP_ENGINE4_WASTEGATE_POS. Description: 527
        private Offset<object> offset1062 = new Offset<object>(0x3610); // Size: RECIP_ENGINE4_TIT_DEGR. Description: 531
        private Offset<object> offset1063 = new Offset<object>(0x3618); // Size: RECIP_ENGINE4_CHT_DEGR. Description: 532
        private Offset<object> offset1064 = new Offset<object>(0x3644); // Size: RECIP_ENGINE4_TANKS_USED. Description: 540
        private Offset<object> offset1065 = new Offset<object>(0x3678); // Size: RECIP_ENGINE3_CARB_HEAT_POS. Description: 474
        private Offset<object> offset1066 = new Offset<object>(0x3680); // Size: RECIP_ENGINE3_ALTERNATE_AIR_POS. Description: 475
        private Offset<object> offset1067 = new Offset<object>(0x3688); // Size: RECIP_ENGINE3_COOLANT_RESERVOIR_PCT. Description: 476
        private Offset<object> offset1068 = new Offset<object>(0x36B0); // Size: RECIP_ENGINE3_STARTER_TORQUE. Description: 483
        private Offset<object> offset1069 = new Offset<object>(0x36B8); // Size: RECIP_ENGINE3_TURBOCHARGER_FAILED. Description: 485
        private Offset<object> offset1070 = new Offset<object>(0x36BC); // Size: RECIP_ENGINE3_EMERGENCY_BOOST_ACTIVE. Description: 486
        private Offset<object> offset1071 = new Offset<object>(0x36C0); // Size: RECIP_ENGINE3_EMERGENCY_BOOST_ELAPSED_TIME. Description: 487
        private Offset<object> offset1072 = new Offset<object>(0x36C8); // Size: RECIP_ENGINE3_WASTEGATE_POS. Description: 488
        private Offset<object> offset1073 = new Offset<object>(0x36D0); // Size: RECIP_ENGINE3_TIT_DEGR. Description: 492
        private Offset<object> offset1074 = new Offset<object>(0x36D8); // Size: RECIP_ENGINE3_CHT_DEGR. Description: 493
        private Offset<object> offset1075 = new Offset<object>(0x3704); // Size: RECIP_ENGINE3_TANKS_USED. Description: 501
        private Offset<object> offset1076 = new Offset<object>(0x3738); // Size: RECIP_ENGINE2_CARB_HEAT_POS. Description: 435
        private Offset<object> offset1077 = new Offset<object>(0x3740); // Size: RECIP_ENGINE2_ALTERNATE_AIR_POS. Description: 436
        private Offset<object> offset1078 = new Offset<object>(0x3748); // Size: RECIP_ENGINE2_COOLANT_RESERVOIR_PCT. Description: 437
        private Offset<object> offset1079 = new Offset<object>(0x3770); // Size: RECIP_ENGINE2_STARTER_TORQUE. Description: 444
        private Offset<object> offset1080 = new Offset<object>(0x3778); // Size: RECIP_ENGINE2_TURBOCHARGER_FAILED. Description: 446
        private Offset<object> offset1081 = new Offset<object>(0x377C); // Size: RECIP_ENGINE2_EMERGENCY_BOOST_ACTIVE. Description: 447
        private Offset<object> offset1082 = new Offset<object>(0x3780); // Size: RECIP_ENGINE2_EMERGENCY_BOOST_ELAPSED_TIME. Description: 448
        private Offset<object> offset1083 = new Offset<object>(0x3788); // Size: RECIP_ENGINE2_WASTEGATE_POS. Description: 449
        private Offset<object> offset1084 = new Offset<object>(0x3790); // Size: RECIP_ENGINE2_TIT_DEGR. Description: 453
        private Offset<object> offset1085 = new Offset<object>(0x3798); // Size: RECIP_ENGINE2_CHT_DEGR. Description: 454
        private Offset<object> offset1086 = new Offset<object>(0x37C4); // Size: RECIP_ENGINE2_TANKS_USED. Description: 462
        private Offset<object> offset1087 = new Offset<object>(0x37F8); // Size: RECIP_ENGINE1_CARB_HEAT_POS. Description: 396
        private Offset<object> offset1088 = new Offset<object>(0x3800); // Size: RECIP_ENGINE1_ALTERNATE_AIR_POS. Description: 397
        private Offset<object> offset1089 = new Offset<object>(0x3808); // Size: RECIP_ENGINE1_COOLANT_RESERVOIR_PCT. Description: 398
        private Offset<object> offset1090 = new Offset<object>(0x3830); // Size: RECIP_ENGINE1_STARTER_TORQUE. Description: 405
        private Offset<object> offset1091 = new Offset<object>(0x3838); // Size: RECIP_ENGINE1_TURBOCHARGER_FAILED. Description: 407
        private Offset<object> offset1092 = new Offset<object>(0x383C); // Size: RECIP_ENGINE1_EMERGENCY_BOOST_ACTIVE. Description: 408
        private Offset<object> offset1093 = new Offset<object>(0x3840); // Size: RECIP_ENGINE1_EMERGENCY_BOOST_ELAPSED_TIME. Description: 409
        private Offset<object> offset1094 = new Offset<object>(0x3848); // Size: RECIP_ENGINE1_WASTEGATE_POS. Description: 410
        private Offset<object> offset1095 = new Offset<object>(0x3850); // Size: RECIP_ENGINE1_TIT_DEGR. Description: 414
        private Offset<object> offset1096 = new Offset<object>(0x3858); // Size: RECIP_ENGINE1_CHT_DEGR. Description: 415
        private Offset<object> offset1097 = new Offset<object>(0x3870); // Size: ENGINE_PRIMER. Description: 361
        private Offset<object> offset1098 = new Offset<object>(0x3884); // Size: RECIP_ENGINE1_TANKS_USED. Description: 423
        private Offset<object> offset1099 = new Offset<object>(0x38A0); // Size: GENERAL_ENGINE4_FAILURE. Description: 594
        private Offset<object> offset1100 = new Offset<object>(0x38A4); // Size: RECIP_ENGINE4_COMBUSTION. Description: 523
        private Offset<object> offset1101 = new Offset<object>(0x38C0); // Size: RECIP_ENGINE4_STARTER. Description: 518
        private Offset<object> offset1102 = new Offset<object>(0x38C0); // Size: GENERAL_ENGINE4_STARTER. Description: 593
        private Offset<object> offset1103 = new Offset<object>(0x3928); // Size: RECIP_ENGINE4_OIL_LEAK_PCT. Description: 536
        private Offset<object> offset1104 = new Offset<object>(0x3940); // Size: RECIP_ENGINE4_DAMAGE_PERCENT. Description: 545
        private Offset<object> offset1105 = new Offset<object>(0x3948); // Size: RECIP_ENGINE4_COMBUSTION_SOUND_PCT. Description: 543
        private Offset<object> offset1106 = new Offset<object>(0x3960); // Size: GENERAL_ENGINE3_FAILURE. Description: 584
        private Offset<object> offset1107 = new Offset<object>(0x3964); // Size: RECIP_ENGINE3_COMBUSTION. Description: 484
        private Offset<object> offset1108 = new Offset<object>(0x3980); // Size: RECIP_ENGINE3_STARTER. Description: 479
        private Offset<object> offset1109 = new Offset<object>(0x3980); // Size: GENERAL_ENGINE3_STARTER. Description: 583
        private Offset<object> offset1110 = new Offset<object>(0x39E8); // Size: RECIP_ENGINE3_OIL_LEAK_PCT. Description: 497
        private Offset<object> offset1111 = new Offset<object>(0x3A00); // Size: RECIP_ENGINE3_DAMAGE_PERCENT. Description: 506
        private Offset<object> offset1112 = new Offset<object>(0x3A08); // Size: RECIP_ENGINE3_COMBUSTION_SOUND_PCT. Description: 504
        private Offset<object> offset1113 = new Offset<object>(0x3A20); // Size: GENERAL_ENGINE2_FAILURE. Description: 574
        private Offset<object> offset1114 = new Offset<object>(0x3A24); // Size: RECIP_ENGINE2_COMBUSTION. Description: 445
        private Offset<object> offset1115 = new Offset<object>(0x3A40); // Size: RECIP_ENGINE2_STARTER. Description: 440
        private Offset<object> offset1116 = new Offset<object>(0x3A40); // Size: GENERAL_ENGINE2_STARTER. Description: 573
        private Offset<object> offset1117 = new Offset<object>(0x3AA8); // Size: RECIP_ENGINE2_OIL_LEAK_PCT. Description: 458
        private Offset<object> offset1118 = new Offset<object>(0x3AC0); // Size: RECIP_ENGINE2_DAMAGE_PERCENT. Description: 467
        private Offset<object> offset1119 = new Offset<object>(0x3AC8); // Size: RECIP_ENGINE2_COMBUSTION_SOUND_PCT. Description: 465
        private Offset<object> offset1120 = new Offset<object>(0x3AE0); // Size: GENERAL_ENGINE1_FAILURE. Description: 564
        private Offset<object> offset1121 = new Offset<object>(0x3AE4); // Size: RECIP_ENGINE1_COMBUSTION. Description: 406
        private Offset<object> offset1122 = new Offset<object>(0x3B00); // Size: RECIP_ENGINE1_STARTER. Description: 401
        private Offset<object> offset1123 = new Offset<object>(0x3B00); // Size: GENERAL_ENGINE1_STARTER. Description: 563
        private Offset<object> offset1124 = new Offset<object>(0x3B68); // Size: RECIP_ENGINE1_OIL_LEAK_PCT. Description: 419
        private Offset<object> offset1125 = new Offset<object>(0x3B80); // Size: RECIP_ENGINE1_DAMAGE_PERCENT. Description: 428
        private Offset<object> offset1126 = new Offset<object>(0x3B88); // Size: RECIP_ENGINE1_COMBUSTION_SOUND_PCT. Description: 426


        public void addOffsetsToList()
        {
            OffsetsList.Add(offset1);
            OffsetsList.Add(offset2);
            OffsetsList.Add(offset3);
            OffsetsList.Add(offset4);
            OffsetsList.Add(offset5);
            OffsetsList.Add(offset6);
            OffsetsList.Add(offset7);
            OffsetsList.Add(offset8);
            OffsetsList.Add(offset9);
            OffsetsList.Add(offset10);
            OffsetsList.Add(offset11);
            OffsetsList.Add(offset12);
            OffsetsList.Add(offset13);
            OffsetsList.Add(offset14);
            OffsetsList.Add(offset15);
            OffsetsList.Add(offset16);
            OffsetsList.Add(offset17);
            OffsetsList.Add(offset18);
            OffsetsList.Add(offset19);
            OffsetsList.Add(offset20);
            OffsetsList.Add(offset21);
            OffsetsList.Add(offset22);
            OffsetsList.Add(offset23);
            OffsetsList.Add(offset24);
            OffsetsList.Add(offset25);
            OffsetsList.Add(offset26);
            OffsetsList.Add(offset27);
            OffsetsList.Add(offset28);
            OffsetsList.Add(offset29);
            OffsetsList.Add(offset30);
            OffsetsList.Add(offset31);
            OffsetsList.Add(offset32);
            OffsetsList.Add(offset33);
            OffsetsList.Add(offset34);
            OffsetsList.Add(offset35);
            OffsetsList.Add(offset36);
            OffsetsList.Add(offset37);
            OffsetsList.Add(offset38);
            OffsetsList.Add(offset39);
            OffsetsList.Add(offset40);
            OffsetsList.Add(offset41);
            OffsetsList.Add(offset42);
            OffsetsList.Add(offset43);
            OffsetsList.Add(offset44);
            OffsetsList.Add(offset45);
            OffsetsList.Add(offset46);
            OffsetsList.Add(offset47);
            OffsetsList.Add(offset48);
            OffsetsList.Add(offset49);
            OffsetsList.Add(offset50);
            OffsetsList.Add(offset51);
            OffsetsList.Add(offset52);
            OffsetsList.Add(offset53);
            OffsetsList.Add(offset54);
            OffsetsList.Add(offset55);
            OffsetsList.Add(offset56);
            OffsetsList.Add(offset57);
            OffsetsList.Add(offset58);
            OffsetsList.Add(offset59);
            OffsetsList.Add(offset60);
            OffsetsList.Add(offset61);
            OffsetsList.Add(offset62);
            OffsetsList.Add(offset63);
            OffsetsList.Add(offset64);
            OffsetsList.Add(offset65);
            OffsetsList.Add(offset66);
            OffsetsList.Add(offset67);
            OffsetsList.Add(offset68);
            OffsetsList.Add(offset69);
            OffsetsList.Add(offset70);
            OffsetsList.Add(offset71);
            OffsetsList.Add(offset72);
            OffsetsList.Add(offset73);
            OffsetsList.Add(offset74);
            OffsetsList.Add(offset75);
            OffsetsList.Add(offset76);
            OffsetsList.Add(offset77);
            OffsetsList.Add(offset78);
            OffsetsList.Add(offset79);
            OffsetsList.Add(offset80);
            OffsetsList.Add(offset81);
            OffsetsList.Add(offset82);
            OffsetsList.Add(offset83);
            OffsetsList.Add(offset84);
            OffsetsList.Add(offset85);
            OffsetsList.Add(offset86);
            OffsetsList.Add(offset87);
            OffsetsList.Add(offset88);
            OffsetsList.Add(offset89);
            OffsetsList.Add(offset90);
            OffsetsList.Add(offset91);
            OffsetsList.Add(offset92);
            OffsetsList.Add(offset93);
            OffsetsList.Add(offset94);
            OffsetsList.Add(offset95);
            OffsetsList.Add(offset96);
            OffsetsList.Add(offset97);
            OffsetsList.Add(offset98);
            OffsetsList.Add(offset99);
            OffsetsList.Add(offset100);
            OffsetsList.Add(offset101);
            OffsetsList.Add(offset102);
            OffsetsList.Add(offset103);
            OffsetsList.Add(offset104);
            OffsetsList.Add(offset105);
            OffsetsList.Add(offset106);
            OffsetsList.Add(offset107);
            OffsetsList.Add(offset108);
            OffsetsList.Add(offset109);
            OffsetsList.Add(offset110);
            OffsetsList.Add(offset111);
            OffsetsList.Add(offset112);
            OffsetsList.Add(offset113);
            OffsetsList.Add(offset114);
            OffsetsList.Add(offset115);
            OffsetsList.Add(offset116);
            OffsetsList.Add(offset117);
            OffsetsList.Add(offset118);
            OffsetsList.Add(offset119);
            OffsetsList.Add(offset120);
            OffsetsList.Add(offset121);
            OffsetsList.Add(offset122);
            OffsetsList.Add(offset123);
            OffsetsList.Add(offset124);
            OffsetsList.Add(offset125);
            OffsetsList.Add(offset126);
            OffsetsList.Add(offset127);
            OffsetsList.Add(offset128);
            OffsetsList.Add(offset129);
            OffsetsList.Add(offset130);
            OffsetsList.Add(offset131);
            OffsetsList.Add(offset132);
            OffsetsList.Add(offset133);
            OffsetsList.Add(offset134);
            OffsetsList.Add(offset135);
            OffsetsList.Add(offset136);
            OffsetsList.Add(offset137);
            OffsetsList.Add(offset138);
            OffsetsList.Add(offset139);
            OffsetsList.Add(offset140);
            OffsetsList.Add(offset141);
            OffsetsList.Add(offset142);
            OffsetsList.Add(offset143);
            OffsetsList.Add(offset144);
            OffsetsList.Add(offset145);
            OffsetsList.Add(offset146);
            OffsetsList.Add(offset147);
            OffsetsList.Add(offset148);
            OffsetsList.Add(offset149);
            OffsetsList.Add(offset150);
            OffsetsList.Add(offset151);
            OffsetsList.Add(offset152);
            OffsetsList.Add(offset153);
            OffsetsList.Add(offset154);
            OffsetsList.Add(offset155);
            OffsetsList.Add(offset156);
            OffsetsList.Add(offset157);
            OffsetsList.Add(offset158);
            OffsetsList.Add(offset159);
            OffsetsList.Add(offset160);
            OffsetsList.Add(offset161);
            OffsetsList.Add(offset162);
            OffsetsList.Add(offset163);
            OffsetsList.Add(offset164);
            OffsetsList.Add(offset165);
            OffsetsList.Add(offset166);
            OffsetsList.Add(offset167);
            OffsetsList.Add(offset168);
            OffsetsList.Add(offset169);
            OffsetsList.Add(offset170);
            OffsetsList.Add(offset171);
            OffsetsList.Add(offset172);
            OffsetsList.Add(offset173);
            OffsetsList.Add(offset174);
            OffsetsList.Add(offset175);
            OffsetsList.Add(offset176);
            OffsetsList.Add(offset177);
            OffsetsList.Add(offset178);
            OffsetsList.Add(offset179);
            OffsetsList.Add(offset180);
            OffsetsList.Add(offset181);
            OffsetsList.Add(offset182);
            OffsetsList.Add(offset183);
            OffsetsList.Add(offset184);
            OffsetsList.Add(offset185);
            OffsetsList.Add(offset186);
            OffsetsList.Add(offset187);
            OffsetsList.Add(offset188);
            OffsetsList.Add(offset189);
            OffsetsList.Add(offset190);
            OffsetsList.Add(offset191);
            OffsetsList.Add(offset192);
            OffsetsList.Add(offset193);
            OffsetsList.Add(offset194);
            OffsetsList.Add(offset195);
            OffsetsList.Add(offset196);
            OffsetsList.Add(offset197);
            OffsetsList.Add(offset198);
            OffsetsList.Add(offset199);
            OffsetsList.Add(offset200);
            OffsetsList.Add(offset201);
            OffsetsList.Add(offset202);
            OffsetsList.Add(offset203);
            OffsetsList.Add(offset204);
            OffsetsList.Add(offset205);
            OffsetsList.Add(offset206);
            OffsetsList.Add(offset207);
            OffsetsList.Add(offset208);
            OffsetsList.Add(offset209);
            OffsetsList.Add(offset210);
            OffsetsList.Add(offset211);
            OffsetsList.Add(offset212);
            OffsetsList.Add(offset213);
            OffsetsList.Add(offset214);
            OffsetsList.Add(offset215);
            OffsetsList.Add(offset216);
            OffsetsList.Add(offset217);
            OffsetsList.Add(offset218);
            OffsetsList.Add(offset219);
            OffsetsList.Add(offset220);
            OffsetsList.Add(offset221);
            OffsetsList.Add(offset222);
            OffsetsList.Add(offset223);
            OffsetsList.Add(offset224);
            OffsetsList.Add(offset225);
            OffsetsList.Add(offset226);
            OffsetsList.Add(offset227);
            OffsetsList.Add(offset228);
            OffsetsList.Add(offset229);
            OffsetsList.Add(offset230);
            OffsetsList.Add(offset231);
            OffsetsList.Add(offset232);
            OffsetsList.Add(offset233);
            OffsetsList.Add(offset234);
            OffsetsList.Add(offset235);
            OffsetsList.Add(offset236);
            OffsetsList.Add(offset237);
            OffsetsList.Add(offset238);
            OffsetsList.Add(offset239);
            OffsetsList.Add(offset240);
            OffsetsList.Add(offset241);
            OffsetsList.Add(offset242);
            OffsetsList.Add(offset243);
            OffsetsList.Add(offset244);
            OffsetsList.Add(offset245);
            OffsetsList.Add(offset246);
            OffsetsList.Add(offset247);
            OffsetsList.Add(offset248);
            OffsetsList.Add(offset249);
            OffsetsList.Add(offset250);
            OffsetsList.Add(offset251);
            OffsetsList.Add(offset252);
            OffsetsList.Add(offset253);
            OffsetsList.Add(offset254);
            OffsetsList.Add(offset255);
            OffsetsList.Add(offset256);
            OffsetsList.Add(offset257);
            OffsetsList.Add(offset258);
            OffsetsList.Add(offset259);
            OffsetsList.Add(offset260);
            OffsetsList.Add(offset261);
            OffsetsList.Add(offset262);
            OffsetsList.Add(offset263);
            OffsetsList.Add(offset264);
            OffsetsList.Add(offset265);
            OffsetsList.Add(offset266);
            OffsetsList.Add(offset267);
            OffsetsList.Add(offset268);
            OffsetsList.Add(offset269);
            OffsetsList.Add(offset270);
            OffsetsList.Add(offset271);
            OffsetsList.Add(offset272);
            OffsetsList.Add(offset273);
            OffsetsList.Add(offset274);
            OffsetsList.Add(offset275);
            OffsetsList.Add(offset276);
            OffsetsList.Add(offset277);
            OffsetsList.Add(offset278);
            OffsetsList.Add(offset279);
            OffsetsList.Add(offset280);
            OffsetsList.Add(offset281);
            OffsetsList.Add(offset282);
            OffsetsList.Add(offset283);
            OffsetsList.Add(offset284);
            OffsetsList.Add(offset285);
            OffsetsList.Add(offset286);
            OffsetsList.Add(offset287);
            OffsetsList.Add(offset288);
            OffsetsList.Add(offset289);
            OffsetsList.Add(offset290);
            OffsetsList.Add(offset291);
            OffsetsList.Add(offset292);
            OffsetsList.Add(offset293);
            OffsetsList.Add(offset294);
            OffsetsList.Add(offset295);
            OffsetsList.Add(offset296);
            OffsetsList.Add(offset297);
            OffsetsList.Add(offset298);
            OffsetsList.Add(offset299);
            OffsetsList.Add(offset300);
            OffsetsList.Add(offset301);
            OffsetsList.Add(offset302);
            OffsetsList.Add(offset303);
            OffsetsList.Add(offset304);
            OffsetsList.Add(offset305);
            OffsetsList.Add(offset306);
            OffsetsList.Add(offset307);
            OffsetsList.Add(offset308);
            OffsetsList.Add(offset309);
            OffsetsList.Add(offset310);
            OffsetsList.Add(offset311);
            OffsetsList.Add(offset312);
            OffsetsList.Add(offset313);
            OffsetsList.Add(offset314);
            OffsetsList.Add(offset315);
            OffsetsList.Add(offset316);
            OffsetsList.Add(offset317);
            OffsetsList.Add(offset318);
            OffsetsList.Add(offset319);
            OffsetsList.Add(offset320);
            OffsetsList.Add(offset321);
            OffsetsList.Add(offset322);
            OffsetsList.Add(offset323);
            OffsetsList.Add(offset324);
            OffsetsList.Add(offset325);
            OffsetsList.Add(offset326);
            OffsetsList.Add(offset327);
            OffsetsList.Add(offset328);
            OffsetsList.Add(offset329);
            OffsetsList.Add(offset330);
            OffsetsList.Add(offset331);
            OffsetsList.Add(offset332);
            OffsetsList.Add(offset333);
            OffsetsList.Add(offset334);
            OffsetsList.Add(offset335);
            OffsetsList.Add(offset336);
            OffsetsList.Add(offset337);
            OffsetsList.Add(offset338);
            OffsetsList.Add(offset339);
            OffsetsList.Add(offset340);
            OffsetsList.Add(offset341);
            OffsetsList.Add(offset342);
            OffsetsList.Add(offset343);
            OffsetsList.Add(offset344);
            OffsetsList.Add(offset345);
            OffsetsList.Add(offset346);
            OffsetsList.Add(offset347);
            OffsetsList.Add(offset348);
            OffsetsList.Add(offset349);
            OffsetsList.Add(offset350);
            OffsetsList.Add(offset351);
            OffsetsList.Add(offset352);
            OffsetsList.Add(offset353);
            OffsetsList.Add(offset354);
            OffsetsList.Add(offset355);
            OffsetsList.Add(offset356);
            OffsetsList.Add(offset357);
            OffsetsList.Add(offset358);
            OffsetsList.Add(offset359);
            OffsetsList.Add(offset360);
            OffsetsList.Add(offset361);
            OffsetsList.Add(offset362);
            OffsetsList.Add(offset363);
            OffsetsList.Add(offset364);
            OffsetsList.Add(offset365);
            OffsetsList.Add(offset366);
            OffsetsList.Add(offset367);
            OffsetsList.Add(offset368);
            OffsetsList.Add(offset369);
            OffsetsList.Add(offset370);
            OffsetsList.Add(offset371);
            OffsetsList.Add(offset372);
            OffsetsList.Add(offset373);
            OffsetsList.Add(offset374);
            OffsetsList.Add(offset375);
            OffsetsList.Add(offset376);
            OffsetsList.Add(offset377);
            OffsetsList.Add(offset378);
            OffsetsList.Add(offset379);
            OffsetsList.Add(offset380);
            OffsetsList.Add(offset381);
            OffsetsList.Add(offset382);
            OffsetsList.Add(offset383);
            OffsetsList.Add(offset384);
            OffsetsList.Add(offset385);
            OffsetsList.Add(offset386);
            OffsetsList.Add(offset387);
            OffsetsList.Add(offset388);
            OffsetsList.Add(offset389);
            OffsetsList.Add(offset390);
            OffsetsList.Add(offset391);
            OffsetsList.Add(offset392);
            OffsetsList.Add(offset393);
            OffsetsList.Add(offset394);
            OffsetsList.Add(offset395);
            OffsetsList.Add(offset396);
            OffsetsList.Add(offset397);
            OffsetsList.Add(offset398);
            OffsetsList.Add(offset399);
            OffsetsList.Add(offset400);
            OffsetsList.Add(offset401);
            OffsetsList.Add(offset402);
            OffsetsList.Add(offset403);
            OffsetsList.Add(offset404);
            OffsetsList.Add(offset405);
            OffsetsList.Add(offset406);
            OffsetsList.Add(offset407);
            OffsetsList.Add(offset408);
            OffsetsList.Add(offset409);
            OffsetsList.Add(offset410);
            OffsetsList.Add(offset411);
            OffsetsList.Add(offset412);
            OffsetsList.Add(offset413);
            OffsetsList.Add(offset414);
            OffsetsList.Add(offset415);
            OffsetsList.Add(offset416);
            OffsetsList.Add(offset417);
            OffsetsList.Add(offset418);
            OffsetsList.Add(offset419);
            OffsetsList.Add(offset420);
            OffsetsList.Add(offset421);
            OffsetsList.Add(offset422);
            OffsetsList.Add(offset423);
            OffsetsList.Add(offset424);
            OffsetsList.Add(offset425);
            OffsetsList.Add(offset426);
            OffsetsList.Add(offset427);
            OffsetsList.Add(offset428);
            OffsetsList.Add(offset429);
            OffsetsList.Add(offset430);
            OffsetsList.Add(offset431);
            OffsetsList.Add(offset432);
            OffsetsList.Add(offset433);
            OffsetsList.Add(offset434);
            OffsetsList.Add(offset435);
            OffsetsList.Add(offset436);
            OffsetsList.Add(offset437);
            OffsetsList.Add(offset438);
            OffsetsList.Add(offset439);
            OffsetsList.Add(offset440);
            OffsetsList.Add(offset441);
            OffsetsList.Add(offset442);
            OffsetsList.Add(offset443);
            OffsetsList.Add(offset444);
            OffsetsList.Add(offset445);
            OffsetsList.Add(offset446);
            OffsetsList.Add(offset447);
            OffsetsList.Add(offset448);
            OffsetsList.Add(offset449);
            OffsetsList.Add(offset450);
            OffsetsList.Add(offset451);
            OffsetsList.Add(offset452);
            OffsetsList.Add(offset453);
            OffsetsList.Add(offset454);
            OffsetsList.Add(offset455);
            OffsetsList.Add(offset456);
            OffsetsList.Add(offset457);
            OffsetsList.Add(offset458);
            OffsetsList.Add(offset459);
            OffsetsList.Add(offset460);
            OffsetsList.Add(offset461);
            OffsetsList.Add(offset462);
            OffsetsList.Add(offset463);
            OffsetsList.Add(offset464);
            OffsetsList.Add(offset465);
            OffsetsList.Add(offset466);
            OffsetsList.Add(offset467);
            OffsetsList.Add(offset468);
            OffsetsList.Add(offset469);
            OffsetsList.Add(offset470);
            OffsetsList.Add(offset471);
            OffsetsList.Add(offset472);
            OffsetsList.Add(offset473);
            OffsetsList.Add(offset474);
            OffsetsList.Add(offset475);
            OffsetsList.Add(offset476);
            OffsetsList.Add(offset477);
            OffsetsList.Add(offset478);
            OffsetsList.Add(offset479);
            OffsetsList.Add(offset480);
            OffsetsList.Add(offset481);
            OffsetsList.Add(offset482);
            OffsetsList.Add(offset483);
            OffsetsList.Add(offset484);
            OffsetsList.Add(offset485);
            OffsetsList.Add(offset486);
            OffsetsList.Add(offset487);
            OffsetsList.Add(offset488);
            OffsetsList.Add(offset489);
            OffsetsList.Add(offset490);
            OffsetsList.Add(offset491);
            OffsetsList.Add(offset492);
            OffsetsList.Add(offset493);
            OffsetsList.Add(offset494);
            OffsetsList.Add(offset495);
            OffsetsList.Add(offset496);
            OffsetsList.Add(offset497);
            OffsetsList.Add(offset498);
            OffsetsList.Add(offset499);
            OffsetsList.Add(offset500);
            OffsetsList.Add(offset501);
            OffsetsList.Add(offset502);
            OffsetsList.Add(offset503);
            OffsetsList.Add(offset504);
            OffsetsList.Add(offset505);
            OffsetsList.Add(offset506);
            OffsetsList.Add(offset507);
            OffsetsList.Add(offset508);
            OffsetsList.Add(offset509);
            OffsetsList.Add(offset510);
            OffsetsList.Add(offset511);
            OffsetsList.Add(offset512);
            OffsetsList.Add(offset513);
            OffsetsList.Add(offset514);
            OffsetsList.Add(offset515);
            OffsetsList.Add(offset516);
            OffsetsList.Add(offset517);
            OffsetsList.Add(offset518);
            OffsetsList.Add(offset519);
            OffsetsList.Add(offset520);
            OffsetsList.Add(offset521);
            OffsetsList.Add(offset522);
            OffsetsList.Add(offset523);
            OffsetsList.Add(offset524);
            OffsetsList.Add(offset525);
            OffsetsList.Add(offset526);
            OffsetsList.Add(offset527);
            OffsetsList.Add(offset528);
            OffsetsList.Add(offset529);
            OffsetsList.Add(offset530);
            OffsetsList.Add(offset531);
            OffsetsList.Add(offset532);
            OffsetsList.Add(offset533);
            OffsetsList.Add(offset534);
            OffsetsList.Add(offset535);
            OffsetsList.Add(offset536);
            OffsetsList.Add(offset537);
            OffsetsList.Add(offset538);
            OffsetsList.Add(offset539);
            OffsetsList.Add(offset540);
            OffsetsList.Add(offset541);
            OffsetsList.Add(offset542);
            OffsetsList.Add(offset543);
            OffsetsList.Add(offset544);
            OffsetsList.Add(offset545);
            OffsetsList.Add(offset546);
            OffsetsList.Add(offset547);
            OffsetsList.Add(offset548);
            OffsetsList.Add(offset549);
            OffsetsList.Add(offset550);
            OffsetsList.Add(offset551);
            OffsetsList.Add(offset552);
            OffsetsList.Add(offset553);
            OffsetsList.Add(offset554);
            OffsetsList.Add(offset555);
            OffsetsList.Add(offset556);
            OffsetsList.Add(offset557);
            OffsetsList.Add(offset558);
            OffsetsList.Add(offset559);
            OffsetsList.Add(offset560);
            OffsetsList.Add(offset561);
            OffsetsList.Add(offset562);
            OffsetsList.Add(offset563);
            OffsetsList.Add(offset564);
            OffsetsList.Add(offset565);
            OffsetsList.Add(offset566);
            OffsetsList.Add(offset567);
            OffsetsList.Add(offset568);
            OffsetsList.Add(offset569);
            OffsetsList.Add(offset570);
            OffsetsList.Add(offset571);
            OffsetsList.Add(offset572);
            OffsetsList.Add(offset573);
            OffsetsList.Add(offset574);
            OffsetsList.Add(offset575);
            OffsetsList.Add(offset576);
            OffsetsList.Add(offset577);
            OffsetsList.Add(offset578);
            OffsetsList.Add(offset579);
            OffsetsList.Add(offset580);
            OffsetsList.Add(offset581);
            OffsetsList.Add(offset582);
            OffsetsList.Add(offset583);
            OffsetsList.Add(offset584);
            OffsetsList.Add(offset585);
            OffsetsList.Add(offset586);
            OffsetsList.Add(offset587);
            OffsetsList.Add(offset588);
            OffsetsList.Add(offset589);
            OffsetsList.Add(offset590);
            OffsetsList.Add(offset591);
            OffsetsList.Add(offset592);
            OffsetsList.Add(offset593);
            OffsetsList.Add(offset594);
            OffsetsList.Add(offset595);
            OffsetsList.Add(offset596);
            OffsetsList.Add(offset597);
            OffsetsList.Add(offset598);
            OffsetsList.Add(offset599);
            OffsetsList.Add(offset600);
            OffsetsList.Add(offset601);
            OffsetsList.Add(offset602);
            OffsetsList.Add(offset603);
            OffsetsList.Add(offset604);
            OffsetsList.Add(offset605);
            OffsetsList.Add(offset606);
            OffsetsList.Add(offset607);
            OffsetsList.Add(offset608);
            OffsetsList.Add(offset609);
            OffsetsList.Add(offset610);
            OffsetsList.Add(offset611);
            OffsetsList.Add(offset612);
            OffsetsList.Add(offset613);
            OffsetsList.Add(offset614);
            OffsetsList.Add(offset615);
            OffsetsList.Add(offset616);
            OffsetsList.Add(offset617);
            OffsetsList.Add(offset618);
            OffsetsList.Add(offset619);
            OffsetsList.Add(offset620);
            OffsetsList.Add(offset621);
            OffsetsList.Add(offset622);
            OffsetsList.Add(offset623);
            OffsetsList.Add(offset624);
            OffsetsList.Add(offset625);
            OffsetsList.Add(offset626);
            OffsetsList.Add(offset627);
            OffsetsList.Add(offset628);
            OffsetsList.Add(offset629);
            OffsetsList.Add(offset630);
            OffsetsList.Add(offset631);
            OffsetsList.Add(offset632);
            OffsetsList.Add(offset633);
            OffsetsList.Add(offset634);
            OffsetsList.Add(offset635);
            OffsetsList.Add(offset636);
            OffsetsList.Add(offset637);
            OffsetsList.Add(offset638);
            OffsetsList.Add(offset639);
            OffsetsList.Add(offset640);
            OffsetsList.Add(offset641);
            OffsetsList.Add(offset642);
            OffsetsList.Add(offset643);
            OffsetsList.Add(offset644);
            OffsetsList.Add(offset645);
            OffsetsList.Add(offset646);
            OffsetsList.Add(offset647);
            OffsetsList.Add(offset648);
            OffsetsList.Add(offset649);
            OffsetsList.Add(offset650);
            OffsetsList.Add(offset651);
            OffsetsList.Add(offset652);
            OffsetsList.Add(offset653);
            OffsetsList.Add(offset654);
            OffsetsList.Add(offset655);
            OffsetsList.Add(offset656);
            OffsetsList.Add(offset657);
            OffsetsList.Add(offset658);
            OffsetsList.Add(offset659);
            OffsetsList.Add(offset660);
            OffsetsList.Add(offset661);
            OffsetsList.Add(offset662);
            OffsetsList.Add(offset663);
            OffsetsList.Add(offset664);
            OffsetsList.Add(offset665);
            OffsetsList.Add(offset666);
            OffsetsList.Add(offset667);
            OffsetsList.Add(offset668);
            OffsetsList.Add(offset669);
            OffsetsList.Add(offset670);
            OffsetsList.Add(offset671);
            OffsetsList.Add(offset672);
            OffsetsList.Add(offset673);
            OffsetsList.Add(offset674);
            OffsetsList.Add(offset675);
            OffsetsList.Add(offset676);
            OffsetsList.Add(offset677);
            OffsetsList.Add(offset678);
            OffsetsList.Add(offset679);
            OffsetsList.Add(offset680);
            OffsetsList.Add(offset681);
            OffsetsList.Add(offset682);
            OffsetsList.Add(offset683);
            OffsetsList.Add(offset684);
            OffsetsList.Add(offset685);
            OffsetsList.Add(offset686);
            OffsetsList.Add(offset687);
            OffsetsList.Add(offset688);
            OffsetsList.Add(offset689);
            OffsetsList.Add(offset690);
            OffsetsList.Add(offset691);
            OffsetsList.Add(offset692);
            OffsetsList.Add(offset693);
            OffsetsList.Add(offset694);
            OffsetsList.Add(offset695);
            OffsetsList.Add(offset696);
            OffsetsList.Add(offset697);
            OffsetsList.Add(offset698);
            OffsetsList.Add(offset699);
            OffsetsList.Add(offset700);
            OffsetsList.Add(offset701);
            OffsetsList.Add(offset702);
            OffsetsList.Add(offset703);
            OffsetsList.Add(offset704);
            OffsetsList.Add(offset705);
            OffsetsList.Add(offset706);
            OffsetsList.Add(offset707);
            OffsetsList.Add(offset708);
            OffsetsList.Add(offset709);
            OffsetsList.Add(offset710);
            OffsetsList.Add(offset711);
            OffsetsList.Add(offset712);
            OffsetsList.Add(offset713);
            OffsetsList.Add(offset714);
            OffsetsList.Add(offset715);
            OffsetsList.Add(offset716);
            OffsetsList.Add(offset717);
            OffsetsList.Add(offset718);
            OffsetsList.Add(offset719);
            OffsetsList.Add(offset720);
            OffsetsList.Add(offset721);
            OffsetsList.Add(offset722);
            OffsetsList.Add(offset723);
            OffsetsList.Add(offset724);
            OffsetsList.Add(offset725);
            OffsetsList.Add(offset726);
            OffsetsList.Add(offset727);
            OffsetsList.Add(offset728);
            OffsetsList.Add(offset729);
            OffsetsList.Add(offset730);
            OffsetsList.Add(offset731);
            OffsetsList.Add(offset732);
            OffsetsList.Add(offset733);
            OffsetsList.Add(offset734);
            OffsetsList.Add(offset735);
            OffsetsList.Add(offset736);
            OffsetsList.Add(offset737);
            OffsetsList.Add(offset738);
            OffsetsList.Add(offset739);
            OffsetsList.Add(offset740);
            OffsetsList.Add(offset741);
            OffsetsList.Add(offset742);
            OffsetsList.Add(offset743);
            OffsetsList.Add(offset744);
            OffsetsList.Add(offset745);
            OffsetsList.Add(offset746);
            OffsetsList.Add(offset747);
            OffsetsList.Add(offset748);
            OffsetsList.Add(offset749);
            OffsetsList.Add(offset750);
            OffsetsList.Add(offset751);
            OffsetsList.Add(offset752);
            OffsetsList.Add(offset753);
            OffsetsList.Add(offset754);
            OffsetsList.Add(offset755);
            OffsetsList.Add(offset756);
            OffsetsList.Add(offset757);
            OffsetsList.Add(offset758);
            OffsetsList.Add(offset759);
            OffsetsList.Add(offset760);
            OffsetsList.Add(offset761);
            OffsetsList.Add(offset762);
            OffsetsList.Add(offset763);
            OffsetsList.Add(offset764);
            OffsetsList.Add(offset765);
            OffsetsList.Add(offset766);
            OffsetsList.Add(offset767);
            OffsetsList.Add(offset768);
            OffsetsList.Add(offset769);
            OffsetsList.Add(offset770);
            OffsetsList.Add(offset771);
            OffsetsList.Add(offset772);
            OffsetsList.Add(offset773);
            OffsetsList.Add(offset774);
            OffsetsList.Add(offset775);
            OffsetsList.Add(offset776);
            OffsetsList.Add(offset777);
            OffsetsList.Add(offset778);
            OffsetsList.Add(offset779);
            OffsetsList.Add(offset780);
            OffsetsList.Add(offset781);
            OffsetsList.Add(offset782);
            OffsetsList.Add(offset783);
            OffsetsList.Add(offset784);
            OffsetsList.Add(offset785);
            OffsetsList.Add(offset786);
            OffsetsList.Add(offset787);
            OffsetsList.Add(offset788);
            OffsetsList.Add(offset789);
            OffsetsList.Add(offset790);
            OffsetsList.Add(offset791);
            OffsetsList.Add(offset792);
            OffsetsList.Add(offset793);
            OffsetsList.Add(offset794);
            OffsetsList.Add(offset795);
            OffsetsList.Add(offset796);
            OffsetsList.Add(offset797);
            OffsetsList.Add(offset798);
            OffsetsList.Add(offset799);
            OffsetsList.Add(offset800);
            OffsetsList.Add(offset801);
            OffsetsList.Add(offset802);
            OffsetsList.Add(offset803);
            OffsetsList.Add(offset804);
            OffsetsList.Add(offset805);
            OffsetsList.Add(offset806);
            OffsetsList.Add(offset807);
            OffsetsList.Add(offset808);
            OffsetsList.Add(offset809);
            OffsetsList.Add(offset810);
            OffsetsList.Add(offset811);
            OffsetsList.Add(offset812);
            OffsetsList.Add(offset813);
            OffsetsList.Add(offset814);
            OffsetsList.Add(offset815);
            OffsetsList.Add(offset816);
            OffsetsList.Add(offset817);
            OffsetsList.Add(offset818);
            OffsetsList.Add(offset819);
            OffsetsList.Add(offset820);
            OffsetsList.Add(offset821);
            OffsetsList.Add(offset822);
            OffsetsList.Add(offset823);
            OffsetsList.Add(offset824);
            OffsetsList.Add(offset825);
            OffsetsList.Add(offset826);
            OffsetsList.Add(offset827);
            OffsetsList.Add(offset828);
            OffsetsList.Add(offset829);
            OffsetsList.Add(offset830);
            OffsetsList.Add(offset831);
            OffsetsList.Add(offset832);
            OffsetsList.Add(offset833);
            OffsetsList.Add(offset834);
            OffsetsList.Add(offset835);
            OffsetsList.Add(offset836);
            OffsetsList.Add(offset837);
            OffsetsList.Add(offset838);
            OffsetsList.Add(offset839);
            OffsetsList.Add(offset840);
            OffsetsList.Add(offset841);
            OffsetsList.Add(offset842);
            OffsetsList.Add(offset843);
            OffsetsList.Add(offset844);
            OffsetsList.Add(offset845);
            OffsetsList.Add(offset846);
            OffsetsList.Add(offset847);
            OffsetsList.Add(offset848);
            OffsetsList.Add(offset849);
            OffsetsList.Add(offset850);
            OffsetsList.Add(offset851);
            OffsetsList.Add(offset852);
            OffsetsList.Add(offset853);
            OffsetsList.Add(offset854);
            OffsetsList.Add(offset855);
            OffsetsList.Add(offset856);
            OffsetsList.Add(offset857);
            OffsetsList.Add(offset858);
            OffsetsList.Add(offset859);
            OffsetsList.Add(offset860);
            OffsetsList.Add(offset861);
            OffsetsList.Add(offset862);
            OffsetsList.Add(offset863);
            OffsetsList.Add(offset864);
            OffsetsList.Add(offset865);
            OffsetsList.Add(offset866);
            OffsetsList.Add(offset867);
            OffsetsList.Add(offset868);
            OffsetsList.Add(offset869);
            OffsetsList.Add(offset870);
            OffsetsList.Add(offset871);
            OffsetsList.Add(offset872);
            OffsetsList.Add(offset873);
            OffsetsList.Add(offset874);
            OffsetsList.Add(offset875);
            OffsetsList.Add(offset876);
            OffsetsList.Add(offset877);
            OffsetsList.Add(offset878);
            OffsetsList.Add(offset879);
            OffsetsList.Add(offset880);
            OffsetsList.Add(offset881);
            OffsetsList.Add(offset882);
            OffsetsList.Add(offset883);
            OffsetsList.Add(offset884);
            OffsetsList.Add(offset885);
            OffsetsList.Add(offset886);
            OffsetsList.Add(offset887);
            OffsetsList.Add(offset888);
            OffsetsList.Add(offset889);
            OffsetsList.Add(offset890);
            OffsetsList.Add(offset891);
            OffsetsList.Add(offset892);
            OffsetsList.Add(offset893);
            OffsetsList.Add(offset894);
            OffsetsList.Add(offset895);
            OffsetsList.Add(offset896);
            OffsetsList.Add(offset897);
            OffsetsList.Add(offset898);
            OffsetsList.Add(offset899);
            OffsetsList.Add(offset900);
            OffsetsList.Add(offset901);
            OffsetsList.Add(offset902);
            OffsetsList.Add(offset903);
            OffsetsList.Add(offset904);
            OffsetsList.Add(offset905);
            OffsetsList.Add(offset906);
            OffsetsList.Add(offset907);
            OffsetsList.Add(offset908);
            OffsetsList.Add(offset909);
            OffsetsList.Add(offset910);
            OffsetsList.Add(offset911);
            OffsetsList.Add(offset912);
            OffsetsList.Add(offset913);
            OffsetsList.Add(offset914);
            OffsetsList.Add(offset915);
            OffsetsList.Add(offset916);
            OffsetsList.Add(offset917);
            OffsetsList.Add(offset918);
            OffsetsList.Add(offset919);
            OffsetsList.Add(offset920);
            OffsetsList.Add(offset921);
            OffsetsList.Add(offset922);
            OffsetsList.Add(offset923);
            OffsetsList.Add(offset925);
            OffsetsList.Add(offset926);
            OffsetsList.Add(offset927);
            OffsetsList.Add(offset928);
            OffsetsList.Add(offset929);
            OffsetsList.Add(offset930);
            OffsetsList.Add(offset931);
            OffsetsList.Add(offset932);
            OffsetsList.Add(offset933);
            OffsetsList.Add(offset934);
            OffsetsList.Add(offset935);
            OffsetsList.Add(offset936);
            OffsetsList.Add(offset937);
            OffsetsList.Add(offset938);
            OffsetsList.Add(offset939);
            OffsetsList.Add(offset940);
            OffsetsList.Add(offset941);
            OffsetsList.Add(offset942);
            OffsetsList.Add(offset943);
            OffsetsList.Add(offset944);
            OffsetsList.Add(offset945);
            OffsetsList.Add(offset946);
            OffsetsList.Add(offset947);
            OffsetsList.Add(offset948);
            OffsetsList.Add(offset949);
            OffsetsList.Add(offset950);
            OffsetsList.Add(offset951);
            OffsetsList.Add(offset952);
            OffsetsList.Add(offset953);
            OffsetsList.Add(offset954);
            OffsetsList.Add(offset955);
            OffsetsList.Add(offset956);
            OffsetsList.Add(offset957);
            OffsetsList.Add(offset958);
            OffsetsList.Add(offset959);
            OffsetsList.Add(offset960);
            OffsetsList.Add(offset961);
            OffsetsList.Add(offset962);
            OffsetsList.Add(offset963);
            OffsetsList.Add(offset964);
            OffsetsList.Add(offset965);
            OffsetsList.Add(offset966);
            OffsetsList.Add(offset967);
            OffsetsList.Add(offset968);
            OffsetsList.Add(offset969);
            OffsetsList.Add(offset970);
            OffsetsList.Add(offset971);
            OffsetsList.Add(offset972);
            OffsetsList.Add(offset973);
            OffsetsList.Add(offset974);
            OffsetsList.Add(offset975);
            OffsetsList.Add(offset976);
            OffsetsList.Add(offset977);
            OffsetsList.Add(offset978);
            OffsetsList.Add(offset979);
            OffsetsList.Add(offset980);
            OffsetsList.Add(offset981);
            OffsetsList.Add(offset982);
            OffsetsList.Add(offset983);
            OffsetsList.Add(offset984);
            OffsetsList.Add(offset985);
            OffsetsList.Add(offset986);
            OffsetsList.Add(offset987);
            OffsetsList.Add(offset988);
            OffsetsList.Add(offset989);
            OffsetsList.Add(offset990);
            OffsetsList.Add(offset991);
            OffsetsList.Add(offset992);
            OffsetsList.Add(offset993);
            OffsetsList.Add(offset994);
            OffsetsList.Add(offset995);
            OffsetsList.Add(offset996);
            OffsetsList.Add(offset997);
            OffsetsList.Add(offset998);
            OffsetsList.Add(offset999);
            OffsetsList.Add(offset1000);
            OffsetsList.Add(offset1001);
            OffsetsList.Add(offset1002);
            OffsetsList.Add(offset1003);
            OffsetsList.Add(offset1004);
            OffsetsList.Add(offset1005);
            OffsetsList.Add(offset1006);
            OffsetsList.Add(offset1007);
            OffsetsList.Add(offset1008);
            OffsetsList.Add(offset1009);
            OffsetsList.Add(offset1010);
            OffsetsList.Add(offset1011);
            OffsetsList.Add(offset1012);
            OffsetsList.Add(offset1013);
            OffsetsList.Add(offset1014);
            OffsetsList.Add(offset1015);
            OffsetsList.Add(offset1016);
            OffsetsList.Add(offset1017);
            OffsetsList.Add(offset1018);
            OffsetsList.Add(offset1019);
            OffsetsList.Add(offset1020);
            OffsetsList.Add(offset1021);
            OffsetsList.Add(offset1022);
            OffsetsList.Add(offset1023);
            OffsetsList.Add(offset1024);
            OffsetsList.Add(offset1025);
            OffsetsList.Add(offset1026);
            OffsetsList.Add(offset1027);
            OffsetsList.Add(offset1028);
            OffsetsList.Add(offset1029);
            OffsetsList.Add(offset1030);
            OffsetsList.Add(offset1031);
            OffsetsList.Add(offset1032);
            OffsetsList.Add(offset1033);
            OffsetsList.Add(offset1034);
            OffsetsList.Add(offset1035);
            OffsetsList.Add(offset1036);
            OffsetsList.Add(offset1037);
            OffsetsList.Add(offset1038);
            OffsetsList.Add(offset1039);
            OffsetsList.Add(offset1040);
            OffsetsList.Add(offset1041);
            OffsetsList.Add(offset1042);
            OffsetsList.Add(offset1043);
            OffsetsList.Add(offset1044);
            OffsetsList.Add(offset1045);
            OffsetsList.Add(offset1046);
            OffsetsList.Add(offset1047);
            OffsetsList.Add(offset1048);
            OffsetsList.Add(offset1049);
            OffsetsList.Add(offset1050);
            OffsetsList.Add(offset1051);
            OffsetsList.Add(offset1052);
            OffsetsList.Add(offset1053);
            OffsetsList.Add(offset1054);
            OffsetsList.Add(offset1055);
            OffsetsList.Add(offset1056);
            OffsetsList.Add(offset1057);
            OffsetsList.Add(offset1058);
            OffsetsList.Add(offset1059);
            OffsetsList.Add(offset1060);
            OffsetsList.Add(offset1061);
            OffsetsList.Add(offset1062);
            OffsetsList.Add(offset1063);
            OffsetsList.Add(offset1064);
            OffsetsList.Add(offset1065);
            OffsetsList.Add(offset1066);
            OffsetsList.Add(offset1067);
            OffsetsList.Add(offset1068);
            OffsetsList.Add(offset1069);
            OffsetsList.Add(offset1070);
            OffsetsList.Add(offset1071);
            OffsetsList.Add(offset1072);
            OffsetsList.Add(offset1073);
            OffsetsList.Add(offset1074);
            OffsetsList.Add(offset1075);
            OffsetsList.Add(offset1076);
            OffsetsList.Add(offset1077);
            OffsetsList.Add(offset1078);
            OffsetsList.Add(offset1079);
            OffsetsList.Add(offset1080);
            OffsetsList.Add(offset1081);
            OffsetsList.Add(offset1082);
            OffsetsList.Add(offset1083);
            OffsetsList.Add(offset1084);
            OffsetsList.Add(offset1085);
            OffsetsList.Add(offset1086);
            OffsetsList.Add(offset1087);
            OffsetsList.Add(offset1088);
            OffsetsList.Add(offset1089);
            OffsetsList.Add(offset1090);
            OffsetsList.Add(offset1091);
            OffsetsList.Add(offset1092);
            OffsetsList.Add(offset1093);
            OffsetsList.Add(offset1094);
            OffsetsList.Add(offset1095);
            OffsetsList.Add(offset1096);
            OffsetsList.Add(offset1097);
            OffsetsList.Add(offset1098);
            OffsetsList.Add(offset1099);
            OffsetsList.Add(offset1100);
            OffsetsList.Add(offset1101);
            OffsetsList.Add(offset1102);
            OffsetsList.Add(offset1103);
            OffsetsList.Add(offset1104);
            OffsetsList.Add(offset1105);
            OffsetsList.Add(offset1106);
            OffsetsList.Add(offset1107);
            OffsetsList.Add(offset1108);
            OffsetsList.Add(offset1109);
            OffsetsList.Add(offset1110);
            OffsetsList.Add(offset1111);
            OffsetsList.Add(offset1112);
            OffsetsList.Add(offset1113);
            OffsetsList.Add(offset1114);
            OffsetsList.Add(offset1115);
            OffsetsList.Add(offset1116);
            OffsetsList.Add(offset1117);
            OffsetsList.Add(offset1118);
            OffsetsList.Add(offset1119);
            OffsetsList.Add(offset1120);
            OffsetsList.Add(offset1121);
            OffsetsList.Add(offset1122);
            OffsetsList.Add(offset1123);
            OffsetsList.Add(offset1124);
            OffsetsList.Add(offset1125);
            OffsetsList.Add(offset1126);
        }

        /// <summary>
        /// Gets an Offset in Object form
        /// </summary>
        /// <param name="type"> The Offset Location EX: 0x3D04</param>
        /// <returns>the Raw offset</returns>
        public Offset getOffset(Int32 Location)
        {
            return OffsetsList.FirstOrDefault(x => x.Address == Location);
        }

        public static void SetupTimer()
        {
            Timer t = new Timer();
            t.Interval = 5000;
            t.Elapsed += T_lapsed;
            t.Enabled = true;
        }

        private static void T_lapsed(object sender, ElapsedEventArgs e)
        {
            //read offsets
        }
    }
}
