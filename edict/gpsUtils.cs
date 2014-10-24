using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Xml;
using System.Text.RegularExpressions;

namespace edict
{
    public enum CoordinateType { longitude, latitude };

    class gpsUtils
    {
        private static double MAX_COMPASS_HEADING = 359.99;
        private static uint DENOMINATOR = 100;
        private static string ORIGINAL_HEADING = "Original GPSImgDirection";

        private static Int32 GPS_VERSION = 0x0;
        private static Int32 GPS_LATITUDE_REF = 0x1;
        private static Int32 GPS_LATITUDE = 0x2;
        private static Int32 GPS_LONGITUDE_REF = 0x3;
        private static Int32 GPS_LONGITUDE = 0x4;
        private static Int32 GPS_ALTITUDE_REF = 0x5;
        private static Int32 GPS_ALTITUDE = 0x6;
        private static Int32 GPS_TIME = 0x7;
        private static Int32 GPS_IMG_HEADING = 0x11;
        private static Int32 GPS_DATE = 0x1D;
        private static Int32 DATE_TIME_EXPOSURE = 0x9003;
        private static Int32 DATE_TIME_DIGITIZED = 0x9004;
        private static Int32 USER_COMMENT = 0x9286;
        private static Int32 HEADING_BACKUP = 0xff00;
        private static Int32 IMAGE_DESCRIPTIOIN = 0x010e;


        /// <summary>
        /// Set GPS offest for where photographer sat in the aircarft during 
        /// the aerial photography. Any of the following GPS information  
        /// that is not in the image will be added using the track log
        /// GPS Information: Latitude, Latitude Ref (N/S), 
        ///                  Longitude, Longitude Ref (E/W), Date, Time
        /// </summary>
        /// <param name="image">image to get/set GPS Exif information</param>
        /// <param name="offset">Angle offset in degrees 0 to 359.9</param>
        /// <param name="trackLog">List of track log entries time, lat, lon, heading, spped</param>
        /// <returns>true if image has been converted, false if cannot convert (no GPS info and no track log)</returns>
        public static bool offsetGpsImgDirection(Image image, int offset, List<TrackLogEntry> trackLog, long gpsImageDeltaTime, bool usingUTC)
        {
            PropertyItem GpsImgDirection;
            uint numerator;
            uint denominator;
            double heading;

            /* Get GPS Heading EXIF data.
             * EXIF Information:
             * Property: GPSImgDirection GPS 
             * Label: Image Direction
             * Value type: Rational 
             * Exif2 type: XmpText 
             * Category: Internal 
             * Description: GPS tag 17, 0x11. Direction of image when captured, values range from 0 to 359.99. 
             * for more inforamtion see: http://www.exiv2.org/tags-xmp-exif.html
             * 
             * This is a byte array where the first 4 bytes is the heading that or
             * direction of motion of the gps. The last 4 bytes is the denomitor
             * to convert the integer to a fraction.
             * 
             */
            try
            {
                // if we cannot get GPS information, then bail (throws exception)
                double origionalHeading = -1;
                if (trackLog == null)
                {
                    // get gps heading (throws exception if item does not exist)
                    GpsImgDirection = image.GetPropertyItem(GPS_IMG_HEADING);

                    // get original direction (-1 if there is no orig)
                    origionalHeading = getOrigionalHeading(image);
                }
                else
                {
                    setMissingGpsInfoFromTrackLog(image, trackLog, gpsImageDeltaTime, usingUTC);
                    GpsImgDirection = image.GetPropertyItem(GPS_IMG_HEADING);
                }

                /* if we are restoring origional heading, then restore from 
                 * the exif special locagtion where we stored the origional
                 * value as Original GPSImgDirection=xxx.x (where xxx.s 
                 * is orig heading) special location 0xff00 HEADING_BACKUP.
                 */
                if (origionalHeading < 0)
                {
                    byte[] bNumerator = new byte[GpsImgDirection.Len / 2];
                    byte[] bDenominator = new byte[GpsImgDirection.Len / 2];
                    Array.Copy(GpsImgDirection.Value, 0, bNumerator, 0, GpsImgDirection.Len / 2);
                    Array.Copy(GpsImgDirection.Value, GpsImgDirection.Len / 2, bDenominator, 0, GpsImgDirection.Len / 2);
                    numerator = convertToInt32U(bNumerator);
                    denominator = convertToInt32U(bDenominator);
                    heading = Convert.ToDouble(numerator) / Convert.ToDouble(denominator);

                    // Save origional heading if getOrigionalHeading() is less then 0;
                    if (origionalHeading < 0)
                    {
                        setOriginalHeading(image, heading);
                    }
                }
                else
                {
                    heading = origionalHeading;
                }

            }
            catch (Exception)
            {
                // file did not have the GPSImgDirection information (0x11)
                return false;
            }

            /* Get the hading the camera was pointing in rather rather then
             * the direction the aircraft was flying. Generally this is
             * the left rear seat lookgin out the side window. In
             * this case that would be an offset of 270 degrees.
             */
            heading = heading + Convert.ToDouble(offset);
            if (heading > MAX_COMPASS_HEADING)
            {
                heading = heading - MAX_COMPASS_HEADING;
            }
            heading = Convert.ToDouble(String.Format("{0:0.0}", heading));
            image.SetPropertyItem(setGpsHeading(GpsImgDirection, heading));

            return true;
        }

