﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using QLViecLam.Data;
using QLViecLam.ViewModels.Helpers;
using System.Xml.Linq;
using QLViecLam.ViewModels.Admin.Manages.TongHop_PhanTich_DuDoan.HeThongBaoCaoThongKe.QuanLyDanhMucDuLieu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Dynamic;
using QLViecLam.Models.Admin.Manages;
using System.Linq;
//using QLViecLam.ViewModels.Systems;

namespace QLViecLam.Helper
{
    public class Helpers
    {
        public static bool CheckChucNang(ISession session, string machucnang, string key)
        {
            string ssadmin = session.GetString("SsAdmin")!;
            dynamic sessionInfo = JsonConvert.DeserializeObject(ssadmin)!;
            if (sessionInfo["PhanLoai"] == "SSA")
            {
                return true;
            }
            else
            {
                string phanquyen = session.GetString("PhanQuyen")!;
                if (!string.IsNullOrEmpty(phanquyen))
                {
                    dynamic info = JsonConvert.DeserializeObject(phanquyen)!;

                    foreach (var item in info)
                    {
                        if (item["MaChucNang"] == machucnang && item[key] == true)
                        {
                            return true;
                        }
                    }
                }
            }
            //return false;
            return true;
        }

        //public static bool Check(ISession session, string machucnang, string key)
        //{
        //    if (!string.IsNullOrEmpty(session.GetString("SsAdmin")))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        GoToSessionOut();
        //    }
        //    string ssadmin = session.GetString("SsAdmin")!;
        //    dynamic sessionInfo = JsonConvert.DeserializeObject(ssadmin)!;
        //    if (sessionInfo["PhanLoai"] == "SSA")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        string phanquyen = session.GetString("PhanQuyen")!;
        //        if (!string.IsNullOrEmpty(phanquyen))
        //        {
        //            dynamic info = JsonConvert.DeserializeObject(phanquyen)!;

        //            foreach (var item in info)
        //            {
        //                if (item["MaChucNang"] == machucnang && item[key] == true)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    GoToPage();
        //                }
        //            }
        //        }
        //    }
        //    //return false;
        //    return true;
        //}

        //public static IActionResult GoToSessionOut()
        //{

        //    return new RedirectToActionResult("SessionOut", "Index", null);
        //}
        //public static IActionResult GoToPage()
        //{
        //    return new RedirectToActionResult("Page", "Index", null);
        //}


        public static string GetMenuMinimize(ISession session)
        {
            if (!string.IsNullOrEmpty(session.GetString("MenuMinimize")))
            {
                return session.GetString("MenuMinimize")!;
            }
            else
            {
                return "False";
            }
        }

        public static string GetSsAdminKey(ISession session, string key)
        {
            if (!string.IsNullOrEmpty(session.GetString("SsAdmin")))
            {
                string ssadmin = session.GetString("SsAdmin")!;
                dynamic sessionInfo = JsonConvert.DeserializeObject(ssadmin)!;
                string value = sessionInfo[key];
                return value;
            }
            else
            {
                return null!;
            }
        }

        public static dynamic GetSsAdmin(ISession session)
        {
            if (!string.IsNullOrEmpty(session.GetString("SsAdmin")))
            {
                string ssadmin = session.GetString("SsAdmin")!;
                dynamic sessionInfo = JsonConvert.DeserializeObject(ssadmin)!;
                return sessionInfo;
            }
            else
            {
                return null!;
            }
        }


        public static string GetThongTinUsers(ISession session)
        {
            string ssadmin = session.GetString("SsAdmin")!;
            dynamic sessionInfo = JsonConvert.DeserializeObject(ssadmin)!;
            string value = sessionInfo["Name"] + " (" + sessionInfo["Username"] + ")";
            return value;
        }

