using System;
using System.Collections.Generic;
using System.Globalization;

namespace TeltonikaParser
{
    /// <summary>
    /// TeltonikaFMXXXX
    /// </summary>
    public class Protocol_TeltonikaFMXXXX
    {
        private static string Parsebytes(Byte[] byteBuffer, int index, int Size)
        {
            return BitConverter.ToString(byteBuffer, index, Size).Replace("-", string.Empty);
        }

        private static string parseIMEI(Byte[] byteBuffer, int size)
        {
            int index = 0;
            var result = Parsebytes(byteBuffer, index, 2);
            return result;
        }

        private static bool checkIMEI(string data)
        {
            Console.WriteLine(data.Length);
            if (data.Length == 15)
                return true;

            return false;
        }

        private static List<Position> ParsePositions(Byte[] byteBuffer, int linesNB)
        {
            int index = 0;
            index += 7;
            uint dataSize = byteBuffer[index];

            index++;
            uint codecID = byteBuffer[index];

            if (codecID == Constants.CODEC_8)
            {
                index++;
                uint NumberOfData = byteBuffer[index];

                Console.WriteLine("{0} {1} {2} ", codecID, NumberOfData, dataSize);

                List<Position> result = new List<Position>();

                index++;
                for (int i = 0; i < NumberOfData; i++)
                {
                    Position position = new Position();

                    var timestamp = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                    index += 8;

                    position.Time = DateTime.Now;

                    var Preority = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                    index++;

                    position.Lo = Int32.Parse(Parsebytes(byteBuffer, index, 4), NumberStyles.HexNumber) / 10000000.0;
                    index += 4;

                    position.La = Int32.Parse(Parsebytes(byteBuffer, index, 4), NumberStyles.HexNumber) / 10000000.0;
                    index += 4;

                    var Altitude = Int16.Parse(Parsebytes(byteBuffer, index, 2), NumberStyles.HexNumber);
                    index += 2;

                    var dir = Int16.Parse(Parsebytes(byteBuffer, index, 2), NumberStyles.HexNumber);

                    if (dir < 90) position.Direction = 1;
                    else if (dir == 90) position.Direction = 2;
                    else if (dir < 180) position.Direction = 3;
                    else if (dir == 180) position.Direction = 4;
                    else if (dir < 270) position.Direction = 5;
                    else if (dir == 270) position.Direction = 6;
                    else if (dir > 270) position.Direction = 7;
                    index += 2;

                    var Satellite = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                    index++;

                    if (Satellite >= 3)
                        position.Status = "A";
                    else
                        position.Status = "L";

                    position.Speed = Int16.Parse(Parsebytes(byteBuffer, index, 2), NumberStyles.HexNumber);
                    index += 2;

                    int ioEvent = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                    index++;
                    int ioCount = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                    index++;
                    //read 1 byte
                    {
                        int cnt = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                        index++;
                        for (int j = 0; j < cnt; j++)
                        {
                            int id = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                            index++;
                            //Add output status
                            switch (id)
                            {
                                case Constants.ACC:
                                    {
                                        var value = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                                        position.Status += value == 1 ? ",ACC off" : ",ACC on";
                                        index++;
                                        break;
                                    }
                                case Constants.DOOR:
                                    {
                                        var value = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                                        position.Status += value == 1 ? ",door close" : ",door open";
                                        index++;
                                        break;
                                    }
                                case Constants.GSM:
                                    {
                                        var value = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                                        position.Status += string.Format(",GSM {0}", value);
                                        index++;
                                        break;
                                    }
                                case Constants.STOP:
                                    {
                                        var value = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                                        position.StopFlag = value == 1;
                                        position.IsStop = value == 1;

                                        index++;
                                        break;
                                    }
                                case Constants.IMMOBILIZER:
                                    {
                                        var value = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                                        position.Alarm = value == 0 ? "Activate Anti-carjacking success" : "Emergency release success";
                                        index++;
                                        break;
                                    }
                                case Constants.GREEDRIVING:
                                    {
                                        var value = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                                        switch (value)
                                        {
                                            case 1:
                                                {
                                                    position.Alarm = "Acceleration intense !!";
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    position.Alarm = "Freinage brusque !!";
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    position.Alarm = "Virage serré !!";
                                                    break;
                                                }
                                            default:
                                                break;
                                        }
                                        index++;
                                        break;
                                    }
                                default:
                                    {
                                        index++;
                                        break;
                                    }
                            }

                        }
                    }

                    //read 2 byte
                    {
                        int cnt = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                        index++;
                        for (int j = 0; j < cnt; j++)
                        {
                            int id = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                            index++;

                            switch (id)
                            {
                                case Constants.Analog:
                                    {
                                        var value = Int16.Parse(Parsebytes(byteBuffer, index, 2), NumberStyles.HexNumber);
                                        if (value < 12)
                                            position.Alarm += string.Format("Low voltage", value);
                                        index += 2;
                                        break;
                                    }
                                case Constants.SPEED:
                                    {
                                        var value = Int16.Parse(Parsebytes(byteBuffer, index, 2), NumberStyles.HexNumber);
                                        position.Alarm += string.Format("Speed", value);
                                        index += 2;
                                        break;
                                    }
                                default:
                                    {
                                        index += 2;
                                        break;
                                    }
                            }
                        }
                    }

                    //read 4 byte
                    {
                        int cnt = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                        index++;
                        for (int j = 0; j < cnt; j++)
                        {
                            int id = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                            index++;

                            switch (id)
                            {
                                case Constants.TEMPERATURE:
                                    {
                                        var value = Int32.Parse(Parsebytes(byteBuffer, index, 4), NumberStyles.HexNumber);
                                        position.Alarm += string.Format("Temperature {0}", value);
                                        index += 4;
                                        break;
                                    }
                                case Constants.ODOMETER:
                                    {
                                        var value = Int32.Parse(Parsebytes(byteBuffer, index, 4), NumberStyles.HexNumber);
                                        position.Mileage = value;
                                        index += 4;
                                        break;
                                    }
                                default:
                                    {
                                        index += 4;
                                        break;
                                    }

                            }


                        }
                    }

                    //read 8 byte
                    {
                        int cnt = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                        index++;
                        for (int j = 0; j < cnt; j++)
                        {
                            int id = byte.Parse(Parsebytes(byteBuffer, index, 1), NumberStyles.HexNumber);
                            index++;

                            switch (id)
                            {
                                case Constants.MANUALCAN00:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN01 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN01:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN01 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN02:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN02 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN03:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN03 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN04:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN04 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN05:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN05 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN06:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN06 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN07:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN07 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN08:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN08 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN09:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN09 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN10:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN10 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN11:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN11 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN12:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN12 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN13:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN13 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN14:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN14 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN15:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN15 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN16:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN16 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN17:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN17 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN18:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN18 = value;
                                        index += 8;
                                        break;
                                    }
                                case Constants.MANUALCAN19:
                                    {
                                        var value = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.MCan.CAN19 = value;
                                        index += 8;
                                        break;
                                    }
                                default:
                                    {
                                        var io = Int64.Parse(Parsebytes(byteBuffer, index, 8), NumberStyles.HexNumber);
                                        position.Status += string.Format(",{0} {1}", id, io);
                                        index += 8;
                                        break;
                                    }

                            }

                        }
                    }

                    result.Add(position);
                    Console.WriteLine(position.ToString());
                }

                return result;
            }
            return null;
        }

        public static Byte[] DealingWithHeartBeat(string data)
        {

            Byte[] result = { 1 };
            if (checkIMEI(data))
            {
                return result;
            }
            return null;
        }

        public static string ParseHeartBeatData(Byte[] byteBuffer, int size)
        {
            var IMEI = parseIMEI(byteBuffer, size);
            if (checkIMEI(IMEI))
            {
                return IMEI;
            }
            else
            {
                int index = 0;
                index += 7;
                uint dataSize = byteBuffer[index];

                index++;
                uint codecID = byteBuffer[index];

                if (codecID == Constants.CODEC_8)
                {
                    index++;
                    uint NumberOfData = byteBuffer[index];

                    return NumberOfData.ToString();
                }

            }
            return string.Empty;
        }
        public static List<Position> ParseData(Byte[] byteBuffer, int size)
        {

            List<Position> result = new List<Position>();
            result = ParsePositions(byteBuffer, size);

            return result;
        }

        public static Position GetGPRSPos(string oneLine)
        {

            return null;
        }
    }
}
