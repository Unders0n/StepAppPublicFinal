using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StepApp.CommonExtensions.Culture
{
    public static class CountriesExtensions
    {



        public static List<RegionInfo> CountriesList(string searchString = "")
        {
            //Creating list
            var cultureList = new List<RegionInfo>();

            //getting  the specific  CultureInfo from CultureInfo class
              CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);



            foreach (CultureInfo getCulture in getCultureInfo)
            {
                try
                {
                    RegionInfo GetRegionInfo;
                    GetRegionInfo = new RegionInfo(getCulture.LCID);
                    //adding each county Name into the arraylist
                    if (!(cultureList.Contains(GetRegionInfo)))
                    {
                        cultureList.Add(GetRegionInfo);
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
                //creating the object of RegionInfo class


            }


            if (!string.IsNullOrEmpty(searchString)) cultureList = cultureList.Where(info => info.EnglishName.ToLower().Contains(searchString.ToLower()) || info.ThreeLetterISORegionName.ToLower().Contains(searchString.ToLower())).ToList();



            /*foreach (var region in regions)
            {
                RegionInfo GetRegionInfo;
                GetRegionInfo = new RegionInfo(region);
            }*/

                /*foreach (CultureInfo getCulture in getCultureInfo)
                {

                    try
                    {
                        RegionInfo GetRegionInfo;
                        GetRegionInfo = new RegionInfo(getCulture.LCID);
                        //adding each county Name into the arraylist
                        if (!(CultureList.Contains(GetRegionInfo)))
                        {
                            CultureList.Add(GetRegionInfo);
                        }
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                    //creating the object of RegionInfo class


                }*/
                //sorting array by using sort method to get countries in order
            cultureList = cultureList.OrderBy(info => info.EnglishName).ToList();
            //returning country list
            return cultureList;
        }
    }
}