        public static bool CheckPermission(ISession session, string roles, string key)
        {
            if (!string.IsNullOrEmpty(session.GetString("SsAdmin")))
            {
                string ssadmin = session.GetString("SsAdmin")!;
                dynamic sessionInfo = JsonConvert.DeserializeObject(ssadmin)!;
                bool ssa = sessionInfo["Sadmin"];
                if (ssa)
                {
                    return true;
                }
                else
                {
                    string per = session.GetString("Permission")!;
                    if (!string.IsNullOrEmpty(per))
                    {
                        dynamic info = JsonConvert.DeserializeObject(per)!;

                        foreach (var item in info)
                        {
                            if (item["Roles"] == roles && item[key] == true)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string ConvertDateToText(DateTime date)
        {
            string date_convert = date.Date.ToString("dd/MM/yyyy");
            if (date_convert == "01/01/0001")
            {
                return " Ngày .. tháng .. năm ....";
            }
            else
            {
                string str = "";
                str += " Ngày " + date.ToString("dd");
                str += " tháng " + date.ToString("MM");
                str += " năm " + date.ToString("yyyy");
                return str;
            }
        }

        public static string ConvertDateToFormView(DateTime date)
        {
            string date_convert = date.Date.ToString("dd/MM/yyyy");
            if (date_convert == "01/01/0001")
            {
                string str = DateTime.Now.Date.ToString("yyyy-MM-dd");
                //string str = string.Format("{0:MM/dd/yyyy}");
                return str;
            }
            else
            {
                string str = date.Date.ToString("yyyy-MM-dd");
                return str;
            }
        }

        public static string ConvertDateTimeToFormView(DateTime date)
        {
            string date_convert = date.Date.ToString("dd/MM/yyyy");
            if (date_convert == "01/01/0001")
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                return str;
            }
            else
            {
                string str = date.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                return str;
            }
        }

        public static string ConvertDbToStrDecimal(double db)
        {
            if (db == 0)
            {
                return "";
            }
            else
            {
                return db.ToString();
            }
        }

        public static string ConvertDbToStr(double db)
        {
            if (db == 0)
            {
                return "";
            }
            else
            {
                string str = String.Format("{0:n0}", db);
                return str;
            }
        }

        public static string ConvertIntToStr(int value)
        {
            if (value == 0)
            {
                return "";
            }
            else
            {
                string str = String.Format("{0:n0}", value);
                return str;
            }
        }

        public static string ConvertDateToStr(DateTime date)
        {

            string str = date.Date.ToString("dd/MM/yyyy");
            if (str == "01/01/0001" || str == "01-01-0001")
            {
                return "";
            }
            else
            {
                return str;
            }
        }

        public static string ConvertDateToStrAjax(DateTime date)
        {

            string str = date.Date.ToString("yyyy-MM-dd");
            if (str == "0001-01-01")
            {
                return "";
            }
            else
            {
                return str;
            }
        }

        public static string ConvertDateTimeToStr(DateTime datetime)
        {
            string str = datetime.Date.ToString("dd/MM/yyyy HH:mm:ss,fff tt");
            return str;
        }

        public static string ConvertDateTimeToText(DateTime datetime)
        {
            string gio = datetime.Hour < 10 ? "0" + datetime.Hour.ToString() : datetime.Hour.ToString();
            string phut = datetime.Minute < 10 ? "0" + datetime.Minute.ToString() : datetime.Minute.ToString();
            string ngay = datetime.Day < 10 ? "0" + datetime.Day.ToString() : datetime.Day.ToString();
            string thang = datetime.Month < 10 ? "0" + datetime.Month.ToString() : datetime.Month.ToString();
            string text = gio + " giờ " + phut + " phút";
            text += ", ngày " + ngay + " tháng " + thang + " năm " + datetime.Year.ToString();
            return text;
        }

        public static double ConvertStrToDb(string str)
        {
            if (str == "")
            {
                return 0;
            }
            else
            {
                double db = double.Parse(str.Replace(",", ""));
                return db;
            }
        }

        public static string ConvertYearToStr(int year)
        {
            if (year == 0)
            {
                return "";
            }
            else
            {
                return year.ToString();
            }
        }

        public static string ConvertMonthToStr(int month)
        {
            if (month == 0)
            {
                return "";
            }
            else
            {
                return month.ToString();
            }
        }

        public static string ConvertDbToMoneyText(double inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            string[] str = result.Split(" ");
            int len_array = str.Count() - 1;
            string new_str = str[0].Substring(0, 1).ToUpper() + str[0].Substring(1);
            for (int i = 1; i <= len_array; i++)
            {
                new_str += " " + str[i];
            }
            if (isNegative) result = "Âm " + new_str;
            return new_str + (suffix ? " đồng chẵn %." : "");
        }

        public static string ConvertDbToText(double inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            string[] str = result.Split(" ");
            int len_array = str.Count() - 1;
            string new_str = str[0].Substring(0, 1).ToUpper() + str[0].Substring(1);
            for (int i = 1; i <= len_array; i++)
            {
                new_str += " " + str[i];
            }
            if (isNegative) new_str = "Âm " + new_str;
            return new_str;
        }

        public static string ConvertToRoman(int num)
        {
            int n = num;
            string result = string.Empty;

            // Dictionary of Roman numerals
            Dictionary<string, int> romanNumerals = new Dictionary<string, int>
            {
            { "M", 1000 },
            { "CM", 900 },
            { "D", 500 },
            { "CD", 400 },
            { "C", 100 },
            { "XC", 90 },
            { "L", 50 },
            { "XL", 40 },
            { "X", 10 },
            { "IX", 9 },
            { "V", 5 },
            { "IV", 4 },
            { "I", 1 }
            };

            foreach (var item in romanNumerals)
            {
                while (n >= item.Value)
                {
                    result += item.Key;
                    n -= item.Value;
                }
            }

            return result;
        }



        public static string ConvertIntToRoman(int roman)
        {
            string str = "";
            switch (roman)
            {
                case 1:
                    str = "I";
                    break;
                case 2:
                    str = "II";
                    break;
                case 3:
                    str = "III";
                    break;
                case 4:
                    str = "IV";
                    break;
                case 5:
                    str = "V";
                    break;
                case 6:
                    str = "VI";
                    break;
                case 7:
                    str = "VII";
                    break;
                case 8:
                    str = "VIII";
                    break;
                case 9:
                    str = "IX";
                    break;
                case 10:
                    str = "X";
                    break;
                case 11:
                    str = "XI";
                    break;
                case 12:
                    str = "XII";
                    break;
                case 13:
                    str = "XIII";
                    break;
                case 14:
                    str = "XIV";
                    break;
                case 15:
                    str = "XV";
                    break;
                case 16:
                    str = "XVI";
                    break;
                case 17:
                    str = "XVII";
                    break;
                case 18:
                    str = "XVIII";
                    break;
                case 19:
                    str = "XIX";
                    break;
                case 20:
                    str = "XX";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }

        public static string[] GetListsDays()
        {
            string[] days = new string[]
               {
                    "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
                    "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
                    "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"
               };
            return days;
        }

        public static string[] GetListsMonths()
        {
            string[] months = new string[]
               {
                    "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
                    "11", "12"
               };
            return months;
        }

        public static string ConvertDayMonthYearToString(string day, string month, string year)
        {
            string str = "";
            if (!string.IsNullOrEmpty(day))
            {
                str += day + "/";
            }
            if (!string.IsNullOrEmpty(month))
            {
                str += month + "/";
            }
            if (!string.IsNullOrEmpty(year))
            {
                str += year;
            }
            return str;
        }

        public static bool CheckFileType(string extension)
        {
            if (extension == ".jpg") return true;
            if (extension == ".jpeg") return true;
            if (extension == ".png") return true;
            if (extension == ".doc") return true;
            if (extension == ".docx") return true;
            if (extension == ".pdf") return true;
            if (extension == ".xls") return true;
            if (extension == ".xlsx") return true;
            return false;
        }

        public static bool CheckFileSize(long size)
        {
            if (size <= 5242880) return true;
            return false;
        }

        public static string[] GetGroupPer()
        {
            string[] group = new string[]
               { "K" , "T", "H", "X"};
            return group;
        }

        public static DateTime ConvertStringToDate(string Ngay, string Thang, string Nam)
        {
            if (string.IsNullOrEmpty(Ngay)) Ngay = "01";
            if (string.IsNullOrEmpty(Thang)) Thang = "01";
            if (string.IsNullOrEmpty(Nam)) Nam = "1900";
            string date = Ngay + "/" + Thang + "/" + Nam;
            DateTime dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return dt;
        }


        //public static string ConvertMonthYearToString(string Thang, string Nam)
        //{
        //    string str = "";
        //    if (!string.IsNullOrEmpty(Thang))
        //    {
        //        str += Thang + " tháng ";
        //    }
        //    if (!string.IsNullOrEmpty(Nam))
        //    {
        //        str += Nam + " năm";
        //    }
        //    return str;
        //}

        //public static string StringtoMD5(string chuoi)
        //{
        //    MD5 mD5 = MD5.Create();
        //    byte[] mahoa = mD5.ComputeHash(Encoding.UTF8.GetBytes(chuoi));
        //    StringBuilder kq = new StringBuilder();
        //    for (int i = 0; i < mahoa.Length; i++)
        //    {
        //        kq.Append(mahoa[i].ToString("x2"));
        //    }
        //    return kq.ToString();
        //}




        //public static string[] NhomChiSoGia()
        //{
        //    string[] csg = new string[]
        //    {
        //        "1","2","3","4"
        //    };
        //    return csg;
        //}

        //public static double Percent(int number, int total)
        //{

        //    return (double)number / total * 100;
        //}



    }
}