        public static bool projectImageGeoReference(Image image, double alt, double vAngle)
        {
            bool success = true;
            try
            {
                latlon gps1 = new latlon(getGpsLat(image), getGpsLong(image));
                latlon gps2 = latLonProject(gps1, getGpsImageHeading(image), alt, vAngle);

                PropertyItem pLat = setGpsCoordinate(gps2.lat);
                image.SetPropertyItem(pLat);

                PropertyItem pLon = setGpsCoordinate(gps2.lon);
                image.SetPropertyItem(pLon);
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public static bool setImageDescription(Image image, string description)
        {
            bool success = true;
            try
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] bDescription = encoding.GetBytes(description);

                PropertyItem pi = createPropertyItem();
                pi.Id = IMAGE_DESCRIPTIOIN;
                pi.Type = 0x2;
                pi.Len = bDescription.Length;
                pi.Value = new byte[bDescription.Length];
                bDescription.CopyTo(pi.Value, 0);
                image.SetPropertyItem(pi);
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public static string getImageDescription(Image image)
        {
            PropertyItem ImageDescription;
            String description = "";
            try
            {
                ImageDescription = image.GetPropertyItem(IMAGE_DESCRIPTIOIN);
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                description = encoding.GetString(ImageDescription.Value);
            }
            catch (Exception) { /* do nothing */ }
            return description;
        }

        public static bool setUserComment(Image image, string comment)
        {
            bool success = true;
            try
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] bComment = encoding.GetBytes(comment);

                PropertyItem pi = createPropertyItem();
                pi.Id = USER_COMMENT;
                pi.Type = 0x2;
                pi.Len = bComment.Length;
                pi.Value = new byte[bComment.Length];
                bComment.CopyTo(pi.Value, 0);
                image.SetPropertyItem(pi);
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public static string getImUserComment(Image image)
        {
            PropertyItem UserComment;
            String comment = "";
            try
            {
                UserComment = image.GetPropertyItem(USER_COMMENT);
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                comment = encoding.GetString(UserComment.Value);
            }
            catch (Exception) { /* do nothing */ }
            return comment;
        }

        public static double getGpsImageHeading(Image image)
        {
            PropertyItem GpsImgDirection = image.GetPropertyItem(GPS_IMG_HEADING);
            byte[] bNumerator = new byte[GpsImgDirection.Len / 2];
            byte[] bDenominator = new byte[GpsImgDirection.Len / 2];
            Array.Copy(GpsImgDirection.Value, 0, bNumerator, 0, GpsImgDirection.Len / 2);
            Array.Copy(GpsImgDirection.Value, GpsImgDirection.Len / 2, bDenominator, 0, GpsImgDirection.Len / 2);
            uint numerator = convertToInt32U(bNumerator);
            uint denominator = convertToInt32U(bDenominator);
            return Convert.ToDouble(numerator) / Convert.ToDouble(denominator);
        }

        /// <summary>
        /// Sets the offset GPS heading into the PropertyItem for GPS headings
        /// </summary>
        /// <param name="pItem">PropertyItem used to store GPS Heading</param>
        /// <param name="heading">Heading to add in degrees 0 to 359.9</param>
        /// <returns>PropertyItem with heading information added</returns>
        private static PropertyItem setGpsHeading(PropertyItem pItem, double heading)
        {
            uint denominator = DENOMINATOR;

            /* Now convert the new heading back to an integer and then to a 
             * byte array. We will always set the denominator to 100 as this 
             * is simple way to store and look at the values. In any event
             * it works when importing the converted imates into RoboGEO.
             */
            Byte[] bHeading = BitConverter.GetBytes(Convert.ToInt32(heading * denominator));
            for (int i = 0; i < 4; i++)
            {
                if (i < bHeading.Length)
                {
                    pItem.Value[i] = bHeading[i];
                }
                else
                {
                    pItem.Value[i] = 0;
                }
            }
            Byte[] bHeadingDiv = BitConverter.GetBytes(Convert.ToInt32(denominator));
            for (int i = 0; i < 4; i++)
            {
                if (i < bHeadingDiv.Length)
                {
                    pItem.Value[i + 4] = bHeadingDiv[i];
                }
                else
                {
                    pItem.Value[i + 4] = 0;
                }
            }
            return pItem;
        }

        /// <summary>
        /// This routine will use, if availabel the GPS date and time to find the closest
        /// track log entry. If GPS time is not available, then the Exif image creation
        /// time will be used (in this case it is assumed that latatude and longitude
        /// are also not in the image Exif data and will add that information to the
        /// image as well.
        /// </summary>
        /// <param name="image">Image to add the GPS informatio to</param>
        /// <param name="trackLog">GPS track log containing date, time, lat, lon, heading, spped</param>
        /// <param name="usingUTC">Camera time is using UTC true, false if using local time</param>
        private static void setMissingGpsInfoFromTrackLog(Image image, List<TrackLogEntry> trackLog, long gpsImageDeltaTime, bool usingUTC)
        {
            // set a very big time tick number so we don't return first track-log as being 
            // closest.
            long closestTime = 621355968000000000;

            // Assuming that only GPS Heading information is missing.
            bool usingLatLon = false;

            // Caclulate time delta to bail from setting GPS informaiton
            // if difference in time between track-log and image time
            // is larger then this time, consider the image cannot be
            // GeoCoded.
            DateTime d1 = new DateTime(2009, 12, 12, 12, 12, 0);
            DateTime d2 = new DateTime(2009, 12, 12, 12, 12, 15);
            long forgetItTime = Math.Abs(d1.Ticks - d2.Ticks);

            // Get GPS date and time, if exceptio is thrown, then get the
            // same information from the Image creation date (in which
            // case we can assume that LAT and LON are missing as well).
            string dateTime;
            try
            {
                dateTime = getGpsDateTime(image);
            }
            catch (Exception)
            {
                dateTime = getCameraDateTime(image);
                usingLatLon = true;
            }

            // Convert string date "2009:10:15:09:36:15" to DateTime object
            string[] dateTimeSplit = dateTime.Split(':');
            DateTime imageDateTime = new DateTime(
                (int)Int32.Parse(dateTimeSplit[0]), (int)Int32.Parse(dateTimeSplit[1]), (int)Int32.Parse(dateTimeSplit[2]),
                (int)Int32.Parse(dateTimeSplit[3]), (int)Int32.Parse(dateTimeSplit[4]), (int)Int32.Parse(dateTimeSplit[5]));


            // If using camera image creation time, then it is the local time. 
            // and needs to be converted to UTC time.
            // **** NOTE: it is assumed that the camera is set to use ZULU time
            // or a UTC offset of zero.
            // If camera is using Local time, then the computer running EDICT
            // needs to be set to the same timezone as the camera. 
            if (usingLatLon == true)
            {
                imageDateTime = imageDateTime.AddTicks(gpsImageDeltaTime);
                if (usingUTC == false)
                {
                    imageDateTime = imageDateTime.ToUniversalTime();
                }
            }

            // now we find the GPS track log time that is closest to the image
            // time. Since the track log is in decending we are traveling 
            // in time towards the track point that matches the image. If 
            // the delta starts to get bigger we have past that time, so 
            // we are done (we have the time, break out of loop).
            long timeDelta;
            TrackLogEntry trackEntry = null;
            foreach (TrackLogEntry tle in trackLog)
            {
                if (tle.speed > 0)
                {
                    timeDelta = Math.Abs(imageDateTime.Ticks - tle.time.Ticks);
                    if (timeDelta > closestTime)
                    {
                        break;
                    }
                    else
                    {
                        closestTime = timeDelta;
                        trackEntry = tle;
                    }
                }
            }

            // if we have a track log entry and the closest time is less 
            // then the time is spaller then the forgetItTime then the
            // image can be GeoCoded
            if (trackEntry != null && closestTime < forgetItTime)
            {
                try
                {
                    //if (usingLatLon == true)
                    //{
                        setImageGpsInfo(image, trackEntry, imageDateTime);
                    //}

                    PropertyItem pi = createPropertyItem();
                    pi.Id = GPS_IMG_HEADING;
                    pi.Type = 0x5;
                    pi.Len = 8;
                    pi.Value = new byte[pi.Len];
                    setGpsHeading(pi, trackEntry.heading);
                    image.SetPropertyItem(pi);

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static TrackLogEntry getHeading(TrackLogEntry tle, double dLat1, double dLon1, double dLat2, double dLon2)
        {
            double lat1 = DegreeToRadian(dLat1);
            double lon1 = DegreeToRadian(dLon1);
            double lat2 = DegreeToRadian(dLat2);

            double lon2 = DegreeToRadian(dLon2); 
            
            double y = Math.Sin(lon2 - lon1) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1);
            double brng = Math.Atan2(y, x);

            /*
            heading = Math.Atan2(Math.Sin(lon2 - lon1) * Math.Cos(lat2), Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1));

            if (heading < 0)
            {
                heading = -1 * heading;
            }
            */

            tle.heading = RadianToDegree(brng);

            return tle;
        }

        /// <summary>
        /// Routine set the GPS Exif informaiton in an image for
        ///     Latitude, Latitude Ref (N/S)
        ///     Longitude, Longitude Ref (E/W)
        ///     GPS Date
        ///     GPS Time
        /// </summary>
        /// <param name="image">Image to be GeoTagged</param>
        /// <param name="trackEntry">Track entry containing the GPS information</param>
        /// <param name="utcTime">The utc time image was taken</param>
        private static void setImageGpsInfo(Image image, TrackLogEntry trackEntry, DateTime utcTime)
        {
            PropertyItem ver = setGpsVersion();
            image.SetPropertyItem(ver);

            coordinate lat = new coordinate(trackEntry.lat, CoordinateType.latitude);
            PropertyItem piLat = setGpsCoordinate(lat);
            image.SetPropertyItem(piLat);

            PropertyItem piLatRef = setGpsReference(lat);
            image.SetPropertyItem(piLatRef);

            coordinate lon = new coordinate(trackEntry.lon, CoordinateType.longitude);
            PropertyItem piLon = setGpsCoordinate(lon);
            image.SetPropertyItem(piLon);

            PropertyItem piLonRef = setGpsReference(lon);
            image.SetPropertyItem(piLonRef);

            PropertyItem alt = setGpsAltitude(trackEntry.altitue);
            image.SetPropertyItem(alt);

            PropertyItem altRef = setGpsAltitudeRef();
            image.SetPropertyItem(altRef);

            PropertyItem piDate = setGpsDate(utcTime);
            image.SetPropertyItem(piDate);

            PropertyItem piTime = setGpsTime(utcTime);
            image.SetPropertyItem(piTime);
        }

        /// <summary>
        /// Return the GPS date and time in the following format
        ///     YYYY:MM:DD:hh:mm:ss
        /// </summary>
        /// <param name="image">Image containing the GPS Information</param>
        /// <returns>returns ":" seperated list of the date and time</returns>
        public static string getGpsDateTime(Image image)
        {
            Encoding ascii = Encoding.ASCII;
            PropertyItem GpsImgTime = image.GetPropertyItem(GPS_TIME);
            PropertyItem GpsDate = image.GetPropertyItem(GPS_DATE);
            string date = ascii.GetString(GpsDate.Value).Trim('\0');
            string time = new GPSRational(GpsImgTime.Value).ToString(":");
            return date + ":" + time;
        }

        public static void syncGpsCameraTime(Image image)
        {
            Encoding ascii = Encoding.ASCII;
            PropertyItem GpsImgTime = image.GetPropertyItem(GPS_TIME);
            PropertyItem GpsDate = image.GetPropertyItem(GPS_DATE);
            string date = ascii.GetString(GpsDate.Value).Trim('\0');
            string time = new GPSRational(GpsImgTime.Value).ToString(":");

            byte[] gpsTimeBytes = ascii.GetBytes(date + " " + time);
            
            // Create Exif Property Item Date Time Original
            //PropertyItem pItem = image.GetPropertyItem(DATE_TIME_EXPOSURE);
            //pItem.Id = 0x9993;
            //image.SetPropertyItem(pItem);
            try
            {
                image.RemovePropertyItem(DATE_TIME_EXPOSURE);
            }
            catch { /* do nothing */ }
            PropertyItem pItemImageDateTimeOriginal = createPropertyItem();
            pItemImageDateTimeOriginal.Id = DATE_TIME_EXPOSURE;
            pItemImageDateTimeOriginal.Type = 0x2;
            pItemImageDateTimeOriginal.Len = gpsTimeBytes.Length + 1;
            pItemImageDateTimeOriginal.Value = new byte[pItemImageDateTimeOriginal.Len];
            gpsTimeBytes.CopyTo(pItemImageDateTimeOriginal.Value, 0);
            image.SetPropertyItem(pItemImageDateTimeOriginal);
            // Create Exif Property Item Date Time Digitized
            try
            {
                image.RemovePropertyItem(DATE_TIME_DIGITIZED);
            }
            catch { /* do nothing */ }
            PropertyItem pItemImageDateTimeDigitized = createPropertyItem();
            pItemImageDateTimeDigitized.Id = DATE_TIME_DIGITIZED;
            pItemImageDateTimeDigitized.Type = 0x2;
            pItemImageDateTimeDigitized.Len = gpsTimeBytes.Length + 1;
            pItemImageDateTimeDigitized.Value = new byte[pItemImageDateTimeDigitized.Len];
            gpsTimeBytes.CopyTo(pItemImageDateTimeDigitized.Value, 0);
            image.SetPropertyItem(pItemImageDateTimeDigitized);
        }

        /// <summary>
        /// Returns the image creation date and time in the following format
        ///     YYYY:MM:DD:hh:mm:ss
        /// </summary>
        /// <param name="image">Image containing the GPS Information</param>
        /// <returns>returns ":" seperated list of the date and time</returns>
        public static string getCameraDateTime(Image image)
        {
            Encoding ascii = Encoding.ASCII;
            PropertyItem CameraDateTime = image.GetPropertyItem(DATE_TIME_EXPOSURE);
            string dateTime = ascii.GetString(CameraDateTime.Value).Trim('\0');
            dateTime = dateTime.Replace(' ', ':');
            return dateTime;
        }

        /// <summary>
        /// Creates and retuns the GPS property item for the Latitude
        /// or longitude.
        /// </summary>
        /// <param name="coord">contains the GPS coordinate as well and its type</param>
        /// <returns>GPS latitude or GPS longitude Exif Property Item</returns>
        private static PropertyItem setGpsCoordinate(coordinate coord)
        {
            byte[] bytes = new byte[24];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = 0;
            }

            Byte[] d = BitConverter.GetBytes(Convert.ToInt32(coord.deg));
            Array.Copy(d, 0, bytes, 0, 4);
            bytes[4] = 1;

            Byte[] m = BitConverter.GetBytes(Convert.ToInt32(coord.min));
            Array.Copy(m, 0, bytes, 8, 4);
            bytes[12] = 1;

            Byte[] s = BitConverter.GetBytes(Convert.ToInt32(Math.Round(coord.sec * 1000)));
            Array.Copy(s, 0, bytes, 16, 4);
            Array.Copy(BitConverter.GetBytes(1000), 0, bytes, 20, 4);

            PropertyItem pItem = createPropertyItem();
            pItem.Id = coord.type == CoordinateType.latitude ? GPS_LATITUDE : GPS_LONGITUDE;
            pItem.Type = 0x5;
            pItem.Len = bytes.Length;
            pItem.Value = bytes;

            return pItem;
        }

        /// <summary>
        /// Sets the GPS coordinate reference as follows
        ///     Latitude N or S
        ///     Longitude E or W
        /// </summary>
        /// <param name="coord">contains the GPS coordinate as well and its type</param>
        /// <returns>GPS Exif Property Item</returns>
        private static PropertyItem setGpsReference(coordinate coord)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(coord.reference);

            PropertyItem pItem = createPropertyItem();
            pItem.Id = coord.type == CoordinateType.latitude ? GPS_LATITUDE_REF : GPS_LONGITUDE_REF;
            pItem.Type = 0x2;
            pItem.Len = 2;
            pItem.Value = new byte[pItem.Len];
            bytes.CopyTo(pItem.Value, 0);

            return pItem;
        }

        public static coordinate getGpsLat(Image image)
        {
            PropertyItem lat = image.GetPropertyItem(GPS_LATITUDE);
            PropertyItem reference = image.GetPropertyItem(GPS_LATITUDE_REF);
            return getGpsCoordinate(lat.Value, reference.Value, CoordinateType.latitude);
        }

        public static coordinate getGpsLong(Image image)
        {
            PropertyItem lon = image.GetPropertyItem(GPS_LONGITUDE);
            PropertyItem reference = image.GetPropertyItem(GPS_LONGITUDE_REF);
            return getGpsCoordinate(lon.Value, reference.Value, CoordinateType.longitude);
        }

        private static coordinate getGpsCoordinate(byte[] cb, byte[] reference, CoordinateType latRef)
        {
            GPSRational c = new GPSRational(cb);
            double hours = c.Hours.ToDouble();
            if (reference[0] == 'S' || reference[0] == 'W')
            {
                hours = hours * -1;
            }
            return new coordinate(hours, c.Minutes.ToDouble(), c.Seconds.ToDouble(), latRef);
        }

        /// <summary>
        /// Creates and sets the GPS time for the image Exif Property Item
        /// </summary>
        /// <param name="utcTime">DateTime utc time</param>
        /// <returns>GPS Exif Property Item</returns>
        private static PropertyItem setGpsTime(DateTime utcTime)
        {
             // Init the byte array to hold Exif value type 0x5
            byte[] bytes = new byte[24];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = 0;
            }

            // Hour 4 bytes (0..3) numerator next 4 bytes (4..7) denomiter.
            Byte[] d = BitConverter.GetBytes(utcTime.Hour);
            Array.Copy(d, 0, bytes, 0, 4);
            bytes[4] = 1; // since we did not multiple out the numerator use 1 for denomitor.

            // Minute 4 bytes (8..11) numerator next 4 bytes (12..15) denomiter.
            Byte[] m = BitConverter.GetBytes(utcTime.Minute);
            Array.Copy(m, 0, bytes, 8, 4);
            bytes[12] = 1; // since we did not multiple out the numerator use 1 for denomitor.

            // Second 4 bytes (16..19) numerator next 4 bytes (20..23) denomiter.
            Byte[] s = BitConverter.GetBytes(utcTime.Second);
            Array.Copy(s, 0, bytes, 16, 4);
            bytes[20] = 1; // since we did not multiple out the numerator use 1 for denomitor.

            // Create Exif Property Item
            PropertyItem pItem = createPropertyItem();
            pItem.Id = GPS_TIME;
            pItem.Type = 0x5;
            pItem.Len = bytes.Length;
            pItem.Value = bytes;

            return pItem;
        }

        /// <summary>
        /// Creates and sets the GPS date for the image Exif Property Item
        /// </summary>
        /// <param name="utcTime">DateTime utc time</param>
        /// <returns>GPS Exif Property Item</returns>
        private static PropertyItem setGpsDate(DateTime utcTime)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(String.Format("{0:yyyy:M:d}", utcTime));

            // Create Exif Property Item
            PropertyItem pItem = createPropertyItem();
            pItem.Id = GPS_DATE;
            pItem.Type = 0x2;
            pItem.Len = 11;
            pItem.Value = new byte[pItem.Len];
            bytes.CopyTo(pItem.Value, 0);

            return pItem;
        }

