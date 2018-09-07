using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
    


namespace GetWeather
{
    public partial class GetWeatherForm : Form
    {
        static public GetWeatherForm instance;

        WeatherAPI api;

        WeatherTransferObject jsonObject;


        public GetWeatherForm()
        {
            if (instance != null)
                return;

            instance = this;   // this is my fake singleton used in other files to work with the UI elements


            InitializeComponent();


            // initialize jsonObject
            jsonObject = new WeatherTransferObject();

            // set default data
            txtboxCountry.Text = "Canada";
            txtboxCity.Text = "Halifax";
  
        }


        public void UpdateWeatherData(WeatherTransferObject dataSet)
        {
            // populate all recieved data to the UI

            lblSetCity.Text = dataSet.name;

            lblSetCountry.Text = dataSet.sys.country.ToString();

            lblCurrentTemp.Text = dataSet.main.temp.ToString();

            lblHumidity.Text = dataSet.main.humidity.ToString();

            lblPressure.Text = dataSet.main.pressure.ToString();

            lblWindSpeed.Text = (dataSet.wind.speed.ToString() + " (mps)"); // meters per second

            lblLong.Text = dataSet.coord.lon.ToString();
            lblLat.Text = dataSet.coord.lat.ToString();

            lblDesc.Text = dataSet.weather[0].description.ToString();
            
        }


        private string GetCountryEnglishName(string countryName, string cityName)
        {            
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & CultureTypes.SpecificCultures);
            RegionInfo region;
            foreach (CultureInfo culture in cultures)
            {
                region = new RegionInfo(culture.LCID);

                if (countryName == region.Name)
                {
                    if (region.Name.ToLower() == "us" || region.Name.ToLower() == "usa" && cityName.ToLower() == "vancouver")
                        break;
                    
                    MessageBox.Show("region: " + region.Name);
                    return region.EnglishName;
                }
            }

            MessageBox.Show("error");
            return countryName;
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtboxCity.Text == null)
            {
                return;
            }
            else if(txtboxCountry.Text == null)
            {
                return;
            }
            else
                api = new WeatherAPI(txtboxCity.Text.ToString(), txtboxCountry.Text.ToString());

        }


        public bool SetIcon(string imageName)
        {
            string imgURL = "http://openweathermap.org/img/w/" + imageName + ".png";

            try
            {
                
                iconBox.Load(imgURL);
            
                iconBox.SizeMode = PictureBoxSizeMode.StretchImage;

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting icon: " + e.Message);

                return false;

               // throw;
            }
            
        }


    }
}