        /// <summary>
        /// Sets the Exif GPS version to 2.2.
        /// </summary>
        /// <returns>GPS Exif Property Item</returns>
        private static PropertyItem setGpsVersion()
        {

            byte[] bytes = new byte[4];
            bytes[0] = 2;
            bytes[1] = 2;
            bytes[2] = 0;
            bytes[3] = 0;

            PropertyItem pItem = createPropertyItem();
            pItem.Id = GPS_VERSION;
            pItem.Type = 0x1;
            pItem.Len = bytes.Length;
            pItem.Value = bytes;

            return pItem;
        }

        /// <summary>
        /// Sets Exif GPS information for altitude.
        /// </summary>
        /// <param name="altitude">metters</param>
        /// <returns>GPS Exif Property Item</returns>
        private static PropertyItem setGpsAltitude(double altitude)
        {
            // Init the byte array to hold Exif value type 0x5
            byte[] bytes = new byte[8];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = 0;
            }

            // Altitude in meters 4 bytes (0..3) numerator next 4 bytes (4..7) denomiter.
            int alt = Int32.Parse(String.Format("{0:0}", altitude));
            Byte[] bAlt = BitConverter.GetBytes(alt);
            Array.Copy(bAlt, 0, bytes, 0, 4);
            bytes[4] = 1;

            PropertyItem pItem = createPropertyItem();
            pItem.Id = GPS_ALTITUDE;
            pItem.Type = 0x5;
            pItem.Len = bytes.Length;
            pItem.Value = bytes;

            return pItem;
        }

        /// <summary>
        /// Sets the Exif GPS altitude reference SEA LEVEL which is a value of zero
        /// </summary>
        /// <returns>GPS Exif Property Item</returns>
        private static PropertyItem setGpsAltitudeRef()
        {
            // Init the byte array Altitude ref for SEA LEVEL is set in a one byte 
            // array whos value is zero.
            byte[] bytes = new byte[1];
            bytes[0] = 0;

            PropertyItem pItem = createPropertyItem();
            pItem.Id = GPS_ALTITUDE_REF;
            pItem.Type = 0x1;
            pItem.Len = bytes.Length;
            pItem.Value = bytes;

            return pItem;
        }

        public static double getGpsAltitude(Image image)
        {

            PropertyItem GpsAltitude = image.GetPropertyItem(GPS_ALTITUDE);
            uint numerator;
            uint denominator;
            double altitude;

            byte[] bNumerator = new byte[GpsAltitude.Len / 2];
            byte[] bDenominator = new byte[GpsAltitude.Len / 2];
            Array.Copy(GpsAltitude.Value, 0, bNumerator, 0, GpsAltitude.Len / 2);
            Array.Copy(GpsAltitude.Value, GpsAltitude.Len / 2, bDenominator, 0, GpsAltitude.Len / 2);
            numerator = convertToInt32U(bNumerator);
            denominator = convertToInt32U(bDenominator);
            altitude = Convert.ToDouble(numerator) / Convert.ToDouble(denominator);

            return altitude;
        }

        /// <summary>
        /// gets the original heading so if the user makes a mistake they can re-run
        /// this program again to correct it without trying to figure out a new angle
        /// to correct to. For example if user ment 90 not 270 the user would then 
        /// have to add 180 degrees to correct this error. We do not want this, the
        /// user should only need to enter 90 and re-run.
        /// </summary>
        /// <param name="image">Image containing the GPS Exif info</param>
        /// <returns>GPS heading in degrees 0..359.9</returns>
        public static double getOrigionalHeading(Image image)
        {
            PropertyItem UserComment;
            double originalHeading = -1;
            /* get UserComment stored in the EXIF data
             */
            try
            {
                UserComment = image.GetPropertyItem(HEADING_BACKUP);
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                string userComment = encoding.GetString(UserComment.Value);
                if (userComment.StartsWith(ORIGINAL_HEADING))
                {
                    Match m = Regex.Match(userComment, @"[\p{N}\.]+");
                    originalHeading = Convert.ToDouble(m.Value);
                }
            }
            catch (Exception)
            {
                originalHeading = -1;
            }
            return originalHeading;
        }

        /// <summary>
        /// sets the original heading so if the user makes a mistake they can re-run
        /// this program again to correct it without trying to figure out a new angle
        /// to correct to. For example if user ment 90 not 270 the user would then 
        /// have to add 180 degrees to correct this error. We do not want this, the
        /// user should only need to enter 90 and re-run.
        /// </summary>
        /// <param name="image">Image containing the GPS Exif info</param>
        /// <param name="originalHeading">GPS heading in degrees 0..359.9</param>
        /// <returns>true if successful</returns>
        public static bool setOriginalHeading(Image image, double originalHeading)
        {
            bool success = true;
            string saveHeading = ORIGINAL_HEADING + "=" + String.Format("{0:0.0}", originalHeading) + "\0";
            try
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] bSaveHeading = encoding.GetBytes(saveHeading);

                PropertyItem pi = createPropertyItem();
                pi.Id = HEADING_BACKUP;
                pi.Type = 0x2;
                pi.Len = bSaveHeading.Length;
                pi.Value = new byte[bSaveHeading.Length];
                bSaveHeading.CopyTo(pi.Value, 0);
                image.SetPropertyItem(pi);
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Creates a property item. Since we cannot create one directly we use the
        /// Assembly and a blank jpeg image to get around this limitation. To be 
        /// fair, in most cercomstancis we should not be creating or modifying Exif
        /// data as that should be done by the camera.
        /// </summary>
        /// <returns>empty property item</returns>
        public static PropertyItem createPropertyItem()
        {
            // Loads a PropertyItem from a Jpeg image stored in the assembly as a resource.
			Assembly assembly = Assembly.GetExecutingAssembly();
			Stream emptyBitmapStream = assembly.GetManifestResourceStream("edict.decoy.jpg");
			System.Drawing.Image empty = System.Drawing.Image.FromStream(emptyBitmapStream);
            return empty.PropertyItems[0];
        }

        /// <summary>
        /// Converts a byte[24] array to an unsigned integer
        /// </summary>
        /// <param name="arr">byte array</param>
        /// <returns>integer</returns>
        public static uint convertToInt32U(byte[] arr)
        {
            if (arr.Length != 4)
            {
                return 0;
            }
            else
            {
                return Convert.ToUInt32(arr[3] << 24 | arr[2] << 16 | arr[1] << 8 | arr[0]);
            }
        }

        /// <summary>
        /// Converts a byte[16] array to an unsigned integer
        /// </summary>
        /// <param name="arr">byte array</param>
        /// <returns>integer</returns>
        public static uint convertToInt16U(byte[] arr)
        {
            if (arr.Length != 2)
            {
                return 0;
            }
            else
            {
                return Convert.ToUInt16(arr[1] << 8 | arr[0]);
            }
        }

        /// <summary>
        /// Determins if the image is a JPEG image.
        /// </summary>
        /// <param name="fileName">filenmme and extension "myImage.jpg"</param>
        /// <returns>true if jpeg image</returns>
        public static bool isImageFile(string fileName)
        {
            bool ret = false;
            string file = fileName.ToLower();
            if (file.EndsWith(".jpg")) { ret = true; }
            if (file.EndsWith(".jpeg")) { ret = true; }
            return ret;
        }

        private static latlon latLonProject(latlon gps1, double baring, double alt, double vAngle)
        {
            double R = 6371.0;
            double lat1 = DegreeToRadian(gps1.lat.decimalDegrees);
            double lon1 = DegreeToRadian(gps1.lon.decimalDegrees);
            double d = alt * Math.Tan(DegreeToRadian(vAngle)) * 0.001;
            double brng = DegreeToRadian(baring);

            double lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(d / R) + Math.Cos(lat1) * Math.Sin(d / R) * Math.Cos(brng));
            coordinate cLat2 = new coordinate(RadianToDegree(lat2), CoordinateType.latitude);

            double lon2 = lon1 + Math.Atan2(Math.Sin(brng) * Math.Sin(d / R) * Math.Cos(lat1), Math.Cos(d / R) - Math.Sin(lat1) * Math.Sin(lat2));
            coordinate cLon2 = new coordinate(RadianToDegree(lon2), CoordinateType.longitude);

            return new latlon(cLat2, cLon2);
        }

        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private static double RadianToDegree(double radian)
        {
            return radian * (180.0 / Math.PI);
        }

        public static double getGoogleElevation(Image image)
        {
            double lat = gpsUtils.getGpsLat(image).decimalDegrees;
            double lon = gpsUtils.getGpsLong(image).decimalDegrees;
            double elevation = 0;
            try
            {
                string googleAltitudeUrl = String.Format(@"http://maps.googleapis.com/maps/api/elevation/xml?locations={0},{1}&sensor=false", lat.ToString().Replace(",", "."), lon.ToString().Replace(",", "."));
                XmlDocument googleElevationDoc = new XmlDocument();
                googleElevationDoc.Load(googleAltitudeUrl);
                string statusCode = googleElevationDoc.GetElementsByTagName("status")[0].InnerText;
                if (statusCode != null && statusCode.Equals("OK"))
                {
                    elevation = Convert.ToDouble(googleElevationDoc.GetElementsByTagName("elevation")[0].InnerText);
                }
            }
            catch (Exception) { /* do nothing */ }
            return elevation;
        }


/*
Returns elevation in meters
http://maps.googleapis.com/maps/api/elevation/xml?locations=39.7391536,-104.9847034&sensor=false
<?xml version="1.0" encoding="UTF-8"?>
<ElevationResponse> 
 <status>OK</status> 
 <result>  
  <location>   
   <lat>39.7391536</lat>   
   <lng>-104.9847034</lng>  
  </location>  
  <elevation>1608.6379395</elevation>  
  <resolution>4.7719760</resolution> 
 </result> 
</ElevationResponse>
*/
    }

    public class coordinate
    {
        public CoordinateType type { get; set; }
        public double deg { get; set; }
        public double min { get; set; }
        public double sec { get; set; }
        public double decimalDegrees { get; set; }
        public string reference { get; set; }

        public coordinate(double decimalDegreesValue, CoordinateType type)
        {
            // Save original value;
            this.decimalDegrees = decimalDegreesValue;
            this.type = type;

            // Set flag if number is negative
            bool neg = decimalDegreesValue < 0d;

            // Work with a positive number
            decimalDegreesValue = Math.Abs(decimalDegreesValue);

            // Get d/m/s components
            this.deg = Math.Floor(decimalDegreesValue);
            decimalDegreesValue -= this.deg;
            decimalDegreesValue *= 60;
            this.min = Math.Floor(decimalDegreesValue);
            decimalDegreesValue -= this.min;
            decimalDegreesValue *= 60;
            this.sec = Math.Round(decimalDegreesValue * 1000) / 1000;

            switch (type)
            {
                case CoordinateType.longitude:
                    this.reference = neg ? "W" : "E";
                    break;

                case CoordinateType.latitude:
                    this.reference = neg ? "S" : "N";
                    break;
            }

        }

        public coordinate(double deg, double min, double sec, CoordinateType type)
        {
            // Save original value;
            this.deg = deg;
            this.min = min;
            this.sec = sec;

            // Set flag if number is negative
            bool neg = deg < 0d;
            int multp = neg ? -1 : 1;

            this.decimalDegrees = multp * (Math.Abs(Math.Floor(deg)) + (min / 60) + (sec / 3600));

            switch (type)
            {
                case CoordinateType.longitude:
                    this.reference = neg ? "W" : "E";
                    break;
                case CoordinateType.latitude:
                    this.reference = neg ? "S" : "N";
                    break;
            }
        }
    }

    public class latlon
    {
        public coordinate lat { get; set; }
        public coordinate lon { get; set; }

        public latlon(coordinate vLat, coordinate vLon)
        {
            this.lat = vLat;
            this.lon = vLon;
        }
    }

    public class Rational
    {
        private Int32 _num;
        private Int32 _denom;

        public Rational(byte[] bytes)
        {
            byte[] n = new byte[4];
            byte[] d = new byte[4];
            Array.Copy(bytes, 0, n, 0, 4);
            Array.Copy(bytes, 4, d, 0, 4);
            _num = BitConverter.ToInt32(n, 0);
            _denom = BitConverter.ToInt32(d, 0);
        }

        public double ToDouble()
        {
            return Math.Round(Convert.ToDouble(_num) / Convert.ToDouble(_denom), 2);
        }

        public string ToString(string separator)
        {
            return _num.ToString() + separator + _denom.ToString();
        }

        public override string ToString()
        {
            return this.ToString("/");
        }
    }

    public class URational
    {
        private UInt32 _num;
        private UInt32 _denom;

        public URational(byte[] bytes)
        {
            byte[] n = new byte[4];
            byte[] d = new byte[4];
            Array.Copy(bytes, 0, n, 0, 4);
            Array.Copy(bytes, 4, d, 0, 4);
            _num = BitConverter.ToUInt32(n, 0);
            _denom = BitConverter.ToUInt32(d, 0);
        }

        public double ToDouble()
        {
            return Math.Round(Convert.ToDouble(_num) / Convert.ToDouble(_denom), 2);
        }

        public override string ToString()
        {
            return this.ToString("/");
        }

        public string ToString(string separator)
        {
            return _num.ToString() + separator + _denom.ToString();
        }
    }

    public class GPSRational
    {
        private Rational _hours;
        private Rational _minutes;
        private Rational _seconds;

        public Rational Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                _hours = value;
            }
        }
        public Rational Minutes
        {
            get
            {
                return _minutes;
            }
            set
            {
                _minutes = value;
            }
        }
        public Rational Seconds
        {
            get
            {
                return _seconds;
            }
            set
            {
                _seconds = value;
            }
        }

        public GPSRational(byte[] bytes)
        {
            byte[] h = new byte[8]; byte[] m = new byte[8]; byte[] s = new byte[8];

            Array.Copy(bytes, 0, h, 0, 8); Array.Copy(bytes, 8, m, 0, 8); Array.Copy(bytes, 16, s, 0, 8);

            _hours = new Rational(h);
            _minutes = new Rational(m);
            _seconds = new Rational(s);
        }

        public override string ToString()
        {
            return _hours.ToDouble() + "° "
                + _minutes.ToDouble() + "' "
                + _seconds.ToDouble() + "\"";
        }

        public string ToString(string separator)
        {
            return _hours.ToDouble() + separator
                + _minutes.ToDouble() + separator +
                _seconds.ToDouble();
        }
    }

    public class GoogleGPS
    {
        public string CountryNameCode { get; set; }
        public string CountryName { get; set; }
        public string AdministrativeAreaName { get; set; }
        public string LocalityName  { get; set; }
        public string ThoroughfareName { get; set; }

        public GoogleGPS(double latitude, double longitude)
        {

            this.CountryNameCode = "";
            this.CountryName = "";
            this.AdministrativeAreaName = "";
            this.LocalityName = "";

            try
            {
                string googleGpsUrl = String.Format(@"http://maps.google.com/maps/geo?output=xml&oe=utf-8&ll={0},{1}&key=asdad", latitude.ToString().Replace(",", "."), longitude.ToString().Replace(",", "."));
                XmlDocument googleGpsDoc = new XmlDocument();
                googleGpsDoc.Load(googleGpsUrl);
                string statusCode = googleGpsDoc.GetElementsByTagName("code")[0].InnerText;
                if (statusCode != null && statusCode.Equals("200"))
                {
                    this.CountryNameCode = googleGpsDoc.GetElementsByTagName("CountryNameCode")[0].InnerText;
                    this.CountryName = googleGpsDoc.GetElementsByTagName("CountryName")[0].InnerText;
                    this.AdministrativeAreaName = googleGpsDoc.GetElementsByTagName("AdministrativeAreaName")[0].InnerText;
                    this.LocalityName = googleGpsDoc.GetElementsByTagName("LocalityName")[0].InnerText;
                    this.ThoroughfareName = googleGpsDoc.GetElementsByTagName("ThoroughfareName")[0].InnerText;
                }
            }
            catch (Exception) { /* do nothing */ }
        }

    }

    public class YahooGPS
    {
        // Error, city, county, state, statecode, country, countrycode
        public string  Error { get; set; }
        public string  City { get; set; }
        public string  County { get; set; }
        public string  State { get; set; }
        public string  StateCode { get; set; }
        public string  Country { get; set; }
        public string  CountryCode { get; set; }

        public YahooGPS(double latitude, double longitude)
        {

            this.Error = "";
            this.City = "";
            this.County = "";
            this.State = "";
            this.StateCode = "";
            this.Country = "";
            this.CountryCode = "";

            try
            {
                string yahooGpsUrl = String.Format(@"http://where.yahooapis.com/geocode?q={0},+{1}&gflags=R&appid=IDCOUe4e", latitude.ToString().Replace(",", "."), longitude.ToString().Replace(",", "."));
                XmlDocument yahooGpsDoc = new XmlDocument();
                yahooGpsDoc.Load(yahooGpsUrl);
                string statusCode = yahooGpsDoc.GetElementsByTagName("Error")[0].InnerText;
                if (statusCode != null && statusCode.Equals("0"))
                {
                    this.City = yahooGpsDoc.GetElementsByTagName("city")[0].InnerText;
                    this.County = yahooGpsDoc.GetElementsByTagName("county")[0].InnerText;
                    this.State = yahooGpsDoc.GetElementsByTagName("state")[0].InnerText;
                    this.StateCode = yahooGpsDoc.GetElementsByTagName("statecode")[0].InnerText;
                    this.Country = yahooGpsDoc.GetElementsByTagName("country")[0].InnerText;
                    this.CountryCode = yahooGpsDoc.GetElementsByTagName("countrycode")[0].InnerText;
                }
            }
            catch (Exception) { /* do nothing */ }
        }

        /*
         * 

Error, city, county, state, statecode, country, countrycode
         <ResultSet version="1.0">
<Error>0</Error>
<ErrorMessage>No error</ErrorMessage>
<Locale>us_US</Locale>
<Quality>99</Quality>
<Found>1</Found>
<Result>
<quality>99</quality>
<latitude>43.600324</latitude>
<longitude>-71.363024</longitude>
<offsetlat>43.600324</offsetlat>
<offsetlon>-71.363024</offsetlon>
<radius>12300</radius>
<name>43.600324, -71.363024</name>
<line1>43.600324, -71.363024</line1>
<line2>Gilford, NH</line2>
<line3/>
<line4>United States</line4>
<house/>
<street/>
<xstreet/>
<unittype/>
<unit/>
<postal/>
<neighborhood/>
<city>Gilford</city>
<county>Belknap County</county>
<state>New Hampshire</state>
<country>United States</country>
<countrycode>US</countrycode>
<statecode>NH</statecode>
<countycode/>
<hash/>
<woeid>2410211</woeid>
<woetype>7</woetype>
<uzip>03249</uzip>
</Result>
</ResultSet> 
        */
    }
}
